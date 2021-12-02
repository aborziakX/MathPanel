//2020, Andrei Borziak
//MathPanel (математическая панель) для работы со скриптами, написанными на C#.
//Область применения – моделирование процессов и визуализация.

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
        //путь к используемому фреймворку
        static string frmPath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\";
        readonly static string sLogFile = "mathpanel.log"; //файл логирования
        readonly static Dictionary<int, Phob> dicPhob = new Dictionary<int, Phob>(); //словарь физических объектов
        static TextBox txtConsole, txtCommand; //окна сообщений и комманд
        static WebBrowser webConsole; //окно веб-браузера
        static System.Windows.Controls.Image imgStatic; //компонент с картинкой
        static System.Windows.Threading.Dispatcher dispObj; //диспетчер UI-потока, через него обращения к элементам UI из других потоков
        //test init
        readonly static string test1 = @"var hz = Math.Sqrt(3);
Dynamo.Console(hz.ToString());
";
        static bool bReady = false; //признак успешной инициализации
        static string keyConsole = "";//ввод с клавиатуры
        static object dynClassInstance = null; //объект типа скрипт    
        static System.Threading.Thread my_thread = null; //текущий поток со скриптом   
        readonly static List<System.Threading.Thread> lstThr = new List<Thread>(); //список всех потоков со скриптами
        static double z_cam = 100;  //z-позиция камеры
        static double x_cam_angle = 1.5;  //угол камеры по горизонтали
        static double y_cam_angle = 1.5;  //угол камеры по вертикали
        //размеры экрана изображения в точке 0,0,0
        static double physWidth = z_cam * Math.Tan(x_cam_angle);
        static double physHeight = z_cam * Math.Tan(y_cam_angle);
        //проектируется на экран html-canvas (в пикселях)
        static int iCanvasWidth = 800;
        static int iCanvasHeight = 600;
        //ящик - система координат, в которой размещены объекты
        static Box box = null;  //границы ящика для рисования
        static Cube boxShape = new Cube();  //ящик для рисования
        static double xBoXTrans = 0, yBoXTrans = 0, zBoXTrans = -100;//смещение ящика в системе камеры
        static double xRotor = -0; //вращение ящика вокруг оси X
        static double yRotor = 0; //вращение ящика вокруг оси Y
        static double zRotor = 0; //вращение ящика вокруг оси Z
        static Mat3 matRotor = new Mat3(); //матрица для вращения
        static int DrawCount = 0; //счетчик рисований
        static bool bAxes = true; //признак построения осей
        static string clrNormal = "#ff0000";    //цвет нормалей
        static string clrStroke = "#999999";    //цвет ребер
        static int widNormal = 3; //ширина нормалей
        static string screenJson = ""; //текст для канваса веб-браузера
        static string user = "", pass = "", server = "", scid = "0"; //логин, пароль, сервер, код сцены для внешнего сайта
        static object locker = new object(); //для синхронизации доступа
        static int loglevel = 0; //уровень логирования
        static string canvasBg = "#000000"; //фон канваса
        static string ext_params = ""; //дополнительный параметр

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
                /* //тест
                Console(string.Format("Try Assembly:"));
                Assembly assembly = Assembly.LoadFile(outputAssembly);
                Type type = assembly.GetType("MathPanelExt.QuadroEqu");
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
                Console(string.Format("x1 {0}, x2 {1}", x1, x2));*/
            }
            catch (Exception ex) { Console(ex.ToString()); }
            return;
        }

        //обработчик кнопки "Новый скрипт"
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            string data = File.ReadAllText("template.cs", Encoding.UTF8);
            textBlock1.Text = data;
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
                    XRotor += 0.1;
                    //matRotor.Build(XRotor, YRotor, ZRotor);
                }
                else if (key == "Down")
                {
                    XRotor -= 0.1;
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

        //компилировать и выполнить скрипт из окна "комманд"
        public static void Process(string s)
        {
            //сборки
            string[] includeAssemblies = { "MathPanel.exe",
                frmPath + "System.dll",
                frmPath + "System.Xaml.dll",
                frmPath + "WindowsBase.dll",
                frmPath + "PresentationFramework.dll",
                frmPath + "PresentationCore.dll"
                , frmPath + "System.Drawing.dll"
                , frmPath + "System.Net.dll"
                , frmPath + "System.Net.Http.dll"
            };
            //пространства имен
            string[] includeNamespaces = { "MathPanel", "MathPanelExt", "System.Net.Sockets" };
            keyConsole = "";
            CompileDynamo(s, null, includeNamespaces, includeAssemblies);
            //создаем новый поток
            try
            {
                if (dynClassInstance != null)
                {
                    my_thread = new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        Type type = dynClassInstance.GetType();
                        MethodInfo methodInfo = type.GetMethod("Execute");
                        try
                        {
                            methodInfo.Invoke(dynClassInstance, null);
                            Dynamo.Console("Скрипт выполнен.");
                        }
                        catch (Exception yyy) { Dynamo.Console(yyy.ToString()); }
                        //my_thread = null;
                    }));
                    my_thread.Start();
                    lstThr.Add(my_thread);
                }
            }
            catch (Exception xxx) { Dynamo.Console(xxx.ToString()); }
        }

        //метод для тестирования
        static object Eval(string code, Type outType = null, string[] includeNamespaces = null, string[] includeAssemblies = null)
        {
            object methodResult = null;
            CompileDynamo(code, outType, includeNamespaces, includeAssemblies);
            if (dynClassInstance != null)
            {
                Type type = dynClassInstance.GetType();
                MethodInfo methodInfo = type.GetMethod("Execute");
                methodResult = methodInfo.Invoke(dynClassInstance, null);
            }
            return methodResult;
        }

        //создать объект типа скрипт
        static object CompileDynamo(string code, Type outType = null, string[] includeNamespaces = null, string[] includeAssemblies = null)
        {
            StringBuilder namespaces = null;
            object methodResult = null;
            dynClassInstance = null;
            using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
            {
                //ICodeCompiler codeCompiler = codeProvider.CreateCompiler();//obsolete!
                CompilerParameters compileParams = new CompilerParameters
                {
                    CompilerOptions = "/t:library",
                    GenerateInMemory = true
                };

                int ipos = code.IndexOf("///[DLL]");
                if (ipos > 0)
                {
                    int ipos2 = code.IndexOf("[/DLL]", ipos);
                    if (ipos2 > 0)
                    {
                        compileParams.ReferencedAssemblies.Add("MathPanel.exe");
                        string ass = code.Substring(ipos + 8, ipos2 - ipos - 8);
                        var arr = ass.Split(',');
                        foreach (string _assembly in arr)
                        {
                            compileParams.ReferencedAssemblies.Add(frmPath + _assembly.Trim());
                        }
                    }
                }
                else if (includeAssemblies != null && includeAssemblies.Length > 0)
                {
                    foreach (string _assembly in includeAssemblies)
                    {
                        compileParams.ReferencedAssemblies.Add(_assembly);
                    }
                }

                if (includeNamespaces != null && includeNamespaces.Length > 0)
                {
                    namespaces = new StringBuilder();
                    foreach (string _namespace in includeNamespaces)
                    {
                        namespaces.Append(string.Format("using {0};\n", _namespace));
                    }
                }

                if (code.IndexOf("namespace DynamoCode") < 0)
                    code = string.Format(
                        @"{1}  
                using System;  
                namespace DynamoCode{{  
                    public class Script{{  
                        public {2} Execute(){{  
                            {3} {0};  
                        }}  
                    }}  
                }}",
                        code,
                        namespaces != null ? namespaces.ToString() : null,
                        outType != null ? outType.FullName : "void",
                        outType != null ? "return" : string.Empty
                        );
                CompilerResults compileResult = codeProvider.CompileAssemblyFromSource(compileParams, code);
                //codeCompiler.CompileAssemblyFromSource(compileParams, code);

                if (compileResult.Errors.Count > 0)
                {
                    //throw new Exception(compileResult.Errors[0].ErrorText);
                    Console("compile error: " + compileResult.Errors[0].ErrorText);
                    return "compile error: " + compileResult.Errors[0].ErrorText;
                }
                System.Reflection.Assembly assembly = compileResult.CompiledAssembly;
                dynClassInstance = assembly.CreateInstance("DynamoCode.Script");
                /*Type type = dynClassInstance.GetType();
                MethodInfo methodInfo = type.GetMethod("Execute");
                methodResult = methodInfo.Invoke(dynClassInstance, null);*/
            }
            return methodResult;
        }

        //API
        /// <summary>
        /// преобразовать вещественное число в строку с точностью 4 знака после запятой
        /// </summary>
        /// <param name="d">число</param>
        public static string D2S(double d)
        {   //F4
            string s = d.ToString("G4", CultureInfo.InvariantCulture.NumberFormat);
            if( s.Contains("NaN"))
            {
                int kk = 0;
            }
            return s;
        }
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
                if (bReady) txtConsole.Text += (s + (bNewLine ? "\r\n" : ""));
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
                if (bReady) txtConsole.Text = "";
            });
        }

        /// <summary>
        /// логирование
        /// </summary>
        public static void Log(string s)
        {
            try
            {
                StreamWriter sw = new StreamWriter(sLogFile, true, Encoding.UTF8);
                sw.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss} {1}", DateTime.Now, s));
                sw.Close();
            }
            catch (Exception se)
            {
                Console(se.Message);
            }
        }

        /// <summary>
        /// convert hexadecimal string to byte array
        /// </summary>
        /// <param name="hex2">hexadecimal string</param>
        public static byte[] HexStringToByteArray(string hex2)
        {
            string hex = hex2;
            string pref = "0xFFFE";
            if (hex.IndexOf(pref) == 0 && hex.Length > 10)
                hex = hex.Substring(pref.Length);
            else if (hex.IndexOf("0x") == 0)
                hex = hex.Substring(2);

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Byte Array convert To Int
        /// </summary>
        /// <param name="bt">byte array</param>
        public static int ByteArrayToInt(byte[] bt)
        {
            int j = 0;
            for (int i = 0; i < bt.Length && i < 8; i++)
                j = (j << 8) + (int)bt[i];
            return j;
        }

        /// <summary>
        /// Byte Array Reverse convert To Int
        /// </summary>
        /// <param name="bt">byte array</param>
        public static int ByteArrayReverseToInt(byte[] bt)
        {
            int j = 0;
            for (int i = bt.Length - 1; i >= 0; i--)
                j = (j << 8) + (int)bt[i];
            return j;
        }

        /// <summary>
        /// Convert integer to string of hexa-bytes in reverse order (small byte first)
        /// </summary>
        /// <param name="k">integer</param>
        public static string IntToByteArrayReverseString(int k)
        {
            byte b0 = (byte)(k & 0xff);
            byte b1 = (byte)((k >> 8) & 0xff);
            byte b2 = (byte)((k >> 16) & 0xff);
            byte b3 = (byte)((k >> 24) & 0xff);

            string s0 = Convert.ToString(b0, 16);
            if (s0.Length == 1) s0 = "0" + s0;
            string s1 = Convert.ToString(b1, 16);
            if (s1.Length == 1) s1 = "0" + s1;
            string s2 = Convert.ToString(b2, 16);
            if (s2.Length == 1) s2 = "0" + s2;
            string s3 = Convert.ToString(b3, 16);
            if (s3.Length == 1) s3 = "0" + s3;
            return "\\x" + (s0 + s1 + s2 + s3).ToUpper();
        }

        /// <summary>
        /// convert string to char values
        /// </summary>
        /// <param name="hex">string</param>
        /// <param name="bAscii">if false, output as 4 hexadecimal bytes</param>
        /// <param name="bHex">if true, output as hexadecimal</param>
        /// var hz = Dynamo.StringToCharValues("41АндрейAndrei");
        public static string StringToCharValues(string s, bool bAscii = true, bool bHex = true)
        {
            string hex2 = "";
            var x = s.ToCharArray();
            for (int i = 0; i < x.Length; i++)
            {
                int k = (int)x[i];
                if (hex2 != "") hex2 += "-";
                if (bHex)
                {
                    if(bAscii) hex2 += k.ToString("X2");
                    else
                    {   //low byte first
                        var q = k.ToString("X4");
                        hex2 += q.Substring(2, 2);
                        hex2 += "-";
                        hex2 += q.Substring(0, 2);
                    }                    
                }
                else hex2 += k.ToString();
            }
            return hex2;
        }

        /// <summary>
        /// создать Phob с добавлением в словарь физических объектов
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        /// <param name="z">z координата</param>
        public static int PhobNew(double x, double y, double z = 0)
        {
            Phob pt = new Phob(x, y, z);
            dicPhob.Add(pt.Id, pt);
            return pt.Id;
        }

        /// <summary>
        /// количество физических объектов Phob в словаре
        /// </summary>
        public static int PhobCount()
        {
            return dicPhob.Count;
        }

        /// <summary>
        /// задать новые координаты Phob объекту
        /// </summary>
        /// <param name="id">Phob id</param>
        public static void PhobSet(int id, double x, double y, double z = 0)
        {
            if (!dicPhob.ContainsKey(id)) return;
            Phob pt = dicPhob[id];
            if (pt == null) return;
            pt.x = x;
            pt.y = y;
            pt.z = z;
        }

        //!!если возвращать Phob, то была ошибка создания exe
        /// <summary>
        /// получить Phob объект по его id
        /// </summary>
        /// <param name="id">Phob id</param>
        public static Phob PhobGet(int id)
        {
            if (!dicPhob.ContainsKey(id))
                return null;
            //return id as object;
            Phob pt = dicPhob[id];
            return pt;
        }

        /// <summary>
        /// удалить Phob объект по его id
        /// </summary>
        /// <param name="id">Phob id</param>
        public static void PhobDel(int id)
        {
            if (!dicPhob.ContainsKey(id))
                return;
            dicPhob.Remove(id);
        }

        /// <summary>
        /// задать атрибут Phob объекту
        /// </summary>
        /// <param name="id">Phob id</param>
        /// <param name="key">ключ атрибута</param>
        /// <param name="value">значение атрибута</param>
        public static void PhobAttrSet(int id, string key, string value)
        {
            if (!dicPhob.ContainsKey(id)) return;
            Phob pt = dicPhob[id];
            if (pt == null) return;
            pt.AttrSet(key, value);
        }

        /// <summary>
        /// получить атрибут Phob объекта
        /// </summary>
        /// <param name="id">Phob id</param>
        /// <param name="key">ключ атрибута</param>
        /// <return>значение атрибута</return>
        public static string PhobAttrGet(int id, string key)
        {
            if (!dicPhob.ContainsKey(id)) return null;
            Phob pt = dicPhob[id];
            if (pt == null) return null;
            return pt.AttrGet(key);
        }

        /// <summary>
        /// получить последнее нажатие на клавиатуре
        /// </summary>
        /// <return>последнее нажатие</return>
        public static string KeyConsole
        {
            get
            {
                var s = keyConsole;
                keyConsole = "";
                return s;
            }
        }

        /// <summary>
        /// взять/задать box (ящик) для визуализации
        /// </summary>
        public static Box SceneBox
        {
            get
            {
                return box;
            }
            set
            {
                box = value;
                boxShape.Reshape(box.x0, box.x1, box.y0, box.y1, box.z0, box.z1);
                xBoXTrans = -(box.x0 + box.x1) / 2;
                yBoXTrans = -(box.y0 + box.y1) / 2;
                zBoXTrans = -box.z1 - z_cam * 0.1;
                //optimize camera angle
                CameraZ = z_cam;
            }
        }

        public static double XBoXTrans
        {
            get
            {
                return xBoXTrans;
            }
            set
            {
                xBoXTrans = value;
            }
        }
        public static double YBoXTrans
        {
            get
            {
                return yBoXTrans;
            }
            set
            {
                yBoXTrans = value;
            }
        }
        public static double ZBoXTrans
        {
            get
            {
                return zBoXTrans;
            }
            set
            {
                zBoXTrans = value;
            }
        }
        public static double XRotor
        {
            get
            {
                return xRotor;
            }
            set
            {
                xRotor = value;
                matRotor.Build(xRotor, yRotor, zRotor);
            }
        }
        public static double YRotor
        {
            get
            {
                return yRotor;
            }
            set
            {
                yRotor = value;
                matRotor.Build(xRotor, yRotor, zRotor);
            }
        }
        public static double ZRotor
        {
            get
            {
                return zRotor;
            }
            set
            {
                zRotor = value;
                matRotor.Build(xRotor, yRotor, zRotor);
            }
        }

        /// <summary>
        /// получить/задать z-координату камеры в координатах камеры
        /// </summary>
        public static double CameraZ
        {
            get
            {
                return z_cam;
            }
            set
            {
                if (value > 0 && value < 1000000)
                {
                    z_cam = value;
                    double w = box != null ? (box.x1 - box.x0) : iCanvasWidth;
                    double h = box != null ? (box.y1 - box.y0) : iCanvasHeight;
                    x_cam_angle = Math.Atan((w * 0.5) / z_cam);
                    y_cam_angle = Math.Atan((h * 0.5) / z_cam);
                    physWidth = z_cam * Math.Tan(x_cam_angle);// * Math.PI / 360.0);
                    physHeight = z_cam * Math.Tan(y_cam_angle);// * Math.PI / 360.0);
                }
            }
        }

        /// <summary>
        /// получить/задать bAxes для рисования координат
        /// </summary>
        public static bool BAxes
        {
            get
            {
                return bAxes;
            }
            set
            {
                bAxes = value;
            }
        }

        /// <summary>
        /// получить Json строку, переданную для канваса
        /// </summary>
        public static string ScreenJson
        {
            get
            {
                return screenJson;
            }
        }

        /// <summary>
        /// сохранить Json строку в файл
        /// </summary>
        /// <param name="fname">путь к файлу</param>
        public static void ScreenJsonSave(string fname)
        {
            try
            {
                File.WriteAllText(fname, screenJson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console(ex.ToString());
            }
        }

        /// <summary>
        /// считать Json строку из файла
        /// </summary>
        /// <param name="fname">путь к файлу</param>
        public static string ScreenJsonLoad(string fname)
        {
            string s;
            try
            {
                s = File.ReadAllText(fname, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console(ex.ToString());
                return "";
            }
            return s;
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
        /// преобразовать координаты в системе ящика в координаты в системе камеры (старый метод)
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        /// <param name="z">z координата</param>
        /// <param name="radius">радиус объекта</param>
        /// <param name="bScreen">проецировать координаты на экран</param>
        public static void Traslate2CameraOld(ref double x, ref double y, ref double z, ref double radius, bool bScreen = false)
        {
            double x_n, y_n, z_n;
            //вращать вокруг Z
            if (zRotor != 0)
            {
                x_n = x * Math.Cos(zRotor) - y * Math.Sin(zRotor);
                y_n = x * Math.Sin(zRotor) + y * Math.Cos(zRotor);
                x = x_n;
                y = y_n;
            }

            //вращать вокруг X
            if (xRotor != 0)
            {
                y_n = y * Math.Cos(xRotor) - z * Math.Sin(xRotor);
                z_n = y * Math.Sin(xRotor) + z * Math.Cos(xRotor);
                y = y_n;
                z = z_n;
            }

            //вращать вокруг Y
            if (yRotor != 0)
            {
                z_n = z * Math.Cos(yRotor) - x * Math.Sin(yRotor);
                x_n = z * Math.Sin(yRotor) + x * Math.Cos(yRotor);
                z = z_n;
                x = x_n;
            }

            //сдвинуть
            x += xBoXTrans;
            y += yBoXTrans;
            z += zBoXTrans;

            if (bScreen)
                Traslate2Screen(ref x, ref y, ref z, ref radius);
        }

        /// <summary>
        /// преобразовать координаты в системе ящика в координаты в системе камеры
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        /// <param name="z">z координата</param>
        /// <param name="radius">радиус объекта</param>
        /// <param name="bScreen">проецировать координаты на экран</param>
        public static void Traslate2Camera(ref double x, ref double y, ref double z, ref double radius, bool bScreen = false)
        {
            Vec3 v = new Vec3(x, y, z);
            Vec3 v_new = new Vec3();
            //вращать
            matRotor.Mult(v, ref v_new);

            //сдвинуть
            x = v_new.x + xBoXTrans;
            y = v_new.y + yBoXTrans;
            z = v_new.z + zBoXTrans;

            if (bScreen)
                Traslate2Screen(ref x, ref y, ref z, ref radius);
        }

        /// <summary>
        /// транслировать координаты объекта в системе камеры и размер в координаты на экране
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        /// <param name="z">z координата</param>
        /// <param name="radius">радиус (размер) объекта</param>
        public static void Traslate2Screen(ref double x, ref double y, ref double z, ref double radius)
        {
            double d_squeeze = (z_cam) / (z_cam - z);
            x *= d_squeeze;
            y *= d_squeeze;
            radius *= d_squeeze;
        }

        /// <summary>
        /// данные для рисования ребер ящика (старый метод)
        /// </summary>
        static string BoxEdgesOld()
        {
            double x, y, z, radius = 1, x_fr, y_fr, z_fr;
            double rad = Math.Abs(1.0 / ((box.x1 - box.x0) * iCanvasWidth));

            string data = "";
            //задняя сторона
            for (int j = 0; j < 5; j++)
            {
                if (j == 0)
                {
                    x = box.x0;
                    y = box.y0;
                }
                else if (j == 1)
                {
                    x = box.x1;
                    y = box.y0;
                }
                else if (j == 2)
                {
                    x = box.x1;
                    y = box.y1;
                }
                else if (j == 3)
                {
                    x = box.x0;
                    y = box.y1;
                }
                else
                {
                    x = box.x0;
                    y = box.y0;
                }
                z = box.z0;

                //привязать к экрану
                Traslate2Camera(ref x, ref y, ref z, ref radius, true);
                if (z >= z_cam) continue;

                if (data != "") data += ",";
                //параметры рисования: размеры, цвет, стиль
                data += string.Format("{{\"x\":{0}, \"y\":{1}, \"clr\":\"#cccccc\", \"rad\":\"{3}\", \"sty\":\"{2}\"}}",
                    D2S(x), D2S(y),
                    j == 4 ? "line_end" : "line", D2S(rad));
            }

            //передняя сторона
            for (int j = 0; j < 5; j++)
            {
                if (j == 0)
                {
                    x = box.x0;
                    y = box.y0;
                }
                else if (j == 1)
                {
                    x = box.x1;
                    y = box.y0;
                }
                else if (j == 2)
                {
                    x = box.x1;
                    y = box.y1;
                }
                else if (j == 3)
                {
                    x = box.x0;
                    y = box.y1;
                }
                else
                {
                    x = box.x0;
                    y = box.y0;
                }
                z = box.z1;

                //привязать к экрану
                Traslate2Camera(ref x, ref y, ref z, ref radius, true);
                if (z >= z_cam) continue;

                if (data != "") data += ",";
                //параметры рисования: размеры, цвет, стиль
                data += string.Format("{{\"x\":{0}, \"y\":{1}, \"clr\":\"#cccccc\", \"rad\":\"{3}\", \"sty\":\"{2}\"}}",
                    D2S(x), D2S(y),
                    j == 4 ? "line_end" : "line", D2S(rad));
            }

            //боковины
            for (int j = 0; j < 4; j++)
            {
                if (j == 0)
                {
                    x = box.x0;
                    y = box.y0;
                    x_fr = box.x0;
                    y_fr = box.y0;
                }
                else if (j == 1)
                {
                    x = box.x1;
                    y = box.y0;
                    x_fr = box.x1;
                    y_fr = box.y0;
                }
                else if (j == 2)
                {
                    x = box.x1;
                    y = box.y1;
                    x_fr = box.x1;
                    y_fr = box.y1;
                }
                else
                {
                    x = box.x0;
                    y = box.y1;
                    x_fr = box.x0;
                    y_fr = box.y1;
                }
                z = box.z0;
                z_fr = box.z1;

                //привязать к экрану
                Traslate2Camera(ref x, ref y, ref z, ref radius, true);
                //привязать к экрану
                Traslate2Camera(ref x_fr, ref y_fr, ref z_fr, ref radius, true);
                if (z >= z_cam || z_fr >= z_cam) continue;

                if (data != "") data += ",";
                data += string.Format("{{\"x\":{0}, \"y\":{1}, \"clr\":\"#cccccc\", \"rad\":\"{3}\", \"sty\":\"{2}\"}}",
                    D2S(x), D2S(y),
                    "line", D2S(rad));

                data += string.Format(", {{\"x\":{0}, \"y\":{1}, \"clr\":\"#cccccc\", \"rad\":\"{3}\", \"sty\":\"{2}\"}}",
                    D2S(x_fr), D2S(y_fr),
                    "line_end", D2S(rad));
            }

            return data.ToString();
        }

        /// <summary>
        /// данные для рисования ребер ящика 
        /// </summary>
        static string BoxEdges()
        {           
            double x, y, z, radius = 1, x_fr, y_fr, z_fr;
            double rad = Math.Abs(1.0 / ((box.x1 - box.x0) * iCanvasWidth));
            
            string text;
            var data = new StringBuilder();
            string ss = DrawShape(boxShape, null);
            if (ss != "")
            {
                data.Append(ss);
            }

            if (bAxes)
            {
                x = box.x0;
                y = box.y0;
                z = box.z0;
                Traslate2Camera(ref x, ref y, ref z, ref radius, false);
                if (z >= z_cam) return "";
                Traslate2Screen(ref x, ref y, ref z, ref radius);

                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                    {
                        x_fr = box.x1;
                        y_fr = box.y0;
                        z_fr = box.z0;
                        text = "X";
                    }
                    else if (j == 1)
                    {
                        x_fr = box.x0;
                        y_fr = box.y1;
                        z_fr = box.z0;
                        text = "Y";
                    }
                    else
                    {
                        x_fr = box.x0;
                        y_fr = box.y0;
                        z_fr = box.z1;
                        text = "Z";
                    }
                    Traslate2Camera(ref x_fr, ref y_fr, ref z_fr, ref radius, false);
                    if (z_fr >= z_cam) continue;
                    Traslate2Screen(ref x_fr, ref y_fr, ref z_fr, ref radius);

                    if (data.Length > 0) data.Append(",");
                    data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"lnw\":\"{5}\", \"txt\":\"{6}\"}}",
                        D2S(x), D2S(y),
                        "line", D2S(rad),
                        clrNormal, widNormal, "");

                    data.AppendFormat(", {{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"lnw\":\"{5}\", \"txt\":\"{6}\"}}",
                        D2S(x_fr), D2S(y_fr),
                        "line_end", D2S(rad),
                        clrNormal, widNormal, text);
                }
            }

            return data.ToString();
        }
        static string DrawShape(GeOb shape, Phob ph)
        {
            if (shape == null) return "";
            StringBuilder data = new StringBuilder();
            double radius = 1;
            double rad = (2 * physWidth) / iCanvasWidth;    //1 pixel
            Vec3 vref;
            Vec3 vTemp = new Vec3();
            //для каждой грани найти координаты в системе камеры
            foreach (var fac in shape.lstFac)
            {
                for (int j = 0; j < fac.Count; j++)
                {
                    if (j == 0)
                    {
                        fac.v1_cam.Copy(fac.v1);
                        vref = fac.v1_cam;
                    }
                    else if (j == 1)
                    {
                        fac.v2_cam.Copy(fac.v2);
                        vref = fac.v2_cam;
                    }
                    else if (j == 2)
                    {
                        fac.v3_cam.Copy(fac.v3);
                        vref = fac.v3_cam;
                    }
                    else
                    {
                        fac.v4_cam.Copy(fac.v4);
                        vref = fac.v4_cam;
                    }

                    //scale
                    if (shape.scaleX != 1) vref.x *= shape.scaleX;
                    if (shape.scaleY != 1) vref.y *= shape.scaleY;
                    if (shape.scaleZ != 1) vref.z *= shape.scaleZ;
                    //rotate                    
                    if (shape.XRotor != 0 || shape.YRotor != 0 || shape.ZRotor != 0)
                    {
                        /*double x_n, y_n, z_n;
                        x_n = vref.x * Math.Cos(shape.ZRotor) - vref.y * Math.Sin(shape.ZRotor);
                        y_n = vref.x * Math.Sin(shape.ZRotor) + vref.y * Math.Cos(shape.ZRotor);
                        vref.x = x_n;
                        vref.y = y_n;*/
                        Vec3 v = new Vec3(vref.x, vref.y, vref.z);
                        Vec3 v_new = new Vec3();
                        shape.Rotate(v, ref v_new);

                        vref.x = v_new.x;
                        vref.y = v_new.y;
                        vref.z = v_new.z;
                    }
                    //translate
                    vref.Add(shape.vShift);

                    //adjust with PhOb
                    if( ph != null )
                        vref.Add(ph.x, ph.y, ph.z);

                    //adjust using camera position
                    Traslate2Camera(ref vref.x, ref vref.y, ref vref.z, ref radius, false);
                }
            }

            //sort
            List<Facet3> lst = shape.lstFac;
            try
            {   //can fail while scene is reloaded
                lst.Sort(delegate (Facet3 fx, Facet3 fy)
                {
                    double x_t0, y_t0, z_t0, x_t1, y_t1, z_t1;
                    fx.Center(out x_t0, out y_t0, out z_t0, true);
                    fy.Center(out x_t1, out y_t1, out z_t1, true);
                    return z_t0 >= z_t1 ? 1 : -1;
                });
            }
            catch (Exception) { };

            //prepare to screen
            foreach (var fac in lst)
            {
                //light aproximation
                fac.CalcNormal(true);
                //fac.dark = fac.normal_cam.z <= 0.0 ? 0.1 : (fac.normal_cam.z + 0.5) / 1.5;
                //if (DrawCount % 100 == 1)
                //Console(fac.name + ",dark=" + dark);
                fac.Center(out double x, out double y, out double z, true);
                vTemp.Copy(-x, -y, z_cam - z); //vector to camera
                double dLen = vTemp.Length();
                if (dLen == 0) continue;
                double cosfi = vTemp.ScalarProduct(fac.normal_cam) / dLen;  //косинус угла между вектором на камеру и нормалью
                fac.dark = (cosfi < 0.1) ? 0.1 : cosfi;

                if (z >= z_cam - 20) continue;//слишком близко
                if (shape.iFill == 0 && !shape.bDrawBack) continue; //не рисуем
                if (cosfi <= 0.0 && ((shape.iFill & 2) == 0) && !shape.bDrawBack) continue;//не видно грань и не рисуем ребра

                //?? filter by screen x,y

                double x1 = x, y1 = y, z1 = z;
                if (shape.bDrawNorm)
                {
                    x1 = x + fac.normal_cam.x * shape.radius * 0.25;
                    y1 = y + fac.normal_cam.y * shape.radius * 0.25;
                    z1 = z + fac.normal_cam.z * shape.radius * 0.25;
                }

                for (int j = 0; j <= fac.Count; j++)
                {
                    if (j < fac.Count)
                    {
                        if (j == 0) vref = fac.v1_cam;
                        else if (j == 1) vref = fac.v2_cam;
                        else if (j == 2) vref = fac.v3_cam;
                        else vref = fac.v4_cam;

                        Traslate2Screen(ref vref.x, ref vref.y, ref vref.z, ref radius);
                    }
                    else vref = fac.v1_cam;

                    string title = "";
                    if (j == fac.Count && ph != null && ph.AttrGet("text") != null)
                        title = ph.AttrGet("text");

                    string edges = "";
                    //цвет ребер как грани
                    if((shape.iFill & 3) == 1) edges = string.Format(",\"csk\":\"{0}\"", fac.ColorDarkHtml(fac.dark));

                    string style = j == fac.Count ? ((((shape.iFill & 1) == 1) && cosfi > 0.0) ? "line_endf" : "line_end") : "line";

                    if (data.Length != 0) data.Append(",");
                    data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\"{6}}}",
                        D2S(vref.x), D2S(vref.y),
                        style, D2S(rad),
                        fac.ColorDarkHtml(fac.dark), title, edges);
                }

                if (shape.bDrawNorm)
                {
                    //normals
                    if (data.Length != 0) data.Append(",");
                    Traslate2Screen(ref x, ref y, ref z, ref radius);
                    data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\"}}",
                        D2S(x), D2S(y),
                        "line", D2S(rad),
                        clrNormal, "", widNormal);
                    Traslate2Screen(ref x1, ref y1, ref z1, ref radius);
                    data.AppendFormat(",{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{7}  {5}\", \"lnw\":\"{6}\"}}",
                        D2S(x1), D2S(y1),
                        "line_end", D2S(rad),
                        clrNormal, Math.Round(cosfi, 3), widNormal, fac.name);
                }
            }
            //if (DrawCount % 100 == 1) Console(data);
            return data.ToString();
        }
        
        /// <summary>
        /// передать JSON данные для визуализации в canvas
        /// </summary>
        public static void SceneJson(string s_json, bool bSecond = false)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            screenJson = s_json;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                webConsole.InvokeScript("ext_json", screenJson, bSecond);
            });
        }

        /// <summary>
        /// подготовить данные для визуализации на канвасе
        /// <param name="bCons">вывод в окно сообщений</param>
        /// </summary>
        public static void SceneDraw(bool bCons = false)
        {
            DrawCount++;//номер итерации вывода
            var data = new StringBuilder();//для ускорения работы со строками
            int i = 0;
            //размеры физической области
            double dX0 = 0, dX1 = 1, dY0 = 0, dY1 = 1;

            //добавить границы ящика
            if (box != null) data.Append(BoxEdges());

            //по всей коллекции физических объектов
            List<Phob> lst = dicPhob.Values.ToList();
            foreach (Phob ph in lst)
            {
                //сохранить координаты
                ph.SaveCoord();
                //спроецировать координаты на экран
                Traslate2Camera(ref ph.x, ref ph.y, ref ph.z, ref ph.radius, true);
                if (box == null)
                {   //расширяем размеры физической области
                    if (i == 0)
                    {
                        dX0 = ph.x;
                        dX1 = dX0 + 0.1;
                        dY0 = ph.y;
                        dY1 = dY0 + 0.1;
                    }
                    else
                    {
                        if (dX0 > ph.x) dX0 = ph.x;
                        if (dX1 < ph.x) dX1 = ph.x;
                        if (dY0 > ph.y) dY0 = ph.y;
                        if (dY1 < ph.y) dY1 = ph.y;
                    }
                }
                i++;
            }
            //сортировать по удаленности от камеры
            lst.Sort(delegate (Phob x, Phob y)
            {
                return x.z >= y.z ? 1 : -1;
            });

            //добавляем изображение каждого объекта в Json для рисования
            foreach (Phob ph in lst)
            {
                if (ph.z < z_cam)
                {   //камера видит объект
                    if (data.Length > 0) data.Append(",");
                    if(ph.bDrawAsLine == false)
                        data.Append(ph.ToJson());
                    else
                    {
                        double radius = 0.001;
                        double px = ph.p1.x;
                        double py = ph.p1.y;
                        double pz = ph.p1.z;
                        var xxx = ph.AttrGet("clr");
                        var yyy = ph.AttrGet("lnw");
                        var txt1 = ph.AttrGet("txt1");
                        var txt2 = ph.AttrGet("txt2");
                        var fontsize = ph.AttrGet("fontsize");
                        Dynamo.Traslate2Camera(ref px, ref py, ref pz, ref radius, true);
                        data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\", \"fontsize\":\"{7}\"}}",
                            D2S(px), D2S(py), "line", D2S(radius),
                            string.IsNullOrEmpty(xxx) ? clrNormal : xxx, string.IsNullOrEmpty(txt1) ? "" : txt1, 
                            string.IsNullOrEmpty(yyy) ? "3" : yyy, string.IsNullOrEmpty(fontsize) ? "" : fontsize);
                        
                        px = ph.p2.x;
                        py = ph.p2.y;
                        pz = ph.p2.z;
                        Dynamo.Traslate2Camera(ref px, ref py, ref pz, ref radius, true);
                        data.AppendFormat(",{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\", \"fontsize\":\"{7}\"}}",
                            D2S(px), D2S(py), "line_end", D2S(radius),
                            string.IsNullOrEmpty(xxx) ? clrNormal : xxx, string.IsNullOrEmpty(txt2) ? "" : txt2, 
                            string.IsNullOrEmpty(yyy) ? "3" : yyy, string.IsNullOrEmpty(fontsize) ? "" : fontsize);
                    }
                }
                //восстановить координаты
                ph.RestoreCoord();
            }

            //уточняем размеры физической области
            if (box == null)
            {
                var extra = (dX1 - dX0) * 0.01;
                dX0 -= extra;
                dX1 += extra;
                extra = (dY1 - dY0) * 0.01;
                dY0 -= extra;
                dY1 += extra;
            }
            else
            {
                /*dX0 = box.x0;
                dX1 = box.x1;
                dY0 = box.y0;
                dY1 = box.y1;*/
                dX1 = physWidth;
                dX0 = -dX1;
                dY1 = physHeight;
                dY0 = -dY1;
            }
            //параметры рисования: размеры, цвет, стиль
            string opt = string.Format("{{\"x0\": {0}, \"x1\": {1}, \"y0\": {2}, \"y1\": {3}, \"clr\": \"#ff0000\", \"csk\": \"{6}\", \"sty\": \"circle\", \"size\":2, \"wid\": {4}, \"hei\": {5}, \"bg\": \"{7}\" }}",
                D2S(dX0), D2S(dX1),
                D2S(dY0), D2S(dY1),
                iCanvasWidth, iCanvasHeight, clrStroke, canvasBg);
            screenJson = string.Format("{{\"options\":{0}, \"data\":[{1}]}}", opt, data.ToString());

            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });
        }

        /// <summary>
        /// подготовить данные для визуализации на канвасе используя формы
        /// <param name="bBx">рисовать оси</param>
        /// <param name="bCons">вывод в окно сообщений</param>
        /// <return>attribute value</return>
        /// </summary>
        public static void SceneDrawShape(bool bBx = true, bool bCons = false)
        {
            DateTime dt1 = DateTime.Now;
            DrawCount++;
            int i = 0;
            double dX0 = 0, dX1 = 1, dY0 = 0, dY1 = 1;
            var data = new StringBuilder();
            string starter = "{{\"data\":[";
            data.AppendFormat(starter);

            //add box edges
            if (box != null && bBx) data.Append(BoxEdges());

            //sort by z-position
            List<Phob> lst = dicPhob.Values.ToList();
            foreach (Phob ph in lst)
            {   //adjust using camera position
                ph.SaveCoord();
                Traslate2Camera(ref ph.x, ref ph.y, ref ph.z, ref ph.radius, true);
                if (box == null)
                {
                    if (i == 0)
                    {
                        dX0 = ph.x;
                        dX1 = dX0 + 0.1;
                        dY0 = ph.y;
                        dY1 = dY0 + 0.1;
                    }
                    else
                    {
                        if (dX0 > ph.x) dX0 = ph.x;
                        if (dX1 < ph.x) dX1 = ph.x;
                        if (dY0 > ph.y) dY0 = ph.y;
                        if (dY1 < ph.y) dY1 = ph.y;
                    }
                }
                i++;
            }
            lst.Sort(delegate (Phob x, Phob y)
            {
                return x.z >= y.z ? 1 : -1;
            });

            foreach (Phob ph in lst)
            {
                if (ph.z < z_cam && ph.Shape == null)
                {
                    if (data.Length > starter.Length) data.Append(",");
                    if (ph.bDrawAsLine == false)
                        data.Append(ph.ToJson());
                    else
                    {
                        double radius = 0.001;
                        double px = ph.p1.x;
                        double py = ph.p1.y;
                        double pz = ph.p1.z;
                        var xxx = ph.AttrGet("clr");
                        var yyy = ph.AttrGet("lnw");
                        var txt1 = ph.AttrGet("txt1");
                        var txt2 = ph.AttrGet("txt2");
                        var fontsize = ph.AttrGet("fontsize");
                        Dynamo.Traslate2Camera(ref px, ref py, ref pz, ref radius, true);
                        data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\", \"fontsize\":\"{7}\"}}",
                            D2S(px), D2S(py), "line", D2S(radius),
                            string.IsNullOrEmpty(xxx) ? clrNormal : xxx, string.IsNullOrEmpty(txt1) ? "" : txt1, 
                            string.IsNullOrEmpty(yyy) ? "3" : yyy, string.IsNullOrEmpty(fontsize) ? "" : fontsize);

                        px = ph.p2.x;
                        py = ph.p2.y;
                        pz = ph.p2.z;
                        Dynamo.Traslate2Camera(ref px, ref py, ref pz, ref radius, true);
                        data.AppendFormat(",{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\", \"fontsize\":\"{7}\"}}",
                            D2S(px), D2S(py), "line_end", D2S(radius),
                            string.IsNullOrEmpty(xxx) ? clrNormal : xxx, string.IsNullOrEmpty(txt2) ? "" : txt2, 
                            string.IsNullOrEmpty(yyy) ? "3" : yyy, string.IsNullOrEmpty(fontsize) ? "" : fontsize);
                    }
                }
                ph.RestoreCoord();

                if (ph.Shape != null)
                {
                    string ss = DrawShape(ph.Shape, ph);
                    if (ss != "")
                    {
                        if (data.Length > starter.Length) data.Append(",");
                        data.Append(ss);
                    }
                }
            }

            if (box == null)
            {
                var extra = (dX1 - dX0) * 0.01;
                dX0 -= extra;
                dX1 += extra;
                extra = (dY1 - dY0) * 0.01;
                dY0 -= extra;
                dY1 += extra;
            }
            else
            {
                /*dX0 = box.x0;
                dX1 = box.x1;
                dY0 = box.y0;
                dY1 = box.y1;*/
                dX1 = physWidth;
                dX0 = -dX1;
                dY1 = physHeight;
                dY0 = -dY1;
            }

            string opt = string.Format("{{\"x0\": {0}, \"x1\": {1}, \"y0\": {2}, \"y1\": {3}, \"clr\": \"#ff0000\", \"csk\": \"{6}\", \"sty\": \"circle\", \"size\":2, \"wid\": {4}, \"hei\": {5}, \"bg\": \"{7}\" }}",
                D2S(dX0), D2S(dX1),
                D2S(dY0), D2S(dY1),
                iCanvasWidth, iCanvasHeight, clrStroke, canvasBg);
            //screenJson = string.Format("{{\"data\":[{1}], \"options\":{0}}}", opt, data.ToString());
            data.AppendFormat("], \"options\":{0}}}", opt);
            screenJson = data.ToString();

            DateTime dt2 = DateTime.Now;
            TimeSpan diff = dt2 - dt1;
            int ms = (int)diff.TotalMilliseconds;
            if((loglevel & 0x1) > 0)
                Log("SceneDrawShape ms=" + ms + ", len=" + screenJson.Length);

            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });
        }

        /// <summary>
        /// удалить все Phob объекты со сцены
        /// </summary>
        public static void SceneClear()
        {
            foreach (var thr in lstThr)
            {
                if (thr != my_thread && thr.IsAlive) thr.Abort();
            }
            dicPhob.Clear();
            //смещение ящика в системе камеры
            xBoXTrans = 0;
            yBoXTrans = 0;
            zBoXTrans = -100;
            xRotor = -1.50899693899575;// -75 * Math.PI / 180.0; //вращение ящика вокруг оси X
            yRotor = -0.0853981633974484;// - 45 * Math.PI / 180.0; //вращение ящика вокруг оси Y
            zRotor = 0.1;//0; //вращение ящика вокруг оси Z
            matRotor.Build(XRotor, YRotor, ZRotor);
            screenJson = "";
        }

        /// <summary>
        /// сохранить все Phob объекты в файл
        /// </summary>
        /// <param name="fname">путь к файлу</param>
        public static void SceneSave(string fname)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fname, false, Encoding.UTF8);
                /*sw.WriteLine("{\"data\":[");
                int i = 0;
                foreach (Phob ph in lst)
                {
                    sw.WriteLine(string.Format("{1}{0}", ph.ToJson(), i > 0 ? "," : ""));
                    i++;
                }
                sw.WriteLine("]}");*/

                Scene sc = new Scene();
                sc.xRotor = xRotor;
                sc.yRotor = yRotor;
                sc.zRotor = zRotor;
                sc.xBoXTrans = xBoXTrans;
                sc.yBoXTrans = yBoXTrans;
                sc.zBoXTrans = zBoXTrans;
                sc.x_cam_angle = x_cam_angle;
                sc.y_cam_angle = y_cam_angle;
                sc.z_cam = z_cam;
                if (box != null)
                    sc.box.Copy(box.x0, box.x1, box.y0, box.y1, box.z0, box.z1);
                sc.lst = dicPhob.Values.ToList();
                foreach (Phob ph in sc.lst)
                {
                    ph.Dic2List();
                }
                //sc.dic.Add("key", "val");
                //sc.dic.Add("key2", "val2");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                //serializer.RegisterConverters(new JavaScriptConverter[] { new JSconverter() });
                sw.WriteLine(serializer.Serialize(sc));
                sw.Close();
            }
            catch (Exception se)
            {
                Console(se.Message);
                Log(se.Message);
            }
        }

        /// <summary>
        /// восстановить все Phob объекты из файла
        /// </summary>
        /// <param name="fname">путь к файлу</param>
        public static void SceneLoad(string fname)
        {
            string s = File.ReadAllText(fname, Encoding.UTF8);
            //Console(s);
            SceneClear();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //serializer.RegisterConverters(new JavaScriptConverter[] { new JSconverter() });
            Scene sc = serializer.Deserialize<Scene>(s);
            foreach (var ph in sc.lst)
            {
                ph.DicFromList();
                dicPhob.Add(ph.Id, ph);
            }
            xRotor = sc.xRotor;
            yRotor = sc.yRotor;
            zRotor = sc.zRotor;
            xBoXTrans = sc.xBoXTrans;
            yBoXTrans = sc.yBoXTrans;
            zBoXTrans = sc.zBoXTrans;
            x_cam_angle = sc.x_cam_angle;
            y_cam_angle = sc.y_cam_angle;
            z_cam = sc.z_cam;
            if (box != null)
            {
                box.Copy(sc.box.x0, sc.box.x1, sc.box.y0, sc.box.y1, sc.box.z0, sc.box.z1);
                boxShape.Reshape(box.x0, box.x1, box.y0, box.y1, box.z0, box.z1);
            }
            matRotor.Build(XRotor, YRotor, ZRotor);
        }

        /// <summary>
        /// массив идентификаторов объектов сцены
        /// </summary>
        public static int [] SceneIds()
        {
            return dicPhob.Keys.ToArray();
        }

        /// <summary>
        /// кинетическая энергия объектов сцены
        /// </summary>
        public static double SceneEnergy()
        {
            double dEn = 0;
            foreach (Phob ph in dicPhob.Values)
            {
                dEn += ph.KineticEnergy();
            }
            return dEn;
        }

        /// <summary>
        /// импульс объектов сцены
        /// </summary>
        public static void SceneImpulse(out double ix, out double iy, out double iz)
        {
            ix = 0;
            iy = 0;
            iz = 0;
            foreach (Phob ph in dicPhob.Values)
            {
                ph.Impulse(out double x, out double y, out double z);
                ix += x;
                iy += y;
                iz += z;
            }
        }

        /// <summary>
        /// Scene Min Distance to point
        /// </summary>
        public static double SceneMinDistance(double ix, double iy, double iz)
        {
            double dMin = Double.MaxValue;
            foreach (Phob ph in dicPhob.Values)
            {
                double d = ph.Distance(ix, iy, iz);
                if (d < dMin) dMin = d;
            }
            return dMin;
        }

        /// <summary>
        /// Scene facets number
        /// </summary>
        public static int SceneFacets()
        {
            int dEn = 0;
            foreach (Phob ph in dicPhob.Values)
            {
                var shp = ph.Shape;
                if (shp != null)
                    dEn += shp.lstFac.Count();
            }
            return dEn;
        }

        /// <summary>
        /// Scene facets area
        /// </summary>
        public static double SceneFacetsArea()
        {
            double dEn = 0;
            foreach (Phob ph in dicPhob.Values)
            {
                var shp = ph.Shape;
                if (shp != null)
                    foreach (var fac in shp.lstFac)
                    {
                        fac.CalcNormal();
                        dEn += fac.area;
                    }
            }
            return dEn;
        }

        /*
        void DbTest()
        {
            var db = new MathPanelExt.DB();
            if (!db.Connect("Host=127.0.0.1;Username=soapbus;Password=1;Database=soapbus"))
            {
                Console(db.LastError);
                return;
            }

            object[][] res = db.Vals(@"select * from tblScriplet;");
            if (res == null)
            {
                Console(db.LastError);
                return;
            }
            int i = 0;
            foreach (var v in res)
            {
                int j = 0;
                foreach (var v2 in v)
                {
                    Console("[" + i + "," + j + "]=" + v2.ToString());
                    if (v2 as byte[] != null)
                    {
                        byte[] bt = v2 as byte[];
                        int fw = ByteArrayToInt(bt);
                        int rev = ByteArrayReverseToInt(bt);
                        Console("len=" + bt.Length + ", forw=" + fw + ", rev=" + rev);
                        Console("s=" + IntToByteArrayReverseString(rev));
                    }
                    j++;
                }
                Console("==");
                i++;
            }

            db.Execute("INSERT INTO tblScripresult (scid,plevel,dtModif,data) VALUES (1,1,NOW(),'c1d1')");
            db.Close();
        }
        */

        //https://stackoverflow.com/questions/6695208/uri-escapedatastring-invalid-uri-the-uri-string-is-too-long
        /*static string MyEscape(string originalString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < originalString.Length; i++)
            {
                if ((originalString[i] >= 'a' && originalString[i] <= 'z') ||
                    (originalString[i] >= 'A' && originalString[i] <= 'Z') ||
                    (originalString[i] >= '0' && originalString[i] <= '9'))
                {
                    stringBuilder.Append(originalString[i]);
                }
                else
                {
                    stringBuilder.AppendFormat("%{0:X2}", (int)originalString[i]);
                }
            }

            string result = stringBuilder.ToString();
            return result;
        }*/

        /// <summary>
        /// Save Scriplet result on server
        /// </summary>
        public static void SaveScripresult()
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            /*//мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                webConsole.InvokeScript("ext_SaveScripresult", user, pass, server, scid, screenJson);
            });*/

            //Log("len=" + screenJson.Length + ",screenJson=" + screenJson);
            string js = MathPanelExt.Rest.EncodeString(screenJson);//too long// Uri.EscapeDataString(screenJson);
            //Log("js=" + js);
            //string decod = DecodeString(js);
            //if (screenJson != decod) 
                //Log("encode/decode error");

            string data = string.Format("param1={0}&param2={1}&param3=0&param4={2}&param5={3}",
                Uri.EscapeDataString(user), Uri.EscapeDataString(pass), scid, js);
            string s = MathPanelExt.Rest.Post(server + "?box=save_scripresult&r=" + DateTime.Now.Millisecond, data, "", "");
            Console("post=" + s);
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
            //code = "";//??
            string data = string.Format("param1={0}&param2={1}&param3=0&param4={2}&param5={3}&code={4}",
                Uri.EscapeDataString(user), Uri.EscapeDataString(pass), Uri.EscapeDataString(title), Uri.EscapeDataString(desct), Uri.EscapeDataString(code));
            string s = MathPanelExt.Rest.Post(server + "?box=scriplet&r=" + DateTime.Now.Millisecond, data, "", "");
            if( s.IndexOf("{\"data\":\"") == 0 )
            {
                string s2 = s.Substring(9);
                int pos = s2.IndexOf("\"");
                if (pos > 0)
                    scid = s2.Substring(0, pos);
            }
            Console("post=" + s);
        }

        /// <summary>
        /// Load Scriplet result from server
        /// </summary>
        public static string [] LoadScripresult(string scid, string scrid)
        {
            if (!bReady) return null;
            string data = string.Format("param1={0}&param2={1}&param3=0&param4={2}&param5={3}",
                Uri.EscapeDataString(user), Uri.EscapeDataString(pass), scid, scrid);
            string s = MathPanelExt.Rest.Post(server + "?box=sel_scripresult&r=" + DateTime.Now.Millisecond, data, "", "");
            //Log(s);
            int i;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, System.Collections.ArrayList> obj = serializer.Deserialize<Dictionary<string, System.Collections.ArrayList>>(s);
            if (obj != null && obj.ContainsKey("data"))
            {
                var obj2 = obj["data"];
                if (obj2 != null && obj2.Count > 0)
                {
                    string[] ss = new string[obj2.Count];
                    for(i = 0; i < obj2.Count; i++)
                    {
                        ss[i] = "";
                        var obj3 = obj2[i] as Dictionary<string, object>;
                        if (obj3 != null && obj3.ContainsKey("data") && obj3.ContainsKey("options"))
                        {
                            var opt = obj3["options"] as Dictionary<string, object>;
                            var dat = obj3["data"] as System.Collections.ArrayList;
                            if (opt != null && dat != null)
                            {
                                var sb = new StringBuilder();

                                sb.Append("{\"data\":[");
                                int m = 0, n;
                                foreach (var arr in dat)
                                {
                                    if (m > 0) sb.Append(",");
                                    var dic = arr as Dictionary<string, object>;
                                    n = 0;
                                    sb.Append("{");
                                    foreach (var pair in dic)
                                    {
                                        if (n > 0) sb.Append(",");
                                        sb.AppendFormat("\"{0}\":\"{1}\"", pair.Key, pair.Value.ToString().Replace(",", "."));
                                        n++;
                                    }
                                    sb.Append("}");
                                    m++;
                                }

                                sb.Append("],\"options\":{");
                                n = 0;
                                foreach (var pair in opt)
                                {
                                    if (n > 0) sb.Append(",");
                                    sb.AppendFormat("\"{0}\":\"{1}\"", pair.Key, pair.Value.ToString().Replace(",", "."));
                                    n++;
                                }
                                sb.Append("}}");

                                ss[i] = sb.ToString();
                                //Log(ss[i]);
                            }
                        }
                    }
                    return ss;
                }
            }
            return null;
        }

        /// <summary>
        /// Save Scriplet image on server
        /// </summary>
        /// <param name="fname">путь к файлу</param>
        public static string SaveScripImage(string fpath, string content)
        {
            if (!bReady) return "";

            List<Tuple<string, string, string, bool>> data = new List<Tuple<string, string, string, bool>>();
            var param1 = new Tuple<string, string, string, bool>("param1", user, "", false);
            var param2 = new Tuple<string, string, string, bool>("param2", pass, "", false);
            var param3 = new Tuple<string, string, string, bool>("param3", "0", "", false);
            var param4 = new Tuple<string, string, string, bool>("param4", scid, "", false);
            
            var file = new Tuple<string, string, string, bool>("PictureUpload", fpath, content, true);
            data.Add(param1);
            data.Add(param2);
            data.Add(param3);
            data.Add(param4);
            data.Add(file);

            string s = MathPanelExt.Rest.Upload(server + "?box=upload_file&r=" + DateTime.Now.Millisecond, "", "", data);
            Console("post=" + s);
            return s;
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
            while (true)
            {
                //мы запускаем код в UI потоке
                dispObj.Invoke(delegate
                {
                    ext_params = (string)webConsole.InvokeScript("ext_params");
                });
                if (ext_params != "") break;
                System.Threading.Thread.Sleep(100);
            }
            return ext_params;
        }

        /// <summary>
        /// метод для вызова тестов
        /// </summary>
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
