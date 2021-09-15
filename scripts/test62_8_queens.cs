//test62_8_queens.cs
//расставить 8 королев на шахматной доске, чтобы не били друг друга
//92 из 40320
using MathPanel;
using System.Net.Sockets;
using System;

///сборки
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{
    public class Script
    {
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
                    }
                    else curRow++;
                }
                else
                {   //сдвиг в текущем ряду
                }
            }
            
        }
    }
}
