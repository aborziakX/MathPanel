//test9_cube
Dynamo.SceneClear();
int id = Dynamo.PhobNew(0, 0, 0);
Dynamo.Console(id.ToString());
var hz = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz.ToString());

Cube cub = new Cube(20, "Yellow");
cub.bDrawNorm = true;
hz.Shape = cub;

Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
Dynamo.ZBoXTrans = -30;
Dynamo.CameraZ = 40;
Dynamo.SceneDrawShape(true, true);
Dynamo.Console(Dynamo.ScreenJson);

for(int i = 0; i< 1000; i++)
{
    Dynamo.SceneDrawShape(true);
    if(i % 40 == 0) 
    {
        double ix, iy, iz;
//Dynamo.SceneImpulse(out ix, out iy, out iz);
Dynamo.Console(Dynamo.SceneEnergy().ToString());
    }
    System.Threading.Thread.Sleep(50); 
}
