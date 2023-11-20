//test27_cone_pyramid
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Specialized;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test29_covid");
            //Dynamo.Scriplet("test29_covid", "Страшная ковидла 19");
            Dynamo.SceneClear();

            double radius = 10;

            int id0 = Dynamo.PhobNew(-0, 0, 0);
            var hz0 = Dynamo.PhobGet(id0) as Phob;
            var t0 = new Sphere(2 * radius, "Magenta", 32);
            //t0.Fractal(1, 0.5);
            t0.DivideFractal(1);

            //t0.bDrawNorm = true;
            hz0.Shape = t0;
            double[] radv = { 1.0, 0.9, 0.85, 0.82, 0.85, 0.9, 0.95 };
            /*
            int id1 = Dynamo.PhobNew(-0, 0, 11);
            var hz1 = Dynamo.PhobGet(id1) as Phob;
            var t1 = new RadiusVar(2, radv, "Yellow", 12);
            //t1.Fractal(1);
            hz1.Shape = t1;

            int id2 = Dynamo.PhobNew(-11, 0, 0);
            var hz2 = Dynamo.PhobGet(id2) as Phob;
            var t2 = new RadiusVar(2, radv, "Yellow", 12);
            //t2.Fractal(1);
            t2.YRotor = Math.PI * 0.5;
            hz2.Shape = t2;*/

            Random rnd = new Random();
            double x, y, z, d, r0, heightMin = 4.0, rad, h;
            int n = 0;
            var vX = new Vec3(1, 0, 0);
            var vY = new Vec3(0, 1, 0);
            var vZ = new Vec3(0, 0, 1);
            double teta = Math.PI / 6; //30 degrees
            double fi;
            for (int i = 0; i < 150; i++)
            {
                h = heightMin + 4 * rnd.NextDouble();
                rad = radius + h / 2;
                x = rnd.NextDouble() * 2 * rad - rad;
                d = Math.Sqrt(rad * rad - x * x);
                y = rnd.NextDouble() * d;
                if (rnd.NextDouble() < 0.5) y = -y;
                z = Math.Sqrt(rad * rad - x * x - y * y);
                if (rnd.NextDouble() < 0.5) z = -z;
                if (Dynamo.SceneMinDistance(x, y, z) < 5) continue;
                int id3 = Dynamo.PhobNew(x, y, z);
                var hz3 = Dynamo.PhobGet(id3) as Phob;
                var t3 = //new Cube(4, "Yellow");
                    new RadiusVar(h, radv, "Yellow", 8, 1);
                int c1 = 128 + rnd.Next(127);
                int c2 = 128 + rnd.Next(127);
                var cc = System.Drawing.Color.FromArgb(c1 > c2 ? c1 : c2, c1 > c2 ? c2 : c1, 0 + rnd.Next(127));
                t3.SetColor(cc);
                //t3.Fractal(1, 0.1);
                t3.DivideFractal(1);
                /*t3.ZRotor = Math.Atan2(y, x);
                if (Math.Abs(x) > Math.Abs(y))
                    t3.YRotor = x > 0 ? Math.Acos(z / rad) : -Math.Acos(z / rad);
                else t3.XRotor = y > 0 ? -Math.Acos(z / rad) : Math.Acos(z / rad);*/

                if (Math.Abs(z) < rad * 0.95)
                {   //not a pole
                    fi = Math.Asin(z / rad);
                    double cos2 = Math.Cos(fi) * Math.Cos(fi);
                    vZ.Copy(x, y, z);   //normal
                    if (Math.Abs(z) > rad * 0.05)
                        vY.Copy(x * ( 1 / cos2 - 1), y * (1 / cos2 - 1), -z);
                    else vY.Copy(0, 0, 1);
                    Vec3.Product(vY, vZ, ref vX);
                    vX.Normalize();
                    vY.Normalize();
                    vZ.Normalize();
                    t3.RotateVec(vX, vY, vZ);

                    double sc = vZ.ScalarProduct(vY);
                    Vec3.Product(vY, vZ, ref vX);
                    //Dynamo.Console("sc=" + sc + ",len=" + vX.Length());
                }

                hz3.Shape = t3;
                n++;
            }
            Dynamo.Console("total RadiusVar=" + n);
            Dynamo.Console("total fac=" + Dynamo.SceneFacets());
            /*//test
            for (int i = 0; i <= 6; i++)
            {
                fi = Math.PI / 12 * i;
                z = rad * Math.Sin(fi);
                r0 = rad * Math.Cos(fi);
                x = r0 * Math.Cos(teta);
                y = r0 * Math.Sin(teta);
                int id3 = Dynamo.PhobNew(x, y, z);
                var hz3 = Dynamo.PhobGet(id3) as Phob;
                var t3 = new Cube(2, "Yellow");
                t3.scaleZ = 2;
                t3.scaleX = 0.5;

                if (i < 6)
                {
                    double cos2 = Math.Cos(fi) * Math.Cos(fi);
                    vZ.Copy(x, y, z);   //normal
                    vY.Copy(x / cos2, y / cos2, 0);
                    Vec3.Product(vY, vZ, ref vX);
                    vX.Normalize();
                    vY.Normalize();
                    vZ.Normalize();
                    t3.RotateVec(vX, vY, vZ);
                }
                hz3.Shape = t3;
            }*/

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.BAxes = false;
            Dynamo.BDrawBox = false;
            Dynamo.SceneDrawShape(true, false);
            Dynamo.Console("js len=" + Dynamo.ScreenJson.Length);

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                Dynamo.SceneDrawShape(true, false);
                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                int ms = (int)diff.TotalMilliseconds;
                if (i % 10 == 0)
                {
                    Dynamo.Console("ms=" + ms);
                }
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }
                System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
            }
        }
    }
}
