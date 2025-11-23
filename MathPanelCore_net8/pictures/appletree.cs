// Буланов Арсений; abulanov03@gmail.com
// 2D визуализация (яблоня с яблоками)

Dynamo.Console("Apple tree and apples!");

DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

double appleWidth = 10;
double appleHeight = 15;
double appleBranchWidth = 2;
double appleBranchHeight = 25;

double x = 400, y = 300;

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";
// Axis
var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 800, 600, true));

// Background
string s10 = string.Format(sOptFormat, "#ffffff", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

s9 = "";

// Trunk
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(380, 20, 420, 300, true)); 
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// Leaves
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(150, 150, 400, 300, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#03c04a", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
for (int i = 0; i < 20; i++)
{
    double angle = Math.PI * 2 / 20 * i;
    x = 400 + 150 * Math.Cos(angle);
    y = 300 + 150 * Math.Sin(angle);
    double r = 30;
    s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(r, r, x, y, 0, Math.PI * 2, 64, opt));
    s10 = string.Format(sOptFormat, "#03c04a", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10, true);
}

// Apples
x = 400;
y = 300;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#d30000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 500;
y = 320;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#fff700", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 350;
y = 200;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#597d35", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 300;
y = 380;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#597d35", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 290;
y = 280;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#fff700", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 410;
y = 400;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#d30000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

x = 470;
y = 220;
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(x + appleWidth / 2 - appleBranchWidth / 2, y, x + appleWidth / 2 + appleBranchWidth / 2, y + appleBranchHeight, true));
s10 = string.Format(sOptFormat, "#80471c", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x, y, 0, Math.PI * 2, 64, opt));
s9 += ("," + MathPanelExt.QuadroEqu.DrawEllipse(appleWidth, appleHeight, x + appleWidth, y, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#d30000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);