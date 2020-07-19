using board;
using chess_game.board;

namespace chess
{
    class Peasant : Piece
    {
        private ChessMatch Match;
        public Peasant(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }
        public override string ToString()
        {
            return "P";
        }

        private bool EnemyExist(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != this.Color;
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

                //#EspecialPlay en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.validPositionTest(left) && EnemyExist(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.validPositionTest(right) && EnemyExist(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
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

                //#EspecialPlay en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.validPositionTest(left) && EnemyExist(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.validPositionTest(right) && EnemyExist(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
