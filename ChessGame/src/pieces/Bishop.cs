using System;
using System.Collections.Generic;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src.pieces
{
    internal class Bishop : Piece
    {
        private List<List<Squares>> bishopMoves = new List<List<Squares>>();

        public Bishop(Colors color) : base(Pieces.Bishop, color, 3)
        {
        }

        public override void CreateNewAllMoves()
        {
            // Clear previous moves
            ClearAllCurrentPieceMoves();
            bishopMoves.Clear();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            char xTemp = x;
            int yTemp = y;

            // Left Up moves
            List<Squares> leftUp = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp++;
                xTemp--;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
                leftUp.Add(GetEnumSquare(xTemp + "" + yTemp));
            }
            bishopMoves.Add(leftUp);

            yTemp = y;
            xTemp = x;

            // Right Up moves
            List<Squares> rightUp = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp++;
                xTemp++;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
                rightUp.Add(GetEnumSquare(xTemp + "" + yTemp));
            }
            bishopMoves.Add(rightUp);

            yTemp = y;
            xTemp = x;

            // Down Right moves
            List<Squares> downRight = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp--;
                xTemp++;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
                downRight.Add(GetEnumSquare(xTemp + "" + yTemp));
            }
            bishopMoves.Add(downRight);

            yTemp = y;
            xTemp = x;

            // Down Lerft moves
            List<Squares> downLeft = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp--;
                xTemp--;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
                downLeft.Add(GetEnumSquare(xTemp + "" + yTemp));
            }
            bishopMoves.Add(downLeft);
        }

        public override void CreateNewAllLegalMoves(Board board, List<Board.Squares> allMoves)
        {
            // Clear previous legal moves
            ClearAllCurrentLegalPieceMoves();

            bool blockedLeftUp = false;
            bool blockedRightUp = false;
            bool blockedDownRight = false;
            bool blockedDownLeft = false;

            int currentMovesSet = -1;
            foreach (List<Squares> squareSet in bishopMoves)
            {
                currentMovesSet++;
                foreach (Squares square in squareSet)
                {
                    Square squareOnBoard = board.GetBoardSquare(square);

                    // Bishop left up moves
                    if (currentMovesSet == 0 && !blockedLeftUp)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedLeftUp);
                        if (blocked)
                        {
                            blockedLeftUp = true;
                        }
                    }
                    // Bishop right up moves
                    else if (currentMovesSet == 1 && !blockedRightUp)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedRightUp);
                        if (blocked)
                        {
                            blockedRightUp = true;
                        }
                    }
                    // Bishop down right moves
                    else if (currentMovesSet == 2 && !blockedDownRight)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedDownRight);
                        if (blocked)
                        {
                            blockedDownRight = true;
                        }
                    }
                    // Bishop down left moves
                    else if (currentMovesSet == 3 && !blockedDownLeft)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedDownLeft);
                        if (blocked)
                        {
                            blockedDownLeft = true;
                        }
                    }
                }
            }
        }
    }
}
