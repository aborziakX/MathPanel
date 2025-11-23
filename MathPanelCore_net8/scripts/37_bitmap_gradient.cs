//test37_bitmap_gradient
Dynamo.Console("test37_bitmap_gradient");
            //Dynamo.Scriplet("test37_bitmap_gradient", "Просто gradient");
            Dynamo.SceneClear();

            System.Drawing.Color[] colors = { 
                System.Drawing.Color.Red,
                System.Drawing.Color.Orange,
                System.Drawing.Color.Yellow,
                System.Drawing.Color.Green,
                System.Drawing.Color.Blue,
                /*System.Drawing.Color.Magenta,
                System.Drawing.Color.Cyan,
                System.Drawing.Color.White,
                System.Drawing.Color.Green,*/
            };

            //var bm = new BitmapSimple(20, 20, colors);
            var bm = new BitmapSimple(200, 200, System.Drawing.Color.White, System.Drawing.Color.Blue, false);
            bm.Randomize(10000, 10);
            bm.Save(@"scenes\white_blue_200.png");

            int id = Dynamo.PhobNew(-0, 0, 15);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            //разбить единственную грань из 4-х вершин на мелкие
            //предполагаем v1 - нижняя слева, далее против часовой
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //second
            bm = new BitmapSimple(200, 200, System.Drawing.Color.DarkBlue, System.Drawing.Color.Blue, false);
            bm.Randomize(10000, 10);
            bm.Save(@"scenes\dark_blue_200.png");

            id = Dynamo.PhobNew(-0, 0, 5);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //third
            Tuple<System.Drawing.Color, int, int>[] focus =
            {
                new Tuple<System.Drawing.Color, int, int>(System.Drawing.Color.Red, 0, 0),
                new Tuple<System.Drawing.Color, int, int>(System.Drawing.Color.Green, 100, 100),
                new Tuple<System.Drawing.Color, int, int>(System.Drawing.Color.Blue, 199, 199),
            };
            bm = new BitmapSimple(200, 200, focus);
            bm.Randomize(10000, 10);
            bm.Save(@"scenes\red_green_blue_200.png");

            id = Dynamo.PhobNew(-0, 0, -5);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;

            //forth
            Tuple<System.Drawing.Color, int, int>[] focus2 =
            {
                new Tuple<System.Drawing.Color, int, int>(System.Drawing.Color.Red, 0, 0),
                new Tuple<System.Drawing.Color, int, int>(System.Drawing.Color.Green, 100, 100),
                new Tuple<System.Drawing.Color, int, int>(System.Drawing.Color.White, 199, 199),
            };
            bm = new BitmapSimple(200, 200, focus2);
            bm.Randomize(100000, 10);
            bm.Save(@"scenes\red_green_white_rand_200.png");

            id = Dynamo.PhobNew(-0, 0, -15);
            hz = Dynamo.PhobGet(id) as Phob;
            t1 = new OneFacet(new Vec3(-10, 0, -5), new Vec3(10, 0, -5), new Vec3(10, 0, 5), new Vec3(-10, 0, 5), "Yellow", false, false);
            t1.DivideIfOne4(20, 20, bm);
            hz.Shape = t1;


            Dynamo.Console("total fac=" + Dynamo.SceneFacets());
            Dynamo.Console("total area=" + Dynamo.SceneFacetsArea());

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(false, false);

            for (int i = 0; i < 1000; i++)
            {
                DateTime dt1 = DateTime.Now;
                Dynamo.SceneDrawShape(false, false);
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
