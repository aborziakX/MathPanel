Dynamo.SceneClear();
Dynamo.Console("test72_1000_nodes");

Random rand = new Random(); //генератор случайных чисел
int n = 0;
for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < 10; j++)
    {
        for (int k = 0; k < 10; k++)
        {
            int id = Dynamo.PhobNew(-20 + 4 * i, -20 + 4 * j, -20 + 4 * k);
            if( n % 7 == 1 )
                Dynamo.PhobAttrSet(id, "sty", "dots");
            else if (n % 7 == 2) Dynamo.PhobAttrSet(id, "sty", "tri");
            Phob ph = Dynamo.PhobGet(id);
            ph.radius = rand.NextDouble() * 0.6;

            if (n % 4 == 0)
                Dynamo.PhobAttrSet(id, "clr", "#00aa00");
            else if (n % 4 == 1) Dynamo.PhobAttrSet(id, "clr", "#ffaa00");
            else if (n % 4 == 2) Dynamo.PhobAttrSet(id, "clr", "#00ffff");
            else Dynamo.PhobAttrSet(id, "clr", "#0000ff");

            n++;
        }
    }
}
Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
Dynamo.BAxes = true;
Dynamo.BDrawBox = true; 
Dynamo.SceneDrawShape(true, true);

for (int i = 0; i < 1000; i++)
{
    DateTime dt1 = DateTime.Now;
    Dynamo.SceneDrawShape(true);
    DateTime dt2 = DateTime.Now;
    TimeSpan diff = dt2 - dt1;
    int ms = (int)diff.Milliseconds;
    if (i % 40 == 0)
    {
        Dynamo.Console("ms=" + ms);
    }
    string resp = Dynamo.KeyConsole;
    if (resp == "Q")
    {
        break;
    }
    System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
}
