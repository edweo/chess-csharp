using System;
using System.Collections.Generic;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src.pieces
{
    internal class Knight : Piece
    {
        public Knight(Colors color) : base(Pieces.Knight, color, 3)
        {
        }

        public override void CreateNewAllMoves()
        {
            // Clear previous moves
            ClearAllCurrentPieceMoves();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            // Uppar half moves
            y++;
            x--;
            x--;
            AddPieceMove(GetEnumSquare(x + "" + y));

            y++;
            x++;
            AddPieceMove(GetEnumSquare(x + "" + y));

            x++;
            x++;
            AddPieceMove(GetEnumSquare(x + "" + y));

            x++;
            y--;
            AddPieceMove(GetEnumSquare(x + "" + y));

            // Lower half moves
            y--;
            y--;
            AddPieceMove(GetEnumSquare(x + "" + y));

            y--;
            x--;
            AddPieceMove(GetEnumSquare(x + "" + y));

            x--;
            x--;
            AddPieceMove(GetEnumSquare(x + "" + y));

            y++;
            x--;
            AddPieceMove(GetEnumSquare(x + "" + y));
        }

        public override void CreateNewAllLegalMoves(Board board, List<Board.Squares> allMoves)
        {
            // Clear previous legal moves
            ClearAllCurrentLegalPieceMoves();

            foreach (Squares square in allMoves)
            {
                Square squareOnBoard = board.GetBoardSquare(square);

                // Regular move
                if (!squareOnBoard.IsOccupied)
                {
                    PieceMove legalMove = new PieceMove(MoveType.RegularMove, squareOnBoard, square);
                    AddLegalPieceMove(legalMove);
                }
                // Capture move
                else if (squareOnBoard.IsOccupied)
                {
                    if (squareOnBoard.CurrentPiece.PieceColor != this.PieceColor)
                    {
                        PieceMove legalMove = new PieceMove(MoveType.CaptureMove, squareOnBoard, square);
                        AddLegalPieceMove(legalMove);
                    }
                }
            }
        }
    }
}
