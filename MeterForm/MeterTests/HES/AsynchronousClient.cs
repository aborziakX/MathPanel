using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace MeterForm.MeterTests.HES
{
    class AsynchronousClient
    {
        // The port number for the remote device.  
        private const int port = 11000;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;

        //Start position
        static int START_POSITION = 0;

        public /*static*/ void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the
                // remote device is "host.contoso.com".  
                //IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); //ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                //Send(client, "This is a test<EOF>");
                Send(client, MakeRequest());// new byte[] { 0x77, 0x06, 0x54, 0x65, 0x73, 0x74 });
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(client);
                receiveDone.WaitOne();

                // Write the response to the console.  
                //Console.WriteLine("Response received : {0}", response);
                string str = String.Concat(response.Select(x => ((int)x).ToString("X2") + "-"));
                MeterWindow.Console(String.Format("Response received : {0}",
                     str)); //response));

                // Release the socket.  
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                //Console.WriteLine("Socket connected to {0}",
                //    client.RemoteEndPoint.ToString());
                MeterWindow.Console(String.Format("Socket connected to {0}",
                    client.RemoteEndPoint.ToString()));

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //state.sb.Append(BitConverter.ToString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                        ParseAnswer(state.sb);
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }
        private static void Send(Socket client, byte[] byteData)
        {
            // Convert the string data to byte data using ASCII encoding.  
            //byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                MeterWindow.Console(String.Format("Sent {0} bytes to server.", bytesSent));

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private byte[] MakeRequest()
        {
            byte[] data = new byte[9];
            data[0] = 0x77; // Id
            data[1] = 0x09; // Full length of packet
            data[2] = 0x10; // Command 0x10 - Get instantaneous values
            data[3] = 0x07; // Day
            data[4] = 0x0B; // Mon
            data[5] = 0x15; // Year - 2000
            data[6] = 0x0A; // Hours
            data[7] = 0x00; // Minutes
            data[8] = 0x00; // Seconds

            return data;
        }
        private static void ParseAnswer(StringBuilder sb)
        {
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sb.ToString());

            BinaryWriter binWriter = new BinaryWriter(new MemoryStream());
            BinaryReader binReader;
            binWriter.Write(byteArray);
            binReader = new BinaryReader(binWriter.BaseStream);
            binReader.BaseStream.Position = START_POSITION;

            byte id = binReader.ReadByte();
            byte fulllength = binReader.ReadByte();
            byte command = binReader.ReadByte();
            byte day = binReader.ReadByte();
            byte mon = binReader.ReadByte();
            byte year = binReader.ReadByte();
            byte hrs = binReader.ReadByte();
            byte min = binReader.ReadByte();
            byte sec = binReader.ReadByte();
            DateTime dt = new DateTime(year + 2000, mon, day, hrs, min, sec);
            MeterWindow.SetDateTime(dt.ToString());
            Int16 activePower = binReader.ReadInt16();
            MeterWindow.SetActivePower(activePower.ToString());
            Int16 reactivePower = binReader.ReadInt16();
            MeterWindow.SetReactivePower(reactivePower.ToString());
            Int16 apparentPower = binReader.ReadInt16();
            MeterWindow.SetApparentPower(apparentPower.ToString());
        }
    }
    // State object for receiving data from remote device.  
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}
