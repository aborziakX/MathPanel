//#define OLDCODE
//раскомментируйте OLDCODE , чтобы использовать первоначальный код методов из книги
//2020-2023, Andrei Borziak
//MathPanel (математическая панель) для работы со скриптами, написанными на C#.
//Область применения – моделирование процессов и визуализация.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Web.Script.Serialization;

namespace MathPanel
{
    public partial class Dynamo //: Window
    {
        readonly static string sLogFile = "mathpanel.log"; //файл логирования
        readonly static Dictionary<int, Phob> dicPhob = new Dictionary<int, Phob>(); //словарь физических объектов
        static bool bReady = false; //признак успешной инициализации
        static string keyConsole = ""; //ввод с клавиатуры
        static string sConsoleText = ""; //текст в консоли
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
        static bool bDrawBox = true; //признак рисования ящика
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

        static bool bOldCode = false; //if true - incorrect order of rotations

        public static bool BOldCode
        {
            get
            {
                return bOldCode;
            }
            set
            {
                bOldCode = value;
            }
        }

        //API
        /// <summary>
        /// преобразовать вещественное число в строку с точностью 4 знака после запятой
        /// </summary>
        /// <param name="d">число</param>
        public static string D2S(double d)
        {   //F4
            string s = d.ToString("G4", CultureInfo.InvariantCulture.NumberFormat);
            if (s.Contains("NaN"))
            {
                int kk = 0;
            }
            return s;
        }
        /// <summary>
        /// получить текст в окне сообщений
        /// </summary>
        public static string ConsoleText
        {
            get
            {
                return sConsoleText;
            }
        }
        /// <summary>
        /// логирование
        /// </summary>
        public static void Log(string s)
        {
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
                    Console(se.Message);
                }
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
                    if (bAscii) hex2 += k.ToString("X2");
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
                if (box != null)
                {
                    boxShape = new Cube();//2022-08-15
                    boxShape.iFill = 2; //ребра
                    boxShape.Reshape(box.x0, box.x1, box.y0, box.y1, box.z0, box.z1);
                    xBoXTrans = -(box.x0 + box.x1) / 2;
                    yBoXTrans = -(box.y0 + box.y1) / 2;
                    zBoXTrans = -box.z1 - z_cam * 0.1;
                }
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
        /// получить/задать bDrawBox для рисования ящика
        /// </summary>
        public static bool BDrawBox
        {
            get
            {
                return bDrawBox;
            }
            set
            {
                bDrawBox = value;
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

#if OLDCODE
        /// <summary>
        /// преобразовать координаты в системе ящика в координаты в системе камеры (старый метод)
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        /// <param name="z">z координата</param>
        /// <param name="radius">радиус объекта</param>
        /// <param name="bScreen">проецировать координаты на экран</param>
        public static void Traslate2Camera(ref double x, ref double y, ref double z, ref double radius, bool bScreen = false)
        {
            double x_n, y_n, z_n;
            if (bOldCode)
            {//old
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
            }
            else
            {   //new version
                //вращать вокруг Y
                if (yRotor != 0)
                {
                    z_n = z * Math.Cos(yRotor) - x * Math.Sin(yRotor);
                    x_n = z * Math.Sin(yRotor) + x * Math.Cos(yRotor);
                    z = z_n;
                    x = x_n;
                }

                //вращать вокруг X
                if (xRotor != 0)
                {
                    y_n = y * Math.Cos(xRotor) - z * Math.Sin(xRotor);
                    z_n = y * Math.Sin(xRotor) + z * Math.Cos(xRotor);
                    y = y_n;
                    z = z_n;
                }

                //вращать вокруг Z
                if (zRotor != 0)
                {
                    x_n = x * Math.Cos(zRotor) - y * Math.Sin(zRotor);
                    y_n = x * Math.Sin(zRotor) + y * Math.Cos(zRotor);
                    x = x_n;
                    y = y_n;
                }
            }

            //сдвинуть
            x += xBoXTrans;
            y += yBoXTrans;
            z += zBoXTrans;

            if (bScreen)
                Traslate2Screen(ref x, ref y, ref z, ref radius);
        }
#else
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
#endif

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

#if OLDCODE
        /// <summary>
        /// данные для рисования ребер ящика (старый метод)
        /// </summary>
        static string BoxEdges()
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
#else
        /// <summary>
        /// данные для рисования ребер ящика 
        /// </summary>
        static string BoxEdges()
        {
            double x, y, z, radius = 1, x_fr, y_fr, z_fr;
            double rad = Math.Abs(1.0 / ((box.x1 - box.x0) * iCanvasWidth));

            string text;
            var data = new StringBuilder();
            if (bDrawBox)
            {
                string ss = DrawShape(boxShape, null);
                if (ss != "")
                {
                    data.Append(ss);
                }
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
#endif
        /// <summary>
        /// подготовить данные формы для визуализации в canvas
        /// </summary>
        /// <param name="shape">форма объекта</param>
        /// <param name="ph">сам объект</param>
        static string DrawShape(GeOb shape, Phob ph)
        {
            if (shape == null) return "";
            StringBuilder data = new StringBuilder();
            double radius = 1;
            double rad = (2 * physWidth) / iCanvasWidth;    //1 pixel
            Vec3 vref;
            Vec3 vTemp = new Vec3();
            shape.Transform();

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

                    //добавить координаты объекта
                    if (ph != null)
                        vref.Add(ph.x, ph.y, ph.z);

                    //перейти в СКК
                    Traslate2Camera(ref vref.x, ref vref.y, ref vref.z, ref radius, false);
                }
            }

            //сортировать по удалению от камеры
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

            //данные на экран для каждой грани
            foreach (var fac in lst)
            {
                //игра света
                fac.CalcNormal(true);
                //fac.dark = fac.normal_cam.z <= 0.0 ? 0.1 : (fac.normal_cam.z + 0.5) / 1.5;
                //if (DrawCount % 100 == 1)
                //Console(fac.name + ",dark=" + dark);
                fac.Center(out double x, out double y, out double z, true);
                vTemp.Copy(-x, -y, z_cam - z); //вектор к камере
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
                    if ((shape.iFill & 3) == 1) edges = string.Format(",\"csk\":\"{0}\"", fac.ColorDarkHtml(fac.dark));

                    string style = j == fac.Count ? ((((shape.iFill & 1) == 1) && cosfi > 0.0) ? "line_endf" : "line_end") : "line";

                    if (data.Length != 0) data.Append(",");
                    data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\"{6}}}",
                        D2S(vref.x), D2S(vref.y),
                        style, D2S(rad),
                        fac.ColorDarkHtml(fac.dark), title, edges);
                }

                if (shape.bDrawNorm)
                {
                    //нормали
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
        /// список граней формы для визуализации в canvas
        /// </summary>
        /// <param name="shape">форма объекта</param>
        /// <param name="ph">сам объект</param>
        static List<Facet3> DrawShapeFac(GeOb shape, Phob ph)
        {
            List<Facet3> lstFac = new List<Facet3>();   //список граней
            if (shape == null) return lstFac;
            StringBuilder data = new StringBuilder();
            double radius = 1;
            double rad = (2 * physWidth) / iCanvasWidth;    //1 pixel
            Vec3 vref;
            Vec3 vTemp = new Vec3();
            shape.Transform();

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

                    //добавить координаты объекта
                    if (ph != null)
                        vref.Add(ph.x, ph.y, ph.z);
                    fac.phob = ph;

                    //перейти в СКК
                    Traslate2Camera(ref vref.x, ref vref.y, ref vref.z, ref radius, false);
                }

                //игра света
                fac.CalcNormal(true);
                fac.Center(out double x, out double y, out double z, true);
                if (z >= z_cam - 20) continue;//слишком близко

                vTemp.Copy(-x, -y, z_cam - z); //вектор к камере
                double dLen = vTemp.Length();
                if (dLen == 0) continue;
                double cosfi = vTemp.ScalarProduct(fac.normal_cam) / dLen;  //косинус угла между вектором на камеру и нормалью
                fac.dark = (cosfi < 0.1) ? 0.1 : cosfi;

                if (shape.iFill == 0 && !shape.bDrawBack) continue; //не рисуем
                if (cosfi <= 0.0 && ((shape.iFill & 2) == 0) && !shape.bDrawBack) continue;//не видно грань и не рисуем ребра

                fac.cosfi = cosfi;
                fac.shape = shape;
                fac.phob = ph;
                lstFac.Add(fac);
            }
            return lstFac;
        }

        /// <summary>
        /// подготовить данные для визуализации на канвасе
        /// </summary>
        /// <param name="bCons">вывод в окно сообщений</param>
        public static void SceneDrawHelper(bool bCons = false)
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

            /*if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });*/
        }

