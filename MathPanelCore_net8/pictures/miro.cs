//Автор: Александр Диков dsasha0102@gmail.com
Dynamo.Console("Miro started!");

string[] clr =
{
    "#cd1b27", "#447cb9", "#21853d", "#f4dc3c", "#242225"
};
//   red,       blue,      green,     yellow,    black

double rad = 4;
int i;

// Reference
System.Drawing.Color[] clrs =
{
    System.Drawing.Color.White
};
//Dynamo.SceneClear();

var s1 = MathPanelExt.QuadroEqu.DrawBitmap(1, 1, clrs, 0, 0);
string s2 = "{\"options\":{\"x0\": 1, \"x1\": 1, \"y0\": 1, \"y1\": 1, \"clr\": \"#000000\", \"sty\": \"dots\", \"size\":40, \"lnw\": 3, \"wid\": 800, \"hei\": 1100, \"img\":\"https://www.le-flamant-rose.org/miro/photos/naissance_jour.jpg\" }";
s2 += ", \"data\":[" + s1 + "]}";
//Dynamo.SceneJson(s2);
//имеется задержка в рисовании img, поэтому заснуть и повторить
System.Threading.Thread.Sleep(300);
//Dynamo.SceneJson(s2);

//Background
string BG1 = MathPanelExt.QuadroEqu.DrawLine(2.5, 12, 11, -10);
string BGParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#21853d\", \"sty\": \"line\", \"size\":10, \"lnw\": 550, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string BGData1 = BGParam1 + ", \"data\":[" + BG1 + "]}";
Dynamo.SceneJson(BGData1, true);

string BG2 = MathPanelExt.QuadroEqu.DrawLine(-12, 12, -2.5, -12);
string BGParam2 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#447cb9\", \"sty\": \"line\", \"size\":10, \"lnw\": 550, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string BGData2 = BGParam2 + ", \"data\":[" + BG2 + "]}";
Dynamo.SceneJson(BGData2, true);

// Bottom
string bottomFill = MathPanelExt.QuadroEqu.DrawEllipse(13, 8, -1, -2, Math.PI * 4.22/3, Math.PI * 4.85/3, 64);
string bottomFillParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 200, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
bottomFillParam += ", \"data\":[" + bottomFill + "]}";
Dynamo.SceneJson(bottomFillParam, true);

//Ellipse
string headFill = MathPanelExt.QuadroEqu.DrawEllipse(4, 2.1, 0, 4, Math.PI/3.4, Math.PI*2/3, 64);
string headFillParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#f4dc3c\", \"sty\": \"line\", \"size\":0, \"lnw\": 300, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
headFillParam += ", \"data\":[" + headFill + "]}";
Dynamo.SceneJson(headFillParam, true);

string ellipseFill = MathPanelExt.QuadroEqu.DrawEllipse(2, 4.1, -1, -2, 0, Math.PI * 2.1, 64);
string ellipseFillParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#cd1b27\", \"sty\": \"line\", \"size\":0, \"lnw\": 300, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
ellipseFillParam += ", \"data\":[" + ellipseFill + "]}";
Dynamo.SceneJson(ellipseFillParam, true);

string ellipse = MathPanelExt.QuadroEqu.DrawEllipse(6, 7, -1, -2, 0, Math.PI * 2, 64);
string ellipseParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 20, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
ellipseParam += ", \"data\":[" + ellipse + "]}";
Dynamo.SceneJson(ellipseParam, true);

//Pionts
string piont1= MathPanelExt.QuadroEqu.DrawEllipse(0.45, 0.4, -9, 0, 0, Math.PI * 2, 64);
string piontParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 40, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
piontParam1 += ", \"data\":[" + piont1+ "]}";
Dynamo.SceneJson(piontParam1, true);

string piont2= MathPanelExt.QuadroEqu.DrawEllipse(0.45, 0.4, -9, -9, 0, Math.PI * 2.1, 64);
string piontParam2 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 40, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
piontParam2 += ", \"data\":[" + piont2+ "]}";
Dynamo.SceneJson(piontParam2, true);

string line = MathPanelExt.QuadroEqu.DrawLine(-9, 0, -9, -9);
string lineParam = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 10, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string lineData = lineParam + ", \"data\":[" + line + "]}";
Dynamo.SceneJson(lineData, true);

string piont3= MathPanelExt.QuadroEqu.DrawEllipse(0.45, 0.4, 8, -3, 0, Math.PI * 2.1, 64);
string piontParam3 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 40, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
piontParam3 += ", \"data\":[" + piont3+ "]}";
Dynamo.SceneJson(piontParam3, true);

