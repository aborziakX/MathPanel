//Автор: Шатилов Илья, popactsa.main@yandex.ru
//Винни-Пух
 
            Dynamo.Console("Vinnie Pooh");
            double rad = 2;
            int i;
            DrawOpt opt = new DrawOpt();
            opt.bFill = true;
            opt.clr = "#fffa00";
            opt.csk = "#000000";
            opt.sty = "line";
            opt.lnw = "2";

            // небо
            string s = MathPanelExt.QuadroEqu.DrawRect(-7, -10, 7, 10, true);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #EBF0EA\", \"sty\": \"line\", \"size\":0, \"lnw\": 1,             \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);

                double x = -2;
                double y = 6;
                opt.clr = "#FEFE22";
                opt.csk = opt.clr;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, x, y, 0, 2*Math.PI, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4CB067\", \"sty\": \"line\", \"size\":0, \"lnw\": 6, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

            // земля
                x = 0;
                y = -10.5;
                opt.clr = "#4CB067";
                opt.csk = opt.clr;
                s = MathPanelExt.QuadroEqu.DrawEllipse(12, 6, x, y, 0, Math.PI, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4CB067\", \"sty\": \"line\", \"size\":0, \"lnw\": 6, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

            // нижние лапы
                x = 3.3;
                y = -5.2;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/4, rad/3, x, y, -Math.PI/6, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                x = 2.5;
                y = -5.2;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/4, rad/3, x, y, -Math.PI/7, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                // ушки 1
                x = 0.05;
                y = 0.25;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/5, rad/2.5, x, y, -Math.PI /4.5 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                x = 3.15;
                y = 0.25;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/2.5, rad/1.7, x, y, -Math.PI/12, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

            // верхние лапы 1
                x = -0.1;
                y = -3.0;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/4, rad/2.4, x, y, -Math.PI/20, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);



            // каркас медведя
                x = 2.35;
                y = -3;
                opt.clr = "#652905";
                opt.csk = "#200D07";
                opt.lnw = "4";
                s = MathPanelExt.QuadroEqu.DrawEllipse(1.25*rad, 1.1*rad, x, y, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 2.15;
                y = -3 + rad*0.9;
                opt.clr = "#652905";
                opt.csk = "#200D07";
                opt.lnw = "0.1";
                s = MathPanelExt.QuadroEqu.DrawEllipse(1.25*rad, 1.1*rad, x, y, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#652905\", \"sty\": \"line\", \"size\":1, \"lnw\": 0, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 2.15;
                y = -3 + rad*0.9;
                opt.clr = "#200D07";
                opt.csk = opt.clr;
                opt.lnw = "4";
                opt.bFill = false;
                s = MathPanelExt.QuadroEqu.DrawEllipse(1.25*rad, 1.1*rad, x, y, Math.PI/0.85, -Math.PI*1/5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#652905\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                // ушки 2
                x = -0.22;
                y = -0.25;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/6/1.3, rad/5/1.3, x, y, Math.PI /8.5 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 0.40;
                y = 0.55;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/5/1.3, rad/5/1.3, x, y, Math.PI /5 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                x = 2.85;
                y = 0.85;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/8, rad/6, x, y, -Math.PI/12, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 3.75;
                y = 0.50;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/8, rad/6, x, y, -Math.PI/12, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                x = 2.2;
                y = -1.20;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad/5.5, rad/5.5, x, y, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 2.0;
                y = -1.25;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad/5.5, rad/5.5, x, y, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                x = 2.3;
                y = -2.10;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/8, rad/35, x, y, Math.PI /10, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                // окологлазие
                x = 1.80;
                y = -0.1;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/1.8, rad/3.5, x, y, Math.PI /15 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                x = 1.1;
                y = -0.65;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/4, rad/3, x, y, -Math.PI /12 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 2.7;
                y = -0.25;
                opt.csk = "#28100C"; 
                opt.clr = "#28100C";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/4, rad/3, x, y, Math.PI /5 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


                // зрачки
                x = 1.2;
                y = -0.6;
                opt.csk = "#200D07";
                opt.clr = "#FEFBB6";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/7, rad/5, x, y, Math.PI /10 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 2.7;
                y = -0.3;
                opt.csk = "#200D07";
                opt.clr = "#FEFBB6";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/7, rad/5, x, y, Math.PI /10 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 1.2;
                y = -0.6;
                opt.csk = "#200D07";
                opt.clr = "#200D07";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/7/2.5, rad/5/2.5, x, y, Math.PI /10 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);

                x = 2.7;
                y = -0.3;
                opt.csk = "#200D07";
                opt.clr = "#200D07";
                opt.lnw = "1.0";
                opt.bFill = true;
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/7/2.5, rad/5/2.5, x, y, Math.PI /10 ,0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


            // верхние лапы 2
                x = 4.3;
                y = -3.3;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/4, rad/2.4, x, y, Math.PI/10, 0, Math.PI * 2, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);


            // когти правые верхние лапы
                x = 4.82;
                y = -3.35;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/9, rad/7, x, y, Math.PI / 2, Math.PI / 2, Math.PI * 1.5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);   

                x = 4.78;
                y = -3.6;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/9, rad/7, x, y, Math.PI / 2, Math.PI / 2, Math.PI * 1.5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true); 

                x = 4.75;
                y = -3.8;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/9, rad/7, x, y, Math.PI / 2, Math.PI / 2, Math.PI * 1.5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);    


            // когти левые верхние лапы
                x = -0.2;
                y = -3.75;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/9, rad/7, x, y, 0, Math.PI / 2, Math.PI * 1.5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);   

                x = -0.10;
                y = -3.75;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/9, rad/7, x, y, 0, Math.PI / 2, Math.PI * 1.5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);  

                x = 0.10;
                y = -3.75;
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";
                s = MathPanelExt.QuadroEqu.DrawRotatedEllipse(rad/9, rad/7, x, y, 0, Math.PI / 1.5, Math.PI * 1.5, 64, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true); 

             // когти левые нижние лапы
                x = 2.2;
                y = -5.6;                   
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";

                s = MathPanelExt.QuadroEqu.DrawLine(x, y, x + 0.1, y - 0.5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);   

                x = 2.4;
                y = -5.55;                   
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";

                s = MathPanelExt.QuadroEqu.DrawLine(x, y, x + 0.1, y - 0.5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);  

                x = 2.6;
                y = -5.5;                   
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";

                s = MathPanelExt.QuadroEqu.DrawLine(x, y, x + 0.1, y - 0.5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);  

// когти правые нижние лапы
                x = 3.4;
                y = -5.6;                   
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";

                s = MathPanelExt.QuadroEqu.DrawLine(x, y, x-0.1, y - 0.5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);   

                x = 3.2;
                y = -5.55;                   
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";

                s = MathPanelExt.QuadroEqu.DrawLine(x, y, x-0.1, y - 0.5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);  

                x = 3.0;
                y = -5.5;                   
                opt.clr = "#200D07";
                opt.csk = "#200D07";
                opt.bFill = false;
                opt.lnw = "3.0";

                s = MathPanelExt.QuadroEqu.DrawLine(x, y, x - 0.1, y - 0.5, opt);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#200D07\", \"sty\": \"line\", \"size\":1, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);  

