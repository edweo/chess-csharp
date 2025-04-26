using System;
using System.Collections.Generic;

namespace ChessGame.src
{
    public class Board
    {
        private List<List<Square>> AllSquaresList;

        public Board()
        {
            AllSquaresList = new List<List<Square>>();
            CreateAllBoardSquares();
        }

        /// <summary>
        /// Squares by coordinate
        /// </summary>
        public enum Squares
        {
            A1,A2,A3,A4,A5,A6,A7,A8,
            B1,B2,B3,B4,B5,B6,B7,B8,
            C1,C2,C3,C4,C5,C6,C7,C8,
            D1,D2,D3,D4,D5,D6,D7,D8,
            E1,E2,E3,E4,E5,E6,E7,E8,
            F1,F2,F3,F4,F5,F6,F7,F8,
            G1,G2,G3,G4,G5,G6,G7,G8,
            H1,H2,H3,H4,H5,H6,H7,H8, 
            None
        }

        public static Squares GetEnumSquare(string coordinate)
        {   
            foreach (Squares square in Enum.GetValues(typeof(Squares)))
            {   
                if ((square.ToString().ToUpper()).Equals(coordinate.ToUpper()))
                {
                    return square;
                }
            }

            return Squares.None;
        }

        /// <summary>
        /// Creates the 64 square objects of the board
        /// and stores them in the 2D array list.
        /// Vertical orientation -columns from A to H
        /// </summary>
        private void CreateAllBoardSquares()
        {
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8" };

            for (int i = 0; i < 8; i++)
            {   
                List<Square> currentList = new List<Square>();
                string currentLetter = letters[i];
                for (int k = 0; k < 8; k++)
                {
                    string currentCoordinate = currentLetter + numbers[k];

                    // White Squares
                    if (i % 2 != 0 && k % 2 == 0)
                    {
                        Square square = new Square(currentCoordinate, Piece.Colors.White);
                        currentList.Add(square);
                    }
                    else if (i % 2 == 0 && k % 2 != 0)
                    {
                        Square square = new Square(currentCoordinate, Piece.Colors.White);
                        currentList.Add(square);
                    }
                    // Black Squares
                    if (i % 2 != 0 && k % 2 != 0)
                    {
                        Square square = new Square(currentCoordinate, Piece.Colors.Black);
                        currentList.Add(square);
                    }
                    else if (i % 2 == 0 && k % 2 == 0)
                    {
                        Square square = new Square(currentCoordinate, Piece.Colors.Black);
                        currentList.Add(square);
                    }
                }
                AllSquaresList.Add(currentList);
            }
        }

        public List<List<Square>> GetAllSquareList()
        {
            return this.AllSquaresList;
        }

        public void SetOccupiedSquare(Squares square, bool isOccupied, Piece piece)
        {
            GetBoardSquare(square).IsOccupied = isOccupied;
            GetBoardSquare(square).CurrentPiece = piece;
        }

        /// <summary>
        /// Returns a specific square.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Square GetBoardSquare(Squares wantedSquare)
        {
            string wantedCoordinate = wantedSquare.ToString();

            foreach (List<Square> column in AllSquaresList)
            {
                foreach (Square square in column)
                {
                    if ((square.SquareCoordinate).Equals(wantedCoordinate))
                    {
                        return square;
                    }
                }
            }
            return null;
        }
    }
}
