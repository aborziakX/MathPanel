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
            //ящик повернут относительно камеры, ось Z вверх, ось X вправо
            double dRad = 10.5;

            int id = Dynamo.PhobNew(0, 0, 0);
            Dynamo.Console(id.ToString());
            var hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.Console(hz.ToString());

            var bm = new BitmapSimple(@"C:\c_devel\images\world1960.jpg");
            bool bMap = false;
            var t4 = new Sphere(20, "Yellow", bMap ? 200 : 20);//20 - diameter
            if( bMap ) t4.SetBitmap(bm);
            else t4.iFill = 2;//edges
            hz.Shape = t4;

            var vGreenw = new Vec3(0, -dRad, 0);
            //некие координаты на Земле
            //-Y - white sphere, точка на экваторе Африки, нулевой меридиан
            id = Dynamo.PhobNew(0, -dRad, 0); 
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffffff");
            //Dynamo.PhobAttrSet(id, "txt", "0/0 white");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;
            Dynamo.PhobAttrSet(id, "txt2", "0/0");
            //Dynamo.PhobAttrSet(id, "txt1", "O");
            Dynamo.PhobAttrSet(id, "lnw", "2");
            hz.bDrawAsLine = true;
            hz.p1.Copy(-0, -0, -0);
            hz.p2.Copy(0, -dRad, 0);

            //X - voilet sphere, точка на экваторе, 90 меридиан
            id = Dynamo.PhobNew(dRad, 0, 0);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ff00ff");
            //Dynamo.PhobAttrSet(id, "txt", "0/90 voilet");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;
            Dynamo.PhobAttrSet(id, "txt2", "0/90");
            //Dynamo.PhobAttrSet(id, "txt1", "O");
            Dynamo.PhobAttrSet(id, "lnw", "2");
            hz.bDrawAsLine = true;
            hz.p1.Copy(-0, -0, -0);
            hz.p2.Copy(dRad, 0, 0 );

            //blue sphere, точка на северном полюсе
            id = Dynamo.PhobNew(0, 0, dRad);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "txt2", "Северный полюс");
            Dynamo.PhobAttrSet(id, "txt1", "O");
            hz.bDrawAsLine = true;
            hz.p1.Copy(-0, -0, -0);
            hz.p2.Copy(0, -0, dRad);
            Dynamo.PhobAttrSet(id, "clr", "#0000ff");
            //Dynamo.PhobAttrSet(id, "txt", "North blue");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            Dynamo.PhobAttrSet(id, "lnw", "2");
            hz.radius = 0.3;

            var mat = new Mat3();
            //Moscow,
            var lat = 55.7522;
            var lon = 37.6156;
            mat.Build(-(lat * Math.PI) / 180, 0, (lon * Math.PI) / 180);
            Dynamo.Console("m1=" + mat.ToString());
            Vec3 vMoscow = new Vec3();
            mat.Mult(vGreenw, ref vMoscow);
            Dynamo.Console(string.Format("vMoscow x={0}, y={1}, z={2}", vMoscow.x, vMoscow.y, vMoscow.z));

            id = Dynamo.PhobNew(vMoscow.x, vMoscow.y, vMoscow.z);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ff0000");
            Dynamo.PhobAttrSet(id, "txt", "Москва");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            //Гавана 
            lat = 23.133;
            lon = 360.0 - 82.383;
            mat.Build(-(lat * Math.PI) / 180, 0, (lon * Math.PI) / 180);
            Dynamo.Console("m2=" + mat.ToString());
            Vec3 vHabana = new Vec3();
            mat.Mult(vGreenw, ref vHabana);
            Dynamo.Console(string.Format("vHabana x={0}, y={1}, z={2}", vHabana.x, vHabana.y, vHabana.z));

            id = Dynamo.PhobNew(vHabana.x, vHabana.y, vHabana.z);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#ffff00");
            Dynamo.PhobAttrSet(id, "txt", "Гавана");
            Dynamo.PhobAttrSet(id, "fontsize", "20");
            hz.radius = 0.3;

            //перемножим - найдем ось поворота
            Vec3 vRot = Vec3.Product(vMoscow, vHabana);
            vRot.Normalize();
            vRot.Scale(dRad);
            Dynamo.Console(string.Format("vRot x={0}, y={1}, z={2}", vRot.x, vRot.y, vRot.z));
            id = Dynamo.PhobNew(vRot.x, vRot.y, vRot.z);
            hz = Dynamo.PhobGet(id) as Phob;
            Dynamo.PhobAttrSet(id, "clr", "#00ff00");
            Dynamo.PhobAttrSet(id, "txt", "rotation axe");
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
            Dynamo.Console(string.Format("vY x={0}, y={1}, z={2}", vY.x, vY.y, vY.z));

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
                Dynamo.Console(string.Format("A x={0}, y={1}, z={2}", a.x, a.y, a.z));

                id = Dynamo.PhobNew(a.x, a.y, a.z);
                hz = Dynamo.PhobGet(id) as Phob;
                Dynamo.PhobAttrSet(id, "clr", "#00ff00");
                hz.radius = 0.2;
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
