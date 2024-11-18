//Холодов Денис Кириллович denis-xolodov-h2@mail.ru

using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
	public class Script
	{  
		public void Execute()
        {
            Dynamo.Console("Snail Garry!");
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.sty = "line";

            double rad = 0.5;
            //фон
            opt.clr = "#ffffff";
            string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, opt);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ff0000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            opt.clr = null;

            //первая дуга
            s = MathPanelExt.QuadroEqu.DrawSector(rad*10, rad*10, 0, 0, 0, Math.PI * 5, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #124185 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawSector(rad*9, rad*9, 0.8, 0, 0, Math.PI * 5, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #ffffff \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //вторая дуга
            s = MathPanelExt.QuadroEqu.DrawSector(rad*10*8/9.5, rad*10*8/9.5, 0.8, 0, 0, Math.PI * 5, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #124185 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawSector(rad*7.8, rad*7.8, 0.2, 0, 0, Math.PI * 5, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #ffffff \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //лучи
            int i;
            for (i = 0; i < 12; i++)
            {
                double phi = 2*Math.PI*i/12;
                double amp = 2;

                string Snowflake1 = MathPanelExt.QuadroEqu.DrawLine(0, 0, amp*Math.Sin(phi), amp*Math.Cos(phi));
                string SnowflakeParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e62f33\", \"sty\": \"line\", \"size\":10, \"lnw\": 10, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                string SnowflakeData1 = SnowflakeParam1 + ", \"data\":[" + Snowflake1 + "]}";
                Dynamo.SceneJson(SnowflakeData1, true);

                s = MathPanelExt.QuadroEqu.DrawSector(rad*0.19, rad*0.19, amp*Math.Sin(phi), amp*Math.Cos(phi), 0, Math.PI * 2, 512, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #e62f33 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                phi = 2*Math.PI*i/12 + 2*Math.PI/24;
                amp = 3;

                Snowflake1 = MathPanelExt.QuadroEqu.DrawLine(0, 0, amp*Math.Sin(phi), amp*Math.Cos(phi));
                SnowflakeParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e62f33\", \"sty\": \"line\", \"size\":10, \"lnw\": 10, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                SnowflakeData1 = SnowflakeParam1 + ", \"data\":[" + Snowflake1 + "]}";
                Dynamo.SceneJson(SnowflakeData1, true);

                s = MathPanelExt.QuadroEqu.DrawSector(rad*0.19, rad*0.19, amp*Math.Sin(phi), amp*Math.Cos(phi), 0, Math.PI * 2, 512, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #e62f33 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }

            //длинный луч
            double phi1 = 2*Math.PI*2/12;
            double amp1 = 7;
            double amp2 = 4;
            double amp3 = 2;

            string Snowflake = MathPanelExt.QuadroEqu.DrawLine(0, 0, amp1*Math.Sin(phi1), amp1*Math.Cos(phi1));
            string SnowflakeParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e62f33\", \"sty\": \"line\", \"size\":10, \"lnw\": 10, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            string SnowflakeData = SnowflakeParam + ", \"data\":[" + Snowflake + "]}";
            Dynamo.SceneJson(SnowflakeData, true);

            s = MathPanelExt.QuadroEqu.DrawSector(rad*0.17, rad*0.17, amp1*Math.Sin(phi1), amp1*Math.Cos(phi1), 0, Math.PI * 5, 32, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #e62f33 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            phi1 = 2*Math.PI*8/12;
            amp1 = 7;
            amp2 = 5;
            amp3 = 3.84;

            Snowflake = MathPanelExt.QuadroEqu.DrawLine(0, 0, amp3*Math.Sin(phi1), amp3*Math.Cos(phi1));
            SnowflakeParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e62f33\", \"sty\": \"line\", \"size\":10, \"lnw\": 10, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            SnowflakeData = SnowflakeParam + ", \"data\":[" + Snowflake + "]}";
            Dynamo.SceneJson(SnowflakeData, true);

            Snowflake = MathPanelExt.QuadroEqu.DrawLine(amp2*Math.Sin(phi1), amp2*Math.Cos(phi1), amp1*Math.Sin(phi1), amp1*Math.Cos(phi1));
            SnowflakeParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e62f33\", \"sty\": \"line\", \"size\":10, \"lnw\": 10, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            SnowflakeData = SnowflakeParam + ", \"data\":[" + Snowflake + "]}";
            Dynamo.SceneJson(SnowflakeData, true);

            s = MathPanelExt.QuadroEqu.DrawSector(rad*0.17, rad*0.17, amp1*Math.Sin(phi1), amp1*Math.Cos(phi1), 0, Math.PI * 5, 32, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #e62f33 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
        } 
    }  
}