using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    
    public GameObject GamePanel;

    public GameObject PipesHolder;

    public Transform[] Pipes;

    [SerializeField] private int _totalPipe = 0;

    public int[] ManyRotsIndex; 

    [SerializeField] public string PuzzleKey = "Pipe";

    public int _correctPipes = 0;

    public bool isRotating = false;

    void Start()
    {

        _totalPipe = PipesHolder.transform.childCount;

        Pipes = new Transform[_totalPipe];

        for (int i = 0; i < Pipes.Length ; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i);
        }
    }

    public void CheckCorrect()
    {
        _correctPipes = 0;
        for (int i = 0; i < Pipes.Length; i++)
        {
            if (ManyRotsIndex.Contains(i))
            {
                if (Pipes[i].rotation.z == 1 || Pipes[i].rotation.z == -1)
                {
                    _correctPipes++;
                }
            }

            if (Mathf.Round(Pipes[i].rotation.z) == 0)
            {
                _correctPipes++;
            }

        }
        
        Debug.Log(_correctPipes);

        if (_correctPipes == _totalPipe)
        {
            Debug.Log("Win");

            StartCoroutine(Win());
        }

    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed

        Debug.Log("Win");
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }



}
