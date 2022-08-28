using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DualSquareBehavior : MonoBehaviour
{
    BoardSpot boardSpotLeft, boardSpotRight;

    public ParticleSystem particleObject;

    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.GetGameManagerSingleton();

        FillBoardSpots();
    }

    void FillBoardSpots()
    {
        boardSpotLeft = transform.GetChild(0).GetComponent<BoardSpot>();
        boardSpotRight = transform.GetChild(1).GetComponent<BoardSpot>();

        boardSpotLeft.squareBehavior = this;
        boardSpotRight.squareBehavior = this;

        boardSpotLeft.spotSide = Piece.Side.left;
        boardSpotRight.spotSide = Piece.Side.right;

    }

    public void CheckSquareFittedPieces()
    {
        if (boardSpotLeft.fittedPiece != null && boardSpotRight.fittedPiece != null)
        {
            if (boardSpotLeft.fittedPiece.twinPiece.transform == boardSpotRight.fittedPiece.transform)
            {
                Point();
            }
        }
    }

    void Point()
    {
        gameManager.correctCount++;

        boardSpotLeft.fittedPiece.BlockPiece();
        boardSpotRight.fittedPiece.BlockPiece();

        particleObject.transform.position = transform.position;
        particleObject.Play();

        gameManager.CheckIfGameHasEnded();

    }


}
