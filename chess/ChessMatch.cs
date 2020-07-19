using board;
using chess_game.board;
using System.Collections.Generic;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Capturated;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.white;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Capturated = new HashSet<Piece>();
            PiecesDistribution();
        }

        public void MovementExecute(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.MovementIncrement();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.addPiece(p, destiny);
            if (CapturedPiece != null)
                Capturated.Add(CapturedPiece);
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Capturated)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux; 
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            Board.addPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void PiecesDistribution()
        {
            PlaceNewPiece('c', 1, new Tower(Board, Color.white));
            PlaceNewPiece('c', 2, new Tower(Board, Color.white));
            PlaceNewPiece('d', 2, new Tower(Board, Color.white));
            PlaceNewPiece('e', 2, new Tower(Board, Color.white));
            PlaceNewPiece('e', 1, new Tower(Board, Color.white));
            PlaceNewPiece('d', 1, new King(Board, Color.white));

            PlaceNewPiece('c', 7, new Tower(Board, Color.black));
            PlaceNewPiece('c', 8, new Tower(Board, Color.black));
            PlaceNewPiece('d', 7, new Tower(Board, Color.black));
            PlaceNewPiece('e', 8, new Tower(Board, Color.black));
            PlaceNewPiece('e', 7, new Tower(Board, Color.black));
            PlaceNewPiece('d', 8, new King(Board, Color.black));
        }
    }
}
