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

//telnet alizar.habrahabr.ru 80
//GET / HTTP/1.1\r\nHost: alizar.habrahabr.ru\r\n\r\n

namespace MathPanelExt
{
    public class SocketClient
    {
        static object locker = new object();
        static int BUFSIZE = 4096;
        static string sLogFile = "socket_client.log";
        static int loglevel = 0;

        static string test1 = "GET / HTTP/1.1\r\nHost: {0}\r\n\r\n";

        public string sSend = "";
        public int nIter = 10;
        int port;
        string name = "tunnel", host;
        Socket cliSocket;
        IPEndPoint proxyEndPoint;
        byte[] buffer = new byte[BUFSIZE];
        DateTime dtSess;
        Random rnd = new Random();
        StringBuilder builder = new StringBuilder();

        public SocketClient(string _name, string _host, int _port)
        {
            name = _name;
            host = _host;
            port = _port;

            dtSess = DateTime.Now;

            IPAddress proxyIP = null;
            try
            {
                proxyIP = IPAddress.Parse(host);
            }
            catch (FormatException)
            {   // get the IP address
                proxyIP = Dns.GetHostEntry(host).AddressList[0];
            }

            proxyEndPoint = new IPEndPoint(proxyIP, port);
            Log("SocketClient created", 3);
        }
        void Connect()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            cliSocket = s;

            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            //s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 30000);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 30000);
            //задержка при закрытии, если имеются неотправленные данные
            LingerOption linger = new LingerOption(true, 10);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, linger);
            s.ReceiveBufferSize = BUFSIZE;
            s.SendBufferSize = BUFSIZE;

            cliSocket.Connect(proxyEndPoint);
            Log("SocketClient connected", 3);
        }
        public void Run()
        {
            //running_ = true;
            try 
            { 
                for (int i = 0; i < nIter; i++)
                {
                    Connect();

                    string message;
                    if(string.IsNullOrEmpty(sSend))
                        message = string.Format(test1, name);
                    else
                    {
                        if(sSend.IndexOf("{0}") >= 0)
                            message = string.Format(sSend, name);
                        else message = sSend;
                    }
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    cliSocket.Send(data);
                    Log("sent " + message, 3);
                    Thread.Sleep(rnd.Next(100));

                    // получаем ответ
                    builder.Clear();
                    int bytes; // количество полученных байт
                    do
                    {
                        bytes = cliSocket.Receive(buffer, buffer.Length, 0);
                        builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                    }
                    while (cliSocket.Available > 0);
                    Log("от сервера: " + builder.ToString(), 3);

                    // закрываем сокет
                    cliSocket.Shutdown(SocketShutdown.Both);
                    cliSocket.Close();

                    Thread.Sleep(rnd.Next(100));
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString(), 3);
            }
            //running_ = false;
        }

        public string LastResponse()
        {
            return builder.ToString();
        }

        //log messages to console and file
        static void Log(String s, int newlevel = 0)
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
        /*
        static void Main2(string[] args)
        {
            Log("Main started", 3);
            List<Thread> theList = new List<Thread>();
            List<SocketClient> sockList = new List<SocketClient>();

            string s, host = "", name;
            int port = 0;
            try
            {
                s = ConfigurationManager.AppSettings["loglevel"];
                if (!string.IsNullOrEmpty(s))
                    loglevel = int.Parse(s);

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

                Console.WriteLine("Press 'q' to quit");
                for (int i = 0; i < 10; i++)
                {
                    SocketClient tn = new SocketClient(name + i, host, i == 0 ? port+1 : port);
                    if (i == 0)
                        tn.nIter = 20;
                    sockList.Add(tn);

                    Thread workerThread = new Thread(tn.Run);
                    // Start thread with a parameter  
                    workerThread.Start();
                    theList.Add(workerThread);
                    //tn.Run();
                    //Thread.Sleep(1000);
                }
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
                            for (int m = 0; m < theList.Count; m++)
                            {
                                Socket sock = sockList[m].cliSocket;
                                if (sock.Blocking)
                                    sock.Dispose(); //to break listenning

                                var workerThread = theList[m];
                                if (workerThread.IsAlive)
                                {
                                    sockList[m].running_ = false;
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
            catch (Exception ex)
            {
                Log(ex.ToString(), 3);
            }
            finally
            {
                Log("client finished", 3);
                Console.WriteLine("Press 'Enter'");
                Console.ReadLine();
            }
        }
        */
    }
}
