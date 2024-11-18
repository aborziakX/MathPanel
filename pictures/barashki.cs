// Мирзоева Ясмин; yasmin.cowork@gmail.com
// 2D визуализация (Барашки на лугу)

Dynamo.Console("Sheep!");

DrawOpt opt = new DrawOpt();
opt.bFill = true;
opt.sty = "line";

double x, y;
int width = 900, height = 600;

string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 900, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 900, \"hei\": 600, \"second\": \"{2}\" }}";

// Axis
var figure_text = (MathPanelExt.QuadroEqu.DrawRect(0, 0, width, height, true));

// Background
string data_append = string.Format(sOptFormat, "#00bfff", "1", "undefined");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append);

// Background2
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(450, 100, 450, 100, 0, Math.PI, 64, opt));
data_append = string.Format(sOptFormat, "#2dad2a", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawRect(0, 0, 900, 100, true)); 
data_append = string.Format(sOptFormat, "#2dad2a", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

//sun
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 60, 800, 520, 0, 2 * Math.PI, 64, opt));
data_append = string.Format(sOptFormat, "#ffcc33", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);


//clouds
x = 500;
y = 400;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 400;
y = 540;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 150;
y = 350;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 250;
y = 500;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-40, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 550;
y = 480;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 700;
y = 350;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 800;
y = 481;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

x = 70;
y = 550;
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x-30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(30, 30, x+30, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

// grass
int w = 0;
for (int i = 50; i < 850; i+=75)
{
    for (int j = 20; j < 170; j+=40)
    {
        if (Math.Sin(i*j) > 0.3)
        {
            x = i;
            y = j;
            figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(15, 20, x, y+10, 3*Math.PI/4, 3*Math.PI/2, 64, opt));
            data_append = string.Format(sOptFormat, "#145912", "3", "1");
            data_append += ", \"data\":[" + figure_text + "]}";
            Dynamo.SceneJson(data_append, true);
            figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(15, 20, x, y+10, -Math.PI/2, Math.PI/4, 64, opt));
            data_append = string.Format(sOptFormat, "#145912", "3", "1");
            data_append += ", \"data\":[" + figure_text + "]}";
            Dynamo.SceneJson(data_append, true);
            figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(5, 20, x, y+10, Math.PI/2, 3*Math.PI/2, 64, opt));
            data_append = string.Format(sOptFormat, "#145912", "3", "1");
            data_append += ", \"data\":[" + figure_text + "]}";
            Dynamo.SceneJson(data_append, true);
            figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(5, 20, x, y+10, -Math.PI/2, Math.PI/2, 64, opt));
            data_append = string.Format(sOptFormat, "#145912", "3", "1");
            data_append += ", \"data\":[" + figure_text + "]}";
            Dynamo.SceneJson(data_append, true);
        }
    }
}


// sheep
x = 800;
y = 100;
//legs
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x-20, y-60, x-30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x+20, y-60, x+30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

//tail body head
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(15, 15, x+60, y+10, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x-55, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 10, x-55, y+40, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x-35, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x-75, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x-65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x-45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x-65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x-45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(8, 4, x-55, y+5, Math.PI, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

// sheep
x = 550;
y = 250;
//legs
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x-20, y-60, x-30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x+20, y-60, x+30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

//tail body head
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(15, 15, x+60, y+10, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x-55, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 10, x-55, y+40, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x-35, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x-75, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x-65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x-45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x-65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x-45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(8, 3, x-55, y+5, Math.PI, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

// inverted sheep
x = 150;
y = 200;
//legs
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x-20, y-60, x-30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x+20, y-60, x+30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

//tail body head
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(15, 15, x-60, y+10, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+55, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 10, x+55, y+40, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x+35, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x+75, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x+65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x+45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x+65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x+45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(8, 3, x+55, y+5, Math.PI, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

// inverted sheep
x = 350;
y = 80;
//legs
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x-20, y-60, x-30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawRect(x+20, y-60, x+30, y, true)); 
data_append = string.Format(sOptFormat, "#262120", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

//tail body head
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(15, 15, x-60, y+10, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(60, 30, x, y, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(20, 30, x+55, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 10, x+55, y+40, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x+35, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(10, 12, x+75, y+35, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#6b514c", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x+65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(3, 3, x+45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x+65, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(1, 1, x+45, y+20, 0, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#000000", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);
figure_text = (MathPanelExt.QuadroEqu.DrawEllipse(8, 3, x+55, y+5, Math.PI, Math.PI*2, 64, opt));
data_append = string.Format(sOptFormat, "#ffffff", "3", "1");
data_append += ", \"data\":[" + figure_text + "]}";
Dynamo.SceneJson(data_append, true);

