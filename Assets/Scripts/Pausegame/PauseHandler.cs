using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    public GameObject Player; // Reference to the player GameObject

    public GameObject PausePanel; // Reference to the PausePanel GameObject

    public Button PauseButton; // Reference to the PauseButton UI element
    
    private bool _isPaused = false; // Flag to track whether the game is currently paused

    private Interactor _interactorScript; // Reference to the Interactor script

    private SpriteRenderer _playerSpriteRenderer; // Reference to the player's SpriteRenderer component

    private string[] _puzzleKeys1 = { "Genetic", "Wooden", "Earth", "Moon", "Time" }; // Arrays storing puzzle keys for Stage 1

    private string[] _puzzleKeys2 = { "Wire", "Pipe", "Kitchenware", "Santa", "Christmas" }; // Arrays storing puzzle keys for Stage 2

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references and set up event listeners

        // Find the PauseButton GameObject and get its Button component
        PauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        
        // Add a listener to the PauseButton that will call PauseGame when clicked
        PauseButton.onClick.AddListener(PauseGame);

        // Get the player's SpriteRenderer component
        _playerSpriteRenderer = Player.GetComponent<SpriteRenderer>();

        // Find the GameObject with the "Interactable" tag and get its Interactor component
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }


    // Update is called once per frame
    void Update()
    {
        // Check for the Escape key to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                // If already paused, resume the game
                ResumeGame();
            }
            else
            {
                // If not paused, initiate pause
                PauseGame();
            }
        }
    }

    //  Method to navigate to the main menu
    /*
     * This method is intended to be associated with a button click event in the Unity Inspector.
     * To use it, drag the GameObject with this script onto the button's OnClick event,
     * and select this method ("GoToMainMenu") from the dropdown.
     */
    public void GoToMainMenu()
    {
        // Reset time scale, switch to the main menu scene, and unpause
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
        _isPaused = false;
    }

    //  Method to pause the game
    /*
     * This method is intended to be associated with a button click event in the Unity Inspector.
     * To use it, drag the GameObject with this script onto the button's OnClick event,
     * To use it, drag the GameObject with this script onto the button's OnClick event,
     * and select this method ("PauseGame") from the dropdown.
     */
    public void PauseGame()
    {
        // Activate the PausePanel, freeze time, and set the pause flag
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    //  Method to restart a specific stage
    /*  
     * This method is intended to be associated with a button click event in the Unity Inspector.
     * To use it, drag the GameObject with this script onto the button's OnClick event,
     * and select this method ("RestartStage") from the dropdown. 
     */
    public void RestartStage(int stage)
    {
        if(stage == 1)
        {
            // Reset puzzle-related PlayerPrefs for Stage 1
            // Iterate through each puzzle key in Stage 1
            foreach (string key in _puzzleKeys1)
            {
                // Update the corresponding Stage1 field to 0
                SaveLoadManager.UpdateStageField<Stage1>(key, 0);

                // Set the PlayerPrefs for the puzzle key to 0
                PlayerPrefs.SetInt(key, 0);

                // Set the PlayerPrefs for the Stage1 score to 0
                PlayerPrefs.SetInt("Stage1_score", 0);

                // Save the PlayerPrefs changes
                PlayerPrefs.Save();
            }

            // Reset time scale, unpause, and load Stage1 scene
            // Set the time scale back to normal
            Time.timeScale = 1f;

            // Unset the pause flag
            _isPaused = false;

            // Load the "Stage1" scene
            SceneManager.LoadScene("Stage1");
        }
        else if(stage == 2)
        {
            // Reset puzzle-related PlayerPrefs for Stage 2
            // Iterate through each puzzle key in Stage 2
            foreach (string key in _puzzleKeys2)
            {
                // Update the corresponding Stage2 field to 0
                SaveLoadManager.UpdateStageField<Stage2>(key, 0);

                // Set the PlayerPrefs for the puzzle key to 0
                PlayerPrefs.SetInt(key, 0);

                // Set the PlayerPrefs for the Stage2 score to 0
                PlayerPrefs.SetInt("Stage2_score", 0);

                // Save the PlayerPrefs changes
                PlayerPrefs.Save();
            }

            // Reset time scale, unpause, and load Stage2 scene
            // Set the time scale back to normal
            Time.timeScale = 1f;

            // Unset the pause flag
            _isPaused = false;

            // Load the "Stage2" scene
            SceneManager.LoadScene("Stage2");
        }
    }

    //  Method to resume the game
    /*  
     * This method is intended to be associated with a button click event in the Unity Inspector.
     * To use it, drag the GameObject with this script onto the button's OnClick event,
     * and select this method ("ResumeGame") from the dropdown. 
     */
    public void ResumeGame()
    {
        // Deactivate the PausePanel, resume time, and unset the pause flag
        // Set the PausePanel GameObject to inactive
        PausePanel.SetActive(false);

        // Set the time scale back to normal
        Time.timeScale = 1f;

        // Unset the pause flag
        _isPaused = false;
    }

}

