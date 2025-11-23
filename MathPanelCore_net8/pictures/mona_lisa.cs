// Авдоченко Ангелина, 01491@list.ru
// Мона Лиза (Джоконда)

Dynamo.Console("Mona Lisa");

DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 400, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 400, \"hei\": 600, \"second\": \"{2}\" }}";

// Фон - небо
var s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(0, 450, 400, 600, true));
string s10 = string.Format(sOptFormat, "#A9E5E1", "1", "undefined");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Фон - небо -> деревья слева
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(100, 50, 50, 450, 0, Math.PI * 2, 64, opt));
s10 = string.Format(sOptFormat, "#4E7231", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Фон - небо -> деревья справа
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 50, 300, 450, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#669640", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Фон - вода
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(0, 300, 400, 450, true));
s10 = string.Format(sOptFormat, "#4EC5CA", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Фон - блики на воде -> серия слева

for (int i = 0; i < 14; i++)
{
    double x = -50 + i * 8;
    double y = 315 + i * 10;
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(90, 8, x, y, 0, Math.PI * 2, 32, opt));
    s10 = string.Format(sOptFormat, "#1FADB2", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}

// Фон - блики на воде -> серия слева 2

for (int i = 0; i < 8; i++)
{
    double x = -20 + i * 18;
    double y = 335 + i * 15;
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(60 + i * 3, 4, x, y, 0, Math.PI * 2, 32, opt));
    s10 = string.Format(sOptFormat, "#1D9296", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}

// Фон - блики на воде -> серия справа

for (int i = 0; i < 8; i++)
{
    double x = 210 + i * 15;
    double y = 445 - i * 15;
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 6, x, y, 0, Math.PI * 2, 32, opt));
    s10 = string.Format(sOptFormat, "#1FADB2", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}

// Фон - блики на воде -> серия справа 2

for (int i = 0; i < 6; i++)
{
    double x = 210 + i * 18;
    double y = 335 + i * 15;
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(60 + i * 3, 4, x, y, 0, Math.PI * 2, 32, opt));
    s10 = string.Format(sOptFormat, "#1D9296", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}

// Фон - светлые блики на воде

for (int i = 0; i < 4; i++)
{
    double x = 210 + i * 15;
    double y = 305 + i * 30;
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 6, x, y, 0, Math.PI * 2, 32, opt));
    s10 = string.Format(sOptFormat, "#A8EDF0", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}
for (int i = 0; i < 4; i++)
{
    double x = 210 - i * 25;
    double y = 315 + i * 30;
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 6, x, y, 0, Math.PI * 2, 32, opt));
    s10 = string.Format(sOptFormat, "#A8EDF0", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}

// Фон - земля
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 400, 300, true));
s10 = string.Format(sOptFormat, "#93CA4E", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Фон - деревья
for (int i = 0; i < 6; i++)
{
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(7, 100 - i * 5, i * 15, 230, 0, Math.PI, 32, opt));
    s10 = string.Format(sOptFormat, "#015D33", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}
for (int i = 0; i < 6; i++)
{
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(17, 150 - i * 15, 400 - i * 25, 180, 0, Math.PI, 32, opt));
    s10 = string.Format(sOptFormat, "#819759", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}
for (int i = 0; i < 6; i++)
{
    s9 = "";
    s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 70 - i * 5, 400 - i * 20, 180, 0, Math.PI, 32, opt));
    s10 = string.Format(sOptFormat, "#808000", "3", "1");
    s10 += ", \"data\":[" + s9 + "]}";
    Dynamo.SceneJson(s10);
}

