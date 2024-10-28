//Матвей Кузнецов, mk-kuz2003@yandex.ru
Dynamo.Console("it_is_cat_on_watermelon!");

// Настройки для рисования
DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

// Фон
string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, true);
string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #DEB887\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Арбуз
s = MathPanelExt.QuadroEqu.DrawEllipse(5, 4, 0, -1, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#006400\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Полосы на арбузе
for (int i = -5; i <= 0; i++) {
    s = MathPanelExt.QuadroEqu.DrawEllipse(5, 0+0.8*i, 0, -1, 0, Math.PI, 64, opt);
    s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#2E8B57\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    s10 += ", \"data\":[" + s + "]}";
    Dynamo.SceneJson(s10, true);
	
    s = MathPanelExt.QuadroEqu.DrawEllipse(5, 0+0.8*i+0.4, 0, -1, 0, Math.PI, 64, opt);
    s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#006400\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    s10 += ", \"data\":[" + s + "]}";
    Dynamo.SceneJson(s10, true); }

for (int i = 5; i >= 0; i--) {
    s = MathPanelExt.QuadroEqu.DrawEllipse(5, 0+0.8*i+0.4, 0, -1, 0, Math.PI, 64, opt);
    s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#006400\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    s10 += ", \"data\":[" + s + "]}";
    Dynamo.SceneJson(s10, true);
	
    s = MathPanelExt.QuadroEqu.DrawEllipse(5, 0+0.8*i, 0, -1, 0, Math.PI, 64, opt);
    s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#2E8B57\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    s10 += ", \"data\":[" + s + "]}";
    Dynamo.SceneJson(s10, true); }

// Лапка левая для котика правая для нас (должна быть за телом)
s = MathPanelExt.QuadroEqu.DrawEllipse(0.75, 3, 4, 0, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B5B5B5\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.75, 0.5, 4, -2.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

//отдельные пальчики на лапке
s = MathPanelExt.QuadroEqu.DrawLine(4, -2.75, 4, -3.1);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(4.35, -2.7, 4.45, -2.93);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(4- 0.35, -2.7, 4 - 0.45, -2.93);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Тело кота
s = MathPanelExt.QuadroEqu.DrawEllipse(4.3, 3.2, 0, 2, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#969696\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

//Лапки правые для котика левые для нас (должна быть перед телом)
s = MathPanelExt.QuadroEqu.DrawEllipse(0.95, 3.5, 0.5, 0, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B5B5B5\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.83, 0.55, 0.5, -3, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



s = MathPanelExt.QuadroEqu.DrawEllipse(0.85, 2.5, -4, 0, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#B5B5B5\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.65, 0.45, -4, -2.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Пушок или как называется
s = MathPanelExt.QuadroEqu.DrawEllipse(3, 2.5, 2, 3.25, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#eeeeee\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

//отдельные пальчики на этих лапках
s = MathPanelExt.QuadroEqu.DrawLine(0.5, -3.3, 0.5, -3.6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0.5 + 0.38, -3.3, 0.5 + 0.48, -3.5);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0.5 - 0.38, -3.3, 0.5 - 0.48, -3.5);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

			
			
s = MathPanelExt.QuadroEqu.DrawLine(-4, -2.7, -4, -3);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-4 + 0.32, -2.71, -4 + 0.42, -2.9);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-4 - 0.32, -2.71, -4 - 0.42, -2.9);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);


// Голова кота
s = MathPanelExt.QuadroEqu.DrawEllipse(3, 2.5, 2, 4, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#C9C9C9\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



// Глаза
s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 0.5, 3, 4.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.5, 0.5, 1, 4.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#FFFFFF\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



s = MathPanelExt.QuadroEqu.DrawEllipse(0.45, 0.45, 3, 4.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#1faee9\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.45, 0.45, 1, 4.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#1faee9\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



s = MathPanelExt.QuadroEqu.DrawEllipse(0.25, 0.25, 3, 4.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.25, 0.25, 1, 4.5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);


// Усы
for (int i = -1; i <= 1; i++) {
s = MathPanelExt.QuadroEqu.DrawLine(2.2, 3.5 + 0.15*i, 3.2, 3.5+0.4*i);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #eeeeee\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(1.8, 3.5 + 0.15*i, 0.8, 3.5+0.4*i);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #eeeeee\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
}

// Носик
s = MathPanelExt.QuadroEqu.DrawLine(2.2, 3.7, 1.8, 3.7);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(2.2, 3.7, 2, 3.5);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(1.8, 3.7, 2, 3.5);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Мордочка
s = MathPanelExt.QuadroEqu.DrawLine(2, 3.5, 2, 3);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(2, 3, 1.85, 2.85);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
			
s = MathPanelExt.QuadroEqu.DrawLine(2, 3, 2.15, 2.85);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Ушки
s = MathPanelExt.QuadroEqu.DrawLine(4-0.2, 5.6+0.2, 4.1-0.2, 7-0.2);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(3.2, 6, 4, 7-0.2);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(4, 6, 3, 6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(3.8, 6.5, 3.5, 6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



s = MathPanelExt.QuadroEqu.DrawLine(4, 5.6, 4.1, 7);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A9A9A9\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(2.8, 6, 4.1, 7);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A9A9A9\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(4.1, 5.6, 2.8, 6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A9A9A9\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



s = MathPanelExt.QuadroEqu.DrawLine(0.2, 5.6+0.2, 0.1, 7-0.2);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0.8, 6, 0, 7-0.2);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0, 5.9, 1, 6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0.2, 6.5, 0.5, 6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



s = MathPanelExt.QuadroEqu.DrawLine(0, 5.6, -0.1, 7);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A9A9A9\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(1.2, 6, -0.1, 7);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A9A9A9\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(0, 5.6, 1.2, 6);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#A9A9A9\", \"sty\": \"line\", \"size\":0, \"lnw\": 10, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);