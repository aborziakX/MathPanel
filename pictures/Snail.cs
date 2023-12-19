using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Text.RegularExpressions;

//Насыров Ирик; irik-ufa2013@yandex.ru
//2D визуализация (Улитка)

namespace DynamoCode
{  
 public class Script
 {
        public void Execute()
        {
            Dynamo.Console("Snail Garry!");
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            //opt.clr = "#fffa00";
            //opt.csk = "#000000";
            opt.sty = "line";
            //opt.lnw = "2";

            double rad = 0.5;
            //фон
            opt.clr = "#000000";
            string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, opt);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFE0 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            opt.clr = null;

            //задняя часть
            s = MathPanelExt.QuadroEqu.DrawSector(rad*4, rad*5, -2, -0.7, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFA07A \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*4, rad*5, -2, -0.7, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //на чем глаза
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(0.4,-2.7, 0.2, 2.5, 15, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B0E0E6 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(0.8,-2.7, 0.2, 2.5, -15, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B0E0E6 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(0.4,-2.7, 0.2, 2.5, 15, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(0.8,-2.7, 0.2, 2.5, -15, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //глаза
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 1.5, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#98FB98 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 0, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#98FB98 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 1.5, 0, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000  \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 0, 0, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000  \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.35, rad*0.35, 1.6, 0.1, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FF6347 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.35, rad*0.35, 0.1, 0.1, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FF6347 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.1, rad*0.1, 1.6, 0.1, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.1, rad*0.1, 0.1, 0.1, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //спираль
            opt.bFill = false;
            s = MathPanelExt.QuadroEqu.DrawSpiral(rad*0.7, rad*0.7, -1.8, -1, -Math.PI*0.6, Math.PI * 1.5, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #DC143C \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            opt.bFill = true;

            s = MathPanelExt.QuadroEqu.DrawSector(rad*5, rad, -1, -3, 0, Math.PI, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B0E0E6  \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-3.5, -3.2, 1.5, -3, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9ACD32 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.8, rad*0.4, -2, 1, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #4169E1 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.4, rad*0.2, -3, 0.7, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #4169E1 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.4, rad*0.2, -1, 0.4, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #4169E1 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.25, rad*0.15, -3.35, 0.2, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #4169E1 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.25, rad*0.15, -0.65, -0.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #4169E1 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //пятнышки
            int i;
            for (i = 0; i < 5; i++)
            {
                double x = -3.25+1.12*i;
                double y = -3.2;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.5, rad*0.3, x, y, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #9ACD32 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);
            }

            //ноги
            opt.clr = "#000000";
            opt.csk = "#000000";
            for (i = 0; i < 4; i++)
            {
                double x = -2.7+1.12*i;
                double y = -3.3;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad*0.5, rad*0.3, x, y, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFFFE0 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);
            }

        }
    }  
}