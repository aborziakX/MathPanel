//test38_bitmap_gradient
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test38_bitmap_gradient");
            //Dynamo.Scriplet("test38_bitmap_gradient", "Просто gradient");
            Dynamo.SceneClear();

            //first: синее небо с небольшими вариациями
            int nNoise = 20;
            int iNoiceStrenth = 100;
            var dbl = System.Drawing.Color.Blue;// DarkBlue;
            var wht = System.Drawing.Color.LightGray;//White

            var rnd = new Random();
            int alpha, red, green, blue;
            Tuple<System.Drawing.Color, int, int>[] focus = new Tuple<System.Drawing.Color, int, int>[nNoise];
            for (int i = 0; i < focus.Length; i++)
            {
                int x = rnd.Next(800);
                int y = rnd.Next(600);

                alpha = dbl.A;

                red = dbl.R + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2;
                if (red < 0) red = 0;
                else if (red > 255) red = 255;

                green = dbl.G + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2;
                if (green < 0) green = 0;
                else if (green > 255) green = 255;

                blue = dbl.B + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2;
                if (blue < 0) blue = 0;
                else if (blue > 255) blue = 255;

                var clr = System.Drawing.Color.FromArgb(alpha, red, green, blue);

                focus[i] = new Tuple<System.Drawing.Color, int, int>(clr, x, y);
            }
            var bm = new BitmapSimple(800, 600, focus);
            bm.Randomize(100000, 10);
            bm.Save(@"dark_blue_rand_800.png");

            var id = Dynamo.PhobNew(-0, 0, -15);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //second: синее-белое небо
            nNoise = 40;
            iNoiceStrenth = 30;
            Tuple<System.Drawing.Color, int, int>[] focus2 = new Tuple<System.Drawing.Color, int, int>[nNoise];
            for (int i = 0; i < focus2.Length; i++)
            {
                int x = rnd.Next(800);
                int y = rnd.Next(600);
                int chy = rnd.Next(4);

                alpha = dbl.A;

                red = (chy != 0 ? dbl.R : wht.R) + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2;
                if (red < 0) red = 0;
                else if (red > 255) red = 255;

                green = (chy != 0 ? dbl.G : wht.G) + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2;
                if (green < 0) green = 0;
                else if (green > 255) green = 255;

                blue = (chy != 0 ? dbl.B : wht.B) + rnd.Next(0, iNoiceStrenth) - iNoiceStrenth / 2;
                if (blue < 0) blue = 0;
                else if (blue > 255) blue = 255;

                var clr = System.Drawing.Color.FromArgb(alpha, red, green, blue);

                focus2[i] = new Tuple<System.Drawing.Color, int, int>(clr, x, y);
            }
            bm = new BitmapSimple(800, 600, focus2);
            bm.Randomize(100000, 10);
            bm.Save(@"white_blue_rand_800.png");

            id = Dynamo.PhobNew(-0, 0, -5);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //third: небо с серыми каплями
            bm = new BitmapSimple(800, 600, System.Drawing.Color.White, System.Drawing.Color.Blue, true);
            for (int i = 0; i < 40; i++)
            {
                int x = rnd.Next(800);
                int y = rnd.Next(600);
                int wi = rnd.Next(50, 200);
                int he = rnd.Next(50, 150);
                double d = rnd.NextDouble() * 0.75;
                var cc = he > wi ? System.Drawing.Color.LightGray : System.Drawing.Color.Gray;
                bm.Drop(cc, x, y, wi, d);
            }
            bm.Randomize(100000, 10);
            bm.Save(@"white_blue_drop_800.png");

            id = Dynamo.PhobNew(-0, 0, 5);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //fourth: небо с серыми каплями 2
            bm = new BitmapSimple(800, 600, System.Drawing.Color.Blue, System.Drawing.Color.Red, true);
            for (int i = 0; i < 40; i++)
            {
                int x = rnd.Next(800);
                int y = rnd.Next(600);
                int wi = rnd.Next(50, 200);
                int he = rnd.Next(50, 150);
                if (1.0 * wi / he > 2) wi = he * 2;
                if (1.0 * he / wi > 2) he = wi * 2;
                double d = rnd.NextDouble() * 0.75;
                var cc = he > wi ? System.Drawing.Color.LightGray : System.Drawing.Color.Gray;
                bm.Drop(cc, x, y, wi, he, d, false);
            }
            bm.Randomize(100000, 10);
            bm.Save(@"blue_red_drop2_800.png");

            id = Dynamo.PhobNew(-0, 0, 15);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            Dynamo.Console("total fac=" + Dynamo.SceneFacets());
            Dynamo.Console("total area=" + Dynamo.SceneFacetsArea());

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(false, false);

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                Dynamo.SceneDrawShape(false, false);
                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                int ms = (int)diff.TotalMilliseconds;
                if (i % 40 == 0)
                {
                    Dynamo.Console("ms=" + ms);
                }
                if (i == 0 || Dynamo.KeyConsole == "S")
                {
                    //Dynamo.SaveScripresult();
                }
                System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
            }
        }

    }
}
