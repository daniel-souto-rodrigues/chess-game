using board;
using chess;
using chess_game.board;
using System;
using System.Collections.Generic;

namespace chess_game
{
    class Screen
    {
        public static void MatchPrint(ChessMatch match)
        {
            BoardPrint(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Shift: " + match.Shift);
            if (!match.Finished)
            {
                Console.WriteLine("Current Player: " + match.CurrentPlayer);
                if (match.Check)
                    Console.WriteLine("CHECK!");
            }
            else
            {
                Console.WriteLine("CHEQUEMATE!");
                Console.WriteLine("Winner is: " + match.CurrentPlayer);
            }

        }

        private static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("White: ");
            HashSetPrint(match.CapturedPieces(Color.white));
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            HashSetPrint(match.CapturedPieces(Color.black));
            Console.ForegroundColor = aux;
        }

        private static void HashSetPrint(HashSet<Piece> _hashSets)
        {
            Console.Write("[");
            foreach(Piece x in _hashSets)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine("]");
        }

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
