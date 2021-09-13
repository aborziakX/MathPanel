//test61_DCP_picture
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

//найти кратчайший путь из Москвы в Гавану

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test61_DCP_picture");
            string sDir = @"C:\c_devel\Nikitaev\64\Learn\";

            //create our BitmapSimple object
            var bm = new BitmapSimple(sDir + "Bla_6_grayadap.png");

            int n = bm.width * bm.height, i;
            //Dynamo.Console("n=" + n);
            //return;
            double[] xRe = new double[n];
            double[] xIm = new double[n];
            double[] foRe = new double[n];
            double[] foIm = new double[n];
            double[] xRe_2 = new double[n];
            double[] xIm_2 = new double[n];

            double dMin = double.MaxValue, dMax = double.MinValue;
            for (i = 0; i < n; i++)
            {
                xRe[i] = bm.map[i] & 0xFF;//gray
                xIm[i] = 0;
                if (dMin > xRe[i]) dMin = xRe[i];
                if (dMax < xRe[i]) dMax = xRe[i];
            }
            Dynamo.Console("xRe min" + dMin + ", max " + dMax);
            //like xRe min109, max 255

            int rc = DSP.FaFT(n, xRe, xIm, foRe, foIm);
            Dynamo.Console("rc=" + rc);
            if (rc >= 0)
            {
                //действительная часть, симметрия, не нули, где есть фазы
                dMin = double.MaxValue;
                dMax = double.MinValue;
                for (i = 0; i < n; i++)
                {
                    if (dMin > foRe[i]) dMin = foRe[i];
                    if (dMax < foRe[i]) dMax = foRe[i];
                }
                Dynamo.Console("foRe min" + dMin + ", max " + dMax);
                //like foRe min-27560,400033065, max 688521

                //мнимая часть - асимметрия, не нули, где есть частоты
                dMin = double.MaxValue;
                dMax = double.MinValue;
                for (i = 0; i < n; i++)
                {
                    if (i < n / 2)
                    {
                        Dynamo.Console(i + "," + foIm[i]);
                        if (dMin > foIm[i]) dMin = foIm[i];
                        if (dMax < foIm[i]) dMax = foIm[i];
                    }
                }
                Dynamo.Console("foIm min" + dMin + ", max " + dMax);
                //like foIm min-32422,9112343641, max 32422,9112748386

                //make a new pict
                int w = bm.width, h = bm.height / 2, gray;
                System.Drawing.Color[] colors = { System.Drawing.Color.Black };

                bm = new BitmapSimple(w, h, colors);
                for (i = 0; i < n / 2; i++)
                {
                    gray = (int)Math.Abs((foIm[i] / dMin) * 255);
                    if (gray > 255) gray = 255;
                    var cc2 = System.Drawing.Color.FromArgb(255, gray, gray, gray).ToArgb();
                    bm.map[i] = cc2;
                }

                var fn = @"C:\c_devel\images\freq.png";
                bm.Save(fn);
            }
        }
    }
}
