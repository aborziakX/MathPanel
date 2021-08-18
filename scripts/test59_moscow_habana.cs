//test59_moscow_habana
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

//найти кратчайший путь из Москвы в Гавану

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        public void Execute()
        {
            Dynamo.SceneClear();
            int id = Dynamo.PhobNew(0, 0, 0);
            Dynamo.Console(id.ToString());
            var hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.Console(hz.ToString());

            var bm = new BitmapSimple(@"world1960.jpg");
            bool bMap = false;
            var t4 = new Sphere(20, "Yellow", bMap ? 200 : 20);//20 - diameter
            if( bMap ) t4.SetBitmap(bm);
            else t4.iFill = 2;//edges
            hz.Shape = t4;
            

            //white sphere, точка на экваторе Африки, нулевой меридиан
            id = Dynamo.PhobNew(-10.5, 0, 0);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffffff");
            Dynamo.PhobAttrSet(id, "txt", "0/0");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            double dRad = 10.5;
            var mat = new Mat3();
            //Moscow,
            var lat = 55.7522;
            var lon = 37.6156;
            mat.Build(0, ((lat) * Math.PI) / 180, ((180 - lon) * Math.PI) / 180);
            mat.Scale(dRad, dRad, dRad);
            Dynamo.Console(string.Format("vMoscow x {0}, y {1}, z {2}", mat.a.x, mat.a.y, mat.a.z));
            Vec3 vMoscow = new Vec3(mat.a.x, mat.a.y, mat.a.z);

            id = Dynamo.PhobNew(mat.a.x, mat.a.y, mat.a.z);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ff0000");
            Dynamo.PhobAttrSet(id, "txt", "Moscow");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            //Гавана 
            lat = 23.133;
            lon = -82.383;
            mat.Build(0, ((lat) * Math.PI) / 180, ((180 - lon) * Math.PI) / 180);
            mat.Scale(dRad, dRad, dRad);
            Dynamo.Console(string.Format("vHabana x {0}, y {1}, z {2}", mat.a.x, mat.a.y, mat.a.z));
            Vec3 vHabana = new Vec3(mat.a.x, mat.a.y, mat.a.z);

            id = Dynamo.PhobNew(mat.a.x, mat.a.y, mat.a.z);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffff00");
            Dynamo.PhobAttrSet(id, "txt", "Гавана");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            //перемножим - найдем ось поворота
            Vec3 vRot = Vec3.Product(vMoscow, vHabana);
            vRot.Normalize();
            vRot.Scale(dRad);
            Dynamo.Console(string.Format("vRot x {0}, y {1}, z {2}", vRot.x, vRot.y, vRot.z));
            id = Dynamo.PhobNew(vRot.x, vRot.y, vRot.z);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#00ff00");
            Dynamo.PhobAttrSet(id, "txt", "ax");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            //угол
            double sca = vMoscow.ScalarProduct(vHabana); //len(a) * len(b) * cos(fi)
            double fi = Math.Acos(sca/(dRad * dRad));
            Dynamo.Console("fi " + (fi * 180) / Math.PI); //87degree

            //vMoscow - new X, vRot - new Z, find new Y
            Vec3 vY = Vec3.Product(vRot, vMoscow);
            vY.Normalize();
            vY.Scale(dRad);
            Dynamo.Console(string.Format("vY x {0}, y {1}, z {2}", vY.x, vY.y, vY.z));

            //rotate
            Vec3 a = new Vec3();
            Vec3 b = new Vec3();
            int nPoints = 30;
            for ( int m = 1; m < nPoints; m++)
            {
                var zRotor = (fi * m) / nPoints;
                a.Copy(vMoscow);
                b.Copy(vY);
                a.Scale(Math.Cos(zRotor));
                b.Scale(Math.Sin(zRotor));
                a.Add(b);
                Dynamo.Console(string.Format("A x {0}, y {1}, z {2}", a.x, a.y, a.z));

                id = Dynamo.PhobNew(a.x, a.y, a.z);
                hz = Dynamo.PhobGet(id) as Phob;
                Dynamo.PhobAttrSet(id, "clr", "#00ff00");
                hz.radius = 0.1;
            }

            Dynamo.SceneBox = new Box(-20, 20, -20, 20, -20, 20);
            Dynamo.SceneDrawShape(true, false);
            Dynamo.Console("total fac=" + Dynamo.SceneFacets());
            Dynamo.Console("total area=" + Dynamo.SceneFacetsArea());

            for (int i = 0; i < 1000; i++)
            {
                Dynamo.SceneDrawShape(true);
                //if (Dynamo.KeyConsole == "D")
                    //Dynamo.ScreenJsonSaveDB("1");
                if (i % 40 == 0)
                {
                    double ix, iy, iz;
                }
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
