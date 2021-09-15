//test37_bitmap2
using MathPanel;
using MathPanelExt;
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
            Dynamo.Console("test37_bitmap2");
            //путь к папке 
            string sDir = @"C:\c_devel\images\";

            var fn = sDir + "test37_bitmap1.png";
            //создать объект BitmapSimple из файла 
            var bm = new BitmapSimple(fn);
            //нанести черный прямоугольник на него
            bm.Pixel(15, 15, 255, 0, 0, 0, 10, 10);
            var fn_2 = sDir + "test37_bitmap2.png";
            //сохранить
            bm.Save(fn_2);
            Dynamo.SetBitmapImage(fn_2);
        }
    }
}
