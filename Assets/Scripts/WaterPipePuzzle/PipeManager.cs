using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    public int TotalPipes = 0;

    [SerializeField]
    int CorrectPipes = 0;

    public GameObject GamePanel;
    public string PuzzleKey = "Pipe";


    void Start()
    {
        TotalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[TotalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        CorrectPipes += 1;

        Debug.Log("Correct Move");

        if(CorrectPipes == TotalPipes)
        {
            Debug.Log("You Win!");
            scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
            
        }
    }

    public void WrongMove()
    {
        CorrectPipes -= 1;
    }
}
