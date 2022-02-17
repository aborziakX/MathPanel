//histo_cubes
Dynamo.ConsoleClear();
Dynamo.Console("histo_cubes");
Dynamo.SceneClear();
double DT = 0.050; //шаг в секундах
double time = 0;

//мостовая
int id3 = Dynamo.PhobNew(20, 20, -1.0);
var hz3 = Dynamo.PhobGet(id3) as Phob;
Cube cub3 = new Cube(2, "Gray");
cub3.scaleX = 20;
cub3.scaleY = 20;
cub3.scaleZ = 0.5;
hz3.Shape = cub3;

string[] colors = { "Red", "Orange", "Yellow", "Green", "Blue", "Pink", "Gray" };
//создать объект типа "генератор случайных чисел"
Random rnd = new Random();

double[] zArr = {18.8839873852599,
8.32065322823853,
18.9990373882461,
5.41470267130747,
18.8865752885521,
17.7038089268393,
17.4836813181097 };

for (int i = 0; i < colors.Length; i++) //rows
{
    double y = 0.5 + 4.5 * i;
    for (int j = 0; j < 10; j++) //cols
    {   //cube
        if (j % 2 == 1) continue;
        //if(j < 8) continue;
        double x = 0.5 + 4.0 * j;
        double z = rnd.NextDouble() * 20;
        int id4 = Dynamo.PhobNew(x, y, z);
        var hz4 = Dynamo.PhobGet(id4) as Phob;
        Cube cub4 = new Cube(2, colors[i]);
        //cub4.iFill = 3;//1-грани, 2-ребра, 3-все
        cub4.Divide(2);
        cub4.scaleX = 1;
        cub4.scaleY = 1;
        cub4.scaleZ = z;
        hz4.Shape = cub4;
    }
}


//ящик сцены
Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 40);
Box bx = Dynamo.SceneBox;
Dynamo.XRotor = -0.47;
Dynamo.YRotor = -0.3;
Dynamo.ZRotor = -0.5;
Dynamo.SceneDrawShape(true);

for (int i = 0; i < 1000; i++)
{
    Dynamo.SceneDrawShape(true);
    if (i % 20 == 0)
    {
        Dynamo.Console("time=" + time + ", " + Dynamo.XRotor + ", " + Dynamo.YRotor + ", " + Dynamo.ZRotor);
    }
    System.Threading.Thread.Sleep(50);

    time += DT;

    string resp = Dynamo.KeyConsole;
    if (resp == "Q")
    {
        break;
    }
}
