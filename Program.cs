using System;
using board;
using chess;

namespace chess_game
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();
               

                while (!match.Finished)
                {
                    Console.Clear();
                    Screen.BoardPrint(match.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ChessPositionRead().ToPosition();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.ChessPositionRead().ToPosition();

                    match.MovementExecute(origin, destiny);
                }                
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
