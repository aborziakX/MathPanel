//test50_life_game.cs
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
/*
Игра 'Жизнь' придумана британским математиком John Conway в 1970 году. Удачно имитирует поведение колоний клеток. 
Использует всего 3 правила, но генерирует интересные орнаменты.
Правило 1. Мертвая клетка становится живой, если она граничит ровно с тремя живыми.
Правило 2. Живая продолжает жить, если граничит с двумя или тремя живыми.
Правило 3. Во всех остальных случаях клетка является мертвой. Живая умирает от нехватки пищи 
(слишком много соседей) или потери тепла (слишком мало соседей).
Так и люди, в одиночку гибнут, в толпе нивелируются.
*/

///сборки для использования
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        //инициализировать область случайным образом
        void Init(int [] arr, Random rnd, int nInit)
        {
            int sz = arr.Length;
            for (int i = 0; i < sz; i++) arr[i] = 0;//мертвая

            for (int i = 0; i < nInit; i++)
            {
                //найти случайную позицию
                arr[rnd.Next(sz)] = 1;//живая
            }
        }

        //добавить одну живую клетку
        void addLive(int[] arr, Random rnd)
        {
            int sz = arr.Length;
            //найти случайную позицию
            int k = rnd.Next(sz);
            arr[k] = 1;
        }

        //обновить цвет клеток, arr - текущее состояние,
        //arrCol - массив цветов для колонии,
        //clrs - 2 цвета визуализации
        void updateCells(int[] arr, System.Drawing.Color[] arrCol, System.Drawing.Color[] clrs)
        {
            int sz = arr.Length;
            for (int k = 0; k < sz; k++)
            {
                clrs[k] = arr[k] > 0 ? arrCol[1] : arrCol[0];
            }
        }

        //новая итерация, arr - текущее состояние,
        //m - число строк, n - число колонок
        //arrNeighb - число соседей
        void Iterate(int[] arr, int m, int n, int [] arrNeighb)
        {
            int sz = arr.Length;
            int i, ind;
            //подготовиться к вычислению числа соседей
            for (i = 0; i < sz; i++) arrNeighb[i] = 0;

            for (i = 0; i < sz; i++)
            {   //проверить всех 8 соседей клетки с индексом i
                //Dynamo.Console("i=" + i);
                if (i % n == 0)
                {   //клетка на левой границе
                }
                else
                {   //не на левой границе, посмотреть
                    ind = i - 1;//сосед слева
                    if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                    ind = i - 1 - n;//слева и сверху
                    if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                    ind = i - 1 + n;//слева и снизу
                    if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                }

                if (i % n == n - 1)
                {   //на правой границе
                }
                else
                {   //не на правой границе, посмотреть
                    ind = i + 1;//справа
                    if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                    ind = i + 1 - n;//справа и сверху
                    if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                    ind = i + 1 + n;//справа и снизу
                    if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                }

                //сверху
                ind = i - n;
                if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
                //снизу
                ind = i + n;
                if (ind >= 0 && ind < sz && arr[ind] == 1) arrNeighb[i]++;
            }

            //новая генерация
            for (i = 0; i < sz; i++)
            {
                if (arr[i] == 0 && arrNeighb[i] == 3)
                {   //новая живая клетка - Правило 1
                    arr[i] = 1;
                }
                else if (arr[i] == 1 && (arrNeighb[i] == 2 || arrNeighb[i] == 3))
                {   //живи - Правило 2
                }
                else
                {   //умри - Правило 3
                    arr[i] = 0;
                }
            }
        }

        public void Execute()
        {
            Dynamo.Console("test50_life_game");

            //цвета: черный - мертвая клетка, зеленый - живая
            System.Drawing.Color[] arrCol = { System.Drawing.Color.Black, System.Drawing.Color.Green };
            int m = 40; //число строк
            int n = 50; //число колонок
            int nInit = 500;    //начальное число живых клеток
            int sz = m * n;     //размер таблицы
            int[] arr = new int[sz];        //массив для состояния клеток
            int[] arrNeighb = new int[sz];  //массив для числа соседей
            //массив для цветов
            System.Drawing.Color[] clrs = new System.Drawing.Color[sz];
            //число итераций
            int NPOINTS = 1000;
            //генератор случайных чисел
            Random rnd = new Random();

            //задаем парметры рисования options: используем таблицу (m * n) на канвасе 800 * 600
            string sOpt = "{\"options\":{\"x0\": 0, \"x1\": " + n + ", \"y0\": 0, \"y1\": " + m + ", \"clr\": \"#00ff00\", \"sty\": \"dots\", \"size\":20, \"lnw\": 2, \"wid\": 800, \"hei\": 600 }";

            //инициализируем колонию
            Init(arr, rnd, nInit);

            //обновляем цвета
            updateCells(arr, arrCol, clrs);

            //цикл по заданному количеству итераций
            for (int i = 0; i < NPOINTS; i++)
            {
                //создать код для bitmap
                var s1 = QuadroEqu.DrawBitmap(m, n, clrs);

                //нарисовать
                var sJson = sOpt + ", \"data\":[" + s1 + "]}";
                Dynamo.SceneJson(sJson);

                //заснуть ненадолго
                System.Threading.Thread.Sleep(500);
                //проверить клавиатуру
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {   //давай до свидания
                    break;
                }
                if (resp == "A")
                {   //добавить клетку
                    addLive(arr, rnd);
                }

                //следующая итерация
                Iterate(arr, m, n, arrNeighb);

                //обновляем цвета
                updateCells(arr, arrCol, clrs);
            }
        }
    }
}
