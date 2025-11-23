//medved.cs
//Арина Смаль arina.smal@mail.ru


            Dynamo.Console("test55_flower started!");

            double rad = 4;

            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            //opt.clr = "#fffa00";
            //opt.csk = "#000000";
            opt.sty = "line";
            //opt.lnw = "2";

            //тело
            string s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad * 1.3, 0, 0, 0, Math.PI * 2, 64, opt);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.6, rad * 0.85, 0, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#CD5C5C \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //уши
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.23, rad * 0.23, 1.35, 7.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.23, rad * 0.23, -1.35, 7.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.09, rad * 0.09, -1.35, 7.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#CD5C5C\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.09, rad * 0.09, 1.35, 7.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#CD5C5C\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);



            //малая окружность
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.5, rad * 0.5, 0, 6, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //лапы
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.2, rad * 0.2, 3.5, 2.8, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.2, rad * 0.2, -3.5, 2.8, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //лапки
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.3, rad * 0.1, -2, -4.8, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.3, rad * 0.1, 2, -4.8, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //глаза
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.05, rad * 0.05, 0.8, 6.7, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.05, rad * 0.05, -0.8, 6.7, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.02, rad * 0.02, -0.7, 6.6, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.02, rad * 0.02, 0.9, 6.6, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //нос
            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.03, rad * 0.03, 0, 6, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.1, rad * 0.07, 0, 5.2, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 0.1, rad * 0.07, 0, 5.3, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B22222\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

