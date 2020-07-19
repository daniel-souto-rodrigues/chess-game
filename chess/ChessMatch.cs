using board;
using chess_game.board;
using Microsoft.VisualBasic;
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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

    public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.white;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Pieces = new HashSet<Piece>();
            Capturated = new HashSet<Piece>();
            PiecesDistribution();
        }

        public Piece MovementExecute(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.MovementIncrement();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.addPiece(p, destiny);
            if (CapturedPiece != null)
                Capturated.Add(CapturedPiece);

            //#EspecialPlay small-Rock
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(originT);
                T.MovementIncrement();
                Board.addPiece(T, destinyT);
            }

            //#EspecialPlay big-Rock
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(originT);
                T.MovementIncrement();
                Board.addPiece(T, destinyT);
            }

            //#EspecialPlay en passant
            if (p is Peasant)
            {
                if (origin.Column != destiny.Column && CapturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.white)
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    else
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    CapturedPiece = Board.RemovePiece(posP);
                    Capturated.Add(CapturedPiece);
                }
            }
            return CapturedPiece;
        }

        private void UndoMovement(Position origin, Position destiny, Piece capturatedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.MovementDecrement();
            if (capturatedPiece != null)
            {
                Board.addPiece(capturatedPiece, destiny);
                Capturated.Remove(capturatedPiece);
            }
            Board.addPiece(p, origin);

            //#EspecialPlay small-Rock
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(destinyT);
                T.MovementDecrement();
                Board.addPiece(T, originT);
            }

            //#EspecialPlay big-Rock
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(destinyT);
                T.MovementDecrement();
                Board.addPiece(T, originT);
            }
            
            //#EspecialPlay en passant
            if (p is Peasant)
            {
                if (origin.Column != destiny.Column && capturatedPiece == VulnerableEnPassant)
                {
                    Piece peasant = Board.RemovePiece(destiny);
                    Position posP;
                    if(p.Color == Color.white)
                        posP = new Position(3, destiny.Column);
                    else
                        posP = new Position(4, destiny.Column);
                    Board.addPiece(peasant, posP);
                }
            }
        }

        public void PerformMove(Position origin, Position destiny)
        {
            Piece capturatedPiece = MovementExecute(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturatedPiece);
                throw new BoardException("You cannot put yourself in a check!");
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
                Check = true;
            else
                Check = false;

            if (CheckmateTest(Adversary(CurrentPlayer)))
                Finished = true;
            else
            {
                Shift++;
                ChangePlayer();
            }

            Piece p = Board.Piece(destiny);
            
            //#EspecialPlay en passant
            if (p is Peasant && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
                VulnerableEnPassant = p;
            else
                VulnerableEnPassant = null;
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
            foreach (Piece x in Capturated)
            {
                if (x.Color == color)
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

        private Color Adversary(Color color)
        {
            if (color == Color.white)
                return Color.black;
            else
                return Color.white;
        }

        private Piece King(Color color)
        {
            foreach (Piece x in InGamePieces(color))
            {
                if (x is King)
                    return x;
            }
            return null;

        }

        public bool IsInCheck(Color color)
        {
            Piece R = King(color);
            if (R == null)
                throw new BoardException("Not exist king with the color " + color + " on board!");

            foreach (Piece x in InGamePieces(Adversary(color)))
            {
                bool[,] mat = x.PossibleMoviments();
                if (mat[R.Position.Line, R.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckmateTest(Color color)
        {
            if (!IsInCheck(color))
                return false;

            foreach (Piece x in InGamePieces(color))
            {
                bool[,] mat = x.PossibleMoviments();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MovementExecute(origin, destiny);
                            bool checkTest = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);
                            if (!checkTest)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            Board.addPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void PiecesDistribution()
        {
            PlaceNewPiece('a', 1, new Tower(Board, Color.white));
            PlaceNewPiece('b', 1, new Horse(Board, Color.white));
            PlaceNewPiece('c', 1, new Bisp(Board, Color.white));
            PlaceNewPiece('d', 1, new Queen(Board, Color.white));
            PlaceNewPiece('e', 1, new King(Board, Color.white, this));
            PlaceNewPiece('f', 1, new Bisp(Board, Color.white));
            PlaceNewPiece('g', 1, new Horse(Board, Color.white));
            PlaceNewPiece('h', 1, new Tower(Board, Color.white));
            PlaceNewPiece('a', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('b', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('c', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('d', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('e', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('f', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('g', 2, new Peasant(Board, Color.white, this));
            PlaceNewPiece('h', 2, new Peasant(Board, Color.white, this));

            PlaceNewPiece('a', 8, new Tower(Board, Color.black));
            PlaceNewPiece('b', 8, new Horse(Board, Color.black));
            PlaceNewPiece('c', 8, new Bisp(Board, Color.black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.black));
            PlaceNewPiece('e', 8, new King(Board, Color.black, this));
            PlaceNewPiece('f', 8, new Bisp(Board, Color.black));
            PlaceNewPiece('g', 8, new Horse(Board, Color.black));
            PlaceNewPiece('h', 8, new Tower(Board, Color.black));
            PlaceNewPiece('a', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('b', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('c', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('d', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('e', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('f', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('g', 7, new Peasant(Board, Color.black, this));
            PlaceNewPiece('h', 7, new Peasant(Board, Color.black, this));
        }
    }
}
