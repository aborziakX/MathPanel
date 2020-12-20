//test21_springs - пружины
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Globalization;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        static int N = 10;
        static System.Drawing.Color[] clrs =
        {
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.Gainsboro,
            System.Drawing.Color.Red,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Orange,
            System.Drawing.Color.Green,
            System.Drawing.Color.Blue,
            System.Drawing.Color.Magenta,
            System.Drawing.Color.Cyan,
            System.Drawing.Color.White,
            System.Drawing.Color.Green,
        };
        static double dLink = 20;
        static double dKoefSpring = 0.01;
        static double dKoefSpeed = 0.9;
        static double DT = 0.5;

        static void DrawSprings(List<Tuple<int, int>> lstConnect)
        {
            var clrNormal = "#ff0000";
            System.Text.StringBuilder data = new System.Text.StringBuilder();
            double x1, y1, z1, x2, y2, z2, radius = 0.001;
            for (int i = 0; i < lstConnect.Count; i++)
            {
                Tuple<int, int> tup = lstConnect[i];
                int one = tup.Item1;
                int two = tup.Item2;
                var hz1 = Dynamo.PhobGet(one) as Phob;
                var hz2 = Dynamo.PhobGet(two) as Phob;

                x1 = hz1.x;
                y1 = hz1.y;
                z1 = hz1.z;

                x2 = hz2.x;
                y2 = hz2.y;
                z2 = hz2.z;

                Dynamo.Traslate2Camera(ref x1, ref y1, ref z1, ref radius, false);
                Dynamo.Traslate2Screen(ref x1, ref y1, ref z1, ref radius);

                Dynamo.Traslate2Camera(ref x2, ref y2, ref z2, ref radius, false);
                Dynamo.Traslate2Screen(ref x2, ref y2, ref z2, ref radius);

                if (data.Length != 0) data.Append(",");
                data.AppendFormat("{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\"}}",
                    x2.ToString(CultureInfo.InvariantCulture.NumberFormat), y2.ToString(CultureInfo.InvariantCulture.NumberFormat),
                    "line", radius.ToString(CultureInfo.InvariantCulture.NumberFormat),
                    clrNormal, "", 3);
                data.AppendFormat(",{{\"x\":{0}, \"y\":{1}, \"csk\":\"{4}\", \"rad\":\"{3}\", \"sty\":\"{2}\", \"txt\":\"{5}\", \"lnw\":\"{6}\"}}",
                    x1.ToString(CultureInfo.InvariantCulture.NumberFormat), y1.ToString(CultureInfo.InvariantCulture.NumberFormat),
                    "line_end", radius.ToString(CultureInfo.InvariantCulture.NumberFormat),
                    clrNormal, "", 3);
            }
            var s3 = data.ToString();
            string s4 = "{\"options\":{\"x0\": -20, \"x1\": 20, \"y0\": -20, \"y1\": 20, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":40, \"lnw\": 3, \"wid\": 800, \"hei\": 600 , \"second\":1}";
            s4 += ", \"data\":[" + s3 + "]}";
            Dynamo.SceneJson(s4, true);
            //Dynamo.Console("s4=" + s4);
        }
        public void Execute()
        {
            Dynamo.Console("Script started!");
            //Dynamo.Scriplet("test21_springs", "Шары на пружинах");

            Dynamo.SceneClear();
            //создаем сцену
            int SPEED = 1;
            var rnd = new Random();
            //generate PhOb's
            for (int i = 0; i < N; i++)
            {
                double x = rnd.NextDouble() * 40;
                double y = rnd.NextDouble() * 40;
                double z = rnd.NextDouble() * 40;
                int id = Dynamo.PhobNew(x, y, z);
                var hz0 = Dynamo.PhobGet(id) as Phob;
                int sz = rnd.Next(1, 5);
                hz0.radius = sz;
                hz0.mass = sz;
                hz0.AttrSet("clr", Facet3.ColorHtml(clrs[rnd.Next(0, clrs.Length - 1)]));
            }
            //generate connections
            int[] arr = Dynamo.SceneIds();
            List< Tuple<int, int> > lstConnect = new List< Tuple<int, int> > ();
            List<int> lstNum = new List<int>();
            HashSet<int> hs = new HashSet<int>();
            for (int i = 0; i < 2 * N; i++)
            {
                int one = rnd.Next(0, N - 1);
                int two = rnd.Next(0, N - 1);
                if (one == two) continue;
                int num = (one > two ? one * N + two : two * N + one);
                if (lstNum.Contains(num)) continue;
                Tuple<int, int> tup = new Tuple<int, int>(arr[one], arr[two]);
                lstConnect.Add(tup);
                lstNum.Add(num);
                hs.Add(one);
                hs.Add(two);
            }
            for (int i = 0; i < N; i++)
            {
                if (hs.Contains(i)) continue;
                if(i == N - 1)
                {
                    Tuple<int, int> tup = new Tuple<int, int>(arr[i], arr[0]);
                    lstConnect.Add(tup);
                }
                else
                {
                    Tuple<int, int> tup = new Tuple<int, int>(arr[i], arr[i + 1]);
                    lstConnect.Add(tup);
                }
            }

            Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 40);
            Dynamo.SceneDraw();
            DrawSprings(lstConnect);
            int iTotalRes = 0;

            Box bx = Dynamo.SceneBox;
            double dx, dy, dz;
            for (int i = 0; i < 4000; i++)
            {
                //calc new positions
                for (int j = 0; j < arr.Length; j++)
                {
                    var hz = Dynamo.PhobGet(arr[j]) as Phob;
                    //затухание!
                    hz.v_x *= dKoefSpeed;
                    hz.v_y *= dKoefSpeed;
                    hz.v_z *= dKoefSpeed;

                    hz.x += DT * hz.v_x;
                    hz.y += DT * hz.v_y;
                    hz.z += DT * hz.v_z;
                }

                //calc force                   
                for (int j = 0; j < lstConnect.Count; j++)
                {
                    Tuple<int, int> tup = lstConnect[j];
                    int one = tup.Item1;
                    int two = tup.Item2;
                    var hz1 = Dynamo.PhobGet(one) as Phob;
                    var hz2 = Dynamo.PhobGet(two) as Phob;

                    double d = hz1.Distance(hz2);
                    double dKoef = dKoefSpring;
                    //mass & distance
                    if( d < dLink )
                    {   //отталкивание
                        dKoef *= (dLink / d);
                        hz2.v_x += dKoef * (hz2.x - hz1.x) / hz2.mass;
                        hz2.v_y += dKoef * (hz2.y - hz1.y) / hz2.mass;
                        hz2.v_z += dKoef * (hz2.z - hz1.z) / hz2.mass;

                        hz1.v_x -= dKoef * (hz2.x - hz1.x) / hz1.mass;
                        hz1.v_y -= dKoef * (hz2.y - hz1.y) / hz1.mass;
                        hz1.v_z -= dKoef * (hz2.z - hz1.z) / hz1.mass;
                    }
                    else
                    {   //притяжение
                        dKoef *= (d / dLink);
                        hz2.v_x -= dKoef * (hz2.x - hz1.x) / hz2.mass;
                        hz2.v_y -= dKoef * (hz2.y - hz1.y) / hz2.mass;
                        hz2.v_z -= dKoef * (hz2.z - hz1.z) / hz2.mass;

                        hz1.v_x += dKoef * (hz2.x - hz1.x) / hz1.mass;
                        hz1.v_y += dKoef * (hz2.y - hz1.y) / hz1.mass;
                        hz1.v_z += dKoef * (hz2.z - hz1.z) / hz1.mass;
                    }
                }          

                Dynamo.SceneDraw();
                DrawSprings(lstConnect);
                if (i % 5 == 0 && iTotalRes < 100)
                {
                    //Dynamo.SaveScripresult();
                    iTotalRes++;
                }

                if (i % 40 == 0)
                {
                    double ix, iy, iz;
                    Dynamo.SceneImpulse(out ix, out iy, out iz);
                    Dynamo.Console(Dynamo.SceneEnergy().ToString() + ", ix=" + ix + ", iy=" + iy + ", iz=" + iz);
                }
                System.Threading.Thread.Sleep(50); //Мы ждем 1/20 секунду в даном потоке   
            }
        }
    }
}