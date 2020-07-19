using board;

namespace chess_game.board
{
    class Piece
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


    }
}
