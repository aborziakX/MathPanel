//alina zhuk; alya.griboed @gmail.com
//Галактика
Dynamo.SceneClear();
Dynamo.Console("galaxy");

Random rand = new Random(); //генератор случайных чисел
int n = 0;
for (double u = 0; u <40; u+=0.25)
{
    for (double v = -3.4; v < 3.4; v+=0.1)
    {      
        int id = Dynamo.PhobNew(u*Math.Cos(u)-5, u*Math.Sin(u)-5, u*0.5);
        if( n % 3 == 1 )
            Dynamo.PhobAttrSet(id, "sty", "dots");
        else if (n % 3 == 2) Dynamo.PhobAttrSet(id, "sty", "tri");
        Phob ph = Dynamo.PhobGet(id);
        ph.radius = rand.NextDouble() * 0.33;

        if (n % 4 == 0)
            Dynamo.PhobAttrSet(id, "clr", "#FFB6C1");
        else if (n % 4 == 1) Dynamo.PhobAttrSet(id, "clr", "#20B2AA");
        else if (n % 4 == 2) Dynamo.PhobAttrSet(id, "clr", "#FF7F50");
        else Dynamo.PhobAttrSet(id, "clr", " #FF00FF");

        n++;        
    }
}
for (double u = 0; u <40; u+=0.4)
{
    for (double v = -3.4; v < 3.4; v+=0.1)
    {       
        int id = Dynamo.PhobNew(u*Math.Cos(u)+5, u*Math.Sin(u)+5, -10-u*0.4);
        if( n % 3 == 1 )
            Dynamo.PhobAttrSet(id, "sty", "tri");
        else if (n % 3 == 2) Dynamo.PhobAttrSet(id, "sty", "dots");
        Phob ph = Dynamo.PhobGet(id);
        ph.radius = rand.NextDouble() * 0.33;

        if (n % 4 == 0)
            Dynamo.PhobAttrSet(id, "clr", "#BDB76B");
        else if (n % 4 == 1) Dynamo.PhobAttrSet(id, "clr", "#F4A460");
        else if (n % 4 == 2) Dynamo.PhobAttrSet(id, "clr", "#696969");
        else Dynamo.PhobAttrSet(id, "clr", " #B0E0E6");

        n++;       
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
