//test40_bitmap_stars_control
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
            Dynamo.Console("test40_bitmap_stars_control");
            Dynamo.Scriplet("test40_bitmap_stars", "Красно-синее небо");
            int nStars = 40;
            string sDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = sDir + "blue_red_stars_800.png";

            int[] x_coord = new int[nStars];
            int[] y_coord = new int[nStars];

            Random rnd = new Random();
            for (int i = 0; i < nStars; i++)
            {
                x_coord[i] = rnd.Next(800);
                y_coord[i] = rnd.Next(600);
            }

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                //fourth: небо со звездами
                var bm = new BitmapSimple("blue_red_drop2_800.png");
                for (int j = 0; j < nStars; j++)
                {
                    int wi = rnd.Next(1, 3);
                    int he = rnd.Next(1, 3);
                    double d = 0.75 * rnd.NextDouble();
                    var cc = System.Drawing.Color.Yellow;
                    bm.Drop(cc, x_coord[j], y_coord[j], wi, he, d);
                }
                //bm.Randomize(100000, 10);
                var fname = "blue_red_stars_800_" + (i % 10) + ".png";
                bm.Save(fname);
                Dynamo.SetBitmapImage(sDir + fname);
                if (i == 0)
                {
                    var q = Dynamo.SaveScripImage(sDir + fname, "");
                    Dynamo.Console("q=" + q);
                }

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
