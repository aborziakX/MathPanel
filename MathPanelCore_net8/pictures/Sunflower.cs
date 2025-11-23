//Кармацкий Д.Н.

// Настройки для рисования
DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

//Задний фон
string s = MathPanelExt.QuadroEqu.DrawRect(-10, -10, 10, 10, true);
string s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFD1DC\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"_second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);


s = MathPanelExt.QuadroEqu.DrawRotatedRect(0.8, -10, 8.5, 1.5, 90, true);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#008000 \", \"sty\": \"line\", \"size\":0, \"lnw\": 2, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);



//Лепестки
s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 5.0, 0, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FBB117\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 4.619397662556434 , 1.913417161825449, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFE87C\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);


s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 3.5355339059327378, 3.5355339059327378, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FDD017\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 1.9134171618254492 , 4.619397662556434, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FDD017\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 0, 5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FBB117\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -1.9134171618254485 , 4.619397662556434, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFE87C\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -3.5355339059327373 , 3.5355339059327378, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FBB117\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -4.619397662556434 , 1.9134171618254494, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FDD017\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -5, 0, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFE87C\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -4.619397662556434 , -1.9134171618254483, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FBB117\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -3.5355339059327386 , -3.5355339059327373, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FDD017\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, -1.9134171618254516 , -4.619397662556432, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFE87C\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 0, -5, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FDD017\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 1.91341716182545 , -4.619397662556433, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FFE87C\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 3.535533905932737 , -3.5355339059327386, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FBB117\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(1, 1, 4.619397662556432 , -1.913417161825452, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #FDD017\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
//голова
s = MathPanelExt.QuadroEqu.DrawEllipse(4.6, 4.7, 0,0, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #9F5727\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);
//глаза
s = MathPanelExt.QuadroEqu.DrawEllipse(0.45, 1, 1.5 , 2, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(0.45, 1, -1.5 , 2, -3.15, Math.PI, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \" #000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 1, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

//улыбка
s = MathPanelExt.QuadroEqu.DrawEllipse(2, 0.33, 0, -1, 0, Math.PI * 2, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawEllipse(2, 0.33, 0, -0.9, 0, Math.PI * 2, 64, opt);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#9F5727\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawRotatedRect (-1.96, -0.71, -0.7, 0.001, 65);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);

s = MathPanelExt.QuadroEqu.DrawRotatedRect (1.85, -0.71, -0.7, 0.001, 125);
s10 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#000000\", \"sty\": \"line\", \"size\":0, \"lnw\": 3, \"fontsize\":24, \"wid\": 800, \"hei\": 800, \"second\":1 }";
s10 += ", \"data\":[" + s + "]}";
Dynamo.SceneJson(s10, true);