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
        //начало и конец сообщения от клиента
        static string sStart = "Host: ", sEnd = "\r\n\r\n";
        //словарь с поддержкой многопоточности
        static Dictionary<string, string> myDic = new Dictionary<string, string>();
        //для ускорения работы со строками используем StringBuilder
        static StringBuilder sb2 = new StringBuilder();
        //блокировщик для синхронизации доступа
        static object locker = new object();

        //получить значение из словаря по ключу
        static string GetValue(string key)
        {
            //блокировка, доступ к коду только у одного потока
            lock (locker)
            {
                string val = null;
                if (myDic.TryGetValue(key, out val))
                    return val;
            }
            return null;
        }

        //добавить пару ключ-значение в словарь
        static bool AddKeyValue(string key, string val)
        {
            //блокировка, доступ к коду только у одного потока
            lock (locker)
            {
                if (myDic.ContainsKey(key))
                {   //старый ключ
                    myDic[key] = val;
                    return false;
                }
                else
                {   //новый ключ
                    myDic.Add(key, val);
                    return true;
                }
            }
        }

        //обработчик данных от клиента
        static byte[] ProcessMy1(StateObject state)
        {
            byte[] bytesToSend = null;
            if (state.sb.ToString().Contains(sEnd))
            {   //сообщение от клиента принято
                string toSend = "";
                //получаем код клиента
                string cmd = SocketServer.Between(state.sb.ToString(), sStart, sEnd);

                switch (cmd)
                {
                    default:
                        toSend = "A Cmd unknown:" + cmd;
                        break;
                }
                //логирование данных
                SocketServer.Log(Thread.CurrentThread.ManagedThreadId + ", cmd=" + cmd, 3);
                mmm++;
                toSend = mmm + toSend + sEnd;
                SocketServer.Log("toSend=" + toSend);
                //добавляем в словарь код клиента (ключ) и время (значение)
                AddKeyValue(cmd, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //подготовка байтов для отправки клиенту
                bytesToSend = Encoding.UTF8.GetBytes(toSend);
            }
            return bytesToSend;
        }

        //главный метод
        public void Execute()
        {   
            //массив цветов для рисования
            System.Drawing.Color[] clrs = new System.Drawing.Color[300];
            //создаем объект-сервер
            SocketServer tn = new SocketServer("Monitor", "127.0.0.1", 3345);
            //начало сообщения клиента
            tn.sStart = "Host: ";
            //обработчик
            tn.pc_hand = ProcessMy1;
            //запускаем сервер в отдельном потоке
            Thread workerThread = new Thread(tn.RunAsync);
            workerThread.Start();

            Dynamo.Console("Нажать 'q' для завершения");
            int step = 0;
            while (true)
            {
                sb2.Clear(); //очистить текстовый объект
                //формируем текст в виде пар ключ=значение;
                foreach (var dd in myDic)
                {
                    sb2.AppendFormat("{0}={1};", dd.Key, dd.Value);
                }
                var resp = sb2.ToString();
                //Dynamo.Console("$" + resp);

                //инициируем таблицу цветов черным
                for (int j = 0; j < clrs.Length; j++) clrs[j] = System.Drawing.Color.Black;

                DateTime dt0 = DateTime.Now, dt2;
                var arr = resp.Split(';');
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] != "")
                    {   //для каждой пары
                        var arr2 = arr[j].Split('=');
                        //из ключа находим хеш-код и далее индекс в таблице как остаток от деления
                        int k = Math.Abs(arr2[0].GetHashCode()) % 300;
                        //находим время, связанное с ключем
                        dt2 = DateTime.ParseExact(arr2[1], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        //находим задержку
                        TimeSpan difference = dt0 - dt2;
                        //находим градацию цвета, чем меньше задержка, тем краснее ячейка
                        int m = (int)Math.Max(255 - 10 * difference.TotalSeconds, 0);
                        if (m > 255) m = 255;
                        //Dynamo.Console(k + ":" + m);
                        //формируем цвет для ячейки
                        clrs[k] = System.Drawing.Color.FromArgb(m, 0, 0);
                    }
                }

                //рисуем таблицу 15 * 20 из сформированных цветов
                var s1 = MathPanelExt.QuadroEqu.DrawBitmap(15, 20, clrs);
                //задаем парметры рисования options
                string s2 = "{\"options\":{\"x0\": -0.5, \"x1\": 20, \"y0\": -0.5, \"y1\": 15, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":40, \"lnw\": 3, \"wid\": 800, \"hei\": 600, \"second\":1 }";
                s2 += ", \"data\":[" + s1 + "]}";
                //вывод в канвас
                Dynamo.SceneJson(s2);

                //if (step % 3== 0)
                //Dynamo.Console("*", false);
                if (step % 30 == 0)
                    Dynamo.Console("Нажать 'q' для завершения");
                //дать отдохнуть потоку 1 секунду
                System.Threading.Thread.Sleep(1000);
                step++;
                if (Dynamo.KeyConsole == "Q")
                {   //давай до свидания
                    break;
                }
                //else Dynamo.Console(Dynamo.KeyConsole + "Press 'q' to quit");
            }

            if (workerThread.IsAlive)
            {
                workerThread.Interrupt();
                if (!workerThread.Join(2000))
                {   //если не закончил работы в разумное время, прервать
                    workerThread.Abort();
                }
            }
        }
    }
}

    
