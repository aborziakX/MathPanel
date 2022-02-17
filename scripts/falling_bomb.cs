//falling_bomb
Dynamo.ConsoleClear();
Dynamo.Console("falling_bomb");
Dynamo.SceneClear();
double DT = 0.050; //шаг в секундах
double time = 0;

//bomb
bool bGrow = false;
double g = 9.8; //ускорение силы тяжести
double dSize = 5.0;
int idBomb = Dynamo.PhobNew(20, 20, 40);
var phBomp = Dynamo.PhobGet(idBomb) as Phob;
Sphere bomb = new Sphere(2, "Red", 16);
phBomp.Shape = bomb;
bomb.scaleX = dSize;
bomb.scaleY = dSize;
bomb.scaleZ = dSize;

//мостовая
int id3 = Dynamo.PhobNew(20, 20, -1.0);
var hz3 = Dynamo.PhobGet(id3) as Phob;
Cube cub3 = new Cube(2, "Gray");
cub3.scaleX = 20;
cub3.scaleY = 20;
cub3.scaleZ = 0.5;
hz3.Shape = cub3;

//башня
int id2 = Dynamo.PhobNew(5, 25, 9);
var hz2 = Dynamo.PhobGet(id2) as Phob;
Cylinder cub2 = new Cylinder(2, "Yellow", 12);
cub2.scaleX = 2;
cub2.scaleY = 2;
cub2.scaleZ = 9;
hz2.Shape = cub2;

//дом1
int id4 = Dynamo.PhobNew(15, 10, 4);
var hz4 = Dynamo.PhobGet(id4) as Phob;
Cube cub4 = new Cube(2, "Green");
cub4.scaleX = 1;
cub4.scaleY = 1;
cub4.scaleZ = 4;
hz4.Shape = cub4;

//дом2
id4 = Dynamo.PhobNew(25, 10, 4);
hz4 = Dynamo.PhobGet(id4) as Phob;
cub4 = new Cube(2, "Blue");
cub4.scaleX = 2;
cub4.scaleY = 1;
cub4.scaleZ = 4;
hz4.Shape = cub4;

//дом3
id4 = Dynamo.PhobNew(5, 10, 4);
hz4 = Dynamo.PhobGet(id4) as Phob;
cub4 = new Cube(2, "Red");
cub4.scaleX = 1;
cub4.scaleY = 2;
cub4.scaleZ = 4;
hz4.Shape = cub4;

//ящик сцены
Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 40);
Box bx = Dynamo.SceneBox;
Dynamo.XRotor = -Math.PI / 2;
Dynamo.YRotor = -0.1;
Dynamo.ZRotor = -0.1;
Dynamo.SceneDrawShape(true);

for (int i = 0; i < 1000; i++)
{
    Dynamo.SceneDrawShape(true);
    if (i % 20 == 0)
    {
        Dynamo.Console("time=" + time);
    }
    System.Threading.Thread.Sleep(50);

    time += DT;

    Dynamo.UpdatePosition(DT);

    Dictionary<int, Phob> dicPhob = Dynamo.ScenePhobs();
    foreach (var pair in dicPhob)
    {
        var hz = pair.Value;
        if( hz.z < 0 && hz.v_z != 0 )
        {   //вернуть из под земли
            hz.z = 0;
            hz.v_x = 0;
            hz.v_y = 0;
            hz.v_z = 0;
        }
        if (hz.v_x != 0 || hz.v_y != 0 || hz.v_z != 0) 
            hz.v_z -= g * DT;//сила тяжести
    }

    if (bGrow && dSize < 50)
    {
        dSize += 0.3;
        bomb.scaleX = dSize;
        bomb.scaleY = dSize;
        bomb.scaleZ = dSize;
    }
    else if( bomb != null )
    {   //взрыв, осколки летят
        Dynamo.Explode(phBomp, 10.0);
        bomb = null;
    }

    string resp = Dynamo.KeyConsole;
    if (resp == "Q")
    {
        break;
    }
}
