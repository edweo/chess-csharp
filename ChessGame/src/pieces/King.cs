using System;
using System.Collections.Generic;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src.pieces
{
    internal class King : Piece
    {
        public King(Colors color) : base(Pieces.King, color, 0)
        {
        }

        public override void CreateNewAllMoves()
        {
            // Clear previous moves
            ClearAllCurrentPieceMoves();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            char xTemp = x;
            int yTemp = y;

            // 3 upper king moves
            yTemp++;
            xTemp--;
            xTemp--;
            for (int i = 0; i < 3; i++)
            {
                xTemp++;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
            }

            yTemp = y;
            xTemp = x;

            // 3 lower king moves
            yTemp--;
            xTemp--;
            xTemp--;
            for (int i = 0; i < 3; i++)
            {
                xTemp++;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
            }

            yTemp = y;
            xTemp = x;

            // 2 middle side king moves
            xTemp--;
            AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
            xTemp++;
            xTemp++;
            AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
        }

        public override void CreateNewAllLegalMoves(Board board, List<Board.Squares> allMoves)
        {
            // Clear previous legal moves
            ClearAllCurrentLegalPieceMoves();

            foreach (Squares square in allMoves)
            {
                Square squareOnBoard = board.GetBoardSquare(square);

                if (!squareOnBoard.IsOccupied)
                {
                    PieceMove legalMove = new PieceMove(MoveType.RegularMove, squareOnBoard, square);
                    AddLegalPieceMove(legalMove);
                }
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
