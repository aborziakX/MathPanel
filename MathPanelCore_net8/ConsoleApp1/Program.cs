//2021, Andrei Borziak
//cross-platform version of MathPanel - socket server now. test_server.htm as a client
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.CodeAnalysis.Scripting;
using MathPanelExt;
using System.Globalization;
//using System.Web.Script.Serialization;

//2024-10-05 Framework 8
#pragma warning disable CS8603
#pragma warning disable CS8602
#pragma warning disable CS8601
#pragma warning disable CS8600
#pragma warning disable CS8625
#pragma warning disable CS0219


namespace MathPanel
{
    public class Dynamo
    {
        static int mmm = 0;
        readonly static string sStart = "Host: ", sEnd = "\r\n\r\n";
        //multithreading dictionary
        readonly static Dictionary<string, string> myDic = new Dictionary<string, string>();
        readonly static StringBuilder sb2 = new StringBuilder();
        readonly static object locker = new object();

        readonly static string version = " v1.0";
        readonly static string sLogFile = "MathPanelCore.log";
        readonly static Dictionary<int, Phob> dicPhob = new Dictionary<int, Phob>();
        static string keyConsole = "";
        static bool scriptDone = true;
        static string sConsole = "";
        static string sAlert = "";
        static string sHtml = "";
        static string screenJson = "";
        readonly static List<string> lstJson = new List<string>();
        static string txtCommand = "";
        
        static System.Threading.Thread? my_thread = null;
        readonly static List<System.Threading.Thread> lstThr = new List<Thread>();
        static InteractiveAssemblyLoader loader = new InteractiveAssemblyLoader();
        static ScriptOptions scriptOptions = ScriptOptions.Default;
        static IEnumerable<Assembly> assm = System.Linq.Enumerable.Empty<Assembly>();

        static double z_cam = 100;  //z-позиция камеры
        static double x_cam_angle = 1.5;  //угол камеры
        static double y_cam_angle = 1.5;  //угол камеры
        //размеры изображения в 0,0,0
        static double physWidth = z_cam * Math.Tan(x_cam_angle);// * Math.PI / 360.0);
        static double physHeight = z_cam * Math.Tan(y_cam_angle);// * Math.PI / 360.0);
        //проектируется на экран html-canvas (в пикселях)
        static int iCanvasWidth = 800;// (int)((600 * physWidth) / physHeight);
        static int iCanvasHeight = 600;
        //ящик - система координат, в которой размещены объекты
        static Box? box;  //границы ящика для рисования
        static Cube boxShape = new Cube();  //ящик для рисования
        static double xBoXTrans = 0, yBoXTrans = 0, zBoXTrans = -100;//смещение ящика в системе камеры
        static double xRotor = -0; //вращение ящика вокруг оси X
        static double yRotor = 0; //вращение ящика вокруг оси Y
        static double zRotor = 0; //вращение ящика вокруг оси Z
        static Mat3 matRotor = new Mat3();
        static int DrawCount = 0; //счетчик рисований
        static bool bDrawBox = true; //признак рисования ящика
        static bool bAxes = true;
        static string clrNormal = "#ff0000";    //цвет нормалей
        static string clrStroke = "#999999";    //цвет ребер
        static int widNormal = 3;
        static string user = "", pass = "", server = "", scid = "0";
        static int loglevel = 0;
        static string canvasBg = "#000000";
        static string ext_params = "";


