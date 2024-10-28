// Сороко Надежда soroko03@list.ru
// 2D визуализация (Паук)

Dynamo.Console("Spider done");

DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";


//Center
double x = 400, y = 300;

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";

// Axis
var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 800, 600, true));

// Background
string s10 = string.Format(sOptFormat, "#606060", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

//Web

s9 = MathPanelExt.QuadroEqu.DrawEllipse(110, 110, 400, 300, 0, Math.PI * 2, 64);
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = MathPanelExt.QuadroEqu.DrawEllipse(200, 200, 400, 300, 0, Math.PI * 2, 64);
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = MathPanelExt.QuadroEqu.DrawEllipse(300, 300, 400, 300, 0, Math.PI * 2, 64);
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 400, 600));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 400, 0));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 0, 300));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 800, 300));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(0, 0, 800, 600));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(0, 600, 800, 0));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

// body

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(100 , 80 , 400, 300, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

//legs

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 500, 430));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(500, 430, 562, 417));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 300, 430));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(300, 430, 238, 417));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 540, 360));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(540, 360, 600, 340));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 260, 360));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(260, 360, 200, 340));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 550, 310));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(550, 310, 600, 280));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 250, 310));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(250, 310, 200, 280));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 570, 260));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(570, 260, 590, 230));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(400, 300, 230, 260));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(230, 260, 210, 230));
s10 = string.Format(sOptFormat, "#000000", "8", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

//eyes

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(12, 12, 375, 305, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(12, 12, 435, 305, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#ffffff", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(6, 6, 375, 302, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);

s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(6, 6, 435, 302, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#000000", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10, true);
