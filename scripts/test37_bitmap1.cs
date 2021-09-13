//test37_bitmap1
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
            Dynamo.Console("test37_bitmap1");
            //путь к папке 
            string sDir = @"C:\c_devel\images\";

            //массив цветов 
            System.Drawing.Color[] colors = { 
                System.Drawing.Color.Red,
                System.Drawing.Color.Orange,
                System.Drawing.Color.Yellow,
                System.Drawing.Color.Green,
                System.Drawing.Color.Blue,
                System.Drawing.Color.Magenta,
                System.Drawing.Color.Cyan,
                System.Drawing.Color.White,
                System.Drawing.Color.Green,
            };
            
            //создать объект BitmapSimple
            var bm = new BitmapSimple(40, 40, colors);
            var fn = sDir +"test37_bitmap1.png";
            //сохранить в файл
            bm.Save(fn);
            //загрузить файл в компонент Image
            Dynamo.SetBitmapImage(fn);
        }
    }
}
