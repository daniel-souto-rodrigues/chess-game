using board;
using chess_game.board;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
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

        public void PerformMove(Position origin, Position destiny)
        {
            MovementExecute(origin, destiny);
            Shift++;
            ChangePlayer();
        }

        public void OriginPositionValidate(Position pos)
        {
            if (Board.Piece(pos) == null)
                throw new BoardException("There is no piece in the chosen origin position!");
            if (CurrentPlayer != Board.Piece(pos).Color)
                throw new BoardException("Is not your shift");
            if (!Board.Piece(pos).ExistPossibleMoviments())
                throw new BoardException("No available movements for chosen piece");
        }

        public void DestinyPositionValidate(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).CanMoveToPosition(destiny))
                throw new BoardException("Invalid destiny position!");            
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.white)
                CurrentPlayer = Color.black;
            else
                CurrentPlayer = Color.white;
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
