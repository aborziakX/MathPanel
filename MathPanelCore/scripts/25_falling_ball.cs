//test25_falling_ball
            const double g = 9.8; //ускорение
            const double DT = 0.050; //шаг в секундах
            Dynamo.ConsoleClear();
            Dynamo.Console("test25_falling_ball");
            //регистрация на сервере
            //Dynamo.Scriplet("test25_falling_ball", "Падающий мяч 2");
            Dynamo.SceneClear();
            //мяч
            int id = Dynamo.PhobNew(20, 20, 40);
            var hz = Dynamo.PhobGet(id) as Phob;
            Sphere cub = new Sphere(2, "Red", 12);
            hz.Shape = cub;

            //башня
            int id2 = Dynamo.PhobNew(14, 20, 18);
            var hz2 = Dynamo.PhobGet(id2) as Phob;
            Cylinder cub2 = new Cylinder(2, "Yellow", 12);
            cub2.scaleX = 5;
            cub2.scaleY = 5;
            cub2.scaleZ = 18;
            hz2.Shape = cub2;

            //мостовая
            int id3 = Dynamo.PhobNew(20, 20, -1.5);
            var hz3 = Dynamo.PhobGet(id3) as Phob;
            Cube cub3 = new Cube(2, "Gray");
            cub3.scaleX = 20;
            cub3.scaleY = 20;
            cub3.scaleZ = 0.5;
            hz3.Shape = cub3;

            Dynamo.SceneBox = new Box(0, 40, 0, 40, 0, 40);
            Box bx = Dynamo.SceneBox;
            Dynamo.SceneDrawShape(true);
            int iTotalRes = 0;

            for (int i = 0; i < 1000; i++)
            {
                Dynamo.SceneDrawShape(true);
                if (i % 5 == 0 && iTotalRes < 100)
                {   //сохранить изображение на сервере
                    //Dynamo.SaveScripresult();
                    iTotalRes++;
                }
                System.Threading.Thread.Sleep(50);

                hz.z += hz.v_z * DT; //падаем
                if (hz.z < bx.z0 + hz.radius)
                {   //удар о землю
                    hz.z = bx.z0 + hz.radius;
                    hz.v_z = -hz.v_z;
                }
                hz.v_z -= g * DT;//сила тяжести
            }
