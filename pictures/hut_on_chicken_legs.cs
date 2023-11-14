//Автор: Алина Жук, alya.griboed@gmail.com
//Избушка на курьих ножках

using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Text.RegularExpressions;

//test55_my_ip.cs

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{  
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("a hut on chicken legs ");
            double rad = 0.33;
            int i;
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.clr = "#fffa00";
            opt.csk = "#000000";
            opt.sty = "line";
            opt.lnw = "2";

            // каркас дома
            string s = MathPanelExt.QuadroEqu.DrawStar(4 , 3.23, 0, 0, 5, opt);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B8860B\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //12 окружностей
            for (i = 0; i < 6; i++)
            {
                double x = -3.35 + i*0.25;
                double y = 1-i*0.78;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, x, y, 0, Math.PI * 2, 64);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFF00\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }
            for (i = 0; i < 6; i++)
            {
                double x = 3.35 - i*0.25;
                double y = 1-i*0.78;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, x, y, 0, Math.PI * 2, 64);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFF00\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }

             //окна
             s = MathPanelExt.QuadroEqu.DrawRect(-1, -1, 1, 1, false);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDEAD\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             s = MathPanelExt.QuadroEqu.DrawRect(-1, -1, 1, 1, true);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#808000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             s = MathPanelExt.QuadroEqu.DrawRect(-0.5, 2, 0.5, 2.6, false);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDEAD\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             s = MathPanelExt.QuadroEqu.DrawRect(-0.5, 2, 0.5, 2.6, true);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#6C952D\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawLine(0,1.9,0,2.7);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDEAD\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawLine(0,-1,0,1);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDEAD\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawLine(-1,0,1,0);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDEAD\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
             
             //труба
             s = MathPanelExt.QuadroEqu.DrawRect(2, 2.5, 2.5, 4, false);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#584C29\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             s = MathPanelExt.QuadroEqu.DrawRect(2, 2.5, 2.5, 4, true);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8B0000\", \"sty\": \"line\", \"size\":0, \"lnw\": 4, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             s = MathPanelExt.QuadroEqu.DrawRect(1.8, 4, 2.7, 4.2, false);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#584C29\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             s = MathPanelExt.QuadroEqu.DrawRect(1.8, 4, 2.7, 4.2, true);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#370202\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

             //ноги
             s = MathPanelExt.QuadroEqu.DrawRect(-2.35, -3.3, 2.35, -3.5, true);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFA500\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawLine(-1,-6,-1,-3.5);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#800000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawLine(1,-6,1,-3.5);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#800000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //крыша
            //opt.Rotor(-35 * (Math.PI / 180.0));
             s = MathPanelExt.QuadroEqu.DrawRotatedRect(-0.3, 3.9, 5, 0.5, -35, true);
             s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A0522D\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedRect (-0.3, 3.9, 5, 0.5, -35);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8B4513\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedRect (-3.8, 1, 5, 0.5, 35, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A0522D\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedRect (-3.8, 1, 5, 0.5, 35);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8B4513\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //лапы
            for (i = 0; i < 3; i++)
            {
                double x = 1.8 + i*0.6;
                double y = -5.8 -1*Math.Pow(1 - Math.Pow(x, 2), 1/2);
                s = MathPanelExt.QuadroEqu.DrawLine(1,-6,x,y);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#800000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }
            for (i = 0; i < 3; i++)
            {
                double x = -1.8 - i*0.6;
                double y = -5.8 -1*Math.Pow(1 - Math.Pow(x, 2), 1/2);
                s = MathPanelExt.QuadroEqu.DrawLine(-1,-6,x,y);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#800000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }
        }
    }  
}