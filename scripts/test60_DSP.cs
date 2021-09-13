//test60_DCP
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
            Dynamo.Console("test60_DCP");
            //var dsp = new DSP();
            int n = 256, i;
            double[] xRe = new double[n];
            double[] xIm = new double[n];
            double[] foRe = new double[n];
            double[] foIm = new double[n];
            double[] xRe_2 = new double[n];
            double[] xIm_2 = new double[n];

            double dMin = double.MaxValue, dMax = double.MinValue;
            for (i = 0; i < n; i++)
            {
                var fr = (i * 2 * Math.PI) / n;
                xRe[i] = Math.Sin(fr * 1 + 0.5) + Math.Sin(fr * 3 + 0.1)
                    + Math.Sin(fr * 6 + 0.3) + Math.Sin(fr * 20 + 0.4);
                xIm[i] = 0;
                //Dynamo.Console(xRe[i].ToString());
                if (dMin > xRe[i]) dMin = xRe[i];
                if (dMax < xRe[i]) dMax = xRe[i];
            }

            var s1 = QuadroEqu.DrawGraphic(0, 2 * Math.PI, xRe, n);
            string s2 = string.Format(
                "{{\"options\":{{\"x0\": {0}, \"x1\": {1}, \"y0\": {2}, \"y1\": {3}, \"clr\": \"#ff0000\", \"sty\": \"line\", \"size\":2, \"lnw\": 1, \"wid\": 800, \"hei\": 600 }}",
                Dynamo.D2S(-0.001), Dynamo.D2S(2 * Math.PI + 0.001), Dynamo.D2S(dMin - 2*(dMax - dMin)), Dynamo.D2S(dMax)); 
            s2 += ", \"data\":[" + s1 + "]}";
            Dynamo.SceneJson(s2);


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

                var s3 = QuadroEqu.DrawGraphic(0, n/2-1, foRe, n/2);
                string s4 = string.Format(
                    "{{\"options\":{{\"x0\": {0}, \"x1\": {1}, \"y0\": {2}, \"y1\": {3}, \"clr\": \"#0000ff\", \"sty\": \"line\", \"size\":2, \"lnw\": 1, \"wid\": 800, \"hei\": 600, \"second\":1 }}",
                    -1, n/2+1, Dynamo.D2S(dMin- (dMax - dMin)), Dynamo.D2S(dMax+ (dMax - dMin)));
                s4 += ", \"data\":[" + s3 + "]}";
                Dynamo.SceneJson(s4, true);

                //мнимая часть - асимметрия, не нули, где есть частоты
                dMin = double.MaxValue;
                dMax = double.MinValue;
                for (i = 0; i < n; i++)
                {
                    if (dMin > foIm[i]) dMin = foIm[i];
                    if (dMax < foIm[i]) dMax = foIm[i];
                }
                Dynamo.Console("foIm min" + dMin + ", max " + dMax);

                var s5 = QuadroEqu.DrawGraphic(0, n/2-1, foIm, n/2);
                string s6 = string.Format(
                    "{{\"options\":{{\"x0\": {0}, \"x1\": {1}, \"y0\": {2}, \"y1\": {3}, \"clr\": \"#00ff00\", \"sty\": \"line\", \"size\":2, \"lnw\": 1, \"wid\": 800, \"hei\": 600, \"second\":1 }}",
                    -1, n/2+1, Dynamo.D2S(dMin), Dynamo.D2S(2*(dMax- dMin) + dMax));
                s6 += ", \"data\":[" + s5 + "]}";
                Dynamo.SceneJson(s6, true);

                rc = DSP.InvFaFT(n, xRe_2, xIm_2, foRe, foIm);
                Dynamo.Console("rc=" + rc);

                double d = 0;
                for (i = 0; i < n; i++)
                {
                    d += (xRe[i] - xRe_2[i]) * (xRe[i] - xRe_2[i]);
                    d += (xIm[i] - xIm_2[i]) * (xIm[i] - xIm_2[i]);
                    //Dynamo.Console((xRe[i] - xRe_2[i]) + ", " + (xIm[i] - xIm_2[i]));
                }
                Dynamo.Console("err=" + d);
            }


        }
    }
}
