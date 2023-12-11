using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PassCode : MonoBehaviour
{ 
  
    public TMP_Text UiText = null; // UI Text to display the entered passcode

    public GameObject PuzzlePanel; // Reference to the puzzle panel GameObject

    public GameObject FinishStagePanel; // Reference to the finish stage panel GameObject

    [SerializeField] private GameObject WrongPanel; // Serialized GameObjects reference to the wrong panel GameObject

    [SerializeField] private AudioSource _source; // Audio source for correct sound
    
    [SerializeField] private AudioSource _source2; // Audio source for wrong sound

    private string _code ;// The correct passcode retrieved from PlayerPrefs
    
    private string _answer = null; // The entered passcode from player

    private int _answerIndex = 0; // Index to keep track of the current position in the entered passcode

    // Method to handle button clicks for custom numbers.
    // Attach this method to the Unity button's onClick event in the Inspector.
    // Set the 'number' parameter to the desired value represented by the button.
    public void AddNumber(string number)
    {
        // Check if the passcode is not complete
        if (_answerIndex < 6)
        {
            _answerIndex++;
            _answer = _answer + number;
            UiText.text = _answer;
        }
    }

    // Method called when the Enter button is pressed
    public void OnEnter()
    {
        // Retrieve the correct passcode from PlayerPrefs
        _code = PlayerPrefs.GetString("Passcode");

        // Check if the entered passcode is correct
        if (_answer == _code)
        {
            // Play the correct sound
            _source.Play();

            // Handle the correct passcode scenario
            Correct();
        }
        else
        {
            // Play the wrong sound
            _source2.Play();

            // Display the wrong panel temporarily
            WrongPanel.SetActive(true);
            Invoke("DeactivateWrongPanel", 1f);

            // Clear the entered passcode after a delay
            Invoke("DeleteAll", 0.5f);
        }
    }

    // Method to delete the last entered number from the passcode
    public void OnDelete()
    {
        // Check if there are numbers to delete
        if (_answerIndex > 0)
        {
            _answerIndex--;
            _answer = _answer.Substring(0, _answer.Length - 1);
            UiText.text = _answer;
        }
        else if (_answerIndex <= 0)
        {
            _answerIndex = 0;
        }
    }

    // Method to clear the entered passcode
    public void DeleteAll()
    {
        _answerIndex = 0;
        _answer = null;
        UiText.text = _answer;
    }

    // Method to deactivate the wrong panel
    public void DeactivateWrongPanel()
    {
        WrongPanel.SetActive(false);
    }

    // Method called when the passcode is correct
    void Correct()
    {
        // Deactivate the puzzle panel and activate the finish stage panel
        PuzzlePanel.SetActive(false);
        FinishStagePanel.SetActive(true);
    }
}
