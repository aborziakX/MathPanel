//test41_filter_control
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Generic;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test41_filter_control");
            //Dynamo.Scriplet("test41_filter_control", "Красно-синее небо");
            int nStars = 40;
            string sDir = @"C:\c_devel\images\";//AppDomain.CurrentDomain.BaseDirectory;
            string path = sDir + "world1960.jpg";

            List<Tuple<int, int, double>> fil = new List<Tuple<int, int, double>>();
            var m00 = new Tuple<int, int, double>(-1, -1, 0.25);
            var m01 = new Tuple<int, int, double>(-1, 1, 0.25);
            var m10 = new Tuple<int, int, double>(1, -1, 0.25);
            var m11 = new Tuple<int, int, double>(1, 1, 0.25);
            fil.Add(m00);
            fil.Add(m01);
            fil.Add(m10);
            fil.Add(m11);

            for (int i = 0; i < 2; i++)
            {
                var bm = new BitmapSimple(path);
                DateTime dt1 = DateTime.Now;
                var fname = i == 0 ? "world_smooze1_5.png" : "world_filter.png";
                if( i == 0 ) bm.Smooth(1, 5);
                else
                {
                    bm.Filter(fil, 5);
                }
                bm.Save(fname);
                if( i > 0 ) Dynamo.SetBitmapImage(sDir + fname);

                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                int ms = (int)diff.TotalMilliseconds;
                Dynamo.Console("ms=" + ms);

                System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
            }

        }

    }
}
