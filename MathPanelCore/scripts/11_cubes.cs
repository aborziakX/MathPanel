//test11_cubes
Dynamo.SceneClear();
int id = Dynamo.PhobNew(11,11, 10);
Dynamo.Console(id.ToString());
var hz = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz.ToString());

Cube cub = new Cube(20, "Yellow");
cub.bDrawNorm = true;
hz.Shape = cub;

id = Dynamo.PhobNew(41, 10, 5);
Dynamo.Console(id.ToString());
var hz2 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz2.ToString());

Cube cub2 = new Cube(10, "Green");
//cub2.bDrawNorm = true;
hz2.Shape = cub2;

id = Dynamo.PhobNew(10, 60, 5);
Dynamo.Console(id.ToString());
var hz3 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz3.ToString());

Cube cub3 = new Cube(10, "Blue");
//cub3.bDrawNorm = true;
cub3.ZRotor = 0.7;
hz3.Shape = cub3;

id = Dynamo.PhobNew(80, 70, 5);
var hz4 = Dynamo.PhobGet(id) as Phob;
Cube cub4 = new Cube(10, "Red");
//cub3.bDrawNorm = true;
cub4.ZRotor = -0.2;
hz4.Shape = cub4;

Dynamo.SceneBox = new Box(0, 120, 0, 80, 0, 40);
Dynamo.XBoXTrans = -60;
Dynamo.YBoXTrans = -40;
Dynamo.ZBoXTrans = -40;
Dynamo.CameraZ = 100;

Dynamo.YRotor = -45 * Math.PI / 180.0;
Dynamo.XRotor = -75 * Math.PI / 180.0;
Dynamo.SceneDrawShape(true, true);

Dynamo.SceneSave(@"scenes\sc1.txt");
Dynamo.SceneLoad(@"scenes\sc1.txt");

int[] ids = Dynamo.SceneIds();
for(int i = 0; i < ids.Length; i++)
{
    var obj = Dynamo.PhobGet(ids[i]);
    if( obj.Shape != null )
        Dynamo.Console(obj.Shape.ToString());
}
Dynamo.SceneDrawShape(true, true);
return;

for(int i = 0; i< 1000; i++)
{
    DateTime dt1 = DateTime.Now;
    Dynamo.SceneDrawShape(true, false);// i % 40 == 0);
    DateTime dt2 = DateTime.Now;
    TimeSpan diff = dt2 - dt1;
    if(i % 40 == 0) 
    {
        Dynamo.Console("ms=" + diff.Milliseconds);
        //Dynamo.Console(cub.ToString());
        //Dynamo.Console(cub2.ToString());
        //Dynamo.Console(cub3.ToString());
    }
    System.Threading.Thread.Sleep(50); 
}
