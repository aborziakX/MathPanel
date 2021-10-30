//test48_compare_text_files
using MathPanel;
using MathPanelExt;
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
            Dynamo.Console("test48_compare_text_files");
            //путь к папке с файлами 
            string sDir = @"C:\c_devel\data\";
            //файлы для сравнения
            string[] fnames = { "test.htm", "test2.htm" };

            //создать объект класса Similarica
            var solv = new Similarica();

            //чтение строк из 1-го файла
            var dat0 = System.IO.File.ReadAllLines(sDir + fnames[0], System.Text.Encoding.UTF8);
            //чтение строк из 2-го файла
            var dat1 = System.IO.File.ReadAllLines(sDir + fnames[1], System.Text.Encoding.UTF8);

            //найти веса и наилучший путь
            double dScore = solv.Calc(dat0, dat1);
            Dynamo.Console("Score=" + dScore);

            //генерировать цветное выравнивание в html
            var sRs = solv.PrintStrings("font-size:14pt;");
            Dynamo.SetHtml(sRs);
        }
    }
}
