//arch1.cs
Dynamo.SceneClear();
//опции
string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";

//оси
var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 800, 600, true));

//черным фон
string s10 = string.Format(sOptFormat, "#000000", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//lines
s9 = (MathPanelExt.QuadroEqu.DrawArrow(300, 185, 500, 185, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(500, 185, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(510, 115, 310, 115, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(310, 115, "", "line_end"));

s10 = string.Format(sOptFormat, "#ff0000", "5", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

//вершины
s9 = (MathPanelExt.QuadroEqu.DrawRect(100, 200, 300, 100, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(510, 200, 777, 100, false));

//названия
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(120, 130, "Web-browser", "text", "#00ff00", "0", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(530, 130, "MPLinux/webserver", "text", "#00ff00", "0", "24"));

s10 = string.Format(sOptFormat, "#ffff00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);




