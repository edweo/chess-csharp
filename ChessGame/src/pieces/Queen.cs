using System;
using System.Collections.Generic;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src.pieces
{
    internal class Queen : Piece
    {
        private List<List<Squares>> verticalHorizontalMoves = new List<List<Squares>>();
        private List<List<Squares>> diagonalMoves = new List<List<Squares>>();

        public Queen(Colors color) : base(Pieces.Queen, color, 9)
        {
        }

        public override void CreateNewAllMoves()
        {
            // Clear previous moves
            ClearAllCurrentPieceMoves();
            verticalHorizontalMoves.Clear();
            diagonalMoves.Clear();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            char xTemp = x;
            int yTemp = y;

            // Vertical Horizontal Moves -----------------------------------------------
            List<Squares> up = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp++;
                AddPieceMove(GetEnumSquare(x + "" + yTemp));
                up.Add(GetEnumSquare(x + "" + yTemp));
            }
            verticalHorizontalMoves.Add(up);

            yTemp = y;

            List<Squares> down = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp--;
                AddPieceMove(GetEnumSquare(x + "" + yTemp));
                down.Add(GetEnumSquare(x + "" + yTemp));
            }
            verticalHorizontalMoves.Add(down);

            // X-axis moves
            List<Squares> left = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                xTemp--;
                AddPieceMove(GetEnumSquare(xTemp + "" + y));
                left.Add(GetEnumSquare(xTemp + "" + y));
            }
            verticalHorizontalMoves.Add(left);

            xTemp = x;

            List<Squares> right = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                xTemp++;
                AddPieceMove(GetEnumSquare(xTemp + "" + y));
                right.Add(GetEnumSquare(xTemp + "" + y));
            }
            verticalHorizontalMoves.Add(right);
            //-----------------------------------------------------------------------------

            yTemp = y;
            xTemp = x;

            // Diagonal Moves -------------------------------------------------------------
            List<Squares> leftUp = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp++;
                xTemp--;
                AddPieceMove(GetEnumSquare(xTemp + "" + yTemp));
                leftUp.Add(GetEnumSquare(xTemp + "" + yTemp));
            }
            diagonalMoves.Add(leftUp);

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
            diagonalMoves.Add(rightUp);

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
            diagonalMoves.Add(downRight);

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
            diagonalMoves.Add(downLeft);
            //-----------------------------------------------------------------------------
        }

        public override void CreateNewAllLegalMoves(Board board, List<Board.Squares> allMoves)
        {
            // Clear previous legal moves
            ClearAllCurrentLegalPieceMoves();

            bool blockedUp = false;
            bool blockedDown = false;
            bool blockedLeft = false;
            bool blockedRight = false;

            bool blockedLeftUp = false;
            bool blockedRightUp = false;
            bool blockedDownRight = false;
            bool blockedDownLeft = false;

            int currentMovesSet = -1;
            // Vertical Horizontal Legal Moves -------------------------------------------------
            foreach (List<Squares> squareSet in verticalHorizontalMoves)
            {
                currentMovesSet++;
                foreach (Squares square in squareSet)
                {
                    Square squareOnBoard = board.GetBoardSquare(square);

                    if (currentMovesSet == 0 && !blockedUp)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedUp);
                        if (blocked)
                        {
                            blockedUp = true;
                        }
                    }
                    else if (currentMovesSet == 1 && !blockedDown)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedDown);
                        if (blocked)
                        {
                            blockedDown = true;
                        }
                    }
                    else if (currentMovesSet == 2 && !blockedLeft)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedLeft);
                        if (blocked)
                        {
                            blockedLeft = true;
                        }
                    }
                    else if (currentMovesSet == 3 && !blockedRight)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedRight);
                        if (blocked)
                        {
                            blockedRight = true;
                        }
                    }
                }
            }
            // ----------------------------------------------------------------------------------

            // Diagonal Legal Moves -------------------------------------------------------------
            currentMovesSet = -1;
            foreach (List<Squares> squareSet in diagonalMoves)
            {
                currentMovesSet++;
                foreach (Squares square in squareSet)
                {
                    Square squareOnBoard = board.GetBoardSquare(square);

                    if (currentMovesSet == 0 && !blockedLeftUp)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedLeftUp);
                        if (blocked)
                        {
                            blockedLeftUp = true;
                        }
                    }
                    else if (currentMovesSet == 1 && !blockedRightUp)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedRightUp);
                        if (blocked)
                        {
                            blockedRightUp = true;
                        }
                    }
                    else if (currentMovesSet == 2 && !blockedDownRight)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedDownRight);
                        if (blocked)
                        {
                            blockedDownRight = true;
                        }
                    }
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
