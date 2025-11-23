//Андрей Абрамов; abandrey2002@gmail.com

            Dynamo.Console("Notebook!");
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.sty = "line";
            double rad = 0.5;

            string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, true);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #F5DEB3\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
           
            s = MathPanelExt.QuadroEqu.DrawRect(-3.5, -3.2, 3, 1, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#6495ED \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-3.5, -3.2, 3, 1);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-3, -2.7, 2.5, 0.5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #F0E68C \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-3, -2.7, 2.5, 0.5);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3.5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-5,-5,-3.5,-3.2, 3, -3.2, 4.2, -5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #B0C4DE \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            opt.bFill = false;
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-5,-5,-3.5,-3.2, 3, -3.2, 4.2, -5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"  #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            opt.bFill = true;

            s = MathPanelExt.QuadroEqu.DrawRect(-5, -5.5, 4.2, -5, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #7B68EE \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-5, -5.5, 4.2, -5);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-3.7,-4.5,-2.8,-3.5, 2.2, -3.5, 3, -4.5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"  #9ACD32\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            opt.bFill = false;
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-3.7,-4.5,-2.8,-3.5, 2.2, -3.5, 3, -4.5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"  #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            opt.bFill = true;

            int i;
            for (i = 0; i < 6; i++)
            {
                double x = -3.2+1.1*i;
                double y = -2.4+0.8*i;
                s = MathPanelExt.QuadroEqu.DrawLine(x, -4.5, y, -3.5);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);
            }
                s = MathPanelExt.QuadroEqu.DrawLine(-3.3, -4, 2.6, -4);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawLine(-3.5, -4.3, 2.8, -4.3);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawLine(-3, -3.7, 2.35, -3.7);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawEllipse(0.1,0.1, -0.1, 0.7, 0, Math.PI * 2, 64);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
               Dynamo.SceneJson(s10, true);


