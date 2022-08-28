using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerSingleton;

    public int correctCount;

    public List<Vector3> startPiecesPositionsList;

    public UnityEvent finishedGameEvent;


    private void Awake()
    {
        gameManagerSingleton = this;
    }

    public static GameManager GetGameManagerSingleton()
    {
        return gameManagerSingleton;
    }


    void Start()
    {
        FillStartPiecesPositions();
    }


    void FillStartPiecesPositions()
    {
        foreach (Transform child in transform)
        {
            startPiecesPositionsList.Add(child.transform.localPosition);
        }
        ShufflePiecesPositions();
    }


    void ShufflePiecesPositions()
    {

        for (int i = 0; i < startPiecesPositionsList.Count - 1; i++)
        {
            Vector3 temp = startPiecesPositionsList[i];
            int rand = Random.Range(i, startPiecesPositionsList.Count);
            startPiecesPositionsList[i] = startPiecesPositionsList[rand];
            startPiecesPositionsList[rand] = temp;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOLocalMove(startPiecesPositionsList[i], 1);

            transform.GetChild(i).GetComponent<Piece>().StartPosition = startPiecesPositionsList[i];
        }

    }

    public void CheckIfGameHasEnded()
    {
        if (correctCount == 9)
        {            
            finishedGameEvent.Invoke();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
