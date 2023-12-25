//3д визуализации горящего пола с искрами, взлетающими в воздух
//Сураева Анастасия, nestasia.fire@gmail.com

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
            Random rnd = new Random();
            const double g = 9.8; //ускорение
            const double DT = 0.050; //шаг в секундах
            Dynamo.ConsoleClear();
            Dynamo.Console("test25_falling_ball");
            //регистрация на сервере
            //Dynamo.Scriplet("test25_falling_ball", "Падающий мяч");
            Dynamo.SceneClear();
            //мяч
            int id = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz = Dynamo.PhobGet(id) as Phob;
            Sphere cub = new Sphere(rnd.Next(1, 2), "Red", 12);
            hz.Shape = cub;


int id1 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz1 = Dynamo.PhobGet(id1) as Phob;
            Sphere cub1 = new Sphere(rnd.Next(1, 3), "Red", 12);
            hz1.Shape = cub1;

int id2 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz2 = Dynamo.PhobGet(id2) as Phob;
            Sphere cub2 = new Sphere(rnd.Next(1, 3), "Red", 12);
            hz2.Shape = cub2;

int id4 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz4 = Dynamo.PhobGet(id4) as Phob;
            Sphere cub4 = new Sphere(rnd.Next(1, 3), "Red", 12);
            hz4.Shape = cub4;

int id5 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz5 = Dynamo.PhobGet(id5) as Phob;
            Sphere cub5 = new Sphere(rnd.Next(1, 3), "Red", 12);
            hz5.Shape = cub5;

int id6 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz6 = Dynamo.PhobGet(id6) as Phob;
            Sphere cub6 = new Sphere(rnd.Next(1, 2), "Red", 12);
            hz6.Shape = cub6;

int id7 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz7 = Dynamo.PhobGet(id7) as Phob;
            Sphere cub7 = new Sphere(rnd.Next(1, 2), "Red", 12);
            hz7.Shape = cub7;

int id8 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz8 = Dynamo.PhobGet(id8) as Phob;
            Sphere cub8 = new Sphere(rnd.Next(1, 2), "Red", 12);
            hz8.Shape = cub8;

int id9 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz9 = Dynamo.PhobGet(id9) as Phob;
            Sphere cub9 = new Sphere(rnd.Next(1, 2), "Red", 12);
            hz9.Shape = cub9;

int id10 = Dynamo.PhobNew(rnd.Next(0, 40), rnd.Next(0, 40), rnd.Next(0, 4));
            var hz10 = Dynamo.PhobGet(id10) as Phob;
            Sphere cub10 = new Sphere(rnd.Next(1, 2), "Red", 12);
            hz10.Shape = cub10;

            for (int i = 0; i < 40; i = i+2){
            for (int j = 0; j < 40; j = j+2){
            Dynamo.PhobGet(Dynamo.PhobNew(i, j, 4)).Shape = new Cone(3, "Red", 12);    
            }}
for (int i = -1; i < 42; i = i+3){
            for (int j = 0; j < 40; j = j+3){
            Dynamo.PhobGet(Dynamo.PhobNew(i, j, 2)).Shape = new Cone(4, "Red", 12);    
            }}

for (int i = -2; i < 44; i = i+3){
            for (int j = 0; j < 40; j = j+4){
            Dynamo.PhobGet(Dynamo.PhobNew(i, j, 0)).Shape = new Cone(5, "Red", 12);    
            }}

            //мостовая
            int id3 = Dynamo.PhobNew(20, 20, -1.5);
            var hz3 = Dynamo.PhobGet(id3) as Phob;
            Cube cub3 = new Cube(2, "Red");
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

hz.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz.z -= hz.v_z * DT; //падаем

hz1.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz1.z -= hz1.v_z * DT; //падаем

hz2.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz2.z -= hz2.v_z * DT; //падаем

hz4.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz4.z -= hz4.v_z * DT; //падаем

hz5.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz5.z -= hz5.v_z * DT; //падаем

hz6.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz6.z -= hz6.v_z * DT; //падаем

hz7.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz7.z -= hz7.v_z * DT; //падаем

hz8.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz8.z -= hz8.v_z * DT; //падаем

hz9.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz9.z -= hz9.v_z * DT; //падаем

hz10.v_z -= g * DT * rnd.Next(1, 2);//сила тяжести
hz10.z -= hz10.v_z * DT; //падаем

            }
        }
    }
}
