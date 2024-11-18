//Герасимова Валентина, gerasimova.valya2003@mail.ru
Dynamo.Console("Sea landscape");

// Настройки для рисования
DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

// Небо
string s = MathPanelExt.QuadroEqu.DrawRect(-10, -3, 10, 10, true);
string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #a9c3f7\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Море
s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, -3, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #1f5bc2\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Облака
s = MathPanelExt.QuadroEqu.DrawEllipse(4, 1.2, -5, 7, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#eaf0fb\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(2, 0.7, -1, 8, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#eaf0fb\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1.3, 0.3, -8, 4.5, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#eaf0fb\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(5, 1.7, 6, 4, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#eaf0fb\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(2, 0.7, 3, 5.5, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#eaf0fb\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Столбики
s = MathPanelExt.QuadroEqu.DrawRect(-6, -7, -4.5, 1, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9b5840\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawRect(-3, -7, -1.5, 3, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9b5840\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawRect(0, -7, 1.5, 0.5, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9b5840\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawRect(3.5, -7, 5, 1.3, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9b5840\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Птица1
// тело
s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -5.2, 2.3, -Math.PI, Math.PI/8, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 0.5, -6, 2.1, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// клюв
s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.1, -7, 2.1, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e5a052\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// крыло
s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 0.4, -4.9, 2.45, -Math.PI, Math.PI/6, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// хвост
s = MathPanelExt.QuadroEqu.DrawLine(-4.4, 2.6, -3.9, 2.9, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// ноги
s = MathPanelExt.QuadroEqu.DrawLine(-5, 1.5, -5, 1, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-5.2, 1.3, -5.2, 1, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Птица2
// тело
s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -2.2, 4.3, -Math.PI, Math.PI/8, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 1, -1.8, 4.5, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
// клюв
s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.1, -1.3, 5.1, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e5a052\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
// крыло
s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 0.4, -2.5, 4.3, -Math.PI, Math.PI/4, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
// хвост
s = MathPanelExt.QuadroEqu.DrawLine(-3.1, 4.25, -3.7, 4.0, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
// ноги
s = MathPanelExt.QuadroEqu.DrawLine(-2.4, 3.5, -2.4, 3, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-2.1, 3.3, -2.1, 3, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Птица3
// тело
s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 0.7, 2, -Math.PI, Math.PI/8, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 1, 1.1, 2.2, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// клюв
s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.1, 1.6, 2.8, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e5a052\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// крыло
s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 0.4, 0.3, 2, -Math.PI, Math.PI/4, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// хвост
s = MathPanelExt.QuadroEqu.DrawLine(-0.6, 1.5, -0.2, 1.95, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// ноги
s = MathPanelExt.QuadroEqu.DrawLine(0.6, 1.2, 0.6, 0.5, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0.9, 1, 0.9, 0.5, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Птица4
// тело
s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1.5, 4.3, 3.1, -Math.PI, -Math.PI/6, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 1, 3.9, 3, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#5283e5\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.1, 3.4, 3.6, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e5a052\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// крыло
s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 0.8, 4.6, 2.8, -Math.PI, -Math.PI/6, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// хвост
s = MathPanelExt.QuadroEqu.DrawLine(5.0, 2.3, 5.6, 2.5, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 5, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// ноги
s = MathPanelExt.QuadroEqu.DrawLine(4, 1.7, 4, 1.3, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(4.3, 1.9, 4.3, 1.3, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#4635a6\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);