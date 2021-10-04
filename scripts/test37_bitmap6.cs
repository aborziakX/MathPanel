//test37_bitmap6
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
            Dynamo.Console("test37_bitmap6");
            //путь к папке  
            string sDir = @"C:\c_devel\images\";

            string [] fns = {"world1960.jpg", "test37_bitmap6_a.png", "test37_bitmap6_b.png", "test37_bitmap6_с.png" };
            //Здесь берем исходное изображение и с помощью новых методов создаем 3 модификации.
            for (int i = 1; i < fns.Length; i++)
            {
                var bm = new BitmapSimple(sDir + fns[0]);
                if (i == 1)
                    bm.Gray();
                else if (i == 2)
                    bm.BlackWhite();
                else if (i == 3)
                    bm.Smooth(1, 10);
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
