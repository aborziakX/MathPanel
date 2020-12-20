//test34_bitmap
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
            Dynamo.Console("test34_bitmap");
            //Dynamo.Scriplet("test34_bitmap", "Просто грань");
            Dynamo.SceneClear();

            System.Drawing.Color[] colors = { 
                System.Drawing.Color.Red,
                System.Drawing.Color.Orange,
                System.Drawing.Color.Yellow,
                System.Drawing.Color.Green,
                System.Drawing.Color.Blue,
                /*System.Drawing.Color.Magenta,
                System.Drawing.Color.Cyan,
                System.Drawing.Color.White,
                System.Drawing.Color.Green,*/
            };

            //var bm = new BitmapSimple(20, 20, colors);
            //var bm = new BitmapSimple(200, 200, System.Drawing.Color.White, System.Drawing.Color.Blue, false);
            //var bm = new BitmapSimple(@"arrow_red.png");
            var bm = new BitmapSimple(@"world1960.jpg");
            //var bm = new BitmapSimple(@"flag.jpg");

            //bm.Save(@"flag1_4.png", 4);
            //bm.Save(@"world1960.png", 1);
            bm.Save(@"world200.png", 10);

            int id = Dynamo.PhobNew(-0, 0, 0);
            var hz = Dynamo.PhobGet(id) as Phob;
            //var t1 = new Cube(10, "Yellow", 0, 10);
            //var t1 = new Sphere(20, "Yellow", 20, 0.0 * Math.PI, 0.5 * Math.PI, 0, Math.PI / 6);
            var t1 = new Sphere(20, "Yellow", 40);
            //t1.iFill = 3;
            t1.SetBitmap(bm);
            hz.Shape = t1;

            Dynamo.Console("total fac=" + Dynamo.SceneFacets());
            Dynamo.Console("total area=" + Dynamo.SceneFacetsArea());

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(true, false);

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                Dynamo.SceneDrawShape(true, false);
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
