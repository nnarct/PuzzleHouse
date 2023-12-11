using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class ChestPuzzle : MonoBehaviour
{

    public GameObject ChestPuzzleButton; // GameObject that contains Rigidbody2D component for the chest puzzle interactor

    public GameObject ChestBoxUI; // GameObject that contains Image component for the chest puzzle in canvas

    public TMP_Text ParchmentText; // Text component inside the parchment object in chest puzzle panel

    private int _score; // Number that will hold the score of the stage of the current player
    
    private Scene _currentScene; // Scene object that will hold the current scene

    // Start is called before the first frame update
    void Start()
    {
        // Generate a random numeric password from 3 to 6 digits
        ParchmentText.text = GenerateRandomNumericPassword(3, 6) + "#";

        // Get the current scene from scene manager
        _currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the scene's score from player prefs depends on the current scene
        if(_currentScene.name == "Stage1")
        {
            _score = PlayerPrefs.GetInt("Stage1_score", 0);
        }
        else if (_currentScene.name == "Stage2") {
            _score = PlayerPrefs.GetInt("Stage2_score", 0);
        }

        // Check if the score is 5
        CheckChest();
    }

    // This method checks the player's score and adjusts the visibility of
    // the ChestPuzzleButton and ChestBoxUI based on the score condition.
    private void CheckChest()
    {
        // Check if the player's score is equal to , activate the ChestPuzzleButton and ChestBoxUI
        if (_score == 5)
        { 
            ChestPuzzleButton.SetActive(true); // Activate the button to allow interaction with the chest puzzle
            ChestBoxUI.SetActive(true);        // Show the UI for the chest box
        }
        // If the score is less than 5 , deactivate the ChestPuzzleButton and ChestBoxUI
        else if (_score < 5)
        {
            ChestPuzzleButton.SetActive(false); // Deactivate the button as there's no interaction with the chest puzzle
            ChestBoxUI.SetActive(false);        // Hide the UI for the chest box
        }
        // If the score is greater than 5, no action is taken
    }

    // Generate a random numeric password of a specified length and store it in PlayerPrefs
    private string GenerateRandomNumericPassword(int minLength, int maxLength)
    {
        // Determine the length of the password within the specified range
        int passwordLength = UnityEngine.Random.Range(minLength, maxLength + 1); // +1 to make it inclusive
        
        // StringBuilder to build the password
        System.Text.StringBuilder password = new System.Text.StringBuilder();

        // Generate each digit of the password
        for (int i = 0; i < passwordLength; i++)
        {
            // Generate a random digit and append it to the password
            password.Append(UnityEngine.Random.Range(0, 10));
        }

        // Save the generated password in PlayerPrefs with the key "Passcode"
        PlayerPrefs.SetString("Passcode", password.ToString());

        // Save PlayerPrefs to make sure the data is persisted
        PlayerPrefs.Save();

        // Return the generated password
        return password.ToString();
    }
}
