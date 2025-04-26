using ChessGame.src.pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static ChessGame.src.Board;
using static ChessGame.src.Piece;

namespace ChessGame.src
{
    internal class Game
    {
        public int NumberOfMoves { get; set; }
        public bool IsWhiteTurn { get; set; }
        public bool IsBlackKingChecked { get; set; }
        public bool IsWhiteKingChecked { get; set; }
        public bool HasSeelctedPiece { get; set; }
        public Piece CurrentlySelectedPiece { get; set; }

        private List<List<Piece>> whitePieces = new List<List<Piece>>();
        private List<List<Piece>> blackPieces = new List<List<Piece>>();

        private Board board;

        private List<Squares> CurrentlyHighlightedPieceMoves = new List<Squares>();
        private List<Image> CurrentlyDisaplayedGreenDots = new List<Image>();

        private List<List<Piece>> capturedWhitePieces = new List<List<Piece>>();
        private List<List<Piece>> capturedBlackPieces = new List<List<Piece>>();

        public Game()
        {
            board = new Board();
            CreateAllGameChessPieces();
            IsWhiteTurn = true;
            HasSeelctedPiece = false;
            CurrentlySelectedPiece = null;

            IsBlackKingChecked = false;
            IsWhiteKingChecked = false;
        }

        /// <summary>
        /// Creates the complete 32 set of pieces for a game of chess.
        /// </summary>
        private void CreateAllGameChessPieces()
        {
            CreateGamePiecesColorSet(Colors.White);
            CreateGamePiecesColorSet(Colors.Black);
        }

        /// <summary>
        /// Create a set 16 of colored pieces.
        /// </summary>
        /// <param name="isWhiteColor"></param>
        private void CreateGamePiecesColorSet(Colors color)
        {   
            // CREATES EMPTY SETS FOR THE 2D-ARRAY

            // Pawns
            AddPiecesSetToGameSet(CreateSetOfPieces(8, Pieces.Pawn, color), color);

            // Knights
            AddPiecesSetToGameSet(CreateSetOfPieces(2, Pieces.Knight, color), color);

            // Bishops
            AddPiecesSetToGameSet(CreateSetOfPieces(2, Pieces.Bishop, color), color);

            // Rooks
            AddPiecesSetToGameSet(CreateSetOfPieces(2, Pieces.Rook, color), color);

            // Queen
            AddPiecesSetToGameSet(CreateSetOfPieces(1, Pieces.Queen, color), color);

            // King
            AddPiecesSetToGameSet(CreateSetOfPieces(1, Pieces.King, color), color);
        }

        /// <summary>
        /// Create a List<> of specified amount of colored pieces.
        /// </summary>
        /// <param name="isWhiteColor"></param>
        /// <param name="piecesAmount"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        private List<Piece> CreateSetOfPieces(int piecesAmount, Pieces piece, Colors color)
        {   
            List<Piece> pieces = new List<Piece>();
            for (int i = 0; i < piecesAmount; i++)
            {
                pieces.Add(CreateSinglePiece(piece, color));
            }
            return pieces;
        }

