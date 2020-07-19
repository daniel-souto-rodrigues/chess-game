using chess_game.board;

namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            return pieces[pos.Line, pos.Column];
        }

        public bool PieceExist(Position pos)
        {
            PositionValidate(pos);
            return Piece(pos) != null;
        }

        public void addPiece(Piece p, Position pos)
        {
            if (PieceExist(pos))
                throw new BoardException("Already exist a piece on this position!");
            pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public bool validPositionTest(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
                return false;
            return true;    
        }

        public void PositionValidate(Position pos)
        {
            if (!validPositionTest(pos))
                throw new BoardException("Invalid Position!");
        }
    }
}
