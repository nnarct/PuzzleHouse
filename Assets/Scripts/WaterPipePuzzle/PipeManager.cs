using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    public int TotalPipes = 0;

    [SerializeField]
    int CorrectPipes = 0;

    public GameObject CorectPanel;

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
            CorectPanel.SetActive(true);
        }
    }

    public void WrongMove()
    {
        CorrectPipes -= 1;
    }
}
