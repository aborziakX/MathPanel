// Чурсин Алексей; alexytchursin@yandex.ru
// 2D визуализация (дальнобойщик)

Dynamo.Console("Dalnoboyschik!");

DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

double appleWidth = 10;
double appleHeight = 15;
double appleBranchWidth = 2;
double appleBranchHeight = 25;

double x = 400, y = 300;
int width = 800, height = 600;

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";
// Axis
var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, width, height, true));

// Background
string s10 = string.Format(sOptFormat, "#00b300", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

s9 = "";

// Background2
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(0, height/2, width, height, true)); 
s10 = string.Format(sOptFormat, "#0099cc", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// clouds
x = 400;
y = 400;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 350;
y = 540;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 100;
y = 350;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 200;
y = 500;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 500;
y = 480;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 600;
y = 350;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 700;
y = 481;
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// bushes
x = 550;
y = 300;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-50, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 550;
y = 220;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-60, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(50, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 700;
y = 250;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-50, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 200;
y = 250;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-50, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 300;
y = 300;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-50, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 100;
y = 270;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-50, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 400;
y = 270;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x-5, y-50, x+5, y, true)); 
s10 = string.Format(sOptFormat, "#663300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-55, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+50, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
s10 = string.Format(sOptFormat, "#004d00", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// road
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 25, width, 150, true)); 
s10 = string.Format(sOptFormat, "#808080", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

int w = 0;
for (int i = 0; i < 20; i++)
{
    s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(w, 87, w+20, 87, true)); 
    s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10, true);
    w += 40;
}


// track front
x = 400;
y = 300;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(400, 130, 470, 180, true));
s10 = string.Format(sOptFormat, "#BB9C3A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(59, 59, 410, 170, 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#BB9C3A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(30, 30, 420, 190, 0, Math.PI/2, 64, opt));
s10 = string.Format(sOptFormat, "#00b300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(410, 190, 430, 220, true));
s10 = string.Format(sOptFormat, "#00b300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(410, 190, 448, 205, true));
s10 = string.Format(sOptFormat, "#00b300", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);


// track
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(50, 130, 400, 230, true));
s10 = string.Format(sOptFormat, "#A97C1A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// driver
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(428, 190, 432, 205, true));
s10 = string.Format(sOptFormat, "#db9065", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 430, 205, 3*Math.PI/2, 5*Math.PI/2, 64, opt));
s10 = string.Format(sOptFormat, "#db9065", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 430, 205, Math.PI/2-0.4, 3*Math.PI/2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 450, 190, Math.PI/2, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// wheels
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, 100, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#222222", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(15, 15, 100, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#555555", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 100, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, 150, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#222222", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(15, 15, 150, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#555555", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 150, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, 350, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#222222", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(15, 15, 350, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#555555", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 350, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 20, 435, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#222222", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(15, 15, 435, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#555555", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(5, 5, 435, 120, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);


x = 300;
y = 180;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#fff700", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 250;
y = 170;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#597d35", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 100;
y = 190;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#597d35", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 150;
y = 170;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#fff700", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 200;
y = 190;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#d30000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
