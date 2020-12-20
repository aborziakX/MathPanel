//test39_bitmap_transform
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
            Dynamo.Console("test39_bitmap_transform");
            //Dynamo.Scriplet("test39_bitmap_transform", "Просто gradient");
            Dynamo.SceneClear();

            string sDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = sDir + "blue_red_drop2_800.png";
            Dynamo.SetBitmapImage(path);


            //first: красно-синее небо с небольшими вариациями
            int nNoise = 20;
            int iNoiceStrenth = 100;
            var dbl = System.Drawing.Color.Blue;// DarkBlue;
            var wht = System.Drawing.Color.White;

            var rnd = new Random();
            int alpha, red, green, blue;
            
            var bm = new BitmapSimple("blue_red_drop2_800.png");

            var id = Dynamo.PhobNew(-0, 0, -15);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //second: серое небо
            bm.Gray();
            bm.Save(@"gray_800.png");

            id = Dynamo.PhobNew(-0, 0, -5);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //third: черно-белый
            bm = new BitmapSimple("blue_red_drop2_800.png");
            bm.BlackWhite(33);
            bm.Save(@"black_white_800.png");

            id = Dynamo.PhobNew(-0, 0, 5);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;
            
            //fourth: небо со звездами
            bm = new BitmapSimple("blue_red_drop2_800.png");
            for (int i = 0; i < 400; i++)
            {
                int x = rnd.Next(800);
                int y = rnd.Next(600);
                int wi = 1;
                int he = 1;
                double d = 1;
                var cc = System.Drawing.Color.Yellow;
                bm.Drop(cc, x, y, wi, he, d);
            }
            //bm.Randomize(100000, 10);
            bm.Save(@"blue_red_stars_800.png");

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
