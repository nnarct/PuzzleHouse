using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishStageAnimation : MonoBehaviour
{
    [SerializeField] private GameObject PanelBackground; // Panel background object for scaling animation

    [SerializeField] private GameObject EndGamePanel; // End game panel object for deactivation

    [SerializeField] private GameObject Star; // Star object for scaling and moving animation

    [SerializeField] private GameObject UiText; // UI text object for scaling and moving animation

    [SerializeField] private GameObject NextButton; // Next button object for scaling animation

    [SerializeField] private GameObject ReplayButton; // Replay button object for scaling animation

    [SerializeField] private GameObject LightWheel; // Light wheel object for rotating and scaling animation

    private Image _lightWheelImage; // Reference to the Image component of LightWheel for color changes

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Get the Image component of LightWheel for color change
        _lightWheelImage = LightWheel.GetComponent<Image>();

        // Start the random color change coroutine
        StartCoroutine(RandomColorChange());

        // Various animations using LeanTween
        LeanTween.rotateAround(LightWheel, Vector3.forward, -360, 10f).setLoopClamp();
        LeanTween.scale(PanelBackground, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star, new Vector3(2f, 2f, 2f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(ShowText);
        LeanTween.scale(LightWheel, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(Star, new Vector3(0f, 240f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.moveLocal(LightWheel, new Vector3(0f, 240f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(Star, new Vector3(.8f, .8f, .8f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(LightWheel, new Vector3(.7f, .7f, .7f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }

    // Method to show additional text and buttons
    private void ShowText()
    {
        // Animations for showing text and buttons
        LeanTween.scale(UiText, new Vector3(1.7f, 1.7f, 1.7f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(UiText, new Vector3(0f, -118f, 0f), .7f).setDelay(1.6f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.scale(ReplayButton, new Vector3(1f, 1f, 1f), 2f).setDelay(1.9f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(NextButton, new Vector3(1f, 1f, 1f), 2f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(UiText, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void GoToNextScene()
    {
        // Move to the next scene and save the level information
        // Read the player list from the JSON file
        List<PlayerEntry> playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        // Get the player's ID from PlayerPrefs, which was set during the login or player selection process
        int playerId = PlayerPrefs.GetInt("PlayerID");

        // Disable the EndGamePanel to hide it from the UI
        EndGamePanel.SetActive(false);

        // Determine the index of the next scene in the build order
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Update the player's level information in the player list
        playerList[playerId].Level = nextSceneIndex;

        // Save the modified player list back to the JSON file
        FileHandler.SaveToJSON<PlayerEntry>(playerList, "PlayerData.json");

        // Log the index of the next scene for debugging purposes
        Debug.Log(nextSceneIndex);

        // Load the next scene in the build order
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Coroutine for continuous random color changes
    IEnumerator RandomColorChange()
    {
        while (true) // Infinite loop for continuous color changes
        {
            int randomIndex = Random.Range(0, 7);

            // Define the seven possible colors
            Color[] possibleColors =
            {
                new Color(1, 0, Random.value),
                new Color(0, 1, Random.value),
                new Color(Random.value, 0, 1),
                new Color(Random.value, 1, 0),
                new Color(0, Random.value, 1),
                new Color(1, Random.value, 0),
                Color.white // (1, 1, 1)
            };

            // Assign the color based on the random index
            _lightWheelImage.color = possibleColors[randomIndex];

            // Wait for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
        }
    }


}
