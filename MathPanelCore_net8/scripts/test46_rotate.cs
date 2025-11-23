//test46_rotate
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
            Dynamo.Console("test46_rotate");
            //Dynamo.Scriplet("test46_rotate", "Красно-синее небо");
            string sDir = AppDomain.CurrentDomain.BaseDirectory;
            Random rnd = new Random();

            string[] fnames = { "pat1_rot.png", "pat2_rot.png", "pat3_rot.png", "pat4_rot.png", "pat5_rot.png",
                "pat6_rot.png", "pat7_rot.png", "pat8_rot.png", "pat9_rot.png", "pat10_rot.png",
                "pat11_rot.png", "pat12_rot.png"
            };
            System.Drawing.Color[] col_green = { System.Drawing.Color.Green, System.Drawing.Color.Black };
            byte[] patterns = new byte[8];
            for (int i = 0; i < fnames.Length; i++)
            {
                var fn = fnames[i];
                for (int j = 0; j < patterns.Length; j++) patterns[j] = (byte)rnd.Next();
                var bm = new BitmapSimple(sDir + "white_rects.png");
                DateTime dt1 = DateTime.Now;
                //bm.Rotate(30 * (i + 1));
                bm.RotateInMemory(30 * (i + 1));
                DateTime dt2_a = DateTime.Now;
                TimeSpan diff_a = dt2_a - dt1;
                int ms_a = (int)diff_a.TotalMilliseconds;
                Dynamo.Console("rot ms=" + ms_a);

                bm.Save(fn);
                Dynamo.SetBitmapImage(sDir + fn);
                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                int ms = (int)diff.TotalMilliseconds;
                Dynamo.Console("ms=" + ms);

                System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
            }
        }

    }
}
