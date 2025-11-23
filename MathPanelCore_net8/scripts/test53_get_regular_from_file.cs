using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.IO;
using System.Collections.Generic;

//сравнить данные

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{
        //our function to calculate the difference between doubles (objects)
        double my_compare(object x, object y)
        {
            //Dynamo.Console("x " + x.ToString().Replace(",", "."));
            //Dynamo.Console("y " + y.ToString().Replace(",", "."));
            //create a double from a first object
            double x1 = double.Parse(x.ToString().Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            //create a double from a second object
            double y1 = double.Parse(y.ToString().Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            //find the absolute difference
            double dMax = (Math.Abs(x1) > Math.Abs(y1)) ? Math.Abs(x1) : Math.Abs(y1);
            if (dMax == 0) return 0.0;    //equal
            //find the relative difference
            var q = Math.Abs(x1 - y1) / dMax;
            //if the difference is pretty small, return zero, otherwise -1
            return q <= 0.01 ? 0.0 : -1.0;
        }
        public void Execute()
        {
            Dynamo.Console("Script started!");
            var lst = new List<string>();
            var lst_2 = new List<string>();
            string[] lines = File.ReadAllLines(@"C:\temp\Pyramid\energy1_0.txt");
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                var s = lines[i];
                int ipos = s.IndexOf("en=");
                if (ipos > 0)
                {
                    var s2 = s.Substring(ipos + 3);
                    //Dynamo.Console(s2);
                    //sb.AppendLine(s2);
                    lst.Add(s2);
                }
            }
            //File.WriteAllText(@"C:\temp\Pyramid\energy1_1.txt", sb.ToString(), System.Text.Encoding.UTF8);

            lines = File.ReadAllLines(@"C:\temp\Pyramid\energy2.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                var s = lines[i].Trim();
                if (s == "") continue;// s = "0";
                lst_2.Add(s);
            }

            //the first array of doubles
            object[] f0 = lst.ToArray(); //{ 1, 2, 3, 4, 7 };
            //the second array of doubles
            object[] f1 = lst_2.ToArray(); //{ 1.1, 2.01, 3.1, 4.01, 5.1, 6.1, 7 };

            //create an instance of Similarica class
            var solv = new MathPanelExt.Similarica();
            //get the score and weights, third parameter is our delegate
            double dScore = solv.Calc(f0, f1, my_compare);
            //display alignments
            var sRs = solv.PrintStrings("font-size:14pt;");
            Dynamo.SetHtml(sRs);
        }
    }  
}