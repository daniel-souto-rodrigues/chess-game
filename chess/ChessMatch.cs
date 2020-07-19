using board;
using chess_game.board;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.white;
            Finished = false;
            PiecesDistribution();
        }

        public void MovementExecute(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.MovementIncrement();
            //CapturedPiece going to be used later
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.addPiece(p, destiny);
        }

        private void PiecesDistribution()
        {
            Board.addPiece(new Tower(Board, Color.white), new ChessPosition('c', 1).ToPosition());
            Board.addPiece(new Tower(Board, Color.white), new ChessPosition('c', 2).ToPosition());
            Board.addPiece(new Tower(Board, Color.white), new ChessPosition('d', 2).ToPosition());
            Board.addPiece(new Tower(Board, Color.white), new ChessPosition('e', 2).ToPosition());
            Board.addPiece(new Tower(Board, Color.white), new ChessPosition('e', 1).ToPosition());
            Board.addPiece(new King(Board, Color.white), new ChessPosition('d', 1).ToPosition());

            Board.addPiece(new Tower(Board, Color.black), new ChessPosition('c', 7).ToPosition());
            Board.addPiece(new Tower(Board, Color.black), new ChessPosition('c', 8).ToPosition());
            Board.addPiece(new Tower(Board, Color.black), new ChessPosition('d', 7).ToPosition());
            Board.addPiece(new Tower(Board, Color.black), new ChessPosition('e', 8).ToPosition());
            Board.addPiece(new Tower(Board, Color.black), new ChessPosition('e', 7).ToPosition());
            Board.addPiece(new King(Board, Color.black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
