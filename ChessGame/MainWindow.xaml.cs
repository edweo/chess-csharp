using ChessGame.src;
using ChessGame.src.pieces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static ChessGame.src.Board;
using static ChessGame.src.Piece;
using static ChessGame.src.PieceMove;

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDictionary _imagesResource = new ResourceDictionary();
        private ResourceDictionary _canvasResource = new ResourceDictionary();

        private Game game;

        public MainWindow()
        {
            InitializeComponent();

            // Main game class
            game = new Game();

            // Bind game class to XAML
            DataContext = game;

            // Resource Dictionaries
            _imagesResource.Source = new Uri("/styles/Images.xaml", UriKind.RelativeOrAbsolute);
            _canvasResource.Source = new Uri("/styles/CanvasStyle.xaml", UriKind.RelativeOrAbsolute);

            AddStartingPiecesToBoard();
            UpdateAllPieceMoves();
        }

        /// <summary>
        /// Add all starting pices to start the game.
        /// </summary>
        public void AddStartingPiecesToBoard()
        {
            // Pieces list
            List<List<Piece>> whitePiecesList = game.GetWhitePiecesList();
            List<List<Piece>> blackPiecesList = game.GetBlackPiecesList();

            // White Pawns
            Squares[] whitePawnSquares = { Squares.A2, Squares.B2, Squares.C2, Squares.D2, Squares.E2, Squares.F2, Squares.G2, Squares.H2 };
            for (int i = 0; i < 8; i++)
            {
                AddPieceToBoard(whitePiecesList[0][i], game.GetBoard().GetBoardSquare(whitePawnSquares[i]));
            }
            // White Knights
            Squares[] whiteKnightSquares = { Squares.B1, Squares.G1 };
            for (int i = 0; i < 2; i++)
            {
                AddPieceToBoard(whitePiecesList[1][i], game.GetBoard().GetBoardSquare(whiteKnightSquares[i]));
            }
            // White Bishops
            Squares[] whiteBishopSquares = { Squares.C1, Squares.F1 };
            for (int i = 0; i < 2; i++)
            {
                AddPieceToBoard(whitePiecesList[2][i], game.GetBoard().GetBoardSquare(whiteBishopSquares[i]));
            }
            // White Rooks
            Squares[] whiteRooksSquares = { Squares.A1, Squares.H1 };
            for (int i = 0; i < 2; i++)
            {
                AddPieceToBoard(whitePiecesList[3][i], game.GetBoard().GetBoardSquare(whiteRooksSquares[i]));
            }
            // White Queen
            AddPieceToBoard(whitePiecesList[4][0], game.GetBoard().GetBoardSquare(Squares.D1));
            // White King
            AddPieceToBoard(whitePiecesList[5][0], game.GetBoard().GetBoardSquare(Squares.E1));

            // -----------------------------------------------------------------------------------------------------------------------------

            // Black Pawns
            Squares[] blackPawnSquares = { Squares.A7, Squares.B7, Squares.C7, Squares.D7, Squares.E7, Squares.F7, Squares.G7, Squares.H7 };
            for (int i = 0; i < 8; i++)
            {
                AddPieceToBoard(blackPiecesList[0][i], game.GetBoard().GetBoardSquare(blackPawnSquares[i]));
            }
            // Black Knights
            Squares[] blackKnightSquares = { Squares.B8, Squares.G8 };
            for (int i = 0; i < 2; i++)
            {
                AddPieceToBoard(blackPiecesList[1][i], game.GetBoard().GetBoardSquare(blackKnightSquares[i]));
            }
            // Black Bishops
            Squares[] blackBishopSquares = { Squares.C8, Squares.F8 };
            for (int i = 0; i < 2; i++)
            {
                AddPieceToBoard(blackPiecesList[2][i], game.GetBoard().GetBoardSquare(blackBishopSquares[i]));
            }
            // Black Rooks
            Squares[] blackRooksSquares = { Squares.A8, Squares.H8 };
            for (int i = 0; i < 2; i++)
            {
                AddPieceToBoard(blackPiecesList[3][i], game.GetBoard().GetBoardSquare(blackRooksSquares[i]));
            }
            // Black Queen
            AddPieceToBoard(blackPiecesList[4][0], game.GetBoard().GetBoardSquare(Squares.D8));
            // Black King
            AddPieceToBoard(blackPiecesList[5][0], game.GetBoard().GetBoardSquare(Squares.E8));
        }

        /// <summary>
        /// Add a piece to the board
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        /// <param name="border"></param>
        public void AddPieceToBoard(Piece piece, Square square)
        {
            if (!square.IsOccupied)
            {
                // Update Square object
                square.CurrentPiece = piece;
                square.IsOccupied = true;

                // Update Piece's current position
                piece.CurrentPosition = square.SquareCoordinate;
                piece.CurrentSquare = square;

                // Place piece on visual board
                AddPieceImageToSquare(piece, GetCanvasSquare(square));
            }
        }

        public void AddPieceToSquare(Piece piece, Square square, Canvas canvas)
        {
            square.CurrentPiece = piece;
            square.IsOccupied = true;

            // Place piece on visual board
            piece.UpdatePiecePosition(square, canvas);
            AddPieceImageToSquare(piece, GetCanvasSquare(square));
        }

        public void AddPieceImageToSquare(Piece piece, Canvas canvas)
        {
            Image img = GetPieceImage(piece.PieceType, piece.PieceColor);

            canvas.Children.Add(img);
            canvas.MouseLeftButtonDown += Piece_MouseLeftButtonDown;
            piece.PieceCanvas = canvas;
        }

        public void UpdateAllPieceMoves()
        {   
            // White Pieces
            foreach (List<Piece> pieceSet in game.GetWhitePiecesList())
            {
                foreach (Piece piece in pieceSet)
                {   
                    if (piece != null)
                    {
                        piece.GenerateNewAllMoves(game.GetBoard());
                    }
                }
            }

            // Black Pieces
            foreach (List<Piece> pieceSet in game.GetBlackPiecesList())
            {
                foreach (Piece piece in pieceSet)
                {   
                    if (piece != null)
                    {
                        piece.GenerateNewAllMoves(game.GetBoard());
                    }
                }
            }
        }

        /// <summary>
        /// Mouse Click event when pressing a piece.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Piece_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Piece piece = game.GetPieceByCanvas(GetSquareByCanvas(sender as Canvas));
            Console.WriteLine("Piece: " + piece.PieceColor + " " + piece.PieceType + " Square: " + piece.CurrentPosition);

            if (game.IsWhiteTurn && piece.PieceColor == Colors.White)
            {
                PieceMouseButtonEvent(piece);
            }
            else if (!game.IsWhiteTurn && piece.PieceColor == Colors.Black)
            {
                PieceMouseButtonEvent(piece);
            }
        }

        private void PieceMouseButtonEvent(Piece piece)
        {
            if (!game.HasSeelctedPiece)
            {
                // Set new currently selected piece
                game.HasSeelctedPiece = true;
                game.CurrentlySelectedPiece = piece;

                GenerateMovesSelectedPiece(piece);
            }
            else
            {
                SelectAnotherPieceOnSquare(piece);
                GenerateMovesSelectedPiece(piece);
            }
        }

        public void SelectAnotherPieceOnSquare(Piece piece)
        {
            ClearGreenDots(game.GetCurrentlyHighlightedPieceMoves());
            game.ClearCurrentlyHighlightedPieceMoves();
            game.CurrentlySelectedPiece.PieceCanvas.Style = GetCanvasDefaultSquareColor(game.CurrentlySelectedPiece.CurrentSquare);
            game.CurrentlySelectedPiece = piece;
        }

        public void GenerateMovesSelectedPiece(Piece piece)
        {
            // Square and canvas
            Square pieceSquare = game.GetBoard().GetBoardSquare(GetEnumSquare(piece.CurrentPosition));
            Canvas pieceSquareCanvas = GetCanvasSquare(pieceSquare);
            pieceSquareCanvas.Style = _canvasResource["CanvasSelectedSquare"] as Style;

            // Display all legal moves
            List<PieceMove> allLegalMoves = piece.GetAllCurrentPieceLegalMoves();
            game.AddCurrentlyHighlightedPieceMove(allLegalMoves);
            foreach (PieceMove pieceMove in allLegalMoves)
            {
                Square selcetedSquare = game.GetBoard().GetBoardSquare(pieceMove.MoveSquareEnum);
                Canvas selectedBorder = GetCanvasSquare(selcetedSquare);
                Image greenDot = GetGreenDotImage(piece, selectedBorder, selcetedSquare);

                if (selectedBorder != null)
                {
                    selectedBorder.Children.Add(greenDot);
                    game.AddCurrentlyDisaplayedGreenDot(greenDot);
                }
            }
            game.PrintAllMoves(piece);
            game.PrintCurrentlyHighlightedPieceMoves();
        }

        public Image GetGreenDotImage(Piece piece, Canvas canvasSelect, Square square)
        {
            Image img = new Image();

            // Remove piece event click if present
            if (square.IsOccupied)
            {
                canvasSelect.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                img.Style = _imagesResource["icon_GreenCapture"] as Style;
            }
            else
            {
                img.Style = _imagesResource["icon_GreenDot"] as Style;
            }

            canvasSelect.MouseLeftButtonDown += MakeMove_CLick;
            return img;
        }

        private void MakeMove_CLick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Piece pieceToMove = game.CurrentlySelectedPiece;

            // Target square
            Canvas canvasTargetMove = (Canvas)sender;
            Square squareTargetMove = GetSquareByCanvas(canvasTargetMove);

            // Selected piece square
            Canvas canvasSelectedPiece = pieceToMove.PieceCanvas;
            Square squareSelectedPiece = pieceToMove.CurrentSquare;

            // Find the current move
            foreach (PieceMove pieceMove in pieceToMove.GetAllCurrentPieceLegalMoves())
            {
                if (GetCanvasSquare(pieceMove.MoveSquare) == canvasTargetMove)
                {
                    Console.WriteLine("This is move: " + pieceMove.GetPieceMoveType());
                    switch(pieceMove.GetPieceMoveType())
                    {   
                        // Regular move
                        case MoveType.RegularMove:

                            // Update Canvas
                            canvasSelectedPiece.Children.Clear();
                            canvasSelectedPiece.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                            canvasSelectedPiece.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                            canvasSelectedPiece.Style = GetCanvasDefaultSquareColor(squareSelectedPiece);

                            // Update Square
                            squareSelectedPiece.CurrentPiece = null;
                            squareSelectedPiece.IsOccupied = false;

                            // Update Piece
                            AddPieceToSquare(pieceToMove, squareTargetMove, canvasTargetMove);
                            canvasTargetMove.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                            break;

                        // Capture move
                        case MoveType.CaptureMove:

                            Piece capturedPiece = squareTargetMove.CurrentPiece;

                            // Update Canvas
                            canvasSelectedPiece.Children.Clear();
                            canvasSelectedPiece.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                            canvasSelectedPiece.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                            canvasSelectedPiece.Style = GetCanvasDefaultSquareColor(squareSelectedPiece);
                            canvasTargetMove.Children.Clear();

                            // Update Square
                            squareSelectedPiece.CurrentPiece = null;
                            squareSelectedPiece.IsOccupied = false;

                            // Update Pieces
                            game.RemovePieceFromGameBoard(capturedPiece);
                            AddPieceToSquare(pieceToMove, squareTargetMove, canvasTargetMove);
                            canvasTargetMove.MouseLeftButtonDown -= Piece_MouseLeftButtonDown;
                            break;
                    }
                }
            }
            CleanUpAfterMove();
        }

        public void CleanUpAfterMove()
        {
            game.CurrentlySelectedPiece.PieceCanvas.Style = GetCanvasDefaultSquareColor(game.CurrentlySelectedPiece.CurrentSquare);
            game.CurrentlySelectedPiece = null;
            game.HasSeelctedPiece = false;
            ClearGreenDots(game.GetCurrentlyHighlightedPieceMoves());
            game.ClearCurrentlyHighlightedPieceMoves();
            UpdateAllPieceMoves();

            if (game.IsWhiteTurn)
            {
                game.IsWhiteTurn = false;
            }
            else if (!game.IsWhiteTurn)
            {
                game.IsWhiteTurn = true;
            }
        }

        public Style GetCanvasDefaultSquareColor(Square square)
        {   
            if (square.SquareColor == Colors.White)
            {
                return _canvasResource["CanvasWhiteSquare"] as Style;
            }
            else
            {
                return _canvasResource["CanvasBlackSquare"] as Style;
            }
        }

        public void ClearGreenDots(List<Squares> squaresList)
        {   
            List<Image> elementsToRemove = new List<Image>();

            foreach (Squares square in squaresList)
            {
                Square selcetedSquare = game.GetBoard().GetBoardSquare(square);
                Canvas selectedBorder = GetCanvasSquare(selcetedSquare);
                
                if (selectedBorder != null)
                {   
                    // Remove Make move click
                    selectedBorder.MouseLeftButtonDown -= MakeMove_CLick;
                    
                    // Re-add the piece event click if a was occupied
                    // PADARYTI kad atgal remove kai CAPTURE MOVE
                    if (selcetedSquare.IsOccupied)
                    {
                        selectedBorder.MouseLeftButtonDown += Piece_MouseLeftButtonDown;
                    }

                    // SITA FIXINTI KAIP DOTUS PANAIKINTI
                    int elementsCount = selectedBorder.Children.Count;
                    foreach (Image child in selectedBorder.Children)
                    {
                        foreach (Image img in game.GetCurrentlyDisaplayedGreenDots())
                        {
                            if (img == child)
                            {
                                elementsToRemove.Add(child);
                            }
                        }
                    }

                    // Remove the elements
                    foreach (Image image in elementsToRemove)
                    {
                        selectedBorder.Children.Remove(image);
                    }
                }
            }
            game.ClearCurrentlyDisaplayedGreenDots();
        }

        public Square GetSquareByCanvas(object sender)
        {   
            var element = (FrameworkElement)sender;
            var name = element.Name;

            string canvasName = name.ToString();

            string coordinate = canvasName.Substring(7);

            Squares square = GetEnumSquare(coordinate);

            return game.GetBoard().GetBoardSquare(square);
        }

        public Image GetPieceImage(Pieces piece, Colors color)
        {
            Image img = new Image();
            switch (color)
            {
                case Colors.White:
                    switch (piece)
                    {
                        case Pieces.Pawn:
                            img.Style = _imagesResource["icon_WhitePawn"] as Style;
                            break;
                        case Pieces.Knight:
                            img.Style = _imagesResource["icon_WhiteKnight"] as Style;
                            break;
                        case Pieces.Bishop:
                            img.Style = _imagesResource["icon_WhiteBishop"] as Style;
                            break;
                        case Pieces.Rook:
                            img.Style = _imagesResource["icon_WhiteRook"] as Style;
                            break;
                        case Pieces.Queen:
                            img.Style = _imagesResource["icon_WhiteQueen"] as Style;
                            break;
                        case Pieces.King:
                            img.Style = _imagesResource["icon_WhiteKing"] as Style;
                            break;
                    }
                    break;
                case Colors.Black:
                    switch (piece)
                    {
                        case Pieces.Pawn:
                            img.Style = _imagesResource["icon_BlackPawn"] as Style;
                            break;
                        case Pieces.Knight:
                            img.Style = _imagesResource["icon_BlackKnight"] as Style;
                            break;
                        case Pieces.Bishop:
                            img.Style = _imagesResource["icon_BlackBishop"] as Style;
                            break;
                        case Pieces.Rook:
                            img.Style = _imagesResource["icon_BlackRook"] as Style;
                            break;
                        case Pieces.Queen:
                            img.Style = _imagesResource["icon_BlackQueen"] as Style;
                            break;
                        case Pieces.King:
                            img.Style = _imagesResource["icon_BlackKing"] as Style;
                            break;
                    }
                    break;
            }
            return img;
        }

        /// <summary>
        /// Returns border x:Name.
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        public Canvas GetCanvasSquare(Square square)
        {   
            if (square != null)
            {
                string letter = square.SquareCoordinate.Substring(0, 1);
                int number = Convert.ToInt32(square.SquareCoordinate.Substring(1, 1));

                switch (letter)
                {
                    case "A":
                        if (number == 1)
                        {
                            return border_A1;
                        }
                        else if (number == 2)
                        {
                            return border_A2;
                        }
                        else if (number == 3)
                        {
                            return border_A3;
                        }
                        else if (number == 4)
                        {
                            return border_A4;
                        }
                        else if (number == 5)
                        {
                            return border_A5;
                        }
                        else if (number == 6)
                        {
                            return border_A6;
                        }
                        else if (number == 7)
                        {
                            return border_A7;
                        }
                        else
                        {
                            return border_A8;
                        }

                    case "B":
                        if (number == 1)
                        {
                            return border_B1;
                        }
                        else if (number == 2)
                        {
                            return border_B2;
                        }
                        else if (number == 3)
                        {
                            return border_B3;
                        }
                        else if (number == 4)
                        {
                            return border_B4;
                        }
                        else if (number == 5)
                        {
                            return border_B5;
                        }
                        else if (number == 6)
                        {
                            return border_B6;
                        }
                        else if (number == 7)
                        {
                            return border_B7;
                        }
                        else
                        {
                            return border_B8;
                        }

                    case "C":
                        if (number == 1)
                        {
                            return border_C1;
                        }
                        else if (number == 2)
                        {
                            return border_C2;
                        }
                        else if (number == 3)
                        {
                            return border_C3;
                        }
                        else if (number == 4)
                        {
                            return border_C4;
                        }
                        else if (number == 5)
                        {
                            return border_C5;
                        }
                        else if (number == 6)
                        {
                            return border_C6;
                        }
                        else if (number == 7)
                        {
                            return border_C7;
                        }
                        else
                        {
                            return border_C8;
                        }

                    case "D":
                        if (number == 1)
                        {
                            return border_D1;
                        }
                        else if (number == 2)
                        {
                            return border_D2;
                        }
                        else if (number == 3)
                        {
                            return border_D3;
                        }
                        else if (number == 4)
                        {
                            return border_D4;
                        }
                        else if (number == 5)
                        {
                            return border_D5;
                        }
                        else if (number == 6)
                        {
                            return border_D6;
                        }
                        else if (number == 7)
                        {
                            return border_D7;
                        }
                        else
                        {
                            return border_D8;
                        }

                    case "E":
                        if (number == 1)
                        {
                            return border_E1;
                        }
                        else if (number == 2)
                        {
                            return border_E2;
                        }
                        else if (number == 3)
                        {
                            return border_E3;
                        }
                        else if (number == 4)
                        {
                            return border_E4;
                        }
                        else if (number == 5)
                        {
                            return border_E5;
                        }
                        else if (number == 6)
                        {
                            return border_E6;
                        }
                        else if (number == 7)
                        {
                            return border_E7;
                        }
                        else
                        {
                            return border_E8;
                        }

                    case "F":
                        if (number == 1)
                        {
                            return border_F1;
                        }
                        else if (number == 2)
                        {
                            return border_F2;
                        }
                        else if (number == 3)
                        {
                            return border_F3;
                        }
                        else if (number == 4)
                        {
                            return border_F4;
                        }
                        else if (number == 5)
                        {
                            return border_F5;
                        }
                        else if (number == 6)
                        {
                            return border_F6;
                        }
                        else if (number == 7)
                        {
                            return border_F7;
                        }
                        else
                        {
                            return border_F8;
                        }

                    case "G":
                        if (number == 1)
                        {
                            return border_G1;
                        }
                        else if (number == 2)
                        {
                            return border_G2;
                        }
                        else if (number == 3)
                        {
                            return border_G3;
                        }
                        else if (number == 4)
                        {
                            return border_G4;
                        }
                        else if (number == 5)
                        {
                            return border_G5;
                        }
                        else if (number == 6)
                        {
                            return border_G6;
                        }
                        else if (number == 7)
                        {
                            return border_G7;
                        }
                        else
                        {
                            return border_G8;
                        }

                    case "H":
                        if (number == 1)
                        {
                            return border_H1;
                        }
                        else if (number == 2)
                        {
                            return border_H2;
                        }
                        else if (number == 3)
                        {
                            return border_H3;
                        }
                        else if (number == 4)
                        {
                            return border_H4;
                        }
                        else if (number == 5)
                        {
                            return border_H5;
                        }
                        else if (number == 6)
                        {
                            return border_H6;
                        }
                        else if (number == 7)
                        {
                            return border_H7;
                        }
                        else
                        {
                            return border_H8;
                        }
                }
                return null;
            } 
            else
            {
                return null;
            }
        }
    }
}
