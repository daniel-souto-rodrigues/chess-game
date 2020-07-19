using System;
using board;

namespace chess_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.BoardPrint(board);
        }
    }
}