        /// <summary>
        /// Create a single specific chess piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="isWhiteColor"></param>
        /// <returns></returns>
        private Piece CreateSinglePiece(Pieces piece, Colors color)
        {
            switch (piece)
            {
                case Pieces.Pawn:
                    Pawn pawn = new Pawn(color);
                    return pawn;

                case Pieces.Knight:
                    Knight knight = new Knight(color);
                    return knight;

                case Pieces.Bishop:
                    Bishop bishop = new Bishop(color);
                    return bishop;

                case Pieces.Rook:
                    Rook rook = new Rook(color);
                    return rook;

                case Pieces.Queen:
                    Queen queen = new Queen(color);
                    return queen;

                case Pieces.King:
                    King king = new King(color);
                    return king;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Adds a set of peice to the main game pieces set.
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="isWhiteColor"></param>
        private void AddPiecesSetToGameSet(List<Piece> pieces, Colors color)
        {
            switch (color)
            {
                case Colors.White:
                    whitePieces.Add(pieces);
                    break;

                case Colors.Black:
                    blackPieces.Add(pieces);
                    break;

                default:
                    break;
            }
        }
        
        public List<Image> GetCurrentlyDisaplayedGreenDots()
        {
            return CurrentlyDisaplayedGreenDots;
        }

        public void AddCurrentlyDisaplayedGreenDot(Image img)
        {
            CurrentlyDisaplayedGreenDots.Add(img);
        }

        public void RemoveCurrentlyDisaplayedGreenDot(Image img)
        {
            CurrentlyDisaplayedGreenDots.Remove(img);
        }

        public void ClearCurrentlyDisaplayedGreenDots()
        {
            CurrentlyDisaplayedGreenDots.Clear();
        }

        public Board GetBoard()
        {
            return this.board;
        }

        public List<Squares> GetCurrentlyHighlightedPieceMoves()
        {
            return CurrentlyHighlightedPieceMoves;
        }

        public void AddCurrentlyHighlightedPieceMove(List<PieceMove> movesList)
        {
            foreach (PieceMove pieceMove in movesList)
            {
                CurrentlyHighlightedPieceMoves.Add(pieceMove.MoveSquareEnum);
            }
        }

        public void ClearCurrentlyHighlightedPieceMoves()
        {
            CurrentlyHighlightedPieceMoves.Clear();
        } 

        public List<List<Piece>> GetWhitePiecesList()
        {
            return this.whitePieces;
        }

        public List<List<Piece>> GetBlackPiecesList()
        {
            return this.blackPieces;
        }
        
        // SITA PAFIXINTI KAZKA NEZINAU
        public void RemovePieceFromGameBoard(Piece piece)
        {   
            // Determine piece color set
            List<List<Piece>> list;
            if (piece.PieceColor == Colors.White)
            {
                list = whitePieces;
            }
            else
            {
                list = blackPieces;
            }

            // Find piece and remove it from the list
            foreach (List<Piece> setPieces in list)
            {
                foreach (Piece pieceFromSet in setPieces)
                {
                    if (piece == pieceFromSet)
                    {
                        setPieces.Remove(pieceFromSet);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Find a piece by the image reference when clicked.
        /// </summary>
        /// <returns></returns>
        public Piece GetPieceByCanvas(Square square)
        {
            // White Pieces
            foreach (List<Piece> piecesList in whitePieces)
            {
                foreach (Piece piece in piecesList)
                {   
                    if (piece != null)
                    {
                        if (piece.CurrentSquare == square)
                        {   
                            return piece;
                        }
                    }
                }
            }
            
            // Black Pieces
            foreach (List<Piece> piecesList in blackPieces)
            {
                foreach (Piece piece in piecesList)
                {
                    if (piece != null)
                    {
                        if (piece.CurrentSquare == square)
                        {
                            return piece;
                        }
                    }
                }
            }

            return null;
        }

        public void AddWhitePawnToList(Piece piece)
        {
            whitePieces[0].Add(piece);
        }

        public void AddBlackPawnToList(Piece piece)
        {
            blackPieces[0].Add(piece);
        }

        /// <summary>
        /// DEBUGGING PURPOSES
        /// </summary>
        public void PrintCurrentlyHighlightedPieceMoves()
        {
            Console.Write("Highlited moves: ");
            foreach (Squares square in CurrentlyHighlightedPieceMoves)
            {
                Console.Write(square.ToString() + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Debugging.
        /// </summary>
        /// <param name="piece"></param>
        public void PrintAllMoves(Piece piece)
        {
            Console.Write("ALL moves: ");
            foreach (Squares square in piece.GetAllCurrentPieceMoves())
            {
                Console.Write(square.ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}
