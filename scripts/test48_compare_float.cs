//test48_compare_float
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

///assemblies to use
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        //функция для вычисления разницы между объектами типа double
        double my_compare(object x, object y)
        {
            //Dynamo.Console("x " + x.ToString().Replace(",", "."));
            //Dynamo.Console("y " + y.ToString().Replace(",", "."));
            //создать double из первого объекта
            double x1 = double.Parse(x.ToString().Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            //создать double из второго объекта
            double y1 = double.Parse(y.ToString().Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            //найти абсолютную разницу
            double dMax = (Math.Abs(x1) > Math.Abs(y1)) ? Math.Abs(x1) : Math.Abs(y1);
            if (dMax == 0) return 0.0;    //equal
            //найти относительную разницу
            var q = Math.Abs(x1 - y1) / dMax;
            //если разница мала, вернуть 0, иначе -1
            return q <= 0.01 ? 0.0 : -1.0;
        }
        public void Execute()
        {
            Dynamo.Console("test48_compare_float");
            //первый массив из double
            object[] f0 = { 1, 2, 3, 4, 7 };
            //второй массив из double
            object[] f1 = { 1.1, 2.01, 3.1, 4.01, 5.1, 6.1, 7 };

            //создать объект класса Similarica
            var solv = new MathPanelExt.Similarica();
            //найти веса и наилучший путь, третий параметр наш delegate
            double dScore = solv.Calc(f0, f1, my_compare);
            //генерировать цветное выравнивание в html
            var sRs = solv.PrintStrings("font-size:14pt;");
            Dynamo.SetHtml(sRs);
        }
    }
}
