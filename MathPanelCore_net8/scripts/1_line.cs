string s1 = MathPanelExt.QuadroEqu.DrawLine(0, 0, 10, 10);
string s2 = "{\"options\":{\"x0\": -3, \"x1\": 13, \"y0\": -3, \"y1\": 13, \"clr\": \"#ff0000\", \"sty\": \"line\", \"size\":10, \"lnw\": 3, \"wid\": 800, \"hei\": 600 }";
string data = s2 + ", \"data\":[" + s1 + "]}";
Dynamo.Console(data);
Dynamo.SceneJson(data);
