//Сураева Анастасия, nestasia.fire@gmail.com

            Dynamo.Console("it_is_track_car!");
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            //opt.clr = "#fffa00";
            //opt.csk = "#000000";
            opt.sty = "line";
            //opt.lnw = "2";

            //колеса

            string s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -3.4, -3.1, 0, Math.PI * 2, 64, opt);
             string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#614051\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
             s10 += ", \"data\":[" + s + "]}";
             Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -0.5, -3.1, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#614051\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 6.5, -3.1, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#614051\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(0.8, 0.8, -0.5, -3.1, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#293133\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawEllipse(0.8, 0.8, 6.5, -3.1, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#293133\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawEllipse(0.8, 0.8, -3.4, -3.1, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#293133\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(4, 4, -1.6, 1.8, Math.PI*0.2, Math.PI*0.8 , 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#DAA520\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

            //основание машины
                s = MathPanelExt.QuadroEqu.DrawRect(2.2,-3,5.2,5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FF033E \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

              s = MathPanelExt.QuadroEqu.DrawRect(2.5,2,4.9,4.2, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#00BFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
              s = MathPanelExt.QuadroEqu.DrawRect(2.7,2.2,4.7,4, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#87CEEB\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

              s = MathPanelExt.QuadroEqu.DrawRect(-5.2,-2.2,1.8,4.2, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#531A50\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
              s = MathPanelExt.QuadroEqu.DrawRect(-5,-2,1.5,4, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9966CC \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

              s = MathPanelExt.QuadroEqu.DrawRect(1.8,-2,2.2,-1, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#CD2682  \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
              s = MathPanelExt.QuadroEqu.DrawRect(5.2,-2,8.2,2, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#CD2682 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

           //фары

                s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.5, 8, -1.8, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#D76E00\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawEllipse(0.2, 0.4, 8, -1.8, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDC33\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.5, -5.2, -1.8, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#D76E00\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
                s = MathPanelExt.QuadroEqu.DrawEllipse(0.2, 0.4, -5.2, -1.8, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFDC33\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


               //трава и земля
              s = MathPanelExt.QuadroEqu.DrawRect(-6,-5,9,-4.2, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B2EC5D \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
              s = MathPanelExt.QuadroEqu.DrawRect(-6,-7.2,9,-5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#79443B \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

