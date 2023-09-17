Dynamo.SceneClear();
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
var s9 = QuadroEqu.DrawArrow(xCenter, yCenter, xCenter - lenAxe, yCenter, 10);//Z

DrawOpt opt4 = new DrawOpt();
opt4.sty = "line";
opt4.clr = "#0000ff";
opt4.csk = "#ffff00";
opt4.lnw = "1";
opt4.a11 = 2;
opt4.a12 = 0.5;
opt4.a21 = 0.5;
opt4.xTrans = xCenter;
opt4.yTrans = yCenter;
s9 += ("," + QuadroEqu.DrawArrow(xCenter, yCenter, xCenter, yCenter + lenAxe, opt4));//Y

s9 += ("," + QuadroEqu.DrawArrow(xCenter, yCenter, xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2));//X

s9 += ("," + QuadroEqu.DrawArrow(xCenter + 114, yCenter + 114, xCenter + 120, yCenter + 120));

s9 += ("," + QuadroEqu.DrawPoint(xCenter + lenAxe * sqrt2_2, yCenter - lenAxe * sqrt2_2, "X", "dots"));
s9 += ("," + QuadroEqu.DrawPoint(xCenter, yCenter + lenAxe, "Y", "dots"));
s9 += ("," + QuadroEqu.DrawPoint(xCenter - lenAxe, yCenter, "Z", "dots"));
s9 += ("," + QuadroEqu.DrawPoint(xCenter, yCenter, "O", "dots"));

s9 += ("," + QuadroEqu.DrawPoint(zCam, yCenter, "zCam", "dots", "#00ff00", "10", "30"));

//желтым оси
string s10 = string.Format(sOptFormat, "#ffff00", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//DrawBezier2
s9 = QuadroEqu.DrawBezier2(100, 100, 150, 200, 200, 100, 10);
s9 += ("," + QuadroEqu.DrawArrow(199, 102, 200, 100, 10));

//rects
DrawOpt opt3 = new DrawOpt();
opt3.sty = "line";
opt3.csk = "#0000ff";
opt3.xTrans = 290;
opt3.yTrans = 150;
opt3.Rotor(Math.PI / 6);
s9 += ("," + QuadroEqu.DrawRect(220, 100, 360, 200, opt3));

DrawOpt opt2 = new DrawOpt();
opt2.sty = "line";
opt2.csk = "#00ff00";
opt2.bFill = true;
opt2.a11 = 2;
opt2.a12 = 0.5;
opt2.a21 = 0.5;
opt2.xTrans = 460;
opt2.yTrans = 150;
s9 += ("," + QuadroEqu.DrawRect(420, 100, 500, 200, opt2));

//star
DrawOpt opt = new DrawOpt();
opt.sty = "line";
opt.clr = "#0000ff";
opt.csk = "#ffff00";
opt.lnw = "1";
opt.bFill = true;
opt.a11 = 2;
opt.a12 = 0.5;
opt.a21 = 0.5;
opt.xTrans = 100;
opt.yTrans = 500;
s9 += ("," + QuadroEqu.DrawStar(30, 15, 100, 500, 5, opt));

double[] yArr = new double[20];
for (int k = 0; k < 20; k++) yArr[k] = 500 + 100 * Math.Sin(k);
s9 += ("," + QuadroEqu.DrawGraphic(200, 300, yArr));

double[] xArr2 = new double[4];
double[] yArr2 = new double[4];
for (int k = 0; k < 4; k++)
{
    double fi = k * (Math.PI * 0.66);
    if (k == 3) fi = 0;
    xArr2[k] = 600 + 50 * Math.Cos(fi);
    yArr2[k] = 500 + 50 * Math.Sin(fi);
}
s9 += ("," + QuadroEqu.DrawGraphic(xArr2, yArr2));

//Ellipse
s9 += ("," + QuadroEqu.DrawEllipse(50, 100, 500, 300, 0, Math.PI, 20));

s10 = string.Format(sOptFormat, "#ff0000", "5", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);




