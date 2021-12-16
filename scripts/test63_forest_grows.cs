//test63_forest_grows
//лес растет
using MathPanel;
using MathPanelExt;
using System;

///сборки
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        //дерево
        public class ForestTree
        {
            public int x = 0; //позиция
            public double height = 0; //высота
            public double age = 0; //возраст
            public bool bAlive = true; //живой
            public double ageStop = 0; //возраст смерти
        }

        double yGround = 0; //уровень земли
        double heiMax = 500;//максимальная высота
        double ageMax = 50;//примерно достигнет возраста
        //число лет моделирования
        int NITER = 1000;
        //максимальное число деревьев
        int NTREE = 800;
        //число новых деревьев в год
        int YEAR_TREE = 10;
        //примерно сколько лет стоит сухим
        double STAY_DEAD = 10;

        //генератор случайных чисел
        static Random rnd = new Random();

        public static bool IsEvent(double t, double tMax)
        {
            if (t > 2 * tMax) return true;
            double d = rnd.NextDouble();
            double y = Math.Exp(-(t / (2*tMax)));
            return d > y;   //вероятность события стала меньше, чем сгенерированное случайное число
        }

        public void Execute()
        {
            Dynamo.SceneClear();
            Dynamo.Console("test63_forest_grows");

            ForestTree[] arrTree = new ForestTree[NTREE];
            for (int i = 0; i < NTREE; i++) arrTree[i] = null;

            //формат рисования мертвого дерева
            DrawOpt optDead = new DrawOpt();
            optDead.sty = "line";
            optDead.clr = "#ffff00";
            optDead.csk = "#ffff00";
            optDead.lnw = "3";

            //формат рисования
            string sOptFormat = "{{\"options\":{{\"x0\": 0, \"x1\": 800, \"y0\": 0, \"y1\": 600, \"clr\": \"{0}\", \"sty\": \"line\", \"size\":1, \"lnw\": {1}, \"wid\": 800, \"hei\": 600, \"second\": \"{2}\" }}";

            //по числу наблюдений
            for (int i = 0; i < NITER; i++)
            {
                //заснуть
                System.Threading.Thread.Sleep(500);
                //Dynamo.Console("sleep done=" + i);
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }

                string s9 = "";
                //по всем позициям в лесу
                for (int j = 0; j < NTREE; j++)
                {
                    var tree = arrTree[j];
                    if (tree == null) continue;

                    if (tree.bAlive)
                    {
                        tree.age += 1;
                        tree.height += (heiMax - tree.height)/ageMax;//скорость роста замедляется
                        if (tree.height > heiMax) tree.height = heiMax;
                        //вероятность гибели растет с возрастом
                        bool bEvent = IsEvent(tree.age, ageMax);
                        if (bEvent)//tree.age >= ageMax)
                        {
                            tree.bAlive = false;
                            tree.ageStop = i;
                        }
                    }
                    else
                    {
                        //проверить, упало?
                        //if (i - tree.ageStop > STAY_DEAD) 
                        bool bEvent = IsEvent(i - tree.ageStop, STAY_DEAD);
                        if (bEvent)
                            arrTree[j] = null;
                    }

                    if (s9 != "") s9 += ",";
                    s9 += QuadroEqu.DrawArrow(tree.x, yGround, tree.x, yGround + tree.height, 5,
                        (tree.bAlive ? null : optDead));
                }

                //новый посев
                for (int j = 0; j < YEAR_TREE; j++)
                {
                    int k = rnd.Next(NTREE);
                    var tree = arrTree[k];
                    if (tree == null)
                    {   //новое дерево
                        var t = new ForestTree();
                        t.x = k;
                        arrTree[k] = t;
                    }
                }

                //нарисовать сцену
                if (s9 == "") continue;
                string s10 = string.Format(sOptFormat, "#00ff00", "3", "undefined");
                s10 += ", \"data\":[" + s9 + "]}";
                Dynamo.SceneJson(s10);
            }
        }
    }
}
