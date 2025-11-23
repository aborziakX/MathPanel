//sreen_projection_y
//координаты Y-Z, камера, точка, проекция
double sqrt2_2 = Math.Sqrt(2) / 2;
double lenAxe = 240; //длина оси
double xCenter = 500; //центр координат
double yCenter = 300;
double widScreen2 = 150; //половина ширины экрана
double zCam = 360; //позиция камеры
double zObj = 120; //позиция объекта
double yObj = 80; //позиция объекта
double yPro = 0; //позиция объекта на экране

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"fontsize\": 20, \"second\": \"{2}\" }}";

//оси
var s9 = MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter - lenAxe*2, yCenter, 10);//Z
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter, yCenter + lenAxe));//Y
s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter, yCenter, xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2));//X
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "", "line_end"));

//s9 += ("," + MathPanelExt.QuadroEqu.DrawArrow(xCenter + 114, yCenter + 114, xCenter + 120, yCenter + 120));
//s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + 120, yCenter + 120, "", "line_end"));

//тексты
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2, "X", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter + lenAxe, "Y", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - lenAxe*2, yCenter, "Z", "dots"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter, "O", "dots"));

//камера
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - zCam, yCenter, "", "dots", "#00ff00", "10", "30"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter - zCam, yCenter - 60, "zCam", "text", "#00ff00", "0", "30"));

//желтым оси
string s10 = string.Format(sOptFormat, "#ffff00", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//линия от камеры до экрана
yPro = yObj * zCam / (zCam - zObj);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(xCenter - zCam, yCenter, xCenter, yCenter + yPro));
s10 = string.Format(sOptFormat, "#00ff00", "1", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//проекция объекта на ось Z
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(xCenter - zObj, yCenter, xCenter - zObj, yCenter + yObj));
s10 = string.Format(sOptFormat, "#00ffff", "1", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//тексты
s9 = ("" + MathPanelExt.QuadroEqu.DrawPoint(xCenter - zObj, yCenter + yObj, "yObj", "circle"));
s9 += ("," + MathPanelExt.QuadroEqu.DrawPoint(xCenter, yCenter + yPro, "yProect", "circle"));
s10 = string.Format(sOptFormat, "#ff0000", "10", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);