string piont4= MathPanelExt.QuadroEqu.DrawEllipse(0.3, 0.25, 3.5, 6, 0, Math.PI * 2.1, 64);
string piontParam4 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 35, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
piontParam4 += ", \"data\":[" + piont4+ "]}";
Dynamo.SceneJson(piontParam4, true);

string piont5= MathPanelExt.QuadroEqu.DrawEllipse(0.25, 0.2, 0.45, 8, 0, Math.PI * 2.1, 64);
string piontParam5 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 25, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
piontParam5 += ", \"data\":[" + piont5+ "]}";
Dynamo.SceneJson(piontParam5, true);

// Snowflake
string Snowflake1 = MathPanelExt.QuadroEqu.DrawLine(-8, 2, -8, 4);
string SnowflakeParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 5, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string SnowflakeData1 = SnowflakeParam1 + ", \"data\":[" + Snowflake1 + "]}";
Dynamo.SceneJson(SnowflakeData1, true);

string Snowflake2 = MathPanelExt.QuadroEqu.DrawLine(-7, 3, -9, 3);
string SnowflakeParam2 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 5, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string SnowflakeData2 = SnowflakeParam2 + ", \"data\":[" + Snowflake2 + "]}";
Dynamo.SceneJson(SnowflakeData2, true);

string Snowflake3 = MathPanelExt.QuadroEqu.DrawLine(-7.2, 3.8, -8.8, 2.2);
string SnowflakeParam3 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 5, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string SnowflakeData3 = SnowflakeParam3 + ", \"data\":[" + Snowflake3 + "]}";
Dynamo.SceneJson(SnowflakeData3, true);

string Snowflake4 = MathPanelExt.QuadroEqu.DrawLine(-8.8, 3.8, -7.2, 2.2);
string SnowflakeParam4 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 5, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string SnowflakeData4 = SnowflakeParam4 + ", \"data\":[" + Snowflake4 + "]}";
Dynamo.SceneJson(SnowflakeData4, true);

// Lines
string line1 = MathPanelExt.QuadroEqu.DrawLine(0, 3, -5, 10);
string lineParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 20, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string lineData1 = lineParam1 + ", \"data\":[" + line1 + "]}";
Dynamo.SceneJson(lineData1, true);

string line2 = MathPanelExt.QuadroEqu.DrawLine(-3, -6, -5, -10);
string lineParam2 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 20, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string lineData2 = lineParam2 + ", \"data\":[" + line2 + "]}";
Dynamo.SceneJson(lineData2, true);

string line3 = MathPanelExt.QuadroEqu.DrawLine(2, -6, 4, -10);
string lineParam3 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 20, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string lineData3 = lineParam3 + ", \"data\":[" + line3 + "]}";
Dynamo.SceneJson(lineData3, true);

string line4 = MathPanelExt.QuadroEqu.DrawLine(1, 3, 6, 10);
string lineParam4 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 20, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string lineData4 = lineParam4 + ", \"data\":[" + line4 + "]}";
Dynamo.SceneJson(lineData4, true);

string line5 = MathPanelExt.QuadroEqu.DrawLine(0.5, 8, 2, 10);
string lineParam5 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":10, \"lnw\": 20, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
string lineData5 = lineParam5 + ", \"data\":[" + line5 + "]}";
Dynamo.SceneJson(lineData5, true);

//Parabolic
string parapolic1 = MathPanelExt.QuadroEqu.DrawEllipse(8, 5, 1, 4, -Math.PI/4, Math.PI * 0.9, 64);
string parapolicParam1 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 15, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
parapolicParam1 += ", \"data\":[" + parapolic1 + "]}";
Dynamo.SceneJson(parapolicParam1, true);

string parapolic2 = MathPanelExt.QuadroEqu.DrawEllipse(1, 0.7, -6.5, 6.2, Math.PI * 1.1, Math.PI * 2.1, 64);
string parapolicParam2 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 15, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
parapolicParam2 += ", \"data\":[" + parapolic2 + "]}";
Dynamo.SceneJson(parapolicParam2, true);

string parapolic3 = MathPanelExt.QuadroEqu.DrawEllipse(2, 2, 0.5, 8, Math.PI * 1.3, Math.PI * 2.3, 64);
string parapolicParam3 = "{\"options\":{\"x0\": -10, \"x1\": 10, \"y0\": -10, \"y1\": 10, \"clr\": \"#242225\", \"sty\": \"line\", \"size\":0, \"lnw\": 15, \"fontsize\":24, \"wid\": 800, \"hei\": 1100, \"second\":1 }";
parapolicParam3 += ", \"data\":[" + parapolic3 + "]}";
Dynamo.SceneJson(parapolicParam3, true);