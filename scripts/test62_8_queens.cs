//test62_8_queens.cs
//расставить 8 королев на шахматной доске, чтобы не били друг друга
//92 из 40320
using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;

///сборки
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
        System.Drawing.Color[] clrs =
        {
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,

            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,

            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,

            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,

            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,

            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,

            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,

            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,

            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
            System.Drawing.Color.AliceBlue,
            System.Drawing.Color.Aqua,
        };
        bool IsHit(int row, int col, int [] pos)
        {
            int j;
            //проверить по вертикалям
            for( j = 0; j < row; j++)
            {
                if (pos[j] == col) return true;
            }
            //проверить по диагоналям
            for (j = 0; j < row; j++)
            {
                if (pos[j] + (row - j) == col) return true;
                if (pos[j] - (row - j) == col) return true;
            }
            return false;
        }
        public void Execute()
        {
            Dynamo.Console("test62_8_queens.cs, " + 1 * 2 * 3 * 4 * 5 * 6 * 7 * 8);
            int i, curRow = 0; //текущий ряд
            int[] rowPos = new int[8];//8 рядов на доске
            int[] rowPosGood = new int[8];//8 рядов на доске
            for (i = 0; i < 8; i++) rowPos[i] = 0;//неопределено
            int res = 0;
            while(curRow >= 0)
            {
                /* сдвиг в текущем ряду
                если вышли за доску, откат на один ряд 
                иначе проверка
                если нет боя, перейти к следующему ряду
                если бой, сдвиг в текущем ряду
                */

                rowPos[curRow]++;
                if(rowPos[curRow] > 8)
                {   //если вышли за доску, откат на один ряд
                    rowPos[curRow] = 0;
                    curRow--;
                    continue;
                }
                if (curRow == 0)
                {   //в первом ряду все позиции возможны
                    curRow++;
                    continue;
                }

                bool bHit = IsHit(curRow, rowPos[curRow], rowPos);
                if (!bHit)
                {   //нет боя, перейти к следующему ряду
                    if (curRow == 7)
                    {   //вывести результат
                        res++;
                        if (res > 100) return;
                        Dynamo.Console("результат " + res);
                        for (i = 0; i < 8; i++) Dynamo.Console((i + 1) + "=" + rowPos[i]);
                        if( res == 1 )
                        {
                            for (i = 0; i < 8; i++) rowPosGood[i] = rowPos[i];
                        }
                    }
                    else curRow++;
                }
                else
                {   //сдвиг в текущем ряду
                }
            }

            string s1 = QuadroEqu.DrawBitmap(8, 8, clrs);
            string s2 = "{\"options\":{\"x0\": -0.5, \"x1\": 10, \"y0\": -0.7, \"y1\": 10, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":74, \"lnw\": 3, \"wid\": 800, \"hei\": 600 }";
            string s3 = "";
            for (i = 0; i < 8; i++)
            {
                s3 += ",";
                s3 += QuadroEqu.DrawPoint(i + 0, rowPosGood[i] - 1.15, "", "circle", "#ff0000", "0.3", "20");
            }

            s2 += ", \"data\":[" + s1 + s3 + "]}";
            Dynamo.Console(s2);
            Dynamo.SceneJson(s2);
        }
    }
}
