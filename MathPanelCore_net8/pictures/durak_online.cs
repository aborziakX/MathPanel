//Денисов Виктор; denvic8671@iclud.ru
//Дурак онлайн

            Dynamo.Console("durak_online(offline)");

            int i;
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.sty = "line";

            //белый фон
            string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";
            var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 800, 600, true));
            string s10 = string.Format(sOptFormat, "#000000", "1", "undefined");
            s10 += ", \"data\":[" + s9 + "]}";
            Dynamo.SceneJson(s10);

            // кружок
            double rad = 8;
            double coeff = 1.04;
            string s = MathPanelExt.QuadroEqu.DrawEllipse(rad * coeff, rad * coeff, 0, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 0, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            // рот
            double rot = 3;
            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot * 2, rot, -3, -1, -Math.PI / 3, -Math.PI / 2, Math.PI / 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot * 2, rot, -3.3, -0.5, -Math.PI / 3, -Math.PI / 2, Math.PI / 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot, rot * 1.3, -3, -1.7, -Math.PI / 3, -Math.PI / 2, Math.PI / 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot * 1.05, rot * 1.3 * 1.05, -3.2, -1.4, -Math.PI / 3, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot / 2, rot / 2, -4.9, -4, -Math.PI / 3, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot / 2, rot / 2, 2, -1.25, -Math.PI / 3, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rot, rot, -4, 2.9, -Math.PI / 3, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            // брови
            double[] xe = { -5, -0.2 };
            double[] ye = { 0.1, 1.2 };
            double glaZZ = 1.1;
            for (i = 0; i < 2; i++)
            {
                int temp = 2 * i - 1;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(glaZZ * 1.7, glaZZ * 1.1, xe[i] + temp * 0.1, ye[i] + 0.7 + 0.27, temp * 0.3, 0.3, Math.PI - 0.3, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(glaZZ * 1.7, glaZZ * 1.1, xe[i] + temp * 0.1, ye[i] + 0.7 + 0.03, temp * 0.3, 0.3, Math.PI - 0.3, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }

            // глаза
            for (i = 0; i < 2; i++)
            {
                s = MathPanelExt.QuadroEqu.DrawSector(glaZZ * 1.5, glaZZ, xe[i], ye[i] + 0.7, -0.3, -Math.PI + 0.3, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawSector(glaZZ * 1.5, glaZZ, xe[i], ye[i], 0.3, Math.PI - 0.3, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawSector(glaZZ * 1.5 * 0.8, glaZZ * 0.8, xe[i], ye[i] + 0.55, -0.3, -Math.PI + 0.3, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawSector(glaZZ * 1.5 * 0.8, glaZZ * 0.8, xe[i], ye[i], 0.3, Math.PI - 0.3, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(glaZZ * 0.5, glaZZ * 0.5, xe[i] + 0.1, ye[i] + 0.4, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }

            // листочки
            double list = 0.4;
            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(list, list * 15, 5, 0, 0, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#228326\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(list, list * 12, 4, -2, Math.PI / 10, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#228326\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(list, list * 12, 5, -2, -Math.PI / 10, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#228326\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(list, list * 5, 4.5, -1, Math.PI / 15, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#228326\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(list, list * 5, 6, -1.5, -Math.PI / 6, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#228326\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            // цветочки
            double tsvet = 0.5;
            coeff = 1.15;
            double[] x = { 2, 5, 7 };
            double[] y = { 3, 6, 3 };
            //лепестки
            for (int j = 0; j < 3; j++)
            {
                for (i = 0; i < 6; i++)
                {
                    double dx = x[j] + 2 * tsvet * Math.Cos(i * Math.PI / 3);
                    double dy = y[j] + 2 * tsvet * Math.Sin(i * Math.PI / 3);
                    s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(tsvet * 3 * coeff, tsvet * coeff, dx, dy, i * Math.PI / 3 + i * 0.1, 0, Math.PI * 2, 64, opt);
                    s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ad100b\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                    s10 += ", \"data\":[" + s + "]}";
                    Dynamo.SceneJson(s10, true);

                    s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(tsvet * 3, tsvet, dx, dy, i * Math.PI / 3 + i * 0.1, 0, Math.PI * 2, 64, opt);
                    s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#d40001\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                    s10 += ", \"data\":[" + s + "]}";
                    Dynamo.SceneJson(s10, true);
                }
                s = MathPanelExt.QuadroEqu.DrawEllipse(1.6 * tsvet, 1.6 * tsvet, x[j], y[j], 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ad100b\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(1.3 * tsvet, 1.3 * tsvet, x[j], y[j], 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#f5e009\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }

            // рука
            double ruka = 1.1;
            coeff = 1.15;
            // black
            for (i = 0; i < 4; i++)
            {
                s = MathPanelExt.QuadroEqu.DrawEllipse(ruka * 2 * coeff, ruka * coeff, 5, -6 + i * ruka, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                s = MathPanelExt.QuadroEqu.DrawEllipse(ruka * 2, ruka, 5, -6 + i * ruka, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
            }
            //not black
            s = MathPanelExt.QuadroEqu.DrawRect(5.5, -6.5, 5.5 + ruka * 1.3, -6.5 + ruka * 3, true);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            ruka = ruka * 0.9;
            s = MathPanelExt.QuadroEqu.DrawEllipse(ruka * coeff, ruka * 2 * coeff, 3, -3.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

            s = MathPanelExt.QuadroEqu.DrawEllipse(ruka, ruka * 2, 3, -3.5, 0, Math.PI * 2, 64, opt);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#fedb99\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

