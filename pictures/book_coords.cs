//красный шар справа
int id = Dynamo.PhobNew(15, 0, 0);
Dynamo.PhobAttrSet(id, "clr", "#ff0000");
var hz0 = Dynamo.PhobGet(id) as Phob;
hz0.radius = 1;

//желтый шар вверху
id = Dynamo.PhobNew(0, 15, 0);
Dynamo.PhobAttrSet(id, "clr", "#ffff00");
hz0 = Dynamo.PhobGet(id) as Phob;
hz0.radius = 1;

//зеленый шар к нам
id = Dynamo.PhobNew(0, 0, 8);
Dynamo.PhobAttrSet(id, "clr", "#00ff00");
hz0 = Dynamo.PhobGet(id) as Phob;
hz0.radius = 1;

//белый шар по центру
id = Dynamo.PhobNew(0, 0, -0);
Dynamo.PhobAttrSet(id, "clr", "#ffffff");
var hz1 = Dynamo.PhobGet(id) as Phob;
hz1.radius = 0.5;

Dynamo.SceneBox = new Box(-20, 20, -20, 20, -10, 10);
Dynamo.XBoXTrans = 0;
Dynamo.YBoXTrans = 0;
Dynamo.ZBoXTrans = -100;
Dynamo.CameraZ = 100;
Dynamo.ZRotor = 0.9; //вращаем сначала ось Z против часовой стрелки
Dynamo.XRotor = -1.9; //потом вращаем ось X по часовой стрелке
Dynamo.YRotor = 0.0;
//Dynamo.BOldCode = true;
Dynamo.SceneDraw(); //рисовать сцену

//return;

Box bx = Dynamo.SceneBox;
int[] arr = Dynamo.SceneIds();

double dx, dy, dz;
for( int i = 0; i < 400; i++)
{
    //Dynamo.SceneDraw();
    //System.Threading.Thread.Sleep(50); //Мы ждем в даном потоке   
}
