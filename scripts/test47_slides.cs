//test47_slides
using MathPanel;
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
            Dynamo.Console("test47_slides");
            //путь к папке 
            string sDir = @"C:\c_devel\images\";
            //массив файлов-изображений
            string[] fnames = { "pat1_rot.png", "pat2_rot.png", "pat3_rot.png", "pat4_rot.png", "pat5_rot.png",
                "pat6_rot.png", "pat7_rot.png", "pat8_rot.png", "pat9_rot.png", "pat10_rot.png",
                "pat11_rot.png", "pat12_rot.png"
            };
            //по всем файлам
            for (int i = 0; i < fnames.Length; i++)
            {
                var fn = fnames[i];
                //загрузить файл в компонент Image
                Dynamo.SetBitmapImage(sDir + fn);
                //заснуть на 500 мсек
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
