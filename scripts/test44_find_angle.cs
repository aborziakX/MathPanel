//test44_find_angle
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
            Dynamo.Console("test44_find_angle");
            //Dynamo.Scriplet("test44_find_angle", "Красно-синее небо");
            string sDir = AppDomain.CurrentDomain.BaseDirectory;

            string[] fnames = { "white_lines_15deg.png", "white_lines_-15deg.png" };

            for (int i = 0; i < fnames.Length; i++)
            {
                var bm = new BitmapSimple(sDir + fnames[i]);
                DateTime dt1 = DateTime.Now;
                var ang = bm.Angle();
                Dynamo.Console("ang=" + ang);
                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                int ms = (int)diff.TotalMilliseconds;
                Dynamo.Console("ms=" + ms);

                System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
            }

        }

    }
}
