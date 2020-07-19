using board;
using chess_game.board;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "R";
        }
    }
}
