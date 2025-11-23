//test35_facet_bitmap
public void Execute()
{
    Dynamo.Console("test35_facet_bitmap");
    //Dynamo.Scriplet("test35_facet_bitmap", "Просто грань");
    Dynamo.SceneClear();
    var bm = new BitmapSimple(@"images\world200.png");

    int id = Dynamo.PhobNew(-0, 0, 0);
    var hz = Dynamo.PhobGet(id) as Phob;
    var t1 = new OneFacet(new Vec3(-10, 0, 6), new Vec3(10, 0, 6), new Vec3(10, 0, 16), new Vec3(-10, 0, 16), "Yellow", false, false);
    //разбить единственную грань из 4-х вершин на мелкие
    //предполагаем v1 - нижняя слева, далее против часовой
    t1.DivideIfOne4(100, 50, bm);
    hz.Shape = t1;
    //t1.SetBitmapPlane(bm);
    //t1.iFill = 2;

    /* id = Dynamo.PhobNew(-0, 0, -10);
        hz = Dynamo.PhobGet(id) as Phob;
        t1 = new OneFacet(new Vec3(-5, -7, -5), new Vec3(5, -7, -5),
            new Vec3(5, -7, 5), new Vec3(-5, -7, 5), "Red");
        t1.Divide(5);
        t1.SetBitmapPlane(bm);
        hz.Shape = t1;*/

    Dynamo.Console("total fac=" + Dynamo.SceneFacets());

    Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
    Dynamo.SceneDrawShape(true, false);

    for (int i = 0; i < 1000; i++)
    {
        DateTime dt1 = DateTime.Now;
        Dynamo.SceneDrawShape(true, false);
        DateTime dt2 = DateTime.Now;
        TimeSpan diff = dt2 - dt1;
        int ms = (int)diff.TotalMilliseconds;
        if (i % 40 == 0)
        {
            Dynamo.Console("ms=" + ms);
        }
        if (i == 0 || Dynamo.KeyConsole == "S")
        {
            //Dynamo.SaveScripresult();
        }
        System.Threading.Thread.Sleep(ms < 50 ? 50 - ms : 1);
    }
}
Execute();
