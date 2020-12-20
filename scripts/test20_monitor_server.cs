//test20_monitor_server
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
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

        public void Execute()
        {
            System.Drawing.Color[] clrs = new System.Drawing.Color[300];

            SocketServer tn = new SocketServer("Monitor", "127.0.0.1", 3345);
            tn.sStart = "Host: ";
            tn.pc_hand = ProcessMy1;
            Random rnd = new Random();

            Thread workerThread = new Thread(tn.RunAsync);
            workerThread.Start();

            Dynamo.Console("Press 'q' to quit");
            int step = 0;
            while (true)
            {
                sb2.Clear();
                foreach (var dd in myDic)
                {
                    sb2.AppendFormat("{0}={1};", dd.Key, dd.Value);
                }
                var resp = sb2.ToString();
                //Dynamo.Console("$" + resp);

                for (int j = 0; j < clrs.Length; j++) clrs[j] = System.Drawing.Color.Black;
                DateTime dt0 = DateTime.Now, dt2;
                var arr = resp.Split(';');
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] != "")
                    {
                        var arr2 = arr[j].Split('=');
                        int k = Math.Abs(arr2[0].GetHashCode()) % 300;
                        dt2 = DateTime.ParseExact(arr2[1], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = dt0 - dt2; //create TimeSpan object
                        int m = (int)Math.Max(255 - 10 * difference.TotalSeconds, 0);
                        if (m > 255) m = 255;
                        //Dynamo.Console(k + ":" + m);
                        clrs[k] = System.Drawing.Color.FromArgb(m, 0, 0);
                    }
                }

                var s1 = MathPanelExt.QuadroEqu.DrawBitmap(15, 20, clrs);
                string s2 = "{\"options\":{\"x0\": -0.5, \"x1\": 20, \"y0\": -0.5, \"y1\": 15, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":40, \"lnw\": 3, \"wid\": 800, \"hei\": 600, \"second\":1 }";
                s2 += ", \"data\":[" + s1 + "]}";
                Dynamo.SceneJson(s2);

                //if (step % 3== 0)
                //Dynamo.Console("*", false);
                Dynamo.Console("Press 'q' to quit");
                System.Threading.Thread.Sleep(1000);
                step++;
                if (Dynamo.KeyConsole == "Q")//.Enter)
                {
                    break;
                }
                //else Dynamo.Console(Dynamo.KeyConsole + "Press 'q' to quit");
            }

            if (workerThread.IsAlive)
            {
                workerThread.Interrupt();
                if (!workerThread.Join(2000))
                {   // or an agreed resonable time
                    workerThread.Abort();
                }
            }
        }
    }
}

    
