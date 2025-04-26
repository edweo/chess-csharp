using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessGame.src.Board;

namespace ChessGame.src
{
    public class PieceMove
    {
        private MoveType TypeOfMove;
        public Square MoveSquare { get; private set; }
        public Squares MoveSquareEnum { get; private set; }

        public PieceMove(MoveType typeOfMove, Square square, Squares moveSquareEnum)
        {
            TypeOfMove = typeOfMove;
            MoveSquare = square;
            MoveSquareEnum = moveSquareEnum;
        }

        public enum MoveType
        {   
            RegularMove,
            CaptureMove,
            CheckKingMove,
            CastlingMove,
            PinningMove,
            BlockCheckMove,
            PromotionMove,
            EnPassantMove,
            CheckMateMove
        }

        public MoveType GetPieceMoveType()
        {
            return TypeOfMove;
        }
    }
}
