Dynamo.SceneClear();
Dynamo.Console("test75-load-image-3d.cs");

//Dynamo.LoadImage("iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==");
var fname = @"c:\temp\b64.jpg";
var bm = new BitmapSimple(800, 600, System.Drawing.Color.White, System.Drawing.Color.Blue, false);
bm.Save(fname);

var bts = System.IO.File.ReadAllBytes(fname);
var s = System.Convert.ToBase64String(bts);
//Dynamo.Console(s);
Dynamo.LoadImage(s, "jpg", "img1");

System.Threading.Thread.Sleep(150);

int id = Dynamo.PhobNew(1, 2, 3);
Dynamo.Console(id.ToString());
var hz0 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz0.ToString());

id = Dynamo.PhobNew(21, 22, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "size", "20");
Dynamo.PhobAttrSet(id, "clr", "#ffaa00");
Dynamo.PhobAttrSet(id, "sty", "dots");
var hz1 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz1.ToString());

id = Dynamo.PhobNew(11, 12, 3);
Dynamo.PhobAttrSet(id, "sty", "tri");
Dynamo.PhobAttrSet(id, "text", "triangle");
Dynamo.Console(id.ToString());
var hz2 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz2.ToString());

id = Dynamo.PhobNew(31, 18, 3);
Dynamo.Console(id.ToString());
var hz3 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz3.ToString());

Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
Dynamo.ZBoXTrans = -30;
Dynamo.CameraZ = 40;
Dynamo.BAxes = true;
Dynamo.BDrawBox = true;

Dynamo.SetImgBg("img1");
Dynamo.SceneDrawShape(true, true); 

for (int i = 0; i < 1000; i++)
{
    Dynamo.SceneDrawShape(true);
    if (i % 40 == 0)
    {
        double ix, iy, iz;
        //Dynamo.SceneImpulse(out ix, out iy, out iz);
        //Dynamo.Console(Dynamo.SceneEnergy().ToString());
    }
    string resp = Dynamo.KeyConsole;
    if (resp == "Q")
    {
        break;
    }
    System.Threading.Thread.Sleep(50);
}
