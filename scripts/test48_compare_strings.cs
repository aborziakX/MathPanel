//test48_compare_strings
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
            Dynamo.Console("test48_compare_strings");
            //строки для сравнения
            string[] fnames = { "optimal construction", "optimization const" };
            //string[] fnames = { "ANDREI", "ALEXEI" };

            //создать объект класса Similarica
            var solv = new Similarica();
            //найти веса и наилучший путь
            double dScore = solv.Calc(fnames[0], fnames[1]);
            Dynamo.Console("Score=" + dScore);
            //генерировать html-таблицу с весами и наилучшим путем
            var sWg = solv.Printweights("font-size:14pt;");
            //генерировать цветное выравнивание в html
            var sRs = solv.PrintStrings("font-size:14pt;");

            for (int i = 0; i < 10; i++)
            {
                //вывести результат
                Dynamo.SetHtml(i % 2 == 0 ? sWg : sRs);
                //спать
                System.Threading.Thread.Sleep(2000);
            }
            
        }
    }
}
