//Арина Ковалёва; kovaleva1580@mail.ru
//Kandinsky drawing

            Dynamo.Console("Kandinsky!");
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.sty = "line";

            double rad = 0.5;
            string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 0, 10, true);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FF8C00 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedRect(-1,-4, 1.5, 8.5, 15, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#008080 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawSector(rad*4, rad*4, 1, 0.7, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #8B0000\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad*2, rad*2, -2, 3, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedRect(4.8,-2.7, 0.8, 7.5, 0,true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FF4500 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(-3,-2.7, 0, 2.5, 30, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(-3,-3, 0, 2.5, 30, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(-3,-3.3, 0, 2.5, -15, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
          s = MathPanelExt.QuadroEqu.DrawRotatedRect(-4,-2.5, 0, 2.5, -15, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(3.8,2.7, 0, 2.5, -90, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFA07A \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(3.8,3, 0, 2.5, -90, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFA07A \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(4,1, 0, 2.5, 0, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFA07A \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(5,1, 0, 2.5, 0, false);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFA07A \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 1.5, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFD700 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 3, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#98FB98 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawSector(rad*3.5, rad*3.5, 0, 5, 0, Math.PI , 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FF6347 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawSector(rad*2, rad*2, -4, 2, -Math.PI*0.5, Math.PI*0.3 , 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#BC8F8F\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

