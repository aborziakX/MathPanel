//draw flower
int i;
            string[] clr =
            {
                "#ff0000", "#ffaa00", "#ffff00", "#00ff00", "#0000ff", "#ff00ff"
            };
            double rad = 4;
            string s = MathPanelExt.QuadroEqu.DrawEllipse(rad * 2, rad * 2, 0, 0, 0, Math.PI * 2, 64);
            string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10);
System.Threading.Thread.Sleep(500);

            for(i = 0; i < 6; i++)
            {
                double x = rad * Math.Cos((Math.PI * i) / 3);
                double y = rad * Math.Sin((Math.PI * i) / 3);
                s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, x, y, 0, Math.PI * 2, 64);
                s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + clr[i] + "\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
                s10 += ", \"data\":[" + s + "]}";
                Dynamo.SceneJson(s10, true);
System.Threading.Thread.Sleep(500);
            }

            s = MathPanelExt.QuadroEqu.DrawEllipse(rad, rad, 0, 0, 0, Math.PI * 2, 64);
            s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
            s10 += ", \"data\":[" + s + "]}";
            Dynamo.SceneJson(s10, true);