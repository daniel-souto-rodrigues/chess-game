using board;
using chess;

namespace chess_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.addPiece(new Tower(board, Color.black), new Position(0, 0));
            board.addPiece(new Tower(board, Color.black), new Position(1, 3));
            board.addPiece(new King(board, Color.black), new Position(2, 4));

            Screen.BoardPrint(board);
        }
    }
}
