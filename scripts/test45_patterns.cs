//test45_patterns
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
            Dynamo.Console("test45_patterns");
            //Dynamo.Scriplet("test45_patterns", "Красно-синее небо");
            string sDir = AppDomain.CurrentDomain.BaseDirectory;
            Random rnd = new Random();

            string[] fnames = { "pat1.png", "pat2.png", "pat3.png", "pat4.png", "pat5.png" };
            System.Drawing.Color[] col_green = { System.Drawing.Color.Green, System.Drawing.Color.Black };
            byte[] patterns = new byte[8];
            for (int m = 0; m < 50; m++) { 
                for (int i = 0; i < fnames.Length; i++)
                {
                    var fn = fnames[i];
                    for (int j = 0; j < patterns.Length; j++) patterns[j] = (byte)rnd.Next();
                    var bm = new BitmapSimple(800, 600, col_green, patterns);
                    DateTime dt1 = DateTime.Now;
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
}
