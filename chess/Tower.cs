using board;
using chess_game.board;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color) { }
        public override string ToString()
        {
            return "T";
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
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                    break;
                pos.Line = pos.Line - 1;
            }

            //Right
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                    break;
                pos.Column = pos.Column + 1;
            }

            //Below
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                    break;
                pos.Line = pos.Line + 1;
            }

            //Left
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.validPositionTest(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != this.Color)
                    break;
                pos.Column = pos.Column - 1;
            }

            return mat;
        }


    }
}
