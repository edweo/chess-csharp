using System;
using System.Collections.Generic;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src.pieces
{
    internal class Rook : Piece
    {
        private List<List<Squares>> rookMoves = new List<List<Squares>>();

        public Rook(Colors color) : base(Pieces.Rook, color, 5)
        {
        }

        public override void CreateNewAllMoves()
        {
            // Clear previous moves
            ClearAllCurrentPieceMoves();
            rookMoves.Clear();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            char xTemp = x;
            int yTemp = y;

            // Y-axis moves
            List<Squares> up = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp++;
                AddPieceMove(GetEnumSquare(x + "" + yTemp));
                up.Add(GetEnumSquare(x + "" + yTemp));
            }
            rookMoves.Add(up);

            yTemp = y;

            List<Squares> down = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                yTemp--;
                AddPieceMove(GetEnumSquare(x + "" + yTemp));
                down.Add(GetEnumSquare(x + "" + yTemp));
            }
            rookMoves.Add(down);

            // X-axis moves
            List<Squares> left = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                xTemp--;
                AddPieceMove(GetEnumSquare(xTemp + "" + y));
                left.Add(GetEnumSquare(xTemp + "" + y));
            }
            rookMoves.Add(left);

            xTemp = x;

            List<Squares> right = new List<Squares>();
            for (int i = 0; i < 7; i++)
            {
                xTemp++;
                AddPieceMove(GetEnumSquare(xTemp + "" + y));
                right.Add(GetEnumSquare(xTemp + "" + y));
            }
            rookMoves.Add(right);
        }

        public override void CreateNewAllLegalMoves(Board board, List<Squares> allMoves)
        {
            // Clear previous legal moves
            ClearAllCurrentLegalPieceMoves();

            bool blockedUp = false;
            bool blockedDown = false;
            bool blockedLeft = false;
            bool blockedRight = false;

            int currentMovesSet = -1;
            foreach (List<Squares> squareSet in rookMoves)
            {
                currentMovesSet++;
                foreach (Squares square in squareSet)
                {
                    Square squareOnBoard = board.GetBoardSquare(square);

                    // Rook upwards moves
                    if (currentMovesSet == 0 && !blockedUp)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedUp);
                        if (blocked)
                        {
                            blockedUp = true;
                        }
                    }
                    // Rook downwards moves
                    else if (currentMovesSet == 1 && !blockedDown)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedDown);
                        if (blocked)
                        {
                            blockedDown = true;
                        }
                    }
                    // Rook left side moves
                    else if (currentMovesSet == 2 && !blockedLeft)
                    {
                        bool blocked = DetermineWhenMoveIsBlocked(squareOnBoard, square, blockedLeft);
                        if (blocked)
                        {
                            blockedLeft = true;
                        }
                    }
                    // Rook right side moves
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
        }
    }
}
