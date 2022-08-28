using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpot : MonoBehaviour
{
    public Piece.Side spotSide;
    public Piece fittedPiece;
    public DualSquareBehavior squareBehavior;


    public void CheckFittedPieces()
    {
        if (spotSide == fittedPiece.pieceSide)
        {
            squareBehavior.CheckSquareFittedPieces();
        }
    }
}
