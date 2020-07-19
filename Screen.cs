using board;
using chess;
using chess_game.board;
using System;

namespace chess_game
{
    class Screen
    {
        public static void BoardPrint(Board board)
        {
           
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PiecePrint(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void BoardPrint(Board board, bool[,] possiblePositions)
        {
            ConsoleColor defaultBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                        Console.BackgroundColor = changedBackground;
                    else
                        Console.BackgroundColor = defaultBackground;

                    PiecePrint(board.Piece(i, j));
                    Console.BackgroundColor = defaultBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = defaultBackground;
        }

        public static ChessPosition ChessPositionRead()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PiecePrint(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.Color == Color.white)
                    Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

    }
}
