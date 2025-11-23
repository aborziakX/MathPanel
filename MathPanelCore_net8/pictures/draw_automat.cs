//draw_btree.cs
//Конечный автомат

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

//вершины
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(100, 410, 300, 500, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(100, 200, 300, 100, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(510, 200, 750, 100, false));

//названия
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(130, 440, "Прятаться", "circle", "#00ff00", "0.1", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(120, 130, "Искать лист", "circle", "#00ff00", "0.1", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(530, 130, "Домой с листом", "circle", "#00ff00", "0.1", "24"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(330, 240, "Лист найден", "circle", "#00ff00", "0.1", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(310, 40, "Лист доставлен домой", "circle", "#00ff00", "0.1", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(110, 350, "Лиса рядом", "circle", "#00ff00", "0.1", "24"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(250, 300, "Лиса ушла", "circle", "#00ff00", "0.1", "24"));


s10 = string.Format(sOptFormat, "#ffff00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

//DrawBezier2
s9 = MathPanelExt.QuadroEqu.DrawBezier2(300, 190, 400, 250, 500, 190, 10);
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(498, 191, 500, 190, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(500, 190, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawBezier2(510, 110, 400, 50, 310, 110, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(312, 109, 310, 110, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(310, 110, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawBezier2(110, 200, 50, 300, 110, 400, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(109, 398, 110, 400, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(110, 400, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawBezier2(290, 410, 350, 300, 290, 210, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(291, 212, 290, 210, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(290, 210, "", "line_end"));

s10 = string.Format(sOptFormat, "#ff0000", "5", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);





