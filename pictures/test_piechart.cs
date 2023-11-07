using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Text.RegularExpressions;

//test_piechart.cs
//Автор: "Ильяс Фахурдинов" <mv1451003@gmail.com>

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{
        public void Execute()
        {
            Dynamo.Console("test_piechart started!");
            double rad = 4;
            int i;
            string s, s10;
            // белый фон
            s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            DrawOpt opt = new DrawOpt();
            opt.bFill = true;

            double[] value = {1.0, 2.0, 3.0, 4.0};
            string[] name = {"a", "b", "c", "d"};
            // центр диаграммы, угол отсчета
            double x0 = 0, y0 = 0, phi = Math.PI*0.5, phi0;
            Array.Sort(value, name);
            Array.Reverse(value);
            Array.Reverse(name);
            //вычислить сумму
            double sum = 0.0;
            for ( i = 0; i < value.Length; i++)
            {
                sum += value[i];
            }

            for ( i = 0; i < value.Length; i++)
            {
                phi0 = 2 * Math.PI * value[i] / sum - phi;
                //string color = ((int)(16777215 * (phi0 + 0.5 * Math.PI) / (2 * Math.PI))).ToString("x6");
                string color = ((int)(255 * (value[i]) / (sum))).ToString("x2");
                string rcolor = ((int)(255 * (sum-value[i]) / (sum))).ToString("x2");
                if (i % 3 == 0) color = rcolor+color+color;
                if (i % 3 == 1) color = color+rcolor+color;
                if (i % 3 == 2) color = color+color+rcolor;
                opt.clr = "#" + color;
                opt.sty = "line";

                s = MathPanelExt.QuadroEqu.DrawSector(rad, rad, 0, 0, -phi0, phi, 16, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#" + color + "\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                phi = -phi0;
                s = MathPanelExt.QuadroEqu.DrawRect(x0+rad+1, y0+rad-i-1, x0+rad+1+1, y0+rad-i-0.5, true);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#" + color + "\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawRect(x0+rad+1, y0+rad-i-1, x0+rad+1+1, y0+rad-i-0.5);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawText(x0+rad+2.5, y0+rad-i-1.5, name[i]);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }
            
            /*for ( i = 0; i < value.Length; i++)
            {
                s = MathPanelExt.QuadroEqu.DrawLine(x0, y0, rad*Math.Cos(phi), rad*Math.Sin(phi));
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                phi -= 2 * Math.PI * value[i] / sum;
            }*/
            
            /*s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 0, 0, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);*/
        }
    }  
}