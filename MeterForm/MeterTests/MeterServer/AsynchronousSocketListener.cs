using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MeterForm;
using System.Diagnostics;

namespace MeterForm.MeterTests.MeterServer
{
    class AsynchronousSocketListener
    {
        //public Socket listener = null;
        public string MeterSereverState
        {
            get { return _meterSererState; }
        }
        public bool bMeterSereverState
        {
            get { return _bmeterSererState; }
        }

        public int MeterServerPort
        {
            set { _meterSererPort = value; }
        }

        private string _meterSererState;
        private bool _bmeterSererState = true;
        private int _meterSererPort = 11000;
        private bool _bStopServer = false;

        // Thread signal.  
        public /*static*/ ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public /*static*/ void StartListening()
        {
            if (_bStopServer)
            {
                //listener.Shutdown(SocketShutdown.Both);
                //listener.Close();
                //allDone.Close();
                //return;
            }
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".

            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any;// ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, _meterSererPort);// 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                MeterWindow.Console("Meter Server started");

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    //Console.WriteLine("Waiting for a connection...");
                    MeterWindow.Console("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            /*finally
            {
                //listener.Dispose();//.Disconnect(false);//.Shutdown(SocketShutdown.Both);
                //listener.Close();
            }*/
            catch (ThreadAbortException e)
            {
                //Console.WriteLine("Thread - caught ThreadAbortException - resetting.");
                //Console.WriteLine("Exception message: {0}", e.Message);
                //allDone.Close();
                listener.Dispose();//.Shutdown(SocketShutdown.Both);
                //listener.BeginDisconnect()
                _bStopServer = true;
                Thread.ResetAbort();
            }
            catch (Exception e)
            {
                _meterSererState = e.ToString(); // Console.WriteLine(e.ToString());
                _bmeterSererState = false;
                //listener.Shutdown(SocketShutdown.Both);
                //listener.Close();
            }//*/

            //Console.WriteLine("\nPress ENTER to continue...");
            //Dynamo.Console("Waiting for a connection...");
            //Console.Read();

        }

        public /*static*/ void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler;
            try
            {
                handler = listener.EndAccept(ar);
            }
            catch (ObjectDisposedException ex)
            {
                return;
            }


            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public /*static*/ void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));
                //state.sb.Append(BitConverter.ToString(
                //    state.buffer, 0, bytesRead));

                if (state.sb.Length > 2)
                {
                    state.realDataSize = state.sb[1];

                    // Check for end-of-file tag. If it is not there, read
                    // more data.  
                    content = state.sb.ToString();
                    if (state.sb.Length == state.realDataSize)//(content.IndexOf("<EOF>") > -1)
                    {
                        // All the data has been read from the
                        // client. Display it on the console.  
                        //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        //    content.Length, content);
                        string str = String.Concat(content.Select(x => ((int)x).ToString("X2") + "-"));
                        MeterWindow.Console(String.Format("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, str));// content));
                        // Echo the data back to the client.  
                        //Send(handler, content);
                        Send(handler, MakeAnswer());

                        return;
                    }
                }
                // Not all data received. Get more.  
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);


                // Check for end-of-file tag. If it is not there, read
                // more data.  
                /*content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the
                    // client. Display it on the console.  
                    //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    //    content.Length, content);
                    MeterWindow.Console(String.Format("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content));
                    // Echo the data back to the client.  
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }*/
            }
        }

        private /*static*/ void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        private /*static*/ void Send(Socket handler, byte[] byteData)
        {
            // Convert the string data to byte data using ASCII encoding.  
            //byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private /*static*/ void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                MeterWindow.Console(String.Format("Sent {0} bytes to client.", bytesSent));
                //Dynamo.Console(String.Format("Sent {0} bytes to client.", bytesSent));

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void StopServer()
        {
            //listener.Shutdown(SocketShutdown.Both);
            //listener.Close();
            _bStopServer = true;
        }
        private byte[] MakeAnswer()
        {
            byte[] data = new byte[15];
            data[0] = 0x77; // Id
            data[1] = 0x0F; // Full length of packet
            data[2] = 0x10; // Command 0x10 - Get instantaneous values
            data[3] = 0x07; // Day
            data[4] = 0x0B; // Mon
            data[5] = 0x15; // Year - 2000
            data[6] = 0x0A; // Hours
            data[7] = 0x00; // Minutes
            data[8] = 0x00; // Seconds
            //Active power
            data[9] = 0x10; // 
            data[10] = 0x00; // 
            //Reactive power
            data[11] = 0x15; // 
            data[12] = 0x00; //
            //Reactive power
            data[13] = 0x25; // 
            data[14] = 0x00; //

            return data;
        }
    }

    // State object for reading client data asynchronously 
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;

        // Real data size
        public int realDataSize = -1;
    }
}
