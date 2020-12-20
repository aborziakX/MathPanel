//test6_move_brown
Dynamo.Scriplet("test1", "Броуновское движение шаров");

Dynamo.SceneClear();
//создаем сцену
int SPEED = 1;
var rnd = new Random();
int id = Dynamo.PhobNew(1, 2, 3);
Dynamo.Console(id.ToString());
var hz0 = Dynamo.PhobGet(id) as Phob;
hz0.v_x = (rnd.NextDouble() - 0.5) * SPEED;
hz0.v_y = (rnd.NextDouble() - 0.5) * SPEED;
Dynamo.Console(hz0.ToString());

id = Dynamo.PhobNew(11, 12, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "clr", "#ffaa00");
var hz1 = Dynamo.PhobGet(id) as Phob;
hz1.v_x = (rnd.NextDouble() - 0.5) * SPEED;
hz1.v_y = (rnd.NextDouble() - 0.5) * SPEED;
hz1.radius = 2;
hz1.mass = 2;
Dynamo.Console(hz1.ToString());

id = Dynamo.PhobNew(21, 22, 3);
Dynamo.Console(id.ToString());
Dynamo.PhobAttrSet(id, "clr", "#00aaff");
var hz2 = Dynamo.PhobGet(id) as Phob;
hz2.v_x = (rnd.NextDouble() - 0.5) * SPEED;
hz2.v_y = (rnd.NextDouble() - 0.5) * SPEED;
hz2.radius = 2;
hz2.mass = 2;
Dynamo.Console(hz2.ToString());

id = Dynamo.PhobNew(31, 18, 3);
Dynamo.Console(id.ToString());
var hz3 = Dynamo.PhobGet(id) as Phob;
hz3.v_x = (rnd.NextDouble() - 0.5) * SPEED;
hz3.v_y = (rnd.NextDouble() - 0.5) * SPEED;
Dynamo.Console(hz3.ToString());

id = Dynamo.PhobNew(37, 12, 3);
Dynamo.PhobAttrSet(id, "clr", "#00aa00");
Dynamo.Console(id.ToString());
var hz4 = Dynamo.PhobGet(id) as Phob;
hz4.v_x = (rnd.NextDouble() - 0.5) * SPEED;
hz4.v_y = (rnd.NextDouble() - 0.5) * SPEED;
hz4.radius = 3;
hz4.mass = 3;
Dynamo.Console(hz4.ToString());

Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 20);
Dynamo.SceneDraw(true);

Box bx = Dynamo.SceneBox;
int[] arr = Dynamo.SceneIds();
double dx, dy, dz;
for( int i = 0; i < 400; i++)
{
    for(int j = 0; j<arr.Length; j++)
    {
        var hz = Dynamo.PhobGet(arr[j]) as Phob;
        hz.x += hz.v_x;
        hz.y += hz.v_y;
        hz.z += hz.v_z;

        //check border
        if(hz.x < bx.x0 + hz.radius)
        {
            hz.x = bx.x0 + hz.radius;
            hz.v_x = - hz.v_x;
        }
        else if(hz.x > bx.x1 - hz.radius)
        {
            hz.x = bx.x1 - hz.radius;
            hz.v_x = - hz.v_x;
        }

        if(hz.y<bx.y0 + hz.radius)
        {
            hz.y = bx.y0 + hz.radius;
            hz.v_y = - hz.v_y;
        }
        else if(hz.y > bx.y1 - hz.radius)
        {
            hz.y = bx.y1 - hz.radius;
            hz.v_y = - hz.v_y;
        }

        if(hz.z<bx.z0 + hz.radius)
        {
            hz.z = bx.z0 + hz.radius;
            hz.v_z = - hz.v_z;
        }
        else if(hz.z > bx.z1 - hz.radius)
        {
            hz.z = bx.z1 - hz.radius;
            hz.v_z = - hz.v_z;
        }

        //check other particles
        for(int k = j + 1; k<arr.Length; k++)
        {
            var hz_k = Dynamo.PhobGet(arr[k]) as Phob;
            bool bHit = hz.Collision(hz_k);
            if( bHit )
            {   
                hz.Hit(hz_k);
                break;//??
            }
        }
    }
    Dynamo.SceneDraw();
    if( i % 40 == 0) 
    {
        double ix, iy, iz;
        Dynamo.SceneImpulse(out ix, out iy, out iz);
        Dynamo.Console(Dynamo.SceneEnergy().ToString() + ", ix=" + ix + ", iy=" + iy + ", iz=" + iz);
    }
    if(i % 5 == 0) Dynamo.SaveScripresult();
    System.Threading.Thread.Sleep(50); //Мы ждем 1 секунду в даном потоке   
}
