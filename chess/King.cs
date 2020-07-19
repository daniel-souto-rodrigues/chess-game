using board;
using chess_game.board;

namespace chess
{
    class King : Piece
    {
        private ChessMatch Match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.Match = match;
        }


        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }

        private bool TestTowerRock(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Tower && p.Color == this.Color && p.MoveQuantity == 0;
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

            //#EspecialPlay rock
            if (MoveQuantity == 0 && !Match.Check)
            {
                //#EspecialPlay Small-Rock
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (TestTowerRock(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //#EspecialPlay Big-Rock
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (TestTowerRock(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
