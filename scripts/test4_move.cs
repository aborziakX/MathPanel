Dynamo.Console("test4_move");
Dynamo.SceneClear();

int id = Dynamo.PhobNew(1, 2, 3);
Dynamo.Console(id.ToString());
var hz0 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz0.ToString());

id = Dynamo.PhobNew(21, 22, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "size", "20");
Dynamo.PhobAttrSet(id, "clr", "#ffaa00");
Dynamo.PhobAttrSet(id, "sty", "dots");
var hz1 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz1.ToString());

id = Dynamo.PhobNew(11, 12, 3);
Dynamo.PhobAttrSet(id, "sty", "tri");
Dynamo.PhobAttrSet(id, "text", "triangle");
Dynamo.Console(id.ToString());
var hz2 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz2.ToString());

id = Dynamo.PhobNew(31, 18, 3);
Dynamo.Console(id.ToString());
var hz3 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz3.ToString());

Dynamo.SceneDraw();

    System.Threading.Thread.Sleep(1000); //Мы ждем 1 секунду в даном потоке   
    var hz = Dynamo.PhobGet(id) as Phob;
    Dynamo.Console("значение 1");
    hz.y -= 5;
    Dynamo.SceneDraw();
    System.Threading.Thread.Sleep(1000); //Мы ждем 1 секунду в даном потоке   
    Dynamo.Console("значение 2");
    hz.y -= 5;
    Dynamo.SceneDraw();
    System.Threading.Thread.Sleep(1000); //Мы ждем 1 секунду в даном потоке 
    Dynamo.Console("C# 3.0, привет из 2017!");
    hz.y -= 5;
    Dynamo.SceneDraw();
