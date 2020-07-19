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
                Board board = new Board(8, 8);

                board.addPiece(new Tower(board, Color.black), new Position(0, 0));
                board.addPiece(new Tower(board, Color.black), new Position(1, 3));
                board.addPiece(new King(board, Color.black), new Position(2, 4));
                board.addPiece(new King(board, Color.black), new Position(0, 5));
                //test
                board.addPiece(new King(board, Color.white), new Position(6, 6));

                Screen.BoardPrint(board);


            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
