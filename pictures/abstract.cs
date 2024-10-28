//Автор: Слепов Артемий guzeevguzno@gmail.com

// Определим палитру цветов
string[] colors = {
    "#ff6f61", // красный
    "#6b5b95", // фиолетовый
    "#88b04b", // зеленый
    "#f7cac9", // розовый
    "#92a8d1", // синий
    "#f4eb89", // желтый
    "#034f84", // темно-синий
    "#d65076", // малиновый
};

DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

// Фон
string bg = MathPanelExt.QuadroEqu.DrawLine(-10, 0, 10, 0);
string bgParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#ffffff\", \"sty\": \"line\", \"size\":0, \"lnw\": 300, \"wid\": 800, \"hei\": 800, \"second\":1 }";
bgParam += ", \"data\":[" + bg + "]}";
Dynamo.SceneJson(bgParam, true);

// Большой круг в центре
string bigCircle = MathPanelExt.QuadroEqu.DrawEllipse(6, 6, 0, 0, 0, Math.PI * 2, 64);
string bigCircleParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + colors[0] + "\", \"sty\": \"line\", \"lnw\": 200, \"wid\": 800, \"hei\": 800, \"second\":1 }";
bigCircleParam += ", \"data\":[" + bigCircle + "]}";
Dynamo.SceneJson(bigCircleParam, true);

// Маленькие круги
for (int i = 0; i < 5; i++)
{
    double size = 1.5 + i * 0.3;
    double x = Math.Cos(i * Math.PI / 3) * 5;
    double y = Math.Sin(i * Math.PI / 3) * 5;

    string smallCircle = MathPanelExt.QuadroEqu.DrawEllipse(size, size, x, y, 0, Math.PI * 2, 64);
    string smallCircleParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + colors[(i + 1) % colors.Length] + "\", \"sty\": \"line\", \"lnw\": 100, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    smallCircleParam += ", \"data\":[" + smallCircle + "]}";
    Dynamo.SceneJson(smallCircleParam, true);
}

// Полукруги на окружности
for (int i = 0; i < 4; i++)
{
    double startAngle = i * Math.PI / 2;
    string semiCircle = MathPanelExt.QuadroEqu.DrawEllipse(3, 3, 0, 0, startAngle, startAngle + Math.PI, 64);
    string semiCircleParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + colors[(i + 2) % colors.Length] + "\", \"sty\": \"line\", \"lnw\": 150, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    semiCircleParam += ", \"data\":[" + semiCircle + "]}";
    Dynamo.SceneJson(semiCircleParam, true);
}

// Пересекающиеся линии
for (int i = 0; i < 6; i++)
{
    double x1 = -8 + i * 2.5;
    double x2 = 8 - i * 2.5;
    double y1 = 10;
    double y2 = -10;

    string line = MathPanelExt.QuadroEqu.DrawLine(x1, y1, x2, y2);
    string lineParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + colors[i % colors.Length] + "\", \"sty\": \"line\", \"lnw\": 10, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    lineParam += ", \"data\":[" + line + "]}";
    Dynamo.SceneJson(lineParam, true);
}

// Фигуры типа звезд
for (int i = 0; i < 3; i++)
{
    string star = MathPanelExt.QuadroEqu.DrawStar(1.5, 0.5, i * 3 - 3, i * 3 - 2, 6, opt);
    string starParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + colors[(i + 4) % colors.Length] + "\", \"sty\": \"line\", \"lnw\": 15, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    starParam += ", \"data\":[" + star + "]}";
    Dynamo.SceneJson(starParam, true);
}

// Дополнительные мелкие точки
for (int i = 0; i < 10; i++)
{
    double x = -8 + i;
    double y = Math.Sin(i) * 5;

    string point = MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.3, x, y, 0, Math.PI * 2, 64);
    string pointParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"" + colors[i % colors.Length] + "\", \"sty\": \"line\", \"lnw\": 25, \"wid\": 800, \"hei\": 800, \"second\":1 }";
    pointParam += ", \"data\":[" + point + "]}";
    Dynamo.SceneJson(pointParam, true);
}

Dynamo.Console("Abstract art finished!");