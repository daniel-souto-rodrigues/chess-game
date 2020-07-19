using board;

namespace chess_game.board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveQuantity { get; set; }
        public Board Board { get; set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MoveQuantity = 0;
        }

        public void MovementIncrement()
        {
            MoveQuantity++;
        }

        public bool ExistPossibleMoviments()
        {
            bool[,] mat = PossibleMoviments();
            for(int i = 0; i< Board.Lines; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                        return true;
                }
            }
            return false;
        }

        public bool CanMoveToPosition(Position pos)
        {
            return PossibleMoviments()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMoviments();

    }
}
