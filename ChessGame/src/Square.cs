using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessGame.src.Board;
using static ChessGame.src.Piece;

namespace ChessGame.src
{
    public class Square
    {
        public string SquareCoordinate { get; private set; }
        public bool IsOccupied { get; set; }
        public Piece CurrentPiece { get; set; }

        public Colors SquareColor { get; set; }
        public Squares SquareEnum { get; set; }

        public Square(string squareCoordinate, Colors color)
        {
            SquareCoordinate = squareCoordinate;
            SquareEnum = GetEnumSquare(squareCoordinate);
            IsOccupied = false;
            CurrentPiece = null;
            SquareColor = color;
        }
    }
}
