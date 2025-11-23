//Андрей Дягилев; dyagilev_03@bk.ru
//"Pink sweet" Kandinsky drawing

            Dynamo.Console("'Pink sweet' - Kandinsky!");
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.sty = "line";


            //Фон
            string s = MathPanelExt.QuadroEqu.DrawRect(-7, -10, 7, 10, true);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #c87b71\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //2 "ствола"
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-2.7, -1.1, -2.6, -1.1, -2.63, 9, -2.67, 9, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#6a4535 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-1.6, 0.7, -1.5, 0.7, -1.53, 5.5, -1.57, 5.5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#6a4535 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Чаши на левом стволе
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.7, 0.65, -3.4, 1.55, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#836a54\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(1.1, 0.9, -3.75, 3.7, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4b5a47\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.9, 0.8, -3.6, 5, 0, -Math.PI, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#69613c\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.7, 0.6, -3.45, 6.5, 0, -Math.PI, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#cd975b\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.63, 0.35, -3.3, 8, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#cea766\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Чаши на правом стволе
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.15, -1.95, 1.28, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#c58a3c\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.4, 0.3, -2, 2.7, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#a75839\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.37, 0.27, -1.95, 3.2, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#823d2e\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.31, 0.28, -1.91, 4.1, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#93463e\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.28, 0.28, -1.9, 4.8, -Math.PI / 10, -9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#823734\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            //Главная пирамида
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-3.6, -1.7, 1.0, -1.7, -0.8, 1.7, -0.8, 1.7, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e8c14c \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Нижняя полоска пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-3.6, -1.7, 1.0, -1.7, 0.68, -1.1, -3.1, -1.1, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8e4228 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Верхняя полоска пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-1.325, 1.1, -0.5, 1.1, -0.8, 1.7, -0.8, 1.7, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8f2e27 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Прямоугольники внутри пирамиды
            s = MathPanelExt.QuadroEqu.DrawRect(-1.1, -0.3, -0.75, 0.25, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#25383c\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-0.75, -0.3, -0.5, -0.7, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5c4d30\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-1.3, -0.62, -1.1, -0.42, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8e4a33\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-2.1, -0.65, -2, -0.55, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#372010\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-0.6, 0.05, -0.15, 0.1, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#7c4c36\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-0.58, -0.15, -0.03, -0.1, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#7c4c36\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Тень пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1.0, -1.7, 1.7, -1.7, -0.8, 1.7, -0.8, 1.7, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#99693b \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Квадрат справа вверху
            s = MathPanelExt.QuadroEqu.DrawRect(5.5, 8.2, 6.1, 8.8, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#b18d4f\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Прямоугольники справа
            s = MathPanelExt.QuadroEqu.DrawRect(4.4, 1.2, 4.6, 4.2, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#d7ae56\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(4.6, 1.2, 4.8, 4.2, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#d3933b\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(4.8, 1.2, 5, 4.2, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8f322a\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Прямоугольники слева внизу
            s = MathPanelExt.QuadroEqu.DrawRect(-3.3, -9, -3.0, -5.4, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8e6330\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-3.0, -9, -2.7, -5.4, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#bc9373\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-2.7, -9, -2.4, -5.4, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#6b5f49\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Маленькие прямоугольники вверху
            s = MathPanelExt.QuadroEqu.DrawRect(-1.1, 7, -0.95, 7.45, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#d0bea6\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(-0.95, 7, -0.8, 7.45, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#cdb595\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Маленькие прямоугольники посередине
            s = MathPanelExt.QuadroEqu.DrawRect(0.3, 2, 0.4, 2.3, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#a79f7b\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(0.4, 2, 0.5, 2.3, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#cbac73\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Солнце
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.8, 0.8, 2.5, 5.2, 0, Math.PI * 2, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.77, 0.77, 2.5, 5.2, 0, Math.PI * 2, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#704a37\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.4, 0.4, 2.5, 5.2, 0, Math.PI * 2, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.34, 0.34, 2.5, 5.2, 0, Math.PI * 2, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#8a9881\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Криые на солнце
            s = MathPanelExt.QuadroEqu.DrawBezier2(2.5, 5.2, 1.7, 6.2, 2.7, 7.2, 100);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawBezier2(2.5, 5.2, 3.3, 4.2, 2.7, 3.2, 100);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawBezier2(2.5, 5.2, 3.5, 6.2, 4.5, 5.2, 100);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawBezier2(2.5, 5.2, 1.5, 4.2, 0.5, 5.2, 100);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Перекрестие на солнце
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(1.2, 3.7, 0.05, 4, -40, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRotatedRect(3.95, 3.96, 0.05, 4, 50, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Ниточка
            s = MathPanelExt.QuadroEqu.DrawRect(2, -3.4, 3.8, -3.5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#d7be86\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(3.8, -3.4, 3.8, -6.6, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(3.8, -6.5, 4.5, -6.6, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#d7be86\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawRect(4.5, -6.5, 4.5, -9.3, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Правая пирамида
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1, -3.9, 3.2, -3.9, 2.1, -0.5, 2.1, -0.5, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#78897f \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Чаша над пирамидой
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.7, 0.5, 2.1, -0.2, Math.PI / 10, 9 * Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#78897f\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Нижний (нулевой) слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1, -3.9, 3.2, -3.9, 3.16, -3.8, 1.05, -3.8, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#866033 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Первый слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1.05, -3.8, 3.16, -3.8, 3.04, -3.4, 1.16, -3.4, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#cbc8bd \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Второй слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1.16, -3.4, 3.04, -3.4, 2.78, -2.6, 1.42, -2.6, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#40584a \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Третий слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1.42, -2.6, 2.78, -2.6, 2.59, -2, 1.61, -2, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#3f5e61 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Четвертый слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(1.61, -2, 2.59, -2, 2.39, -1.4, 1.81, -1.4, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e8c14c \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Нижняя пирамида
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-0.2, -3.9, 1, -3.9, 0.4, -2, 0.4, -2, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#f5b91e \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Нижний слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-0.45, -4.7, 1.25, -4.7, 1, -3.9, -0.2, -3.9, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#6d3613 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Первый слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-0.2, -3.9, 1, -3.9, 0.9, -3.55, -0.09, -3.55, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#462515 \", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Второй слой пирамиды
            s = MathPanelExt.QuadroEqu.DrawTrapezoid(-0.09, -3.55, 0.9, -3.55, 0.8, -3.25, 0.005, -3.25, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#955132\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);


            //Две дуги
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.45, 0.45, -0.85, -3.3, 0, Math.PI, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.4, 0.4, -0.85, -3.3, 0, 2 * Math.PI, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#c87b71\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.4, 0.4, 2, -5.4, 11 * Math.PI / 10, -Math.PI / 10, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);
            s = MathPanelExt.QuadroEqu.DrawEllipse(0.35, 0.35, 2, -5.4, 0, 2 * Math.PI, 256, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#c87b71\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

