//sreen_projection
//координаты, камера, экран, изображение
double sqrt2_2 = Math.Sqrt(2) / 2;
double lenAxe = 220; //длина оси
double xCenter = 500; //центр координат
double yCenter = 300;
double widScreen2 = 150; //половина ширины экрана
double zCam = 20; //позиция камеры

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";

//оси
var s9 = MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter - lenAxe, yCenter, 10);//Z
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter, yCenter + lenAxe));//Y
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2));//X
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter + 114, yCenter + 114, xCenter + 120, yCenter + 120));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 120, yCenter + 120, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2, "X", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter + lenAxe, "Y", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - lenAxe, yCenter, "Z", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "O", "dots"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(zCam, yCenter, "zCam", "dots", "#00ff00", "10", "30"));

//желтым оси
string s10 = string.Format(sOptFormat, "#ffff00", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//DrawBezier2
s9 = MathPanelExt.QuadroEqu.DrawBezier2(100, 100, 150, 200, 200, 100, 10);
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(199, 102, 200, 100, 10));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(200, 100, "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(220, 100, 300, 200, false));
s9 += ("," + MathPanelExt.QuadroEqu.DrawRect(320, 100, 400, 200, true));

s10 = string.Format(sOptFormat, "#ff0000", "5", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);