// Фон - участки дороги
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 30, 0, 230, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#9D9E42", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(90, 50, 400, 152, 0, Math.PI, 32, opt));
s10 = string.Format(sOptFormat, "#9D9E42", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Фон - земля2
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(0, 0, 400, 150, true));
s10 = string.Format(sOptFormat, "#4C472A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Волосы сзади
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(120, 150, 280, 450, true));
s10 = string.Format(sOptFormat, "#4A1F17", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - основа
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(100, 0, 300, 280, true));
s10 = string.Format(sOptFormat, "#0D2F31", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - левый рукав до плеча
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 150, 100, 135, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#0D2F31", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - правый рукав до плеча
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(70, 150, 300, 135, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#0D2F31", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - дополнение к платью
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(110, 150, 290, 270, true));
s10 = string.Format(sOptFormat, "#164D50", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - правый рукав до кисти
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(125, 9, 295, 46, true));
s10 = string.Format(sOptFormat, "#4F6952", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - правый рукав (скругление на локте)
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(17.5, 17.5, 295, 9 + 17.5, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#4F6952", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Тело - правая кисть
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(40, 20, 140, 25, -Math.PI * 1.4, -Math.PI * 0.3, 34, opt));
s10 = string.Format(sOptFormat, "#DFCDA5", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - левый рукав до кисти
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRotatedRect(95, 26, 160, 35, 5, true));
s10 = string.Format(sOptFormat, "#65866A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Платье - левый рукав (скругление на локте)
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawEllipse(17.5, 17.5, 98, 45, Math.PI * 0.5, Math.PI * 1.6, 32, opt));
s10 = string.Format(sOptFormat, "#65866A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Тело - левая кисть
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(40, 25, 240, 50, -Math.PI * 0.79, Math.PI * 0.4, 64, opt));
s10 = string.Format(sOptFormat, "#DFCDA5", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Тело - вырез платья
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(90, 35, 200, 270, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#DFCDA5", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Волосы - прядь слева
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(117, 280, 140, 467, true));
s10 = string.Format(sOptFormat, "#69342B", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Тело - шея
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(160, 280, 240, 340, true));
s10 = string.Format(sOptFormat, "#DFCDA5", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Тело - голова
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(80, 100, 200, 420, 0, Math.PI * 2, 32, opt));
s10 = string.Format(sOptFormat, "#E6D2AD", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Волосы - сверху
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(90, 100, 200, 430, Math.PI * 0.12, Math.PI * 0.88, 64, opt));
s10 = string.Format(sOptFormat, "#69342B", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Волосы - прядь справа
s9 = "";
s9 += ("" + MathPanelExt.QuadroEqu.DrawRect(260, 280, 283, 467, true));
s10 = string.Format(sOptFormat, "#69342B", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - губы
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(18, 8, 190, 360, -Math.PI, 0, 64, opt));
s10 = string.Format(sOptFormat, "#D5AE9A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - левое верхнее веко
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 15, 155, 420, 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#C3B697", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - левый глаз
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(15, 10, 155, 420, 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#E5F5F0", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - левая радужка
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(6, 10, 158, 420, -Math.PI * 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#21453A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - левое нижнее веко
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(137, 419, 173, 419));
s10 = string.Format(sOptFormat, "#C3B697", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - правое верхнее веко
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(20, 15, 225, 420, 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#C3B697", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - правый глаз
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(15, 10, 225, 420, 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#E5F5F0", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - правая радужка
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawEllipse(6, 10, 228, 420, 0, Math.PI, 64, opt));
s10 = string.Format(sOptFormat, "#21453A", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - правое нижнее веко
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(208, 419, 242, 419));
s10 = string.Format(sOptFormat, "#C3B697", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - правая бровь
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(200, 440, 245, 440));
s10 = string.Format(sOptFormat, "#CCB285", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - левая бровь
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(180, 440, 135, 440));
s10 = string.Format(sOptFormat, "#CCB285", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - нос
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawRect(180, 380, 200, 440, true));
s10 = string.Format(sOptFormat, "#D6C0A4", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);

// Лицо - линия носа (снизу)
s9 = "";
s9 = ("" + MathPanelExt.QuadroEqu.DrawLine(180, 379, 200, 379));
s10 = string.Format(sOptFormat, "#D6C0AF", "3", "1");
s10 += ", \"data\":[" + s9 + "]}";
Dynamo.SceneJson(s10);