//draw_btree.cs
//Двоичное дерево поиска

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
s9 += ("" + MathPanelExt.QuadroEqu.DrawArrow(400, 520, 250 + 30, 380 + 30));//8-3
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400, 520, "", "line_end"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(400, 520, 550 - 30, 380 + 30));//8-10
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400, 520, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(250, 380, 150 + 30, 240 + 30));//3-1
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250, 380, "", "line_end"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(250, 380, 350 - 30, 240 + 30));//3-6
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250, 380, "", "line_end"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(550, 380, 650 - 30, 240 + 30));//10-14
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550, 380, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(350, 240, 250 + 30, 100 + 30));//6-4
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(350, 240, "", "line_end"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(350, 240, 400 - 20, 100 + 35));//6-7
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(350, 240, "", "line_end"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(650, 240, 550 + 20, 100 + 35));//14-13
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(650, 240, "", "line_end"));

//вершины
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400, 520, "8", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250, 380, "3", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550, 380, "10", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(150, 240, "1", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(350, 240, "6", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(650, 240, "14", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250, 100, "4", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400, 100, "7", "circle", "#00cc00", "38", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550, 100, "13", "circle", "#00cc00", "38", "12"));

//названия
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400 - 16, 520 - 20, "8", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250 - 16, 380 - 20, "3", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550 - 16, 380 - 20, "10", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(150 - 16, 240 - 20, "1", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(350 - 16, 240 - 20, "6", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(650 - 16, 240 - 20, "14", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250 - 16, 100 - 20, "4", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(400 - 16, 100 - 20, "7", "circle", "#000000", "0.1", "32"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(550 - 16, 100 - 20, "13", "circle", "#000000", "0.1", "32"));

s10 = string.Format(sOptFormat, "#ffff00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);




