//test43_pixels
/* Создаем битмап зеленого цвета, рисуем на ней красный квадрат, сохраняем. 
  Создаем битмап черного цвета, в первой половине устанавливаем alpha=200 (почти непрозрачно), 
  во второй половине – 100 (полупрозрачно). Накладываем 2-ую битмап на первую, сохраняем. 
*/
using MathPanel;
using System.Net.Sockets;
using System;
using System.Collections.Generic;

///сборки
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test43_pixels");
            //путь к папке  
            string sDir = @"C:\c_devel\images\";

            string[] fnames = { "red" };
            System.Drawing.Color[] col_green = { System.Drawing.Color.Green };
            System.Drawing.Color[] col_black = { System.Drawing.Color.Black };

            for (int i = 0; i < fnames.Length; i++)
            {
                var fn = fnames[i];
                //Создаем битмап зеленого цвета
                var bm = new BitmapSimple(800, 600, col_green);
                //рисуем на ней красный квадрат
                DateTime dt1 = DateTime.Now;
                bm.Pixel(300, 200, 255, 255, 0, 0, 200, 200);
                var fn_2 = sDir + fn + "_pix.png";
                //сохраняем
                bm.Save(fn_2);
                Dynamo.SetBitmapImage(fn_2);
                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                int ms = (int)diff.TotalMilliseconds;
                //display time span
                Dynamo.Console("ms=" + ms);

                //Создаем битмап черного цвета
                var bm2 = new BitmapSimple(800, 600, col_black);
                //в первой половине устанавливаем alpha=200 (почти непрозрачно), 
                
                bm2.Alpha(0, 0, 200, 400, 600);
                //во второй половине – 100(полупрозрачно)
                bm2.Alpha(400, 0, 100, 400, 600);
                //Накладываем 2-ую битмап на первую
                bm.Put(bm2);
                //сохраняем
                fn_2 = sDir + fn + "_pix_alfa.png";
                bm.Save(fn_2);

                System.Threading.Thread.Sleep(2000);
                Dynamo.SetBitmapImage(fn_2);
            }
        }
    }
}
