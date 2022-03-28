//test20_clients.cs
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
        //обработчик данных от клиента
        static void ReceiveMy1(string sData)
        {
            Dynamo.Console(sData);
        }

        //главный метод
        public void Execute()
        {
            string host = "127.0.0.1"; //адрес сервера
            string name = "super_cl"; //префикс кода клиента
            int port = 3345; //порт связи
            int nIter = 10; //число итераций
            int nClients = 300; //число клиентов
            //список потоков
            List<Thread> theList = new List<Thread>();
            //список клиентов
            List<SocketClient> sockList = new List<SocketClient>();

            Dynamo.Console("Нажать 'q' для завершения");
            //создать клиентов
            for (int i = 0; i < nClients; i++)
            {
                SocketClient tn = new SocketClient(name + i, host, port);
                tn.nIter = nIter;
                tn.iSleep = 2000; //задержка после связи 2 секунды
                tn.pc_hand = ReceiveMy1;
                sockList.Add(tn);

                Thread workerThread = new Thread(tn.Run);
                //запустить в отдельном потоке  
                workerThread.Start();
                theList.Add(workerThread);
            }

            int step = 0; //шаг процесса
            while (true)
            {
                if (step % 10 == 0)
                    Dynamo.Console("*");
                Thread.Sleep(1000);
                step++;
                if( Dynamo.KeyConsole == "Q")
                {   //давай до свидания
                    Dynamo.Console("#");
                    for (int m = 0; m < theList.Count; m++)
                    {
                        Socket sock = sockList[m].cliSocket;
                        if (sock.Blocking)
                            sock.Dispose(); //прекратить работу

                        var workerThread = theList[m];
                        if (workerThread.IsAlive)
                        {
                            sockList[m].running_ = false;
                            workerThread.Interrupt();
                            if (!workerThread.Join(2000))
                            {   //если не закончил работы в разумное время, прервать
                                workerThread.Abort();
                            }
                        }
                    }
                    break;
                }
                if (step % 100 == 0) Dynamo.Console("Нажать 'q' для завершения");
            }
        }
    }
}
    
