//2020, Andrei Borziak
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Threading;
using System.Collections.Generic;
using System.IO;

using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Diagnostics;

//async based on https://www.codeproject.com/Articles/745134/csharp-async-socket-server

//in Main we can start several SocketServer in different threads (ports are also different)
//TODO - add lock in static methods!
//clean StringBuilder, read until all data received

namespace MathPanelExt
{
    //for async
    public class StateObject
    {
        static int id_counter = 0;
        public static int BUFSIZE = 4096;
        public int Id { get; }
        public Socket workSocket = null;
        public byte[] buffer = new byte[BUFSIZE];
        public StringBuilder sb = new StringBuilder();

        //AB 2021-06-07 for big data continue reading/sending
        public byte[] bytesToSend = null;
        public int offset = 0;
        public StateObject()
        {
            Id = id_counter++;
        }
    }
    public delegate byte[] PerformCalculation(StateObject state);
    public class SocketServer
    {
        static object locker = new object();
        static int POLL_MS_CONN = 1000;
        static public string sLogFile = "SocketServer_demo.log";
        public static int loglevel = 0;
        public bool bKeep = true;
        ManualResetEvent allDone = new ManualResetEvent(false);
        int port;
        string name = "tunnel", host;
        public string sStart = "", sEnd = "\r\n\r\n";
        Socket cliSocket, sListener;
        byte[] buffer = new byte[StateObject.BUFSIZE];
        bool running_ = false;
        IPEndPoint localEndPoint;
        int mmm = 0;
        DateTime dtSess;
        Random rnd = new Random();
        public PerformCalculation pc_hand = null;
        public SocketServer(string _name, string _host, int _port)
        {
            name = _name;
            host = _host;
            port = _port;

            // Establish the local endpoint for the socket.  
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");// MUST!
            localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.  
            Socket s = new Socket(AddressFamily.InterNetwork/*ipAddress.AddressFamily*/, SocketType.Stream, ProtocolType.Tcp);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            //s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 30000);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 30000);
            LingerOption linger = new LingerOption(true, 10);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, linger);
            s.ReceiveBufferSize = StateObject.BUFSIZE;
            s.SendBufferSize = StateObject.BUFSIZE;

            sListener = s;
            sListener.Bind(localEndPoint);
            sListener.Listen(1000);
        }

