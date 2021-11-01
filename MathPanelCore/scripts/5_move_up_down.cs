//test5_move_up_down
Dynamo.Console("test5_move_up_down");
Dynamo.SceneClear();
//создаем сцену
int id = Dynamo.PhobNew(1, 2, 3);
Dynamo.Console(id.ToString());
var hz0 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz0.ToString());

id = Dynamo.PhobNew(11, 12, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "size", "20");
Dynamo.PhobAttrSet(id, "color", "#ffaa00");
var hz1 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz1.ToString());

id = Dynamo.PhobNew(21, 22, 3);
Dynamo.Console(id.ToString());
var hz2 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz2.ToString());

id = Dynamo.PhobNew(31, 18, 3);
Dynamo.Console(id.ToString());
var hz3 = Dynamo.PhobGet(id) as Phob;
Dynamo.Console(hz3.ToString());

Dynamo.SceneBox=new Box(0, 40, 0, 40, 0, 5);
Dynamo.SceneDraw(true);

    bool bDown = true;
    var hz = Dynamo.PhobGet(id) as Phob;
    for( int i = 0; i < 100; i++)
    {
        if(bDown)
        {
            hz.y -= 1;
            if(hz.y < 1 ) bDown = false;
        }
        else
        {
            hz.y += 1;
            if(hz.y >= 40 ) bDown = true;
        }
        Dynamo.SceneDraw();
        System.Threading.Thread.Sleep(100); //Мы ждем 1 секунду в даном потоке   
    }
