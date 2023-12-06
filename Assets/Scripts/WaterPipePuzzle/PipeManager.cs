using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject GamePanel;
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    private int _totalPipe = 0;

    [SerializeField]

    public string PuzzleKey = "Pipe";
    private int _correctPipes = 0;

    public bool isRotating = false;

    void Start()
    {
        _totalPipe = PipesHolder.transform.childCount;

        Pipes = new GameObject[_totalPipe];

        for (int i = 0; i < Pipes.Length ; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        _correctPipes += 1;

        Debug.Log("Correct Move : " + _correctPipes );

        if(_correctPipes == _totalPipe)
        {
            Debug.Log("You Win!");

            scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
            
            isRotating = false;
        }
    }

    public void WrongMove()
    {
        _correctPipes -= 1;
        Debug.Log("Wrong Move : " + _correctPipes );
    }
}
