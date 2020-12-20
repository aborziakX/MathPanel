//test27_cone_pyramid
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.Console("test27_cone_pyramid");
            Dynamo.SceneClear();

            int id = Dynamo.PhobNew(-10, 0, 0);
            var hz = Dynamo.PhobGet(id) as Phob;
            var t1 = new Cone(10, "Yellow", 12);
            t1.Fractal(1);
            hz.Shape = t1;

            int id2 = Dynamo.PhobNew(10, 0, 0);
            var hz2 = Dynamo.PhobGet(id2) as Phob;
            var t2 = new Cylinder(13, "Green", 3);
            t2.Fractal(2);
            hz2.Shape = t2;

            int id3 = Dynamo.PhobNew(-0, 10, 0);
            var hz3 = Dynamo.PhobGet(id3) as Phob;
            var t3 = new Tetra(13, "Magenta");
            t3.Fractal(3);
            hz3.Shape = t3;

            int id4 = Dynamo.PhobNew(-0, -10, 10);
            var hz4 = Dynamo.PhobGet(id4) as Phob;
            var t4 = new Sphere(13, "Cyan");
            t4.Fractal(1);
            hz4.Shape = t4;

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(true, false);

            for (int i = 0; i < 1000; i++)
            {
                Dynamo.SceneDrawShape(true);
                if (i % 40 == 0)
                {
                    double ix, iy, iz;
                }
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
