using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessGame.src.Board;
using static ChessGame.src.PieceMove;

namespace ChessGame.src.pieces
{
    internal class Pawn : Piece
    {
        
        public Pawn(Colors color) : base(Pieces.Pawn, color, 1)
        {
        }

        public override void CreateNewAllMoves()
        {   
            // Clear previous moves
            ClearAllCurrentPieceMoves();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            if (PieceColor == Colors.White)
            {
                if (!HasMovedOnce)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i == 1)
                        {
                            y++;
                            x--;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            x++;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            x++;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            x--;
                        }
                        else
                        {
                            y++;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            y--;
                        }
                    }
                }
                else
                {
                    y++;
                    x--;
                    AddPieceMove(GetEnumSquare(x + "" + y));
                    x++;
                    AddPieceMove(GetEnumSquare(x + "" + y));
                    x++;
                    AddPieceMove(GetEnumSquare(x + "" + y));
                }
            }
            else if (PieceColor == Colors.Black)
            {
                if (!HasMovedOnce)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        if (i == 1)
                        {
                            y--;
                            x--;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            x++;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            x++;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            x--;
                        }
                        else
                        {
                            y--;
                            AddPieceMove(GetEnumSquare(x + "" + y));
                            y++;
                        }
                    }
                }
                else
                {
                    y--;
                    x--;
                    AddPieceMove(GetEnumSquare(x + "" + y));
                    x++;
                    AddPieceMove(GetEnumSquare(x + "" + y));
                    x++;
                    AddPieceMove(GetEnumSquare(x + "" + y));
                }
            }
        }

        public override void CreateNewAllLegalMoves(Board board, List<Squares> allMoves)
        {
            // Clear previous legal moves
            ClearAllCurrentLegalPieceMoves();

            char x = Convert.ToChar(CurrentPosition.Substring(0, 1));
            int y = Convert.ToInt32(CurrentPosition.Substring(1, 1));

            // Constants depending on the piece color
            int directionY = 1;
            if (PieceColor != Colors.White)
            {
                directionY = -1;
            }

            // Determine if move forward is blocked
            bool blockedForwardMove = false;
            if (!HasMovedOnce)
            {   
                if (allMoves.Count == 4)
                {
                    if (board.GetBoardSquare(allMoves[1]).IsOccupied)
                    {
                        blockedForwardMove = true;
                    }
                }
                else if (allMoves.Count <= 3)
                {
                    if (board.GetBoardSquare(allMoves[0]).IsOccupied)
                    {
                        blockedForwardMove = true;
                    }
                }
            }

            // Filter out legal moves
            foreach (Squares square in allMoves)
            {
                Square squareOnBoard = board.GetBoardSquare(square);

                if (square != Squares.None)
                {
                    // Regular forward move
                    if (!squareOnBoard.IsOccupied && !blockedForwardMove)
                    {   
                        // Regular 1 square move
                        if (square == GetEnumSquare(x + "" + (y + (1*directionY))))
                        {
                            PieceMove legalMove = new PieceMove(MoveType.RegularMove, squareOnBoard, square);
                            AddLegalPieceMove(legalMove);
                        }
                        // First pawn move 2 squares
                        else if (square == GetEnumSquare(x + "" + (y + (2 * directionY))) && !HasMovedOnce)
                        {
                            PieceMove legalMove = new PieceMove(MoveType.RegularMove, squareOnBoard, square);
                            AddLegalPieceMove(legalMove);
                        }
                    }
                    else if (squareOnBoard.IsOccupied)
                    {
                        // Left-capture move
                        if (squareOnBoard.CurrentPiece.PieceColor != this.PieceColor 
                            && square == GetEnumSquare(Convert.ToChar(x - 1) + "" + (y + (1 * directionY)))) {
                            PieceMove legalMove = new PieceMove(MoveType.CaptureMove, squareOnBoard, square);
                            AddLegalPieceMove(legalMove);
                        }
                        // Right-capture move
                        else if (squareOnBoard.CurrentPiece.PieceColor != this.PieceColor 
                            && square == GetEnumSquare(Convert.ToChar(x + 1) + "" + (y + (1 * directionY))))
                        {
                            PieceMove legalMove = new PieceMove(MoveType.CaptureMove, squareOnBoard, square);
                            AddLegalPieceMove(legalMove);
                        }
                    }
                }
            }
        }
    }
}
