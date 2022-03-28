//test51_labyrinth.cs
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
//new
using System.Collections.Generic;

/*
Волновой алгоритм был изобретен для оптимальной разводки печатных плат с микросхемами. 
Здесь применяем для выхода из лабиринта. Выйти из лабиринта гарантировано можно держась рукой за одну из стенок. 
Но это долго и мучительно. Используя волновой алгоритм проще.
Лабиринт можно промоделировать с помощью картинки. Черные пиксели представляют стены, оранжевый пиксель 
справа снизу – начальная точка, зеленый пиксель слева вверху – конечная точка.
*/

///сборки для использования
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        //обновить цвет клеток
        //arrState - состояния клетки
        //arrCol - цвета состояний
        //clrs - цвета клеток
        void updateCells(int[] arrState, System.Drawing.Color[] arrCol, System.Drawing.Color[] clrs)
        {
            int sz = arrState.Length;
            for (int k = 0; k < sz; k++)
            {   //установить цвет согласно состоянию клетки
                clrs[k] = arrCol[arrState[k]];
            }
        }

        //новая итерация
        //arrState - состояния клетки
        //m - число строк
        //n - число колонок
        //lstFront - новый фронт волны
        //lstFrontOld - предыдущий фронт волны
        bool Iterate(int[] arrState, int m, int n, HashSet<int> lstFront, HashSet<int> lstFrontOld)
        {
            bool rc = false;
            int sz = arrState.Length;
            int ind;

            foreach (var j in lstFrontOld)
            {
                arrState[j] = 4; //бала фронтом, стала заполнена

                //проверить всех 4-х соседей
                //что у нас слева?
                if (j % n == 0)
                {   //уже на левой границе
                }
                else
                {   //не на левой границе
                    ind = j - 1;//шаг влево               
                    if (ind >= 0 && ind < sz && arrState[ind] == 1)
                    {
                        arrState[ind] = 5; //пустая клетка становится фронтом
                        lstFront.Add(ind);
                    }
                    if (ind >= 0 && ind < sz && arrState[ind] == 3) rc = true; //цель достигнута
                }

                //что у нас справа?
                if (j % n == n - 1)
                {   //уже на правой границе
                }
                else
                {   //не на правой границе
                    ind = j + 1;//шаг вправо
                    if (ind >= 0 && ind < sz && arrState[ind] == 1)
                    {
                        arrState[ind] = 5; //пустая клетка становится фронтом
                        lstFront.Add(ind);
                    }
                    if (ind >= 0 && ind < sz && arrState[ind] == 3) rc = true; //цель достигнута
                }

                //что у нас сверху?
                ind = j - n;
                if (ind >= 0 && ind < sz && arrState[ind] == 1)
                {
                    arrState[ind] = 5; //пустая клетка становится фронтом
                    lstFront.Add(ind);
                }
                if (ind >= 0 && ind < sz && arrState[ind] == 3) rc = true; //цель достигнута

                //что у нас снизу?
                ind = j + n;
                if (ind >= 0 && ind < sz && arrState[ind] == 1)
                {
                    arrState[ind] = 5; //пустая клетка становится фронтом
                    lstFront.Add(ind);
                }
                if (ind >= 0 && ind < sz && arrState[ind] == 3) rc = true; //цель достигнута
            }

            return rc; //true - цель достигнута
        }

        //проверить на правильность задания лабиринта
        //bm - карта
        //iStart - начальная позиция в карте
        //iFinish - конечная позиция в карте
        //arrState - состояния клетки
        int Check(BitmapSimple bm, out int iStart, out int iFinish, int [] arrState)
        {
            iStart = -1;
            iFinish = -1;
            bool bBlack = false;
            bool bRed = false;
            bool bGreen = false;
            //цвет пустых клеток
            int iWhite = System.Drawing.Color.White.ToArgb();
            //цвет границ
            int iBlack = System.Drawing.Color.Black.ToArgb();
            //цвет начальной клетки
            int iRed = System.Drawing.Color.Red.ToArgb();
            //цвет целевой клетки
            int iGreen = System.Drawing.Color.FromArgb(255, 0, 255, 0).ToArgb();
            for (int k = 0; k < bm.width * bm.height; k++)
            {
                int argb = bm.map[k];
                if (argb == iBlack)
                {   //граница есть
                    bBlack = true;
                    arrState[k] = 0;
                }
                else if (argb == iRed)
                {   //начало есть
                    bRed = true;
                    if (iStart == -1)
                    {
                        iStart = k;
                        arrState[k] = 2;
                    }
                    else
                    {   //пустая
                        bm.map[k] = iWhite;
                        arrState[k] = 1;
                    }
                }
                else if (argb == iGreen)
                {   //цель есть
                    bGreen = true;
                    if (iFinish == -1)
                    {
                        iFinish = k;
                        arrState[k] = 3;
                    }
                    else
                    {   //пустая
                        bm.map[k] = iWhite;
                        arrState[k] = 1;
                    }
                }
                else
                {   //пустая
                    bm.map[k] = iWhite;
                    arrState[k] = 1;
                }
            }
            //0 - все в порядке
            return (bBlack ? 0 : 1) + (bRed ? 0 : 2) + (bGreen ? 0 : 4);
        }

        public void Execute()
        {
            Dynamo.Console("test51_labyrinth");
            //путь к папке с картинкой 
            string sDir = @"c:\temp\";
            //картинка с изображением лабиринта
            string fname = "labyrinth.png";
            //создаем карту из файла
            var bm = new BitmapSimple(sDir + fname);
            Dynamo.Console("bm size " + bm.width + "," + bm.height);

            int m = bm.height;//число строк
            int n = bm.width; //число колонок
            int sz = m * n;   //размер таблицы
            int[] arrState = new int[sz]; //состояние клетки: 0-граница, 1-пустая, 2-начальная, 3-конечная, 4-заполнена, 5-фронт
            //цвета клеток
            System.Drawing.Color[] clrs = new System.Drawing.Color[sz];
            //число итераций
            int NPOINTS = 1000;
            //генератор случайных чисел
            Random rnd = new Random();
            //цвета состояний
            System.Drawing.Color[] arrCol = { 
                System.Drawing.Color.Black, //граница
                System.Drawing.Color.White, //пустая
                System.Drawing.Color.Red,   //начальная
                System.Drawing.Color.Green, //конечная
                System.Drawing.Color.Blue,  //заполнена
                System.Drawing.Color.Orange,//фронт
            };

            //задаем парметры рисования options: используем таблицу (m * n) на канвасе 800 * 600
            string sOpt = "{\"options\":{\"x0\": -0.5, \"x1\": " + (n) + ", \"y0\": -0.5, \"y1\": " + (m) + ", \"clr\": \"#00ff00\", \"sty\": \"dots\", \"size\":20, \"lnw\": 2, \"wid\": 800, \"hei\": 600 }";
            bool bManual = false;    //признак ручного режима

            int iStart; //начальная позиция в карте
            int iFinish; //конечная позиция в карте
            int rc = Check(bm, out iStart, out iFinish, arrState);
            Dynamo.Console("rc=" + rc + ", iStart=" + iStart +", iFinish=" + iFinish);
            if (rc != 0) return;

            HashSet<int> lstFront = new HashSet<int>();   //новый фронт волны
            HashSet<int> lstFrontOld = new HashSet<int>();//предыдущий фронт волны
            lstFront.Add(iStart);

            //обновить таблицу
            updateCells(arrState, arrCol, clrs);

            //циклу по заданному числу итераций
            for (int i = 0; i < NPOINTS; i++)
            {
                //создать код рисования карты
                var s1 = QuadroEqu.DrawBitmap(m, n, clrs, 0, 0, false);
                //Dynamo.Console("quadro done=" + i);

                //нарисовать карту
                var sJson = sOpt + ", \"data\":[" + s1 + "]}";
                Dynamo.SceneJson(sJson);
                //Dynamo.Console("json done=" + i);

                //подождать немного
                System.Threading.Thread.Sleep(50);
                //Dynamo.Console("sleep done=" + i);
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }
                if (bManual)
                {   //в ручном режиме ждем нажатия на клавиатуре N
                    if (resp != "N")
                        continue;
                }

                //новая итерация, новые становятся старыми
                lstFrontOld.Clear();
                foreach(var hz in lstFront) lstFrontOld.Add(hz);
                lstFront.Clear();
                bool rc2 = Iterate(arrState, m, n, lstFront, lstFrontOld);
                //Dynamo.Console("iter done=" + i + ",lstFront.cnt=" + lstFront.Count + ",lstFrontOld.cnt=" + lstFrontOld.Count);

                //обновить клетки таблицы
                updateCells(arrState, arrCol, clrs);
                //Dynamo.Console("upd done=" + i);
                if ( rc2 ) //выход, если цель достигнута
                    break;
            }
        }
    }
}
