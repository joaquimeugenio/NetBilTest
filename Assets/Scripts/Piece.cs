using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    private bool dragging = false;
    private bool isPositioned;
    private bool isOnASpotArea;

    private float distance;    

    public GameObject twinPiece;

    public enum Side
    {
        right,
        left
    }

    public Side pieceSide;

    Transform target;

    Vector3 startPosition;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }



    void OnMouseDown()
    {
        if (!enabled) return;        

        GetComponent<Renderer>().sortingOrder = 4;

        isPositioned = false;

        distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (target != null)
        {
            target.GetComponent<BoxCollider>().enabled = true;

            target.GetComponent<BoardSpot>().fittedPiece = null;
        }
        dragging = true;

    }


    void OnMouseUp()
    {
        if (!enabled) return;

        dragging = false;

        if (isOnASpotArea)
        {
            FitInPiece();
        }
        else
        {
            transform.DOLocalMove(startPosition, 0.5f).
           OnComplete(ResetOrderinLayer);
        }
    }


    void ResetOrderinLayer()
    {
        GetComponent<Renderer>().sortingOrder = 3;
    }


    void Update()
    {

        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }

    }    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JigSawSpot"))
        {
            isOnASpotArea = true;
            target = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("JigSawSpot"))
        {
            isOnASpotArea = true;
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JigSawSpot") && (!isPositioned))
        {
            isOnASpotArea = false;
            target = null;
        }
    }

    void FitInPiece()
    {
        //Got a Position
        isPositioned=true;

        transform.position = target.position;

        BoardSpot targetBoardSpot = target.GetComponent<BoardSpot>();

        targetBoardSpot.fittedPiece = this;

        targetBoardSpot.CheckFittedPieces();

        target.GetComponent<BoxCollider>().enabled = false;

        ResetOrderinLayer();


    }

    public void BlockPiece()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
    }


}
