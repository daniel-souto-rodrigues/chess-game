using board;
using chess_game.board;

namespace chess
{
    class Peasant : Piece
    {
        public Peasant(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "P";
        }

        private bool EnemyExist(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        private bool Free(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.white)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.validPositionTest(pos) && Free(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line - 2, Position.Column);
                if (Board.validPositionTest(pos) && Free(pos) && MoveQuantity == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.validPositionTest(pos) && EnemyExist(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.validPositionTest(pos) && EnemyExist(pos))
                    mat[pos.Line, pos.Column] = true;
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.validPositionTest(pos) && Free(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line + 2, Position.Column);
                if (Board.validPositionTest(pos) && Free(pos) && MoveQuantity == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.validPositionTest(pos) && EnemyExist(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.validPositionTest(pos) && EnemyExist(pos))
                    mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