        //2022-02-17
        /// <summary>
        /// подготовить данные для визуализации на канвасе используя формы
        /// <param name="bBx">рисовать оси</param>
        /// <param name="bCons">вывод в окно сообщений</param>
        /// <return>attribute value</return>
        /// </summary>
        public static void SceneDrawShapeHelper(bool bBx = true, bool bCons = false)
        {
            DateTime dt1 = DateTime.Now;
            DrawCount++;
            int i = 0;
            double dX0 = 0, dX1 = 1, dY0 = 0, dY1 = 1;
            double radius = 1;
            double rad = (2 * physWidth) / iCanvasWidth;    //1 pixel

            var data = new StringBuilder();
            string starter = "{{\"data\":[";
            data.AppendFormat(starter);

            //показать границы ящика
            if (box != null && bBx) data.Append(BoxEdges());

            List<Facet3> lstFac = new List<Facet3>();   //список граней

            //для каждого объекта сцены
            List<Phob> lst = dicPhob.Values.ToList();
            foreach (Phob ph in lst)
            {   //перейти в СКК
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
            //сортировать по удаленности от камеры
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
                    {   //рисовать как линию
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
                //воостановить координаты
                ph.RestoreCoord();

                if (ph.Shape != null)
                {
                    lstFac.AddRange(DrawShapeFac(ph.Shape, ph));
                }
            }

            try
            {   //can fail while scene is reloaded
                lstFac.Sort(delegate (Facet3 fx, Facet3 fy)
                {
                    double x_t0, y_t0, z_t0, x_t1, y_t1, z_t1;
                    fx.Center(out x_t0, out y_t0, out z_t0, true);
                    fy.Center(out x_t1, out y_t1, out z_t1, true);
                    return z_t0 >= z_t1 ? 1 : -1;
                });
            }
            catch (Exception) { };

            //данные на экран для каждой грани
            Vec3 vref;
            foreach (var fac in lstFac)
            {
                var ph = fac.phob;
                var shape = fac.shape;
                var cosfi = fac.cosfi;
                fac.Center(out double x, out double y, out double z, true);
                double x1 = x, y1 = y, z1 = z;

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
                    if ((shape.iFill & 3) == 1) edges = string.Format(",\"csk\":\"{0}\"", fac.ColorDarkHtml(fac.dark));

                    string style = j == fac.Count ? ((((shape.iFill & 1) == 1) && cosfi > 0.0) ? "line_endf" : "line_end") : "line";

                    if (data.Length > starter.Length) data.Append(",");
                    data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\"{6}}}",
                        D2S(vref.x), D2S(vref.y),
                        style, D2S(rad),
                        fac.ColorDarkHtml(fac.dark), title, edges);
                }
                if (shape.bDrawNorm)
                {
                    //нормали
                    x1 = x + fac.normal_cam.x * shape.radius * 0.25;
                    y1 = y + fac.normal_cam.y * shape.radius * 0.25;
                    z1 = z + fac.normal_cam.z * shape.radius * 0.25;

                    if (data.Length > starter.Length) data.Append(",");
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
            //screenJson = string.Format("{{\"data\":[{1}], \"options\":{0}}}", opt, data.ToString());
            data.AppendFormat("], \"options\":{0}}}", opt);
            screenJson = data.ToString();

            DateTime dt2 = DateTime.Now;
            TimeSpan diff = dt2 - dt1;
            int ms = (int)diff.TotalMilliseconds;
            if ((loglevel & 0x1) > 0)
                Log("SceneDrawShape ms=" + ms + ", len=" + screenJson.Length);

            /*if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });*/
        }
        //old low quality approach
        public static void SceneDrawShapeBookHelper(bool bBx = true, bool bCons = false)
        {
            DateTime dt1 = DateTime.Now;
            DrawCount++;
            int i = 0;
            double dX0 = 0, dX1 = 1, dY0 = 0, dY1 = 1;
            var data = new StringBuilder();
            string starter = "{{\"data\":[";
            data.AppendFormat(starter);

            //показать границы ящика
            if (box != null && bBx) data.Append(BoxEdges());

            //для каждого объекта сцены
            List<Phob> lst = dicPhob.Values.ToList();
            foreach (Phob ph in lst)
            {   //перейти в СКК
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
            //сортировать по удаленности от камеры
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
                    {   //рисовать как линию
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
                //воостановить координаты
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
            //параметры рисования: размеры, цвет, стиль
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
            if ((loglevel & 0x1) > 0)
                Log("SceneDrawShape ms=" + ms + ", len=" + screenJson.Length);

            /*if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bCons) Console(screenJson);
                webConsole.InvokeScript("ext_json", screenJson);
            });*/
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
            if (!sc.box.Is1Box())
            {
                //box.Copy(sc.box.x0, sc.box.x1, sc.box.y0, sc.box.y1, sc.box.z0, sc.box.z1);
                //boxShape.Reshape(box.x0, box.x1, box.y0, box.y1, box.z0, box.z1);
                SceneBox = sc.box;
            }
            else box = null;

            matRotor.Build(XRotor, YRotor, ZRotor);
        }

        /// <summary>
        /// массив идентификаторов объектов сцены
        /// </summary>
        public static int[] SceneIds()
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
            if (!bReady) return;
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
        public static void ScripletHelper(string title, string desct, string code)
        {
            if (!bReady) return;
            /*if (!bReady || dispObj.HasShutdownStarted) return;
            string code = "";
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) code = txtCommand.Text;
            });*/
            //code = "";//??
            string data = string.Format("param1={0}&param2={1}&param3=0&param4={2}&param5={3}&code={4}",
                Uri.EscapeDataString(user), Uri.EscapeDataString(pass), Uri.EscapeDataString(title), Uri.EscapeDataString(desct), Uri.EscapeDataString(code));
            string s = MathPanelExt.Rest.Post(server + "?box=scriplet&r=" + DateTime.Now.Millisecond, data, "", "");
            if (s.IndexOf("{\"data\":\"") == 0)
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
        public static string[] LoadScripresult(string scid, string scrid)
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
                    for (i = 0; i < obj2.Count; i++)
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
        /// разбить объект на кучу мелких на основе граней
        /// </summary>
        /// <param name="ph">объект</param>
        /// <param name="speed">скорость осколков</param>
        public static void Explode(Phob ph, double speed = 0)
        {
            var shape = ph.Shape;
            if (shape == null)
                return;
            int cnt = shape.lstFac.Count;
            double cubeVolume = (ph.radius * ph.radius * ph.radius * shape.scaleX * shape.scaleY * shape.scaleZ);
            double newVolume = (cubeVolume * 0.5) / cnt;
            double newSize = Math.Exp(Math.Log(newVolume) / 3);
            System.Drawing.Color clr = shape.clr.GetColor();
            double x, y, z;
            foreach (var fac in shape.lstFac)
            {
                fac.Center(out x, out y, out z, false);
                x = ph.x + x * shape.scaleX;
                y = ph.y + y * shape.scaleY;
                z = ph.z + z * shape.scaleZ;
                int id = PhobNew(x, y, z);
                var hz = PhobGet(id) as Phob;
                Cube cub = new Cube(newSize, clr.Name);
                hz.Shape = cub;

                //найти скорость
                double dLen = Math.Sqrt((x - ph.x) * (x - ph.x) + (y - ph.y) * (y - ph.y) + (z - ph.z) * (z - ph.z));
                if (dLen > 0)
                {
                    dLen = speed / dLen;
                    hz.v_x = (x - ph.x) * dLen;
                    hz.v_y = (y - ph.y) * dLen;
                    hz.v_z = (z - ph.z) * dLen;
                }
            }

            Dynamo.PhobDel(ph.Id);
        }

        /// <summary>
        /// изменить позицию объекта на основе его скорости
        /// </summary>
        /// <param name="DT">шаг времени</param>
        public static void UpdatePosition(double DT)
        {
            foreach (var pair in dicPhob)
            {
                var hz = pair.Value;
                hz.x += hz.v_x * DT;
                hz.y += hz.v_y * DT;
                hz.z += hz.v_z * DT;
            }
        }

        /// <summary>
        /// получить словарь объектов
        /// </summary>
        public static Dictionary<int, Phob> ScenePhobs()
        {
            return dicPhob;
        }
    }
}
