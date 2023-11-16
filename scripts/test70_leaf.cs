//test70_leaf
Dynamo.SceneClear();
int id = Dynamo.PhobNew(0, 0, 0);
Dynamo.Console(id.ToString());
var hz = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz.ToString());

var t4 = new Leaf(15, "Yellow", 8, 3);
//t4.bDrawNorm = true;
hz.Shape = t4;

Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
Dynamo.SceneDrawShape(true, true);

for(int i = 0; i< 1000; i++)
{
    Dynamo.SceneDrawShape(true);
    if(i % 40 == 0) 
    {
        double ix, iy, iz;
    }
    System.Threading.Thread.Sleep(50); 
}
