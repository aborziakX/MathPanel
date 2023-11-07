using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Text.RegularExpressions;

//test_piechart2.cs

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{
        public void Execute()
        {
            Dynamo.ConsoleTextClear();
            Dynamo.Console("test_piechart started!");
            int i;
            string s, s10;
            // серый фон
            s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ddd\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 600, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            double rad = 4;
            double[] aData = { 1, 2, 3, 3.5, 2.5 };
            string[] aClr = { "#f00", "#fa0", "#ff0", "#0f0", "#00f" }; 
            string[] aText = { "a", "bb", "ccc", "dddd", "eeeee" };

            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.clr = "#fffa00";
            opt.csk = "#000000";
            opt.sty = "line";
            opt.lnw = "2";
            //s = MathPanelExt.QuadroEqu.DrawSector(rad, rad, 0, 0, 0, 1.1, 8, opt);
            s = MathPanelExt.QuadroEqu.DrawPie(rad, rad, 0, 0, aData, aClr, aText, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + opt.clr + "\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 600, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true, true);                
        }
    }  
}