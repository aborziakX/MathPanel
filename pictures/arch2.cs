//arch2.cs
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
s9 = (MathPanelExt.QuadroEqu.DrawArrow(300, 175, 500, 175, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(500, 175, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(510, 115, 310, 115, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(310, 115, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(580, 200, 580, 290, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(580, 290, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(670, 300, 670, 210, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(670, 210, "", "line_end"));

s10 = string.Format(sOptFormat, "#ff0000", "5", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

//вершины
s9 = (MathPanelExt.QuadroEqu.DrawRect(50, 200, 300, 100, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(50, 145, 300, 100, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(510, 200, 740, 100, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(510, 145, 740, 100, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(510, 400, 740, 300, false));

//названия
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(70, 149, "Web-browser", "text", "#00ff00", "0", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(70, 108, "канвас графики", "text", "#00ff00", "0", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(540, 149, "IIS / Apache", "text", "#00ff00", "0", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(540, 108, "Python app", "text", "#00ff00", "0", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(540, 330, "База данных", "text", "#00ff00", "0", "24"));

s10 = string.Format(sOptFormat, "#ffff00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);




