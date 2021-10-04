//test37_bitmap5
using MathPanel;
using System.Net.Sockets;
using System;

///сборки
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test37_bitmap5");
            //путь к папке  
            string sDir = @"C:\c_devel\images\";

            string [] fns = { "test37_bitmap5_a.png", "test37_bitmap5_b.png", "test37_bitmap5_с.png", "test37_bitmap5_d.png", "test37_bitmap5_e.png" };
            int [] nNoise = { 100000, 100000, 100000, 100000, 100000 };
            int [] iNoiceStrenth = { 20, 40, 60, 80, 100 };

            //сгенерировать 5 иображений с увеличивающимся уровнем шума 
            for (int i = 0; i < fns.Length; i++)
            {
                var bm = new BitmapSimple(800, 600, System.Drawing.Color.Blue, System.Drawing.Color.Red, true);
                bm.Randomize(nNoise[i], iNoiceStrenth[i]);
                bm.Save(sDir + fns[i]);
            }
            //слайд-шоу
            for ( int i = 0; i < 100; i++ )
            {
                Dynamo.SetBitmapImage(sDir + fns[i % fns.Length]);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
