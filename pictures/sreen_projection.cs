//sreen_projection
//координаты, камера, экран, изображение
double sqrt2_2 = Math.Sqrt(2) / 2;
double lenAxe = 220; //длина оси
double xCenter = 500; //центр координат
double yCenter = 300;
double widScreen2 = 150; //половина ширины экрана
double zCam = 20; //позиция камеры

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":5, \"lnw\": 1, \"wid\": 800, \"hei\": 600, \"second\": \"{1}\" }}";

//оси
var s9 = MathPanelExt.QuadroEqu.DrawLine(xCenter - lenAxe, yCenter, xCenter, yCenter);//Z
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xCenter, yCenter + lenAxe, xCenter, yCenter));//Y
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xCenter, yCenter, xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2));//X

s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2, "X", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter + lenAxe, "Y", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - lenAxe, yCenter, "Z", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "O", "dots"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(zCam, yCenter, "zCam", "dots", "#ff0000", "10", "30"));

//желтым оси
string s10 = string.Format(sOptFormat, "#ffff00", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//серый экран
//точка пересечения экрана с осью X справа
double x1 = xCenter + widScreen2 * sqrt2_2;
double y1 = yCenter - widScreen2 * sqrt2_2;
s9 = MathPanelExt.QuadroEqu.DrawLine(x1, y1 + widScreen2, x1, y1 - widScreen2);//сверху вниз

//точка пересечения экрана с осью X слева
double x2 = xCenter - widScreen2 * sqrt2_2;
double y2 = yCenter + widScreen2 * sqrt2_2;
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(x2, y2 - widScreen2, x2, y2 + widScreen2));//снизу вверх
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(x2, y2 + widScreen2, x1, y1 + widScreen2));//замыкаем

s10 = string.Format(sOptFormat, "#ffffff", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//камера видит весь экран
s9 = MathPanelExt.QuadroEqu.DrawLine(zCam, yCenter, x1, y1 + widScreen2);//1
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(zCam, yCenter, x1, y1 - widScreen2));//2
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(zCam, yCenter, x2, y2 - widScreen2));//3
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(zCam, yCenter, x2, y2 + widScreen2));//4

s10 = string.Format(sOptFormat, "#ff00ff", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//изображение - квадрат
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(zCam, yCenter, xCenter + 100, yCenter + 132));
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(zCam, yCenter, xCenter + 100, yCenter + 68));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 100, yCenter + 100, "", "line_end", "#00ff00", "1", "1"));//close lines

s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter+100, yCenter+100, "b1", "circle", "#00ff00", "32", "12"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 10, yCenter + 84, "b2", "circle", "#99ff99", "25", "12"));

//order! - problem with lines!

s10 = string.Format(sOptFormat, "#00ff00", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);