        static string GetValue(string key)
        {
            lock (locker)
            {
                string? val;
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
                string data = @"2;10
5;55
9;36
7;33";

                string s1 = QuadroEqu.DrawLine(0, 0, 10, 10);
                string s2 = "{\"options\":{\"x0\": -3, \"x1\": 13, \"y0\": -3, \"y1\": 13, \"clr\": \"#ff0000\", \"sty\": \"line\", \"size\":10, \"lnw\": 3, \"wid\": 800, \"hei\": 600 }";
                data = s2 + ", \"data\":[" + s1 + "]}";
 
                switch (cmd)
                {
                    default:
                        toSend = "HTTP/1.1 200 OK\r\nDate: Mon, 08 May 2021 21:12:40 GMT\r\nServer: Apache/2\r\nAccess-Control-Allow-Origin: *\r\nContent-Type: text/html\r\n\r\n<html><body>" + data +"</body></html>\r\n\r\n";
                        break;
                }
                SocketServer.Log("ProcessMy1, thread " + Thread.CurrentThread.ManagedThreadId + ", cmd=" + cmd, 3);
                mmm++;
                toSend = mmm + toSend + sEnd;
                bytesToSend = Encoding.UTF8.GetBytes(toSend);
            }
            return bytesToSend;
        }
        static byte[] ProcessMy2(StateObject state)
        {
            lock (locker)
            {
                byte[] bytesToSend = null;
                if (state.sb.ToString().Contains(sEnd))
                {
                    string toSend = "";
                    //string cmd = SocketServer.Between(state.sb.ToString(), sStart, sEnd);
                    string code = state.sb.ToString();

                    int pos = code.IndexOf("script=");
                    if (pos > 0) code = code.Substring(pos + 7);
                    SocketServer.Log("ProcessMy2, thread " + Thread.CurrentThread.ManagedThreadId + ", codeLen=" + code.Length);
                    code = code.Replace("+", " ");
                    code = Rest.DecodeString(code);
                    SocketServer.Log("ProcessMy2, thread " + Thread.CurrentThread.ManagedThreadId + ", code=" + code);

                    /*string includeNamespaces = @"using MathPanel;
    using MathPanelExt;";
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
                            includeNamespaces,
                            "void",
                            string.Empty
                            );*/
                    if (code == "scene")
                    {   //just do it
                    }
                    else if (code.IndexOf("scene&key=") == 0)
                    {
                        if (code.Length > "scene&key=".Length)
                        {
                            string key = code.Substring("scene&key=".Length, 1);
                            MyPreviewKeyDown(key);
                        }
                    }
                    else
                    {   //start script
                        //??Abort not supported?
                        if (my_thread != null && my_thread.IsAlive)
                        {
                            my_thread.Interrupt();//.Abort();
                            Thread.Sleep(1000);
                        }
                        my_thread = null;
                        SceneClear();
                        txtCommand = code;

                        var script = CSharpScript.Create(code, scriptOptions, null, loader);
                        try
                        {
                            my_thread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                            {
                                try
                                {
                                    var scrState = script.RunAsync().Result;
                                    var rc = scrState.ReturnValue != null ? scrState.ReturnValue.ToString() : "null";
                                    Console("Done");
                                    scriptDone = true;
                                }
                                catch (Exception xxx) { Console("Q:" + xxx.ToString()); scriptDone = true; }
                            }));
                            my_thread.Start();
                            lstThr.Add(my_thread);
                        }
                        catch (Exception xxx) { Console("W:" + xxx.ToString()); scriptDone = true; }
                    }

                    string scene = "";
                    string json = "[";
                    for (int i = 0; i < lstJson.Count; i++)
                    {
                        if (i > 0) json += ",";
                        json += lstJson[i];
                    }
                    json += "]";
                    sConsole = sConsole.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
                    sAlert = sAlert.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
                    sHtml = sHtml.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
                    scene = string.Format("{{\"finished\":{0}, \"console\":\"{1}\", \"alert\":\"{2}\", \"html\":\"{3}\", \"scene\":{4}}}",
                        (scriptDone ? "true" : "false"), sConsole, sAlert, sHtml, json);
                    sConsole = "";
                    sAlert = "";
                    sHtml = "";

                    toSend = "HTTP/1.1 200 OK\r\nDate: Mon, 08 May 2021 21:12:40 GMT\r\nServer: Apache/2\r\nAccess-Control-Allow-Origin: *\r\nContent-Type: text/html\r\n\r\n" +
                                "<html><body>" + scene + "</body></html>\r\n\r\n";

                    //SocketServer.Log("ProcessMy2, thread " + Thread.CurrentThread.ManagedThreadId + ", toSend=" + toSend, 3);
                    mmm++;
                    bytesToSend = Encoding.UTF8.GetBytes(toSend);
                    lstJson.Clear();
                }
                return bytesToSend;
            }
        }

        //API
        /// <summary>
        /// popup alert
        /// </summary>
        /// <param name="s">popup text</param>
        public static void Alert(string s)
        {
            sAlert = s;
        }
        /// <summary>
        /// add text to console window
        /// </summary>
        /// <param name="s">text</param>
        public static void Console(string s, bool bNewLine = true)
        {
            var s2 = s + (bNewLine ? "\n" : "");
            System.Console.WriteLine(s2);
            sConsole += s2;
        }
        /// <summary>
        /// clear console window
        /// </summary>
        public static void ConsoleClear()
        {
            sConsole = "";
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

        public static string D2S(double d)
        {   //F4
            string s = d.ToString("G4", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if( s.Contains("NaN"))
            {
                int kk = 0;
            }
            return s;
        }

        /// <summary>
        /// create Phob
        /// </summary>
        /// <param name="x">value for x-property</param>
        /// <param name="y">value for y-property</param>
        /// <param name="z">value for z-property</param>
        public static int PhobNew(double x, double y, double z = 0)
        {
            Phob pt = new Phob(x, y, z);
            dicPhob.Add(pt.Id, pt);
            return pt.Id;
        }

        /// <summary>
        /// get count of Phob's
        /// </summary>
        public static int PhobCount()
        {
            return dicPhob.Count;
        }

        /// <summary>
        /// set Phob properties
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
        /// get Phob by id
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
        /// delete Phob by id
        /// </summary>
        /// <param name="id">Phob id</param>
        public static void PhobDel(int id)
        {
            if (!dicPhob.ContainsKey(id))
                return;
            dicPhob.Remove(id);
        }

        /// <summary>
        /// set Phob attribute
        /// </summary>
        /// <param name="id">Phob id</param>
        /// <param name="key">attribute key</param>
        /// <param name="value">attribute value</param>
        public static void PhobAttrSet(int id, string key, string value)
        {
            if (!dicPhob.ContainsKey(id)) return;
            Phob pt = dicPhob[id];
            if (pt == null) return;
            pt.AttrSet(key, value);
        }

        /// <summary>
        /// get Phob attribute
        /// </summary>
        /// <param name="id">Phob id</param>
        /// <param name="key">attribute key</param>
        /// <return>attribute value</return>
        public static string PhobAttrGet(int id, string key)
        {
            if (!dicPhob.ContainsKey(id)) return null;
            Phob pt = dicPhob[id];
            if (pt == null) return null;
            return pt.AttrGet(key);
        }
        /// <summary>
        /// returns last pressed key
        /// </summary>
        /// <return>last key</return>

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
        /// get/set box for visualization in canvas
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
        /// get or set z-position of camera in camera coordinates
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
        /// get or set bAxes parameter to draw coordinates
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
        /// get string passed to graphic canvas
        /// </summary>
        public static string ScreenJson
        {
            get
            {
                return screenJson;
            }
        }
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
        /// <summary>
        /// send JSON data for visualization in canvas
        /// </summary>
        public static void SceneJson(string s_json, bool bSecond = false)
        {
            lock (locker)
            {
                if (!bSecond) lstJson.Clear();
                screenJson = s_json;
                lstJson.Add(screenJson);
            }
        }
        /// <summary>
        /// translate box coordinates to camera coordinates
        /// </summary>
        public static void Traslate2Camera(ref double x, ref double y, ref double z, ref double radius, bool bScreen = false)
        {
            Vec3 v = new Vec3(x, y, z);
            Vec3 v_new = new Vec3();
            matRotor.Mult(v, ref v_new);

            //translate
            x = v_new.x + xBoXTrans;
            y = v_new.y + yBoXTrans;
            z = v_new.z + zBoXTrans;

            if (bScreen)
                Traslate2Screen(ref x, ref y, ref z, ref radius);
        }
        /// <summary>
        /// translate camera coordinates to x,y,size on 2d-screen
        /// </summary>
        public static void Traslate2Screen(ref double x, ref double y, ref double z, ref double radius)
        {
            double d_squeeze = (z_cam) / (z_cam - z);
            x *= d_squeeze;
            y *= d_squeeze;
            radius *= d_squeeze;
        }

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
        /// prepare data for visualization in canvas
        /// <param name="bCons">вывод на консоль состояния</param>
        /// <return>attribute value</return>
        /// </summary>
        public static void SceneDraw(bool bCons = false)
        {
            DrawCount++;
            var data = new StringBuilder();
            int i = 0;
            double dX0 = 0, dX1 = 1, dY0 = 0, dY1 = 1;

            //add box edges
            if (box != null) data.Append(BoxEdges());

            //sort by z-position
            List<Phob> lst = dicPhob.Values.ToList();
            foreach (Phob ph in lst)
            {
                //adjust using camera position
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
                if (ph.z < z_cam)
                {
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
                        Dynamo.Traslate2Camera(ref px, ref py, ref pz, ref radius, true);
                        data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\"}}",
                            px.ToString(CultureInfo.InvariantCulture.NumberFormat), py.ToString(CultureInfo.InvariantCulture.NumberFormat),
                            "line", radius.ToString(CultureInfo.InvariantCulture.NumberFormat),
                            string.IsNullOrEmpty(xxx) ? clrNormal : xxx, string.IsNullOrEmpty(txt1) ? "" : txt1, string.IsNullOrEmpty(yyy) ? "3" : yyy);
                        px = ph.p2.x;
                        py = ph.p2.y;
                        pz = ph.p2.z;
                        Dynamo.Traslate2Camera(ref px, ref py, ref pz, ref radius, true);
                        data.AppendFormat(",{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"clr\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\"}}",
                            px.ToString(CultureInfo.InvariantCulture.NumberFormat), py.ToString(CultureInfo.InvariantCulture.NumberFormat),
                            "line_end", radius.ToString(CultureInfo.InvariantCulture.NumberFormat),
                            string.IsNullOrEmpty(xxx) ? clrNormal : xxx, string.IsNullOrEmpty(txt2) ? "" : txt2, string.IsNullOrEmpty(yyy) ? "3" : yyy);
                    }
                }
                ph.RestoreCoord();
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
            var js = string.Format("{{\"options\":{0}, \"data\":[{1}]}}", opt, data.ToString());

            SceneJson(js);
        }
        /// <summary>
        /// prepare data for visualization in canvas using shapes
        /// <param name="bBx">рисовать оси</param>
        /// <param name="bCons">вывод на консоль состояния</param>
        /// <return>attribute value</return>
        /// </summary>
        public static void SceneDrawShape(bool bBx = true, bool bCons = false)
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
            var js = data.ToString();

            DateTime dt2 = DateTime.Now;
            TimeSpan diff = dt2 - dt1;
            int ms = (int)diff.TotalMilliseconds;
            if ((loglevel & 0x1) > 0)
                Log("SceneDrawShape ms=" + ms + ", len=" + js.Length);

            SceneJson(js);
        }

        /// <summary>
        /// remove all Phob's
        /// </summary>
        public static void SceneClear()
        {
            lock (locker)
            {
                foreach (var thr in lstThr)
                {
                    if (thr != my_thread && thr.IsAlive) thr.Interrupt();//.Abort();
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
                lstJson.Clear();
                scriptDone = false;
                sConsole = "";
                sAlert = "";
                sHtml = "";
                bDrawBox = true; //признак рисования ящика
            }
        }

        /// <summary>
        /// save all Phob's to file
        /// </summary>
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

                var ser = Newtonsoft.Json.JsonConvert.SerializeObject(sc);
                sw.WriteLine(ser);
                sw.Close();
            }
            catch (Exception se)
            {
                Console(se.Message);
                Log(se.Message);
            }
        }

        /// <summary>
        /// restore all Phob's from file
        /// </summary>
        public static void SceneLoad(string fname)
        {
            string s = File.ReadAllText(fname, Encoding.UTF8);
            //Console(s);
            SceneClear();
            Scene sc = Newtonsoft.Json.JsonConvert.DeserializeObject<Scene>(s);
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
        /// get all Phob Ids from scene
        /// </summary>
        public static int[] SceneIds()
        {
            return dicPhob.Keys.ToArray();
        }

        /// <summary>
        /// Scene Energy
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
        /// Scene Impulse
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

        /// <summary>
        /// Save Scriplet result on server
        /// </summary>
        public static void SaveScripresult()
        {
            string js = MathPanelExt.Rest.EncodeString(screenJson);
            if (string.IsNullOrEmpty(js)) return;
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
            string code = txtCommand;
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
            string data = string.Format("param1={0}&param2={1}&param3=0&param4={2}&param5={3}",
                Uri.EscapeDataString(user), Uri.EscapeDataString(pass), scid, scrid);
            string s = MathPanelExt.Rest.Post(server + "?box=sel_scripresult&r=" + DateTime.Now.Millisecond, data, "", "");
            //Log(s);
            Dictionary<string, System.Collections.ArrayList> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, System.Collections.ArrayList>>(s);
            int i;
            if (obj != null && obj.ContainsKey("data"))
            {
                var obj2 = obj["data"];
                if (obj2 != null && obj2.Count > 0)
                {
                    string[] ss = new string[obj2.Count];
                    for (i = 0; i < obj2.Count; i++)
                    {
                        ss[i] = "";
                        var obj3 = obj2[i] as Newtonsoft.Json.Linq.JObject;
                        if (obj3 != null && obj3.ContainsKey("data") && obj3.ContainsKey("options"))
                        {
                            var opt = obj3["options"];// as Dictionary<string, object>;
                            var dat = obj3["data"];// as System.Collections.ArrayList;
                            if (opt != null && dat != null)
                            {
                                var sb = new StringBuilder();

                                sb.Append("{\"data\":[");
                                int m = 0, n;
                                foreach (var arr in dat)
                                {
                                    if (m > 0) sb.Append(",");
                                    var dic = arr;// as Dictionary<string, object>;
                                    n = 0;
                                    sb.Append("{");
                                    foreach (var pair2 in dic)
                                    {
                                        var pair = pair2 as Newtonsoft.Json.Linq.JProperty;
                                        if (pair == null) continue;
                                        if (n > 0) sb.Append(",");
                                        sb.AppendFormat("\"{0}\":\"{1}\"", pair.Name, pair.Value.ToString().Replace(",", "."));
                                        n++;
                                    }
                                    sb.Append("}");
                                    m++;
                                }

                                sb.Append("],\"options\":{");
                                n = 0;
                                foreach (var pair2 in opt)
                                {
                                    var pair = pair2 as Newtonsoft.Json.Linq.JProperty;
                                    if (pair == null) continue;
                                    if (n > 0) sb.Append(",");
                                    sb.AppendFormat("\"{0}\":\"{1}\"", pair.Name, pair.Value.ToString().Replace(",", "."));
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
        /// set html into control
        /// </summary>
        /// <param name="data">html string</param>
        public static void SetHtml(string data)
        {
            sHtml = data;
        }
        private static void MyPreviewKeyDown(string key)
        {
            if (key == "Z")
            {
                ZRotor += 0.1;
                //matRotor.Build(XRotor, YRotor, ZRotor);
            }
            else if (key == "W")
            {
                ZRotor -= 0.1;
                //matRotor.Build(XRotor, YRotor, ZRotor);
            }
            else if (key == "U")
            {
                XRotor += 0.1;
                //matRotor.Build(XRotor, YRotor, ZRotor);
            }
            else if (key == "D")
            {
                XRotor -= 0.1;
                //matRotor.Build(XRotor, YRotor, ZRotor);
            }
            else if (key == "L")
            {
                YRotor -= 0.1;
                //matRotor.Build(XRotor, YRotor, ZRotor);
            }
            else if (key == "R")
            {
                YRotor += 0.1;
                //matRotor.Build(XRotor, YRotor, ZRotor);
            }
            else if (key == "z") ZBoXTrans += 0.3;
            else if (key == "w") ZBoXTrans -= 0.3;
            else if (key == "u") YBoXTrans += 0.3;
            else if (key == "d") YBoXTrans -= 0.3;
            else if (key == "l") XBoXTrans -= 0.3;
            else if (key == "r") XBoXTrans += 0.3;
            //Console(key);
            keyConsole = key;
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


        static void Main(string[] args)
        {
            var scr = "System.Console.WriteLine(\"Hello my World!\");";
            foreach(var a in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                Console("A:" + a.FullName, false);
            }
            scriptOptions = scriptOptions.AddReferences(assm);
            scriptOptions = scriptOptions.WithReferences(new[]
            {
                typeof(Dynamo).Assembly,
                typeof(HashSet<int>).Assembly,
                typeof(System.Text.StringBuilder).Assembly
            });

            scriptOptions = scriptOptions.WithImports("System", "System.Collections.Generic", 
                "System.Text", "System.Threading");

            scriptOptions = scriptOptions.AddImports("MathPanel");
            scriptOptions = scriptOptions.AddImports("MathPanelExt");

            var script = CSharpScript.Create(scr, scriptOptions, null, loader);
            var state = script.RunAsync().Result;
            //return;

            SceneClear();
            boxShape.iFill = 2; //ребра
            CameraZ = 100;
            SocketServer.sLogFile = "MathPanelCore.log";

            Console("MathPanelCore started!", false);
            SocketServer.Log("MathPanelCore started", 3);
            List<Thread> theList = new List<Thread>();
            List<SocketServer> sockList = new List<SocketServer>();

            var inireader = new IniReader("MathPanelCore.config");

            string s, host = "", name = "";
            int port = 0;
            try
            {
                s = inireader.AppSettings("loglevel");
                Console("loglevel=" + s, false);
                if (!string.IsNullOrEmpty(s))
                {
                    loglevel = int.Parse(s);
                    SocketServer.loglevel = loglevel;
                }

                s = inireader.AppSettings("name");
                if (!string.IsNullOrEmpty(s))
                    name = s;
                else name = "Tester";
                Console("name=" + name, false);

                s = inireader.AppSettings("port");
                Console("port=" + s, false);
                if (!string.IsNullOrEmpty(s))
                    port = int.Parse(s);

                s = inireader.AppSettings("host");
                if (!string.IsNullOrEmpty(s))
                    host = s;
                Console("host=" + host, false);

                s = inireader.AppSettings("user");
                if (!string.IsNullOrEmpty(s))
                    user = s;
                s = inireader.AppSettings("pass");
                if (!string.IsNullOrEmpty(s))
                    pass = s;
                s = inireader.AppSettings("server");
                if (!string.IsNullOrEmpty(s))
                    server = s;

                //it can be more then 1 server

                SocketServer tn = new SocketServer(name, host, port);
                tn.sStart = "Host: ";
                tn.pc_hand = ProcessMy1;
                tn.bKeep = false;
                sockList.Add(tn);

                Thread workerThread = new Thread(tn.RunAsync);//tn.DoWork);
                // Start thread with a parameter  
                workerThread.Start();
                theList.Add(workerThread);

                //second server
                SocketServer tn2 = new SocketServer(name + 2, host, port + 1);
                tn2.sStart = "Host: ";
                tn2.pc_hand = ProcessMy2;
                tn2.bKeep = false;
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
                        System.Console.Write("*");
                    Thread.Sleep(1000);
                    step++;
                    if (System.Console.In.Peek() > 0)//System.Console.KeyAvailable)
                    {
                        ConsoleKeyInfo kk = System.Console.ReadKey(true);
                        if (kk.Key == System.ConsoleKey.Q)//.Enter)
                        {
                            Console("#", false);
                            foreach (var dd in myDic)
                            {
                                Console(dd.Key + "=" + dd.Value);
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
                                        workerThread.Interrupt(); //Abort();
                                    }
                                }
                            }
                            break;
                        }
                        else Console("Press 'q' to quit", false);
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
                foreach (var thr in lstThr)
                {
                    if (thr.IsAlive) thr.Interrupt(); //Abort();
                }
                SocketServer.Log("MathPanelCore finished", 3);
                Console("Press 'Enter'", false);
                System.Console.ReadLine();
            }
        }
    }
}
