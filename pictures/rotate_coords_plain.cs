//rotate_coord_plain.cs
//координаты X-O-Y, x1-O-y1
double sqrt2_2 = Math.Sqrt(2) / 2;
double lenAxe = 280; //длина оси
double xCenter = 400; //центр координат
double yCenter = 260;
double zRotor = (20.0 / 180.0) * Math.PI;
Dynamo.Console("" + Math.Sin(zRotor));

//крайние точки осей
double xAxeX_0 = xCenter + lenAxe;
double yAxeX_0 = yCenter;
double xAxeY_0 = xCenter;
double yAxeY_0 = yCenter + lenAxe;

//double yE_0 = xCenter + lenAxe;

//крайние точки новых осей после поворота
double xAxeX_1 = xCenter + lenAxe * Math.Cos(zRotor) - 0 * Math.Sin(zRotor);
double yAxeX_1 = yCenter + lenAxe * Math.Sin(zRotor) + 0 * Math.Cos(zRotor);
double xAxeY_1 = xCenter + 0 * Math.Cos(zRotor) - lenAxe * Math.Sin(zRotor);
double yAxeY_1 = yCenter + 0 * Math.Sin(zRotor) + lenAxe * Math.Cos(zRotor);
//в другую сторону
double xAxeX_1min = xCenter - lenAxe * Math.Cos(zRotor) + 0 * Math.Sin(zRotor);
double yAxeX_1min = yCenter - lenAxe * Math.Sin(zRotor) - 0 * Math.Cos(zRotor);
double xAxeY_1min = xCenter - 0 * Math.Cos(zRotor) + lenAxe * Math.Sin(zRotor);
double yAxeY_1min = yCenter - 0 * Math.Sin(zRotor) - lenAxe * Math.Cos(zRotor);


string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";
string s9, s10;

//оси
s9 = ("" + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter - lenAxe, xAxeY_0, yAxeY_0));//Y
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xAxeX_0, yAxeX_0));//X
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xAxeX_0, yAxeX_0, xCenter - lenAxe, yCenter));//-X
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));

//названия осей
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xAxeX_0, yAxeX_0, "X", "dots", "#ffff00", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xAxeY_0, yAxeY_0, "Y", "dots", "#ffff00", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "O", "dots", "#ffff00", "0", "20"));

//угол
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(lenAxe / 4, lenAxe / 4, xCenter, yCenter, 0, zRotor, 5));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(lenAxe / 4, lenAxe / 4, xCenter, yCenter, Math.PI/2, Math.PI / 2 + zRotor, 5));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter+lenAxe / 4 + 5, yCenter, "zRotor", "text", "#ffff00", "0", "16"));

//желтым оси
s10 = string.Format(sOptFormat, "#ffff00", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//новые оси
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawArrow(xAxeY_1min, yAxeY_1min, xAxeY_1, yAxeY_1));//Y1
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xAxeX_1, yAxeX_1));//X1
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xAxeX_1, yAxeX_1, xAxeX_1min, yAxeX_1min));//-X1
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));

//названия осей
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xAxeX_1, yAxeX_1, "X1", "dots", "#ff0000", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xAxeY_1, yAxeY_1, "Y1", "dots", "#ff0000", "0", "20"));

//объект
//s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 174, yCenter + 174, "Obj", "circle", "#ff00ff", "5", "20"));

//красным
s10 = string.Format(sOptFormat, "#ff0000", "1", "2");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//проекции
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawLine(xCenter + lenAxe * Math.Cos(zRotor), yCenter, xAxeX_1, yAxeX_1));//X1 на X0
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xAxeX_1, yAxeX_1, xCenter, yCenter + lenAxe * Math.Sin(zRotor)));//X1 на Y0
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter + lenAxe * Math.Sin(zRotor), "", "line_end"));

s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xCenter, yCenter + lenAxe * Math.Cos(zRotor), xAxeY_1, yAxeY_1));//Y1 на X0
s9 += ("," + MathPanelExt.QuadroEqu.DrawLine(xAxeY_1, yAxeY_1, xCenter - lenAxe * Math.Sin(zRotor), yCenter));//Y1 на Y0

//зеленым оси
s10 = string.Format(sOptFormat, "#00ff00", "1", "2");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

/*
//объект
s9 = "";
if (s9 != "") s9 += ",";
// ("" + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 114, yCenter + 114, "zCam", "circle", "#00ff00", "10", "30"));

//маленькая стрелка
if (s9 != "") s9 += ",";
s9 += (MathPanelExt.QuadroEqu.DrawArrow(xCenter + 114, yCenter + 114, xCenter + 120, yCenter + 120));
//завершить линию
if (s9 != "") s9 += ",";
s9 += (MathPanelExt.QuadroEqu.DrawPoint(xCenter + 120, yCenter + 120, "", "line_end"));

//красным
s10 = string.Format(sOptFormat, "#ff0000", "1", "2");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);*/

//DrawPoint(double x, double y, string text, string style, string clr, string pointsize, string fontsize)






