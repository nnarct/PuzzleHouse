using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    public Button PauseButton;
    
    private bool isPaused = false;

    private Interactor interactorScript;
    private SpriteRenderer playerSpriteRenderer;
    private string[] puzzleKeys = { "genetic", "wooden", "earth", "moon", "time" };

    void Start()
    {
        PauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        PauseButton.onClick.AddListener(PauseGame);
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
       // playerSpriteRenderer.color = new Color(0.3608f, 0.3608f, 0.3608f);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RestartGame(int stage)
    {
        if(stage == 1)
        {
            foreach (string key in puzzleKeys)
            {
                UpdateStage1Field(key, 0);
                PlayerPrefs.SetInt(key, 0);
                PlayerPrefs.SetInt("Stage1-score", 0);
                PlayerPrefs.Save();
            }
            Time.timeScale = 1f;
            isPaused = false;
            SceneManager.LoadScene("Stage1");
        }
    }

    public void ResumeGame()
    {
       // playerSpriteRenderer.color = new Color(1f, 1f, 1f);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    
    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 stage1 = PlayerList[PlayerID].stage1;

        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

        // Check if the field exists
        if (fieldInfo != null)
        {
            // Set the value of the field
            fieldInfo.SetValue(stage1, value);
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, "PlayerData.json");
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }

}