        void Run()
        {
            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            running_ = true;
            while (running_)
            {
                try
                {
                    cliSocket = sListener.Accept();
                    // получаем сообщение
                    //int iDone = DoProxy();
                    dtSess = DateTime.Now;
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    do
                    {
                        bytes = cliSocket.Receive(buffer);
                        builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                    }
                    while (cliSocket.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                    Thread.Sleep(rnd.Next(2000));

                    // отправляем ответ
                    mmm++;
                    string message = mmm + ". ваше сообщение доставлено";
                    buffer = Encoding.UTF8.GetBytes(message);
                    cliSocket.Send(buffer);

                    // закрываем сокет
                    var diffInSeconds = (DateTime.Now - dtSess).TotalSeconds;
                    Log(Thread.CurrentThread.ManagedThreadId + " Сессия окончена, " + diffInSeconds, 3);

                    cliSocket.Shutdown(SocketShutdown.Both);
                    cliSocket.Close();
                }
                catch (ThreadInterruptedException ex1)
                {
                    Log(name + ex1.ToString(), 3);
                }
                catch (ThreadAbortException ex2)
                {
                    Log(name + ex2.ToString(), 3);
                }
                catch (Exception ex)
                {
                    Log(name + ",mmm=" + mmm + ",error:" + ex.ToString(), 3);
                }
                finally
                {
                }
            }
            Log(name + " Поток завершил соединения с клиентами.", 3);
        }

        public void RunAsync()
        {
            Log(name + " listenning port " + port, 3);
            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            running_ = true;
            while (running_)
            {
                try
                {
                    allDone.Reset();
                    //?? listener.Listen(10);
                    sListener.BeginAccept(new AsyncCallback(acceptCallback), sListener);
                    bool isRequest = allDone.WaitOne(new TimeSpan(12, 0, 0));  // Blocks for 12 hours

                    if (!isRequest)
                    {
                        allDone.Set();
                        // Do some work here every 12 hours
                    }
                }
                catch (ThreadInterruptedException ex1)
                {
                    Log(name + ex1.ToString(), 3);
                }
                catch (ThreadAbortException ex2)
                {
                    Log(name + ex2.ToString(), 3);
                }
                catch (Exception ex)
                {
                    Log(name + ",mmm=" + mmm + ",error:" + ex.ToString(), 3);
                }
            }
        }
        
        public void Stop()
        {
            running_ = false;
            allDone.Set();
            //if (sListener.Blocking)
            //sListener.Dispose(); //to break listenning
        }
        //установить соединение, начать прием
        void acceptCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            Socket listener = (Socket)ar.AsyncState;

            if (listener != null)
            {
                Socket handler = listener.EndAccept(ar);

                // Signal main thread to continue
                allDone.Set();

                // Create state
                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, StateObject.BUFSIZE, 0, new AsyncCallback(readCallback), state);
            }
        }
        //завершить прием
        void readCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            if (!IsSocketConnected(handler))
            {
                handler.Close();
                return;
            }

            int read = handler.EndReceive(ar);
            if(loglevel > 0)
                Log(string.Format("Id={0}, read={1}, dlen={2} data={3}", 
                    state.Id, read, state.sb.Length, state.sb.ToString()));

            // Data was read from the client socket.
            if (read > 0)
            {   //AB 2021-06-07 for big data continue reading
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, read));
                string dd = state.sb.ToString();
                int ipos = dd.IndexOf("Content-Length:");
                if (ipos > 0)
                {   //asume html-protocol
                    int ipos2 = dd.IndexOf("\r\n\r\n");
                    if( ipos2 > ipos )
                    {
                        //extract Content-Length
                        int ipos3 = dd.IndexOf("\r\n", ipos);
                        if( ipos3 > 0 )
                        {
                            int iClen = 0;
                            string cl = dd.Substring(ipos + 15, ipos3 - ipos - 15).Trim();
                            if (int.TryParse(cl, out iClen))
                            {
                                if( dd.Length - ipos3 - 2 < iClen )
                                {   //не все, пробуем продолжить читать
                                    handler.BeginReceive(state.buffer, 0, StateObject.BUFSIZE, 0, new AsyncCallback(readCallback), state);
                                    return;
                                }
                            }
                        }
                    }
                }

                if (read == StateObject.BUFSIZE)
                {   //полный буфер, пробуем продолжить читать
                    handler.BeginReceive(state.buffer, 0, StateObject.BUFSIZE, 0, new AsyncCallback(readCallback), state);
                    return;
                }
            }

            byte[] bytesToSend = pc_hand != null ? pc_hand(state) : Process(state);
            if (bytesToSend != null)
            {   //ответ
                state.bytesToSend = bytesToSend;
                state.offset = 0;
                int len = bytesToSend.Length;
                if (len > StateObject.BUFSIZE) len = StateObject.BUFSIZE;
                handler.BeginSend(bytesToSend, 0, len, SocketFlags.None,
                    new AsyncCallback(sendCallback), state);
            }
            else
            {   //нечего вернуть - просто закрыть
                //if (!bKeep)
                    handler.Close();
            }
        }

        //завершить передачу
        void sendCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int iSent = handler.EndSend(ar);
            if (iSent <= 0)
            {
                Log(name + ",mmm=" + mmm + ",error:" + "no data sent", 3);
                handler.Close();
                return;
            }

            if (!IsSocketConnected(handler))
            {
                handler.Close();
                return;
            }

            //AB 2021-06-07 for big data continue sending
            if (state.offset + iSent < state.bytesToSend.Length)
            {
                state.offset += iSent;
                int len = state.bytesToSend.Length - state.offset;
                if (len > StateObject.BUFSIZE) len = StateObject.BUFSIZE;
                handler.BeginSend(state.bytesToSend, state.offset, len, SocketFlags.None,
                    new AsyncCallback(sendCallback), state);
                return;
            }

            if (!bKeep)
            {
                handler.Close();
                return;
            }

            //read a new message
            StateObject newstate = new StateObject();
            newstate.workSocket = handler;
            handler.BeginReceive(newstate.buffer, 0, StateObject.BUFSIZE, 0, new AsyncCallback(readCallback), newstate);
        }

        // Checks if the socket is connected
        public static bool IsSocketConnected(Socket s)
        {
            return !((s.Poll(POLL_MS_CONN, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }
        // Returns the string between str1 and str2
        public static string Between(string str, string str1, string str2)
        {
            string rtn = "";
            int i1 = str.IndexOf(str1, StringComparison.InvariantCultureIgnoreCase);
            if (i1 > -1)
            {
                i1 += str1.Length;
                int i2 = str.IndexOf(str2, i1, StringComparison.InvariantCultureIgnoreCase);
                if (i2 > -1)
                {
                    rtn = str.Substring(i1, i2 - i1);
                }
            }
            return rtn;
        }

        //log messages to console and file
        public static void Log(String s, int newlevel = 0)
        {
            if (newlevel == 0)
                newlevel = loglevel;

            if ((newlevel & 0x1) > 0)
                Console.WriteLine(s);
            if ((newlevel & 0x2) == 0)
                return;
            lock (locker)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(sLogFile, true, Encoding.UTF8);
                    sw.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss} {1}", DateTime.Now, s));
                    sw.Close();
                }
                catch (Exception se)
                {
                    Console.WriteLine(se.Message);
                }
            }
        }

        byte[] Process(StateObject state)
        {
            byte[] bytesToSend = null;
            if (state.sb.ToString().Contains(sEnd))
            {
                string toSend = "";
                string cmd = Between(state.sb.ToString(), sStart, sEnd);

                switch (cmd)
                {
                    case "Hi!":
                        toSend = "How are you?";
                        break;
                    case "Milky Way?":
                        toSend = "No I am not.";
                        break;
                    default:
                        toSend = "Cmd unknown:" + cmd;
                        break;
                }
                Log(Thread.CurrentThread.ManagedThreadId + ", cmd=" + cmd, 3);
                mmm++;
                toSend = mmm + toSend + sEnd;

               bytesToSend = Encoding.UTF8.GetBytes(toSend);
            }
            return bytesToSend;
        }
    }

    class Program
    {
        static int mmm = 0;
        static string sStart = "Host: ", sEnd = "\r\n\r\n";
        //multithreading dictionary
        static Dictionary<string, string> myDic = new Dictionary<string, string>();
        static StringBuilder sb2 = new StringBuilder();
        static object locker = new object();
        static string GetValue(string key)
        {
            lock (locker)
            {
                string val = null;
                if (myDic.TryGetValue(key, out val))
                    return val;
            }
            return null;
        }
        static bool AddKeyValue(string key, string val)
        {
            lock (locker)
            {
                if (myDic.ContainsKey(key))
                {
                    myDic[key] = val;
                    return false;
                }
                else
                {
                    myDic.Add(key, val);
                    return true;
                }
            }
        }
        static byte[] ProcessMy1(StateObject state)
        {
            byte[] bytesToSend = null;
            if (state.sb.ToString().Contains(sEnd))
            {
                string toSend = "";
                string cmd = SocketServer.Between(state.sb.ToString(), sStart, sEnd);

                switch (cmd)
                {
                    default:
                        toSend = "A Cmd unknown:" + cmd;
                        break;
                }
                SocketServer.Log(Thread.CurrentThread.ManagedThreadId + ", cmd=" + cmd, 3);
                mmm++;
                toSend = mmm + toSend + sEnd;

                AddKeyValue(cmd, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                bytesToSend = Encoding.UTF8.GetBytes(toSend);
            }
            return bytesToSend;
        }
        static byte[] ProcessMy2(StateObject state)
        {
            byte[] bytesToSend = null;
            if (state.sb.ToString().Contains(sEnd))
            {
                string toSend = "";
                string cmd = SocketServer.Between(state.sb.ToString(), sStart, sEnd);

                switch (cmd)
                {
                    default:
                        toSend = "B Cmd unknown:" + cmd;
                        break;
                }
                SocketServer.Log(Thread.CurrentThread.ManagedThreadId + ", cmd=" + cmd, 3);
                mmm++;
                //toSend = mmm + toSend + sEnd;
                //AddKeyValue("B" + mmm.ToString(), cmd);
                sb2.Clear();
                foreach (var dd in myDic)
                {
                    sb2.AppendFormat("{0}={1};", dd.Key, dd.Value);
                }
                bytesToSend = Encoding.UTF8.GetBytes(sb2.ToString());
            }
            return bytesToSend;
        }
        /*
        static void Main2(string[] args)
        {
            SocketServer.Log("SocketServer started", 3);
            List<Thread> theList = new List<Thread>();
            List<SocketServer> sockList = new List<SocketServer>();

            string s, host = "", name = "";
            int port = 0;
            try
            {
                s = ConfigurationManager.AppSettings["loglevel"];
                if (!string.IsNullOrEmpty(s))
                    SocketServer.loglevel = int.Parse(s);

                s = ConfigurationManager.AppSettings["name"];
                if (!string.IsNullOrEmpty(s))
                    name = s;
                else name = "Tester";

                s = ConfigurationManager.AppSettings["port"];
                if (!string.IsNullOrEmpty(s))
                    port = int.Parse(s);

                s = ConfigurationManager.AppSettings["host"];
                if (!string.IsNullOrEmpty(s))
                    host = s;

                //it can be more then 1 server

                SocketServer tn = new SocketServer(name, host, port);
                tn.sStart = "Host: ";
                tn.pc_hand = ProcessMy1;
                sockList.Add(tn);

                Thread workerThread = new Thread(tn.RunAsync);//tn.DoWork);
                // Start thread with a parameter  
                workerThread.Start();
                theList.Add(workerThread);

                //second server
                SocketServer tn2 = new SocketServer(name + 2, host, port+1);
                tn2.sStart = "Host: ";
                tn2.pc_hand = ProcessMy2;
                sockList.Add(tn2);

                Thread workerThread2 = new Thread(tn2.RunAsync);
                // Start thread with a parameter  
                workerThread2.Start();
                theList.Add(workerThread2);


                //tn.DoWork();
                int step = 0;
                while (true)
                {
                    if (step % 10 == 0)
                        Console.Write("*");
                    Thread.Sleep(1000);
                    step++;
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo kk = Console.ReadKey(true);
                        if (kk.Key == ConsoleKey.Q)//.Enter)
                        {
                            Console.Write("#");
                            foreach(var dd in myDic)
                            {
                                Console.WriteLine(dd.Key + "=" + dd.Value);
                            }
                            for (int m = 0; m < theList.Count; m++)
                            {
                                sockList[m].Stop();

                                workerThread = theList[m];
                                if (workerThread.IsAlive)
                                {
                                    workerThread.Interrupt();
                                    if (!workerThread.Join(2000))
                                    {   // or an agreed resonable time
                                        workerThread.Abort();
                                    }
                                }
                            }
                            break;
                        }
                        else Console.WriteLine("Press 'q' to quit");
                    }
                }
            }
            catch (ThreadInterruptedException ex1)
            {
                SocketServer.Log(ex1.ToString(), 3);
            }
            catch (ThreadAbortException ex2)
            {
                SocketServer.Log(ex2.ToString(), 3);
            }
            catch (Exception ex)
            {
                SocketServer.Log(ex.ToString(), 3);
            }
            finally
            {
                SocketServer.Log("SocketServer finished", 3);
                Console.WriteLine("Press 'Enter'");
                Console.ReadLine();
            }
        }
        */
    }
}
