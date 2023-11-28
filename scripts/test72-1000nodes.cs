Dynamo.SceneClear();
Dynamo.Console("test72_1000_nodes");

int n = 0;
for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < 10; j++)
    {
        for (int k = 0; k < 10; k++)
        {
            int id = Dynamo.PhobNew(-10 + 2 * i, -10 + 2 * j, -4 * k);
            if( n % 3 == 1 )
                Dynamo.PhobAttrSet(id, "sty", "dots");
            else if (n % 3 == 2) Dynamo.PhobAttrSet(id, "sty", "tri");
            Dynamo.PhobAttrSet(id, "size", "0.01");

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
    Dynamo.SceneDrawShape(true);
    string resp = Dynamo.KeyConsole;
    if (resp == "Q")
    {
        break;
    }
    System.Threading.Thread.Sleep(50);
}
