//book_cube_split
Dynamo.SceneClear();
int id = Dynamo.PhobNew(0, 0, 0);
Dynamo.Console(id.ToString());
var hz = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz.ToString());

Cube cub = new Cube(20, "Yellow");
cub.iFill = 2;
cub.Divide(0);
//cub.bDrawNorm = true;
hz.Shape = cub;

Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
Dynamo.BDrawBox = false;
Dynamo.ZBoXTrans = 0.5;
Dynamo.XBoXTrans = 0.4;
Dynamo.CameraZ = 50;
Dynamo.BAxes = false;
Dynamo.SceneDrawShape(true);

for(int i = 0; i< 1000; i++)
{
    Dynamo.SceneDrawShape(true);
    System.Threading.Thread.Sleep(50); 
}
