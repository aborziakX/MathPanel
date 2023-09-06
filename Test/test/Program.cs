using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Configuration;
using System.IO;

using MathPanel;


namespace MathPanel
{
    public partial class Dynamo
    {
        static string script_dir = ""; //script dir
        static string must_dir = ""; //must dir
        static string mode = "0"; //0-test, 1-generate
        static void Main(string[] args)
        {
            //чтение конфигурации
            string s = ConfigurationManager.AppSettings["netframworkpath"];
            if (!string.IsNullOrEmpty(s))
                frmPath = s;
            s = ConfigurationManager.AppSettings["canvas_width"];
            if (!string.IsNullOrEmpty(s))
                iCanvasWidth = int.Parse(s);
            s = ConfigurationManager.AppSettings["canvas_height"];
            if (!string.IsNullOrEmpty(s))
                iCanvasHeight = int.Parse(s);
            s = ConfigurationManager.AppSettings["canvas_bg"];
            if (!string.IsNullOrEmpty(s))
                canvasBg = (s);
            s = ConfigurationManager.AppSettings["user"];
            if (!string.IsNullOrEmpty(s))
                user = s;
            s = ConfigurationManager.AppSettings["pass"];
            if (!string.IsNullOrEmpty(s))
                pass = s;
            s = ConfigurationManager.AppSettings["server"];
            if (!string.IsNullOrEmpty(s))
                server = s;

            s = ConfigurationManager.AppSettings["loglevel"];
            if (!string.IsNullOrEmpty(s))
                int.TryParse(s, out loglevel);

            s = ConfigurationManager.AppSettings["local"];

            s = ConfigurationManager.AppSettings["script_dir"];
            if (!string.IsNullOrEmpty(s))
                script_dir = s;
            s = ConfigurationManager.AppSettings["must_dir"];
            if (!string.IsNullOrEmpty(s))
                must_dir = s;
            s = ConfigurationManager.AppSettings["mode"];
            if (!string.IsNullOrEmpty(s))
                mode = s;

            System.Console.WriteLine("test started " + mode + "," + script_dir + "," + must_dir);

            //Dynamo.frmPath = frmPath;
            //вывести список файлов
            string[] files1 = Directory.GetFiles(script_dir, "*.cs");
            //for (int i = 0; i < files1.Length; i++) Console.WriteLine(files1[i]);
            //в консоли копить данные - можно сохранить!
            try
            {
                if (mode == "0")
                {   //0-test
                    for (int i = 0; i < files1.Length; i++)
                    {
                        string data = File.ReadAllText(files1[i], Encoding.UTF8);
                        ConsoleTextClear();
                        Process(data);
                        if (my_thread.IsAlive)
                        {
                            if (!my_thread.Join(20000))
                            {   // or an agreed resonable time
                                System.Console.WriteLine("sConsoleText1=" + ConsoleText);
                            }
                            else System.Console.WriteLine("sConsoleText2=" + ConsoleText);
                        }
                        else System.Console.WriteLine("sConsoleText3=" + ConsoleText);

                        string f = files1[i].Replace(script_dir, must_dir) + ".txt";
                        string res = File.ReadAllText(f, Encoding.UTF8);
                        if (res == ConsoleText)
                        {
                            System.Console.WriteLine("success " + files1[i]);
                        }
                        else
                        {
                            System.Console.WriteLine("failed " + files1[i]);
                        }
                    }
                }
                else if (mode == "1")
                {   //1-generate
                    for (int i = 0; i < files1.Length; i++)
                    {
                        string data = File.ReadAllText(files1[i], Encoding.UTF8);
                        ConsoleTextClear();
                        Process(data);
                        if (my_thread.IsAlive)
                        {
                            if (!my_thread.Join(20000))
                            {   // or an agreed resonable time
                                System.Console.WriteLine("sConsoleText1=" + ConsoleText);
                            }
                            else System.Console.WriteLine("sConsoleText2=" + ConsoleText);
                        }
                        else System.Console.WriteLine("sConsoleText3=" + ConsoleText);
                        string f = files1[i].Replace(script_dir, must_dir) + ".txt";
                        File.WriteAllText(f, ConsoleText, Encoding.UTF8);
                    }
                }
            } 
            catch(Exception ex)
            {
                var zz = ex.ToString();
                System.Console.WriteLine(zz);
                Log(zz);
            }
            System.Console.ReadKey();
        }

        // методы аналогичные в MainWindow.xaml.cs , использующие dispObj
        public static void Console(string s, bool bNewLine = true)
        {
            sbConsoleText.Append(s); 
            if(bNewLine) sbConsoleText.Append("\r\n");
            System.Console.WriteLine(s);
        }

        public static void SceneDraw(bool bCons = false)
        {
            SceneDrawHelper(bCons);
            Console(screenJson);
        }

        public static void SceneDrawShape(bool bBx = true, bool bCons = false)
        {
            SceneDrawShapeHelper(bBx, bCons);
            Console(screenJson);
        }
    }
}
