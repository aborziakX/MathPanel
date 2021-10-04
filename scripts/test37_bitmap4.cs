//test37_bitmap4
using MathPanel;
//using MathPanelExt;
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
            Dynamo.Console("test37_bitmap4");
            //путь к папке  
            string sDir = @"C:\c_devel\images\";

            string [] fns = { "test37_bitmap4_a.png", "test37_bitmap4_b.jpg" };

            //сине-красное небо с 3-мя светло-серыми облаками
            var bm = new BitmapSimple(800, 600, System.Drawing.Color.Blue, System.Drawing.Color.Red, true);
            var cc = System.Drawing.Color.LightGray;
            bm.Drop(cc, 100, 100, 80, 80, 0.9, false);//резкое затухание капли
            bm.Drop(cc, 300, 100, 100, 50, 0.9, true);//линейное затухание капли
            bm.Drop(cc, 500, 150, 50, 100, 0.9, false);
            bm.Save(sDir + fns[0]);

            //темносине-красное небо с 3-мя серыми облаками
            var bm2 = new BitmapSimple(800, 600, System.Drawing.Color.DarkBlue, System.Drawing.Color.Red, true);
            cc = System.Drawing.Color.Gray;
            bm2.Drop(cc, 100, 100, 80, 80, 0.9, false);//резкое затухание капли
            bm2.Drop(cc, 300, 100, 100, 50, 0.9, true);//линейное затухание капли
            bm2.Drop(cc, 500, 150, 50, 100, 0.9, false);
            bm2.Save(sDir + fns[1]);

            for ( int i = 0; i < 100; i++ )
            {
                Dynamo.SetBitmapImage(sDir + fns[i % 2]);
                System.Threading.Thread.Sleep(1000);
            }
            /*
            Первый вариант сохраняем в формате png, второй в формате jpeg. 
            Видим, что 2-й файл получился в 3 раза меньше (jpeg использует сжатие с небольшой потерей качества, на глаз незаметно).
            */
        }
    }
}
