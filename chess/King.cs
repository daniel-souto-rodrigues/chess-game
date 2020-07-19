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

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //Above
            pos.SetValues(Position.Line - 1, Position.Column);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //northeast
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //right
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //Southeast
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //below
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //below
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //south-west
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //left
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;
            //northwest
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.validPositionTest(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            return mat;
        }
    }
}
