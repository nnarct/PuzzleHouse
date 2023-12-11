using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField NameInput; // Reference to player name input

    public GameObject ErrorText; // Reference to error text to show when error

    private Button _confirmButton; // button to confirm player name
    
    private List<PlayerEntry> _playerList = new List<PlayerEntry>(); // list of player data

    private string _fileName = "PlayerData.json"; // file name player data
    
    private string[] _existName; // Array to store existing player names

    private int _sceneIndex; // Scene index for tracking the current scene

    // Start is called before the first frame update
    void Start()
    {
        // Get the ConfirmButton component and set it to not interactable initially
        _confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
        _confirmButton.interactable = false;

        // load player data from file
        _playerList = FileHandler.ReadListFromJSON<PlayerEntry>(_fileName);

    }

    // Update is called once per frame
    void Update()
    {
        // Enable or disable the ConfirmButton based on input validation
        _confirmButton.interactable = ValidateInput(NameInput.text);
    }

    // Method to add the player name to the list
    public void AddNameToList()
    {
        // Validate the input and check if the name is unique
        if(ValidateInput(NameInput.text) && ValidateName())
        {
            // Get the next available player ID
            int playerID = _playerList.Count;

            // Get the character name from PlayerPrefs
            string characterName = PlayerPrefs.GetString("Character");

            // Set the player ID and scene index
            PlayerPrefs.SetInt("PlayerID", playerID);
            _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Add a new player entry to the list
            _playerList.Add(new PlayerEntry(NameInput.text, _sceneIndex + 1, characterName));

            // Save the updated player list to the JSON file
            FileHandler.SaveToJSON<PlayerEntry>(_playerList, _fileName);

            // Load the next scene
            SceneManager.LoadSceneAsync(_sceneIndex);
        }
    }

    // Method to go back to the previous scene
    public void BackScene()
    {
        // Get the current scene index
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the previous scene
        SceneManager.LoadSceneAsync(_sceneIndex - 1);
    }

    // Validate the player name input
    Boolean ValidateInput(string text)
    {
        // Check if the input is not null or empty
        if(text == null || text.Length == 0)
        {
            return false;
        }
        return true;
    }

    // Validate the uniqueness of the player name
    Boolean ValidateName()
    {
        foreach(var player in _playerList)
        {
            // Check if the player name already exists
            if(player.PlayerName == NameInput.text)
            {
                // Display error text and animate it
                ErrorText.SetActive(true);
                LeanTween.scale(ErrorText, new Vector3(1f, 1f, 1f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInOutElastic);
                
                // Close the error text after a delay
                Invoke("CloseErrorText", 3f);
                return false;
            }
        }
        return true;
    }

    // Method to close the error text
    void CloseErrorText()
    {
        ErrorText.SetActive(false);
        LeanTween.scale(ErrorText, new Vector3(0f, 0f, 0f), .7f).setEase(LeanTweenType.linear);
    }
}
