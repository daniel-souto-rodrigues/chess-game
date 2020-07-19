using System;
using System.Security.Cryptography;
using board;
using chess;

namespace chess_game
{
    class Program
    {
        static void Main(string[] args)
        {

            ChessMatch match = new ChessMatch();


            while (!match.Finished)
            {
                try
                {
                    Console.Clear();
                    Screen.BoardPrint(match.Board);
                    Console.WriteLine();
                    Console.WriteLine("Shift: " + match.Shift);
                    Console.WriteLine("Current Player: " + match.CurrentPlayer);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ChessPositionRead().ToPosition();
                    match.OriginPositionValidate(origin);

                    bool[,] possiblePositions = match.Board.Piece(origin).PossibleMoviments();

                    Console.Clear();
                    Screen.BoardPrint(match.Board, possiblePositions);

                    Console.WriteLine();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.ChessPositionRead().ToPosition();
                    match.DestinyPositionValidate(origin, destiny);

                    match.PerformMove(origin, destiny);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
