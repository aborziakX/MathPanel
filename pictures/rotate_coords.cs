//sreen_projection
//координаты, камера, экран, изображение
double sqrt2_2 = Math.Sqrt(2) / 2;
double lenAxe = 280; //длина оси
double xCenter = 400; //центр координат
double yCenter = 260;
double widScreen2 = 150; //половина ширины экрана
double zCam = 20; //позиция камеры

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";
string s9, s10;

//оси
s9 = MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter - lenAxe, yCenter, 5);//Z
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter, yCenter + lenAxe));//Y
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2));//X
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));

//названия осей
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2, "X", "dots", "#ffff00", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter + lenAxe, "Y", "dots", "#ffff00", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - lenAxe, yCenter, "Z, Z1", "dots", "#ffff00", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "O", "dots", "#ffff00", "0", "20"));

//желтым оси
s10 = string.Format(sOptFormat, "#ffff00", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//новые оси
s9 = "";

s9 += ("" + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter-40, yCenter + lenAxe-10));//Y
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter + lenAxe * sqrt2_2 + 10, yCenter - lenAxe * sqrt2_2+50));//X
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));

//названия осей
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - 40, yCenter + lenAxe - 10, "X1", "dots", "#ff0000", "0", "20"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + lenAxe * sqrt2_2 + 10, yCenter - lenAxe * sqrt2_2 + 50, "Y1", "dots", "#ff0000", "0", "20"));

//объект
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 114, yCenter + 114, "Obj", "circle", "#ff00ff", "5", "20"));

//красным
s10 = string.Format(sOptFormat, "#ff0000", "1", "2");
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






