using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChristmasPuzzle : MonoBehaviour
{
    public GameObject GamePanel; // Panel that show when finish game

    public GameObject[] CorrectPositions; // Array of correct positions for buttons 

    public ScoreManager scoreManager; // Reference to the ScoreManager script

    public string PuzzleKey = "Christmas"; // Key of this Puzzle

    private Button _lastClickedButton; //last click button

    private Button[] _buttons; // Array to store all buttons in the puzzle

    private int _totalScore; // Total score of the puzzle

    private int _currentScore; // Current score of the puzzle

    // Start is called before the first frame update
    void Start()
    {
        // Get all buttons in the puzzle
        _buttons = GetComponentsInChildren<Button>();

        // Set the total score based on the number of buttons
        _totalScore = _buttons.Length;

        // Randomize button positions
        SwapButtonsRandomly(_buttons);

        // Attach a button click listener to each button
        foreach (Button button in _buttons)
        {
            // Add a listener to the button that will call OnButtonClick when clicked
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    // Method called when a button is clicked
    void OnButtonClick(Button clickedButton)
    {
        // Check if this is the first button clicked
        if (_lastClickedButton == null)
        {
            // Store the clicked button as the last clicked button
            _lastClickedButton = clickedButton;
        }
        else
        {
            // Swap positions if a button was already clicked
            Vector3 tempPosition = _lastClickedButton.transform.position;
            _lastClickedButton.transform.position = clickedButton.transform.position;
            clickedButton.transform.position = tempPosition;

            // Reset the last clicked button
            _lastClickedButton = null;

            // Check correct positions after swapping
            CheckCorrectPosition(CorrectPositions, _buttons);
        }
    }

    // Method to swap the positions of buttons randomly
    void SwapButtonsRandomly(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            // Generate a random index
            int randomIndex = UnityEngine.Random.Range(0, buttons.Length);

            // Swap the positions of buttons[i] and buttons[randomIndex]
            Vector3 tempPosition = buttons[i].transform.position;
            buttons[i].transform.position = buttons[randomIndex].transform.position;
            buttons[randomIndex].transform.position = tempPosition;

            // Reset any visual indication (e.g., color)
            buttons[i].image.color = Color.white;
        }
    }

    // Method to check if buttons are in correct positions
    void CheckCorrectPosition(GameObject[] correctPositions, Button[] buttons)
    {
        // Reset the current score
        _currentScore = 0;
        int positionThreshold = 1; // Distance that close enough

        // Check all button positions are close enough
        for (int i = 0; i < buttons.Length; i++)
        {
            if (Vector3.Distance(correctPositions[i].transform.position, buttons[i].transform.position) <= positionThreshold)
            {
                _currentScore += 1;
            }
        }

        // If all buttons are in correct positions, call the Correct method
        if (_currentScore == _totalScore)
        {
            Correct();
        }
    }

    // Method called when the puzzle is solved
    public void Correct()
    {
        // Call the HandleCorrectAnswer method in the ScoreManager script
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }
}
