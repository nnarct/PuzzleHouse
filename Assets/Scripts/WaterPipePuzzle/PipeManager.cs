using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeManager : MonoBehaviour
{
    public ScoreManager scoreManager; // Reference to the ScoreManager script
    
    public GameObject GamePanel; // Panel that show when finish game

    public GameObject PipesHolder; // Reference to the GameObject containing all pipes

    public Transform[] Pipes; // Array of individual pipe transforms

    public int[] ManyRotsIndex; // Array of indices for pipes that can have multiple rotations

    public int _correctPipes = 0; // variable to count correct pipes

    public bool isRotating = false; // Flag to indicate if pipes are currently being rotated

    [SerializeField] private int _totalPipe = 0; // total of pipes

    [SerializeField] public string PuzzleKey = "Pipe"; // Key of this Puzzle

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the total number of pipes and the Pipes array
        _totalPipe = PipesHolder.transform.childCount;
        Pipes = new Transform[_totalPipe];

        // Populate the Pipes array with individual pipe transforms
        for (int i = 0; i < Pipes.Length ; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i);
        }
    }

    // Method to check if pipes are correctly rotated
    public void CheckCorrect()
    {
        // Reset the count of correctly rotated pipes
        _correctPipes = 0;

        // Iterate through each pipe
        for (int i = 0; i < Pipes.Length; i++)
        {
            // Check if the pipe has multiple possible rotations 
            //       then check rotation is correct rotation 
            if (ManyRotsIndex.Contains(i))
            {
                if (Pipes[i].rotation.z == 1 || Pipes[i].rotation.z == -1)
                {
                    _correctPipes++;
                }
            }

            // check if the pipe has a rotation close to 0 (correct rotation) 
            if (Mathf.Round(Pipes[i].rotation.z) == 0)
            {
                _correctPipes++;
            }

        }

        // check all pipe is correct
        if (_correctPipes == _totalPipe)
        {
            StartCoroutine(Win());
        }

    }

    // Method called after winning to handle key to scoreManager
    IEnumerator Win()
    {
        // Wait for a short duration before triggering the win
        yield return new WaitForSeconds(1.0f); 

        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }
}
