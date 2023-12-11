using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class WinScript : MonoBehaviour
{
    public ScoreManager scoreManager; // Reference to the ScoreManager script

    public GameObject Block; // Reference to the Block GameObject

    public string PuzzleKey = "Wooden"; // Key of this Puzzle

    [SerializeField] public GameObject GamePanel; // Panel that show when finish game

    private Interactor _interactorScript; // Interactor script reference

    private int _pointToWin; // Number of points needed to win

    private int _currentPoint; // Number of points currently earned

    private int _score; // Variable to store the score
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the saved score from PlayerPrefs
        _score = PlayerPrefs.GetInt("Stage1-score", 0);

        // Find and get the Interactor script from GameObject 
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();

        // Set the number of points needed to win
        _pointToWin = Block.transform.childCount;
    }

    // Method to add a point when a puzzle piece is correctly placed  
    // and check if the win conditions are met
    public void AddPoint()
    {
        _currentPoint++;
        CheckWin();
    } 

    // Method to check if the win conditions are met
    private void CheckWin()
    {
        if (_currentPoint >= _pointToWin)
        {
            //show the win message and invoke the Correct method after a delay
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("Correct", .7f);
        }
    }

    // Method called after winning to handle key to scoreManager
    public void Correct()
    {
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }
}
