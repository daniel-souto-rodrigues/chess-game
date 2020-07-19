using board;
using chess_game.board;

namespace chess
{
    class Bisp : Piece
    {
        public Bisp(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //northeast
            pos.SetValues(Position.Line - 1, Position.Column +1);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.SetValues(pos.Line - 1, pos.Column + 1);
            }

            //Southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            //south-west
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.SetValues(pos.Line + 1, pos.Column - 1);
            }

            //northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                    break;
                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            return mat;
        }
    }
}
