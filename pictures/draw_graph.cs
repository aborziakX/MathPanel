//draw_graph.cs
//граф с одной петлей

double sqrt2_2 = Math.Sqrt(2) / 2;
double lenAxe = 220; //длина оси
double xCenter = 500; //центр координат
double yCenter = 300;
double widScreen2 = 150; //половина ширины экрана
double zCam = 20; //позиция камеры

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";

//оси
var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 800, 600, true));

//белым фон
string s10 = string.Format(sOptFormat, "#000000", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

s9 = "";

//ребра
s9 += ("" + MathPanelExt.QuadroEqu.DrawLine(400, 500, 600, 330));//AB
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(600, 330, 550, 100));//BC
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(550, 100, 250, 100));//CD
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(250, 100, 200, 330));//DE
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(200, 330, 400, 500));//EA
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400, 500, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(60, 50, 600 + 60, 330, 0, Math.PI * 2, 24));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(600 + 2*60, 330, "", "line_end"));

//вершины
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400, 500, "A", "circle", "#00cc00", "40", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(600, 330, "B", "circle", "#00cc00", "40", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550, 100, "C", "circle", "#00cc00", "40", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250, 100, "D", "circle", "#00cc00", "40", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(200, 330, "E", "circle", "#00cc00", "40", "12"));

//названия
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400 - 16, 500 - 20, "A", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(600 - 16, 330 - 20, "B", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550 - 16, 100 - 20, "C", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250 - 16, 100 - 20, "D", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(200 - 16, 330 - 20, "E", "circle", "#000000", "0.1", "32"));

s10 = string.Format(sOptFormat, "#ffff00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);




