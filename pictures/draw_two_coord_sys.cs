//draw 2 coordinate systems
Dynamo.SceneClear();
int id = 0;

//first cube
id = Dynamo.PhobNew(41, 10, 5);
Dynamo.Console(id.ToString());
var hz2 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz2.ToString());

Cube cub2 = new Cube(10, "Green");
//cub2.bDrawNorm = true;
hz2.Shape = cub2;


//second cube
id = Dynamo.PhobNew(10, 60, 5);
Dynamo.Console(id.ToString());
var hz3 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz3.ToString());

Cube cub3 = new Cube(10, "Blue");
//cub3.bDrawNorm = true;
cub3.ZRotor = 0.7;
hz3.Shape = cub3;

//мои оси
//X
id = Dynamo.PhobNew(-0, -0, -0);
var hz = Dynamo.PhobGet(id) as Phob;
Dynamo.PhobAttrSet(id, "clr", "#ffff00");
Dynamo.PhobAttrSet(id, "lnw", "1");
Dynamo.PhobAttrSet(id, "txt2", "xC");
Dynamo.PhobAttrSet(id, "txt1", "");
Dynamo.PhobAttrSet(id, "fontsize", "16");
hz.bDrawAsLine = true;
hz.p1.Copy(-120, -20, 120);
hz.p2.Copy(40, -35, 120);

//Y
id = Dynamo.PhobNew(-0, -0, -0);
hz = Dynamo.PhobGet(id) as Phob;
Dynamo.PhobAttrSet(id, "clr", "#ffff00");
Dynamo.PhobAttrSet(id, "lnw", "1");
Dynamo.PhobAttrSet(id, "txt2", "yC");
Dynamo.PhobAttrSet(id, "txt1", "O");
Dynamo.PhobAttrSet(id, "fontsize", "16");
hz.bDrawAsLine = true;
hz.p1.Copy(-120, -20, 120);
hz.p2.Copy(-80, 155, 120);

//Z
id = Dynamo.PhobNew(-0, -0, -0);
hz = Dynamo.PhobGet(id) as Phob;
Dynamo.PhobAttrSet(id, "clr", "#ffff00");
Dynamo.PhobAttrSet(id, "lnw", "1");
Dynamo.PhobAttrSet(id, "txt2", "zC");
Dynamo.PhobAttrSet(id, "txt1", "O");
Dynamo.PhobAttrSet(id, "fontsize", "16");
hz.bDrawAsLine = true;
hz.p1.Copy(-120, -20, 120);
hz.p2.Copy(-120, -20, 280);

//camera
id = Dynamo.PhobNew(-120, -20, 200);
hz = Dynamo.PhobGet(id) as Phob;
hz.radius = 5;
Dynamo.PhobAttrSet(id, "sty", "dots");
Dynamo.PhobAttrSet(id, "txt", "zCam");
Dynamo.PhobAttrSet(id, "fontsize", "24");

//connect centers
id = Dynamo.PhobNew(-0, -0, -0);
hz = Dynamo.PhobGet(id) as Phob;
Dynamo.PhobAttrSet(id, "clr", "#ffffff");
Dynamo.PhobAttrSet(id, "lnw", "1");
Dynamo.PhobAttrSet(id, "txt2", "O");
Dynamo.PhobAttrSet(id, "txt1", "");
Dynamo.PhobAttrSet(id, "fontsize", "16");
hz.bDrawAsLine = true;
hz.p1.Copy(-120, -20, 120);
hz.p2.Copy(-0, -0, 0);


//box
Dynamo.SceneBox = new Box(0, 120, 0, 120, 0, 120);
Dynamo.XBoXTrans = 60;
Dynamo.YBoXTrans = 40;
Dynamo.ZBoXTrans = -340;
Dynamo.CameraZ = 100;

Dynamo.YRotor = -15 * Math.PI / 180.0;
Dynamo.XRotor = 15 * Math.PI / 180.0;

Dynamo.SceneDrawShape(true, true);

