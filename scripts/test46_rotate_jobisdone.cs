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
            string sDir =@"c:\temp\";
            Random rnd = new Random();

            string[] fnames = { "jobisdone_rot1.jpg", "jobisdone_rot2.jpg", "jobisdone_rot3.jpg",  "jobisdone_rot4.jpg", "jobisdone_rot5.jpg"
            };
             byte[] patterns = new byte[8];
            for (int i = 0; i < fnames.Length; i++)
            {
                var fn = fnames[i];
                 var bm = new BitmapSimple(sDir + "jobisdone.jpg");
                DateTime dt1 = DateTime.Now;
                bm.Rotate(-1* (i + 1));
               
                bm.Save(sDir + fn);

                System.Threading.Thread.Sleep(50);
            }
        }

    }
}
