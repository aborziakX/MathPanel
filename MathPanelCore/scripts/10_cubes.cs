//test10_cubes
            Dynamo.Console("test10_cubes");
            Dynamo.SceneClear();

            int id;
            id = Dynamo.PhobNew(-10, 0, 0);
            //Dynamo.Console(id.ToString());
            var hz = Dynamo.PhobGet(id) as Phob;
            //Dynamo.Console(hz.ToString());
            Dynamo.PhobAttrSet(id, "text", "XRotor");
            Cube cub = new Cube(5, "Yellow");
            cub.bDrawNorm = true;
            cub.scaleZ = 2;
            cub.scaleX = 0.5;
            cub.XRotor = 0.3;
            hz.Shape = cub;

            id = Dynamo.PhobNew(0, 0, 0);
            //Dynamo.Console(id.ToString());
            var hz2 = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "text", "YRotor");
            //Dynamo.Console(hz2.ToString());
            Cube cub2 = new Cube(5, "Green");
            cub2.scaleZ = 2;
            cub2.scaleX = 0.5;
            cub2.YRotor = 0.3;
            hz2.Shape = cub2;
            
            id = Dynamo.PhobNew(10, 0, 0);
            //Dynamo.Console(id.ToString());
            var hz3 = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "text", "ZRotor");
            //Dynamo.Console(hz3.ToString());
            Cube cub3 = new Cube(5, "Blue");
            cub3.scaleZ = 2;
            cub3.scaleX = 0.5;
            cub3.ZRotor = 0.3;
            hz3.Shape = cub3;

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(true, false);

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                Dynamo.SceneDrawShape(true, false);// i % 40 == 0);
                DateTime dt2 = DateTime.Now;
                TimeSpan diff = dt2 - dt1;
                if (i % 40 == 0)
                {
                    Dynamo.Console("ms=" + diff.Milliseconds);
                    //Dynamo.Console(cub.ToString());
                    //Dynamo.Console(cub2.ToString());
                    //Dynamo.Console(cub3.ToString());
                }
                System.Threading.Thread.Sleep(50);
            }
