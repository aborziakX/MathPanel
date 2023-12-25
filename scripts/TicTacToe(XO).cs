//Автор: "Ильяс Фахурдинов" <mv1451003@gmail.com>

using MathPanel;
using MathPanelExt;
using System.Net.Sockets;
using System;
using System.Collections.Generic;

///сборки для добавления
///[DLL]System.dll,System.Xaml.dll,WindowsBase.dll,PresentationFramework.dll,PresentationCore.dll,System.Drawing.dll,System.Net.dll,System.Net.Http.dll,System.Core.dll[/DLL]
///
namespace DynamoCode
{   
    public class MinimaxAlphaBeta
    {
    // Оценка состояния поля
    private static int EvaluateState(char[,] board, char player, int depth)
    {
        int playerScore = CheckScore(board, player);
        int opponentScore = CheckScore(board, (player == 'X') ? 'O' : 'X');

        int eval = playerScore - opponentScore;
        // Учитываем глубину для более разнообразной оценки (опционально)
        eval += depth;

        return eval;
    }

    private static int CheckScore(char[,] board, char player)
    {
        int score = 0;

        // Проверка рядов
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                score += EvaluateLine(board[i, j], board[i, j + 1], board[i, j + 2], board[i, j + 3], player);
            }
        }

        // Проверка столбцов
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                score += EvaluateLine(board[i, j], board[i + 1, j], board[i + 2, j], board[i + 3, j], player);
            }
        }

        // Проверка главной диагонали
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                score += EvaluateLine(board[i, j], board[i + 1, j + 1], board[i + 2, j + 2], board[i + 3, j + 3], player);
            }
        }

        // Проверка побочной диагонали
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 3; j < 8; j++)
            {
                score += EvaluateLine(board[i, j], board[i + 1, j - 1], board[i + 2, j - 2], board[i + 3, j - 3], player);
            }
        }

        return score;
    }

    private static int EvaluateLine(char a, char b, char c, char d, char player)
    {
        int score = 0;
        int playerCount = 0;
        int emptyCount = 0;
        int opponentCount = 0;

        if (a == player)
        {
            playerCount++;
        }
        else if (a == '-')
        {
            emptyCount++;
        }
        else
        {
            opponentCount++;
        }

        if (b == player)
        {
            playerCount++;
        }
        else if (b == '-')
        {
            emptyCount++;
        }
        else
        {
            opponentCount++;
        }

        if (c == player)
        {
            playerCount++;
        }
        else if (c == '-')
        {
            emptyCount++;
        }
        else
        {
            opponentCount++;
        }

        if (d == player)
        {
            playerCount++;
        }
        else if (d == '-')
        {
            emptyCount++;
        }
        else
        {
            opponentCount++;
        }

        // Оценка для текущей комбинации в ряду
        // Предпочтение отдается захвату линии в игре и предотвращению линии у противника
        if (playerCount == 4)
        {
            score += 100000;
        }
        else if (playerCount == 3 && emptyCount == 1)
        {
            score += 1000;
        }
        else if (playerCount == 2 && emptyCount == 2)
        {
            score += 100;
        }

        if (opponentCount == 3)
        {
            score -= 10000;
        }
        else if (opponentCount == 3 && emptyCount == 1)
        {
            score -= 100;
        }
        else if (opponentCount == 2 && emptyCount == 2)
        {
            score -= 10;
        }       

        return score;
    }

    // Метод для проверки возможности хода в указанную ячейку
    private static bool IsValidMove(char[,] board, int col)
    {
        if(board[col/8, col%8] != '-') return false;
        return true;
    }

    // Метод для получения вариантов возможных ходов
    private static List<int> GetValidMoves(char[,] board)
    {
        List<int> valid = new List<int>();
        for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == '-')
                    {
                        valid.Add(i*8+j);
                    }
                }
            }
        return valid;
    }

    // Алгоритм минимакс с альфа-бета отсечением
    private static int Minimax(char[,] board, int depth, int alpha, int beta, bool isMaximizingPlayer)
    {
        List<int> validMoves = GetValidMoves(board);
        
        if (depth == 0 || validMoves.Count == 0)
        {
            return EvaluateState(board, 'O', depth); // 'O' - игрок, для которого оптимизируем (можно поменять на актуальное значение)
        }

        if (isMaximizingPlayer)
        {
            int value = int.MinValue;
            foreach (int move in validMoves)
            {
                if (IsValidMove(board, move))
                {
                    // Применяем ход к доске
                    char[,] newBoard = ApplyMove(board, move, 'O'); // 'O' - игрок, для которого оптимизируем

                    value = Math.Max(value, Minimax(newBoard, depth - 1, alpha, beta, false));
                    alpha = Math.Max(alpha, value);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return value;
        }
        else
        {
            int value = int.MaxValue;
            foreach (int move in validMoves)
            {
                if (IsValidMove(board, move))
                {
                    // Применяем ход к доске
                    char[,] newBoard = ApplyMove(board, move, 'X'); // 'X' - противник

                    value = Math.Min(value, Minimax(newBoard, depth - 1, alpha, beta, true));
                    beta = Math.Min(beta, value);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return value;
        }
    }

    public static List<Tuple<int, int>> GetTopMoves(char[,] board, int n, char player)
    {
        List<Tuple<int, int>> topMoves = new List<Tuple<int, int>>();

        foreach (int move in GetValidMoves(board))
        {
            int evaluation = EvaluateState(ApplyMove(board, move, player), player, 0);
            topMoves.Add(Tuple.Create(move, evaluation));
        }

        topMoves.Sort((x, y) => y.Item2.CompareTo(x.Item2));
        return topMoves.GetRange(0, n);
    }


    public static int FindBestMove(char[,] board, int depth, char player)
    {
        int bestMove = -1;
        int bestValue = int.MinValue;
        int alpha = int.MinValue;
        int beta = int.MaxValue;
        List<int> validMoves = GetValidMoves(board);
        List<Tuple<int, int>> TopMoves = GetTopMoves(board, 10, player);
        if (validMoves.Count == 63) return validMoves[36];
        foreach (Tuple<int, int> move in TopMoves)
        {
            int m = move.Item1;
            if (IsValidMove(board, m))
            {
                // Применяем ход к доске
                char[,] newBoard = ApplyMove(board, m, player);

                int value = Minimax(newBoard, depth, alpha, beta, false);
                
                if (value > bestValue)
                {
                    bestValue = value;
                    bestMove = m;
                }
            }
        }

        return bestMove;
    }

    // Метод для применения хода к доске
    private static char[,] ApplyMove(char[,] board, int col, char player)
    {
        char[,] nboard = new char[8 ,8];
        Array.Copy(board, nboard, 64);
        nboard[col/8, col%8] = player;
        return nboard;
    }
    }
	public class Script
	{
        System.Drawing.Color[] clrs = new System.Drawing.Color[64];

        static Tuple<int, int> RandomMove(char [,] Board){
            List<Tuple<int, int>> moves = AvailableMoves(Board);
            Random random = new Random();
            int randomIndex = random.Next(0, moves.Count);
            return moves[randomIndex];
        }

        static List<Tuple<int, int>> AvailableMoves(char[,] board)
        {
            List<Tuple<int, int>> available = new List<Tuple<int, int>>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == '-')
                    {
                        available.Add(Tuple.Create(i, j));
                    }
                }
            }
            return available;
        }

        static bool CheckDraw(char[,] Board){
            if (AvailableMoves(Board).Count > 0) return false;
            return true;
        }

        static bool CheckWin(char[,] Board, char symbol)
        {   
            bool res = false;
            double[] x = {0, 0}, y = {0, 0};
            // Проверка по горизонтали
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Board[i, j] == symbol && Board[i, j + 1] == symbol && Board[i, j + 2] == symbol && Board[i, j + 3] == symbol)
                    {
                        res = true;
                        x[0] = i+0.5; x[1] = i+0.5;
                        y[0] = j; y[1] = j+4;
                    }
                }
            }

            // Проверка по вертикали
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j] == symbol && Board[i + 1, j] == symbol && Board[i + 2, j] == symbol && Board[i + 3, j] == symbol)
                    {
                        res = true;
                        x[0] = i; x[1] = i+4;
                        y[0] = j+0.5; y[1] = j+0.5;
                    }
                }
            }

            // Проверка по диагонали (лево верх - право низ)
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Board[i, j] == symbol && Board[i + 1, j + 1] == symbol && Board[i + 2, j + 2] == symbol && Board[i + 3, j + 3] == symbol)
                    {
                        res = true;
                        x[0] = i; x[1] = i+4;
                        y[0] = j; y[1] = j+4;
                    }
                }
            }

            // Проверка по диагонали (право верх - лево низ)
            for (int i = 0; i < 5; i++)
            {
                for (int j = 7; j > 2; j--)
                {
                    if (Board[i, j] == symbol && Board[i + 1, j - 1] == symbol && Board[i + 2, j - 2] == symbol && Board[i + 3, j - 3] == symbol)
                    {
                        res = true;
                        x[0] = i; x[1] = i+4;
                        y[0] = j+1; y[1] = j-3;
                    }
                }
            }

            if (res){
                string s1 = QuadroEqu.DrawLine(x[0], y[0], x[1], y[1]);
                string s2 = "{\"options\":{\"x0\": -0.05, \"x1\": 11.05, \"y0\": -0.05, \"y1\": 8.05, \"clr\": \"#000000\", \"sty\": \"dots\", \"size\":74, \"lnw\": 3, \"wid\": 800, \"hei\": 600, \"second\":1 }";
                s2 += ", \"data\":[" + s1 + "]}";
                Dynamo.SceneJson(s2);
            }
            return res;
        }


        public void DrawCleanTable()
        {
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int m = (i + j) % 2;
                    clrs[k] = (m == 0 ? System.Drawing.Color.AliceBlue : System.Drawing.Color.Aqua);
                    k++;
                }
            }
            string s1 = QuadroEqu.DrawBitmap(8, 8, clrs, 0.5, 0.5); //default from bottom
            string s2 = "{\"options\":{\"x0\": -0.05, \"x1\": 11.05, \"y0\": -0.05, \"y1\": 8.05, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":74, \"lnw\": 3, \"wid\": 800, \"hei\": 600 }";
            s2 += ", \"data\":[" + s1 + "]}";
            //Dynamo.Console(s2);
            Dynamo.SceneJson(s2);
        }

        public void DrawTable(char[,] Board)
        {
            int k = 0;
            string s3 = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int m = (i + j) % 2;
                    clrs[k] = (m == 0 ? System.Drawing.Color.AliceBlue : System.Drawing.Color.Aqua);
                    if (Board[i, j] == 'O')
                    {
                        s3 += ",";
                        s3 += QuadroEqu.DrawEllipse(0.4, 0.4, i + 0.5, j + 0.5, 0, 2*Math.PI, 64);
                    }
                    if (Board[i, j] == 'X')
                    {
                        s3 += ",";
                        s3 += QuadroEqu.DrawLine(i + 0.1, j + 0.1, i + 0.9, j + 0.9);
                        s3 += ",";
                        s3 += QuadroEqu.DrawLine(i + 0.9, j + 0.1, i + 0.1, j + 0.9);
                    }

                    k++;
                }
            }
            string s1 = QuadroEqu.DrawBitmap(8, 8, clrs, 0.5, 0.5); //default from bottom
            string s2 = "{\"options\":{\"x0\": -0.05, \"x1\": 11.05, \"y0\": -0.05, \"y1\": 8.05, \"clr\": \"#ff0000\", \"sty\": \"dots\", \"size\":74, \"lnw\": 3, \"wid\": 800, \"hei\": 600 }";
            s2 += ", \"data\":[" + s1 + s3 + "]}";
            //Dynamo.Console(s2);
            Dynamo.SceneJson(s2);        
        }

        public void Execute()
        {
            Dynamo.Console("Скрипт стартовал!");
            int xClick = -1; ///x позиция клика мыши
            int yClick = -1; ///y позиция клика мыши
            int xMouse = -1; ///x позиция мыши
            int yMouse = -1; ///y позиция мыши
            int xMouseUp = -1; ///x позиция окончании клика мыши
            int yMouseUp = -1; ///y позиция окончании клика мыши
            bool b_mouseDown = false; ///мышь нажата
            bool b_clickDone = false; ///произошел клик мыши
            
            bool bUseUp = true;


            int dual = 0; // 0 - игра с компьютером; 1 - два игрока
            char symbol = 'X';
            char[,] Board = new char[8, 8]; // представление поля ввиде "-", "О", "Х"
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board[i, j] = '-';
                }
            }
            //DrawTable(9);
            DrawCleanTable();
            int szx = 72;
            int szy = 74; // изменил 75 -> 72; 74 (более точные границы по нажатию)
            //100 секунд активности
            for (int i = 0; i < 1000; i++)
            {
                //заснуть
                System.Threading.Thread.Sleep(100);
                //Dynamo.Console("sleep done=" + i);
                string resp = Dynamo.KeyConsole;
                if (resp == "Q")
                {
                    break;
                }
                Dynamo.GetCanvasMouseInfo(ref xClick, ref yClick,
                    ref xMouse, ref yMouse, ref xMouseUp, ref yMouseUp,
                    ref b_mouseDown, ref b_clickDone);
                if (b_clickDone)
                {
                    Dynamo.Console(i + "=" + xClick + ";" + yClick + ";" +
                        xMouse + ";" + yMouse + ";" + xMouseUp + ";" + yMouseUp + ";" +
                        b_mouseDown + ";" + b_clickDone);
                    int row = (bUseUp ? xMouseUp : xClick) / szx;
                    int col = (600 - (bUseUp ? yMouseUp : yClick)) / szy;
                    if (col >= 8 || col < 0 || row >= 8 || row < 0) continue;
                    Dynamo.Console(row + "," + col);
                    if (Board[row, col] == '-') Board[row, col] = symbol;
                    DrawTable(Board);
                    if (CheckWin(Board, symbol)) {
                        Dynamo.Console("Победил игрок с " + symbol.ToString());
                        break;
                    }
                    if (dual == 1){
                        if (symbol=='X') {symbol='O';}
                        else {symbol='X';}
                    }
                    else {
                        //Tuple<int, int> a = RandomMove(Board);
                        int a = MinimaxAlphaBeta.FindBestMove(Board, 3, 'O');
                        Board[a/8, a%8] = 'O';
                        DrawTable(Board);
                        if (CheckWin(Board, 'O')) {
                            Dynamo.Console("Победил игрок с O");
                            break;
                        }
                    }
                    if (CheckDraw(Board)){
                        Dynamo.Console("Ничья");
                        break;
                    }
                }
                // if (b_mouseDown)
                // {
                //     int row = (xMouse) / sz;
                //     int col = (600 - yMouse) / sz;
                //     if (col >= 8 || col < 0 || row >= 8 || row < 0) continue;
                //     //Dynamo.Console(row + "," + col);
                //     L.Add(row * 8 + col);
                //     DrawTable(L_X, L_0);
                // }
            }
        }
    }  
}