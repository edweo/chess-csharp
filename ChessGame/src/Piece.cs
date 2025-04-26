using ChessGame.src.pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src
{
    public abstract class Piece
    {
        public Colors PieceColor { get; set; }
        public string CurrentPosition { get; set; }
        public bool HasMovedOnce { get; set; }
        public bool HasLegalMoves { get; set; }
        public int MovesCount { get; set; }
        public int PieceValue { get; private set; }
        public bool IsPinned { get; set; }
        public Square CurrentSquare { get; set; }

        public Canvas PieceCanvas { get; set; }

        public Pieces PieceType { get; private set; }

        private List<PieceMove> AllLegalPieceMoves = new List<PieceMove>();
        private List<Squares> AllPieceMoves = new List<Squares>();

        public Piece(Pieces piece, Colors color, int pieceValue)
        {
            this.PieceColor = color;
            this.PieceType = piece;
            this.PieceValue = pieceValue;
            this.CurrentSquare = null;
            this.HasMovedOnce = false;
            this.IsPinned = false;
        }

        public enum Pieces
        {
            Pawn,
            Knight,
            Bishop,
            Rook,
            Queen,
            King
        }

        public enum Colors
        {
            White,
            Black
        }

        public List<PieceMove> GetAllCurrentPieceLegalMoves()
        {
            return AllLegalPieceMoves;
        }

        public void ClearAllCurrentLegalPieceMoves()
        {
            AllLegalPieceMoves.Clear();
        }

        public void AddLegalPieceMove(PieceMove pieceMove)
        {
            AllLegalPieceMoves.Add(pieceMove);
        }

        public List<Squares> GetAllCurrentPieceMoves()
        {
            return AllPieceMoves;
        }

        public void ClearAllCurrentPieceMoves()
        {
            AllPieceMoves.Clear();
        }

        public void AddPieceMove(Squares square)
        {   
            if (square != Squares.None)
            {
                AllPieceMoves.Add(square);
            }
        }

        public void UpdatePiecePosition(Square square, Canvas canvas)
        {
            CurrentSquare = square;
            PieceCanvas = canvas;

            CurrentPosition = canvas.Name.Substring(7);

            if (!HasMovedOnce)
            {
                HasMovedOnce = true;
            }
        }

        public void GenerateNewAllMoves(Board board)
        {   
            CreateNewAllMoves();
            CreateNewAllLegalMoves(board, GetAllCurrentPieceMoves());
        }

        public bool DetermineWhenMoveIsBlocked(Square squareOnBoard, Squares square, bool movePathBlocked)
        {
            if (squareOnBoard != null)
            {

                if (!movePathBlocked)
                {
                    // Regular move
                    if (!squareOnBoard.IsOccupied)
                    {
                        PieceMove legalMove = new PieceMove(MoveType.RegularMove, squareOnBoard, square);
                        AddLegalPieceMove(legalMove);
                        return false;
                    }
                    // Blocked move by same color piece
                    else if (squareOnBoard.IsOccupied
                        && squareOnBoard.CurrentPiece.PieceColor == this.PieceColor)
                    {
                        return true;
                    }
                    // Capture move
                    else if (squareOnBoard.IsOccupied
                        && squareOnBoard.CurrentPiece.PieceColor != this.PieceColor)
                    {
                        PieceMove legalMove = new PieceMove(MoveType.CaptureMove, squareOnBoard, square);
                        AddLegalPieceMove(legalMove);
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract void CreateNewAllMoves();
        public abstract void CreateNewAllLegalMoves(Board board, List<Squares> allMoves);
    }
}
