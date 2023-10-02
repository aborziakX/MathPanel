//#define OLDCODE
//раскомментируйте OLDCODE , чтобы использовать первоначальный код методов из книги
//2020-2023, Andrei Borziak
//MathPanel (математическая панель) для работы со скриптами, написанными на C#.
//Область применения – моделирование процессов и визуализация.
//В этом файле часть кода для .NET Framework xaml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Configuration;
using System.IO;
//for dynamo
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
//for open dialog
using Microsoft.Win32;
using System.Windows.Input;
using System.Web.Script.Serialization;

//была "Неизвестная ошибка сборки, Не удалось загрузить файл или сборку либо одну из их зависимостей. 
//Процесс не может получить доступ к файлу, так как этот файл занят другим процессом. (Исключение из HRESULT: 0x80070020)"	MathPanel"
//исправил путем задания x86 сборки

namespace MathPanel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Dynamo : Window
    {   
        //область статических переменных
        readonly static string version = " v1.0"; //версия
        static TextBox txtConsole, txtCommand; //окна сообщений и комманд
        static WebBrowser webConsole; //окно веб-браузера
        static System.Windows.Controls.Image imgStatic; //компонент с картинкой
        static System.Windows.Threading.Dispatcher dispObj; //диспетчер UI-потока, через него обращения к элементам UI из других потоков
        //test init
        readonly static string test1 = @"var hz = Math.Sqrt(3);
Dynamo.Console(hz.ToString());
";
        //конструктор
        public Dynamo()
        {
            InitializeComponent();

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
            if (!string.IsNullOrEmpty(s))
                DoLocal(s);

            /*//https://www.codeproject.com/Questions/707214/how-make-my-csharp-web-browser-to-support-html5
            try
            {   //you need administrative rights
                //For 32bit OS
                Microsoft.Win32.Registry.SetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                    "MathPanel.exe", 10001, Microsoft.Win32.RegistryValueKind.DWord);

                //For 64bit OS
                Microsoft.Win32.Registry.SetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION",
                    "MathPanel.exe", 10001, Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }*/

            //добавляем номер версии в заголовок
            this.Title += version;
            //добавляем обработчики для кнопок
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            button6.Click += Button6_Click;
            button7.Click += Button7_Click;
            //скрыть веб-браузер и компонент картинки
            web1.Visibility = Visibility.Hidden;
            img1.Visibility = Visibility.Hidden;

            //текущая директория
            string sDir = AppDomain.CurrentDomain.BaseDirectory;
            //загружаем страницу в веб-браузер
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["visual_page"]))
                web1.Navigate("file:///" + sDir + "test_graph.htm");
            else web1.Navigate(ConfigurationManager.AppSettings["visual_page"]);

            //сохраняем объекты окна в статических переменных
            txtCommand = textBlock1;
            txtConsole = textBlock2;
            textBlock1.Text = test1;
            webConsole = web1;
            imgStatic = img1;
            dispObj = Dispatcher;

            //программа успешно стартовала
            bReady = true;
            this.Closing += OnBeforeClosing; //обработчик для остановки потоков
            SceneClear(); //очистить сцену
            boxShape.iFill = 2; //ребра
            CameraZ = 100;

            //выполняем стартовый скрипт, если задан
            s = ConfigurationManager.AppSettings["startup"];
            if (!string.IsNullOrEmpty(s))
            {
                try
                {
                    string data = File.ReadAllText(s, Encoding.UTF8);
                    textBlock1.Text = data;
                    Process(data);
                }
                catch(Exception ex)
                {
                    Console(ex.ToString());
                }
            }

            //здесь были тесты для упрощения синтаксического анализа скриптов
            //DbTest();
            //JavaScriptSerializer jj = new JavaScriptSerializer();
            //var q = jj.Serialize("aa\r\nbb;");
            //Console("q=" + q);

            /*//test1
            var m = new Mat3();
            m.a = new Vec3(1, 5, 3);
            m.b = new Vec3(2, 4, 7);
            m.c = new Vec3(4, 6, 2);
            Console("q=" + m.Determinant()); //must 74*/

            /*//test2
            var m = new Mat3();
            var m2 = new Mat3();
            var m3 = new Mat3();
            m.a = new Vec3(1, 2, 3);
            m.b = new Vec3(0, 1, 4);
            m.c = new Vec3(5, 6, 0);
            Console("orig=" + m.ToString());
            //m.Transp();
            m.Inverse(ref m2);
            Console("inverse=" + m2.ToString());
            m.Mult(m2, ref m3);
            Console("mult=" + m3.ToString());*/
        }

        //вызывается сразу после Window.Close() и может использоваться для отмены
        void OnBeforeClosing(object sender, EventArgs e)
        {
            bReady = false;
            //остановка потоков со скриптами
            foreach (var thr in lstThr)
            {
                if (thr.IsAlive) thr.Abort();
            }
        }

        //обработчик кнопки "Выполнить"
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (!bReady)
            {
                MessageBox.Show("Не готово!");
                return;
            }
            Process(textBlock1.Text);
        }

        //скрыть/показать элементы интерфейса
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if( img1.IsVisible )
            {
                img1.Visibility = Visibility.Hidden;
                Label1.Visibility = Visibility.Hidden;
                Label2.Visibility = Visibility.Hidden;
                sv1.Visibility = Visibility.Hidden;
                textBlock2.Visibility = Visibility.Hidden;
                web1.Visibility = Visibility.Visible;
            }

            Label1.Visibility = Label1.IsVisible ? Visibility.Hidden : Visibility.Visible;
            Label2.Visibility = Label2.IsVisible ? Visibility.Hidden : Visibility.Visible;
            sv1.Visibility = sv1.IsVisible ? Visibility.Hidden : Visibility.Visible;
            textBlock2.Visibility = textBlock2.IsVisible ? Visibility.Hidden : Visibility.Visible;
            web1.Visibility = web1.IsVisible ? Visibility.Hidden : Visibility.Visible;
            button2.Content = Label1.IsVisible ? "График" : "Команды";
        }

        //загрузить скрипт из файла
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string data;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                data = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                textBlock1.Text = data;
                this.Title = "MathPanel " + version + " - " + openFileDialog.FileName;
            }
        }

        //сохранить скрипт в файле
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                CheckFileExists = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(openFileDialog.FileName, textBlock1.Text, Encoding.UTF8);
                this.Title = "MathPanel " + version + " - " + openFileDialog.FileName;
            }
        }

        //обработчик кнопки "Компилировать"
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Not ready");
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                CheckFileExists = false
            };
            if (openFileDialog.ShowDialog() == false) return;
            string fname = openFileDialog.FileName;
            if (fname.LastIndexOf(".cs") == fname.Length - 3)
            {
                var data = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                textBlock1.Text = data;
                fname = fname.Substring(0, fname.Length - 3) + ".dll";
            }
            string sourceDll = textBlock1.Text;
            string outputAssembly = fname;// @"c:\temp\Foo.dll";
            CompilerResults results;
            try
            {
                CSharpCodeProvider provider = new CSharpCodeProvider();
                //признак build dll
                CompilerParameters compilerParams2 = new CompilerParameters
                { OutputAssembly = outputAssembly, GenerateExecutable = false };
                //to allow LINQ
                compilerParams2.ReferencedAssemblies.Add("System.Core.Dll");

                //Компиляция dll
                results = provider.CompileAssemblyFromSource(compilerParams2, sourceDll);
                //Выводим информацию об ошибках
                Console(string.Format("Number of Errors DLL: {0}", results.Errors.Count));
                foreach (CompilerError err in results.Errors)
                {
                    Console(string.Format("ERROR {0}", err.ErrorText));
                }
                if (sourceDll.Contains("QuadroEquDemo"))
                {
                    //тест
                    Console(string.Format("Try Assembly:"));
                    Assembly assembly = Assembly.LoadFile(outputAssembly);
                    Type type = assembly.GetType("MathPanelExt.QuadroEquDemo");
                    MethodInfo method = type.GetMethod("Solve");
                    double x1 = 0, x2 = 0;
                    object[] pars = new object[5];
                    pars[0] = 1.0;
                    pars[1] = 0.0;
                    pars[2] = -1.0;
                    pars[3] = null;
                    pars[4] = null;
                    object res = method.Invoke(null, pars);
                    //bool blResult = (bool)res;
                    //if (blResult)
                    {
                        x1 = (double)pars[3];
                        x2 = (double)pars[4];
                    }
                    Console(string.Format("x1 {0}, x2 {1}", x1, x2));
                }
            }
            catch (Exception ex) { Console(ex.ToString()); }
            return;
        }

        //обработчик кнопки "Новый скрипт"
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            string data = File.ReadAllText("template.cs", Encoding.UTF8);
            textBlock1.Text = data;
            this.Title = "MathPanel " + version + " - ";
        }

        //обработчик кнопки "Картинка"
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            sv1.Visibility = Visibility.Hidden;
            textBlock2.Visibility = Visibility.Hidden;
            web1.Visibility = Visibility.Hidden;
            img1.Visibility = Visibility.Visible;
            button2.Content = "Команды";

            string path;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                var bitmap = new System.Windows.Media.Imaging.BitmapImage(new Uri(path, UriKind.Absolute));
                img1.Source = bitmap;
            }
        }

        //Загрузить файл с изображением в компонент
        public static void SetBitmapImage(string path)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                imgStatic.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(path, UriKind.Absolute));
            });
        }

        //обработчик нажатия клавиатуры
        private void MyPreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.F1)
            string key = e.Key.ToString();
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
            {
                if (key == "OemPlus")
                {
                    ZRotor += 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
                else if (key == "OemMinus")
                {
                    ZRotor -= 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
                else if (key == "Up")
                {
                    XRotor -= 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
                else if (key == "Down")
                {
                    XRotor += 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
                else if (key == "Left")
                {
                    YRotor -= 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
                else if (key == "Right")
                {
                    YRotor += 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
            }
            else
            {
                if (key == "OemPlus") ZBoXTrans += 0.3;
                else if (key == "OemMinus") ZBoXTrans -= 0.3;
                else if (key == "Up") YBoXTrans += 0.3;
                else if (key == "Down") YBoXTrans -= 0.3;
                else if (key == "Left") XBoXTrans -= 0.3;
                else if (key == "Right") XBoXTrans += 0.3;
            }
            //Console(key);
            keyConsole = key;
        }

        //API
        /// <summary>
        /// всплывающее окно
        /// </summary>
        /// <param name="s">текст сообщения</param>
        public static void Alert(string s)
        {
            //s = Eval("1+3", typeof(int)).ToString();
            MessageBox.Show(s);
        }

        //запускаем циклические процессы в отдельном потоке
        //чтобы обновлять интерфейс пользователя, используем запуск кода в UI потоке как орисано по ссылке ниже
        //https://ru.stackoverflow.com/questions/615113/%D0%9F%D0%BE%D1%87%D0%B5%D0%BC%D1%83-thread-sleep-%D0%B2%D0%B5%D0%B4%D1%91%D1%82-%D1%81%D0%B5%D0%B1%D1%8F-%D0%BD%D0%B5%D0%BF%D1%80%D0%B0%D0%B2%D0%B8%D0%BB%D1%8C%D0%BD%D0%BE-%D0%9A%D0%B0%D0%BA-%D0%BC%D0%BD%D0%B5-%D1%81%D0%B4%D0%B5%D0%BB%D0%B0%D1%82%D1%8C-%D0%B7%D0%B0%D0%B4%D0%B5%D1%80%D0%B6%D0%BA%D1%83-%D0%B8%D0%BB%D0%B8-%D0%B4%D0%BB%D0%B8%D0%BD%D0%BD%D1%8B%D0%B5
        /// <summary>
        /// добавить текст в окно сообщений
        /// </summary>
        /// <param name="s">текст</param>
        public static void Console(string s, bool bNewLine = true)
        {
            //txtConsole.Text += (s + "\r\n");
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady)
                {
                    sbConsoleText.Append(s + (bNewLine ? "\r\n" : ""));
                    txtConsole.Text = ConsoleText;
                }
            });
        }

        /// <summary>
        /// очистить текст в окне сообщений
        /// </summary>
        public static void ConsoleClear()
        {
            //txtConsole.Text = "";
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady)
                {
                    ConsoleTextClear();
                    txtConsole.Text = "";
                }
            });
        }

        /*//temp, hide later
        public static void ScreenJsonSaveDB(string scid)
        {
            try
            {
                var db = new MathPanelExt.DB();
                if (!db.Connect("Host=127.0.0.1;Username=soapbus;Password=1;Database=soapbus"))
                {
                    Console(db.LastError);
                    return;
                }
                string q = string.Format("INSERT INTO tblScripresult (scid,plevel,dtModif,data) VALUES ({1},1,NOW(),'{0}')",
                    screenJson, scid);
                if (!db.Execute(q))
                {
                    Console(db.LastError);
                    return;
                }
                db.Close();
            }
            catch (Exception ex)
            {
                Console(ex.ToString());
            }
        }
        public static string[] ScreenJsonLoadDB(string scid, string scrid)
        {
            try
            {
                var db = new MathPanelExt.DB();
                if (!db.Connect("Host=127.0.0.1;Username=soapbus;Password=1;Database=soapbus"))
                {
                    Console(db.LastError);
                    return null;
                }
                string q = string.Format("select scrid, data from tblScripresult WHERE scid={0} AND scrid>{1} ORDER BY scrid limit 1;",
                    scid, scrid);
                object[][] res = db.Vals(q);
                if (res == null)
                {
                    Console(db.LastError);
                    return null;
                }
                int i = 0;
                foreach (var v in res)
                {
                    int j = 0;
                    string[] res2 = new string[2];
                    foreach (var v2 in v)
                    {
                        //Console("[" + i + "," + j + "]=" + v2.ToString());
                        res2[j] = v2.ToString();
                        j++;
                    }
                    //Console("==");
                    i++;
                    return res2;
                }

                db.Close();
            }
            catch (Exception ex)
            {
                Console(ex.ToString());
            }
            return null;
        }*/

        /// <summary>
        /// передать JSON данные для визуализации в canvas
        /// </summary>
        public static void SceneJson(string s_json, bool bSecond = false, bool bCons = false)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            screenJson = s_json;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons)
                {
                    sbConsoleText.Append(screenJson + "\r\n");
                    txtConsole.Text = ConsoleText;
                }
                webConsole.InvokeScript("ext_json", screenJson, bSecond);
            });
        }

        /// <summary>
        /// подготовить данные для визуализации на канвасе
        /// </summary>
        /// <param name="bCons">вывод в окно сообщений</param>
        public static void SceneDraw(bool bCons = false)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            SceneDrawHelper(bCons);
            
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });
        }

        //2022-02-17
        /// <summary>
        /// подготовить данные для визуализации на канвасе используя формы
        /// <param name="bBx">рисовать оси</param>
        /// <param name="bCons">вывод в окно сообщений</param>
        /// <return>attribute value</return>
        /// </summary>
        public static void SceneDrawShape(bool bBx = true, bool bCons = false)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            SceneDrawShapeHelper(bBx, bCons);

            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });
        }
        //old low quality approach
        public static void SceneDrawShapeBook(bool bBx = true, bool bCons = false)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            SceneDrawShapeBookHelper(bBx, bCons);

            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });
        }

        /// <summary>
        /// Save Scriplet on server
        /// </summary>
        public static void Scriplet(string title, string desct)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            string code = "";
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) code = txtCommand.Text;
            });
            Thread.Sleep(100);
            if (code != "")
            ScripletHelper(title, desct, code);
        }

        /// <summary>
        /// Загрузить локализованный интерфейс из файла
        /// </summary>
        /// <param name="fname">путь к файлу</param>
        void DoLocal(string fname)
        {
            string[] lines = File.ReadAllLines(fname, Encoding.UTF8);
            for(int i = 0; i < lines.Length; i++)
            {
                string s = lines[i].Trim();
                if (string.IsNullOrEmpty(s)) continue;
                string[] arr = s.Split('=');
                if (arr.Length != 2) continue;
                s = arr[0].Trim();
                string s2 = arr[1].Trim();
                if (s == "button1") button1.Content = s2;
                else if (s == "button2") button2.Content = s2;
                else if (s == "button3") button3.Content = s2;
                else if (s == "button4") button4.Content = s2;
                else if (s == "button5") button5.Content = s2;
                else if (s == "button6") button6.Content = s2;
                else if (s == "button7") button7.Content = s2;
                else if (s == "Label1") Label1.Content = s2;
                else if (s == "Label2") Label2.Content = s2;
            }

        }

        /// <summary>
        /// передать html код в веб-компонент
        /// </summary>
        /// <param name="data">html код</param>
        public static void SetHtml(string data)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                webConsole.InvokeScript("ext_div", data);
            });
        }
        
        /// <summary>
        /// set html into control
        /// </summary>
        /// <param name="data">html string</param>
        public static string GetParams()
        {
            ext_params = "";
            if (!bReady || dispObj.HasShutdownStarted) return ext_params;
            int n = 0;
            while (n < 1000)
            {
                //мы запускаем код в UI потоке
                dispObj.Invoke(delegate
                {
                    ext_params = (string)webConsole.InvokeScript("ext_params");
                });
                n++;
                if (ext_params != "") break;
                System.Threading.Thread.Sleep(100);
            }
            return ext_params;
        }

        /// <summary>
        /// получить информацию о мыши в канвасе
        /// </summary>
        /// <param name="xClick">x позиция клика мыши</param>
        /// <param name="yClick">y позиция клика мыши</param>
        /// <param name="xMouse"x позиция мыши</param>
        /// <param name="yMouse">y позиция мыши</param>
        /// <param name="xMouseUp">x позиция окончании клика мыши</param>
        /// <param name="yMouseUp">y позиция окончании клика мыши</param>
        /// <param name="b_mouseDown">мышь нажата</param>
        /// <param name="b_clickDone">клик произошел</param>
        public static bool GetCanvasMouseInfo(ref int xClick, ref int yClick,
            ref int xMouse, ref int yMouse, ref int xMouseUp, ref int yMouseUp,
            ref bool b_mouseDown, ref bool b_clickDone)
        {
            var s = "";
            if (!bReady || dispObj.HasShutdownStarted) return false;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                s = (string)webConsole.InvokeScript("ext_mouse");
            });
            int n = 0;
            while (n < 10)
            {
                n++;
                if (s != "")
                {
                    /*engine.*/ParseCanvasMouseInfo(ref s, ref xClick, ref yClick,
                        ref xMouse, ref yMouse, ref xMouseUp, ref yMouseUp,
                        ref b_mouseDown, ref b_clickDone);
                    break;
                }
                System.Threading.Thread.Sleep(100);
            }
            return true;
        }

        /// <summary>
        /// метод для вызова тестов
        /// </summary>
        /// <param name="id">id теста</param>
        public static void GraphExample(string id)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //launch in UI thread
            dispObj.Invoke(delegate
            {
                webConsole.InvokeScript("ext_example" + id);
            });
        }
    }
}
