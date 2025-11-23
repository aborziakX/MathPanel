//Михаил Меркулов , merkulovm12@gmail.com
Dynamo.Console("Room");

// Настройки для рисования
DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

// Обои в горошек
string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, true);
string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #f4d5f4\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

for (double i = -9.5; i < 10; i++){
 for (double j = -2.5; j < 10; j++){
  s = MathPanelExt.QuadroEqu.DrawEllipse(0.05, 0.05, i, j, -Math.PI, Math.PI, 64, opt);
  s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
  s10 += ", \"data\":[" + s + "]}";
  Dynamo.SceneJson(s10, true);
  j++;
 }
 i++;
}


// Окно
s = MathPanelExt.QuadroEqu.DrawRect(-9, 1, -3, 9, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ccffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Рама окна
s = MathPanelExt.QuadroEqu.DrawLine(-9.1, 1, -9.1, 9, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#794c16\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-2.9, 1, -2.9, 9, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#794c16\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-6, 1, -6, 9, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#794c16\", \"sty\": \"line\", \"size\":0, \"lnw\": 12, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-2.8, 1, -9.2, 1, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#794c16\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-2.9, 5, -9.1, 5, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#794c16\", \"sty\": \"line\", \"size\":0, \"lnw\": 12, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(-2.8, 9, -9.2, 9, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#794c16\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Пол
s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, -3, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#c36714\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);


// Часы
s = MathPanelExt.QuadroEqu.DrawEllipse(2, 2, 1, 7, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#313131\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1.7, 1.7, 1, 7, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#efe4bc\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Циферблат
string text = "12";
s = MathPanelExt.QuadroEqu.DrawText(0.65, 7.6, text);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":22, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

text = "3";
s = MathPanelExt.QuadroEqu.DrawText(2.2, 6.25, text);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":22, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

text = "6";
s = MathPanelExt.QuadroEqu.DrawText(0.75, 5, text);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":22, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

text = "9";
s = MathPanelExt.QuadroEqu.DrawText(-0.6, 6.25, text);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":22, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Стрелки
s = MathPanelExt.QuadroEqu.DrawEllipse(0.2, 0.2, 1, 7, -Math.PI, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#C0C0C0\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":20, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(1, 7, 1, 8.1, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#C0C0C0\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(1, 7, 1.8, 7, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#C0C0C0\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Ковёр
s = MathPanelExt.QuadroEqu.DrawRect(-8, -9, 9, -4, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#e9574b\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Телевизор
s = MathPanelExt.QuadroEqu.DrawRect(0, -2.5, 7, 2.5, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawRect(0.5, -2, 6.5, 2, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#021ffd\", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(3.5, 2.3, 5.5, 4.5, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(3.5, 2.3, 1.5, 4.5, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 8, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(1.5, -2.5, 1.5, -3.7, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#202020\", \"sty\": \"line\", \"size\":0, \"lnw\": 30, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawLine(5.5, -2.5, 5.5, -3.7, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#202020\", \"sty\": \"line\", \"size\":0, \"lnw\": 30, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

// Экран телевизора
text = "Нет подключения";
s = MathPanelExt.QuadroEqu.DrawText(1, 0, text);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":22, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

text = ":(";
s = MathPanelExt.QuadroEqu.DrawText(3, -1, text);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff \", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":22, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);