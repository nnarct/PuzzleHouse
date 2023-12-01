using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    public GameObject Player;
    public GameObject PausePanel;
    public Button PauseButton;
    
    private bool _isPaused = false;

    private Interactor _interactorScript;
    private SpriteRenderer _playerSpriteRenderer;
    private string[] _puzzleKeys1 = { "Genetic", "Wooden", "Earth", "Moon", "Time" };
    private string[] _puzzleKeys2 = { "Wire", "Pipe", "Kitchenware", "Santa", "Christmas" };

    void Start()
    {
        PauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        PauseButton.onClick.AddListener(PauseGame);
        _playerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
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
        _isPaused = false;
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
       // playerSpriteRenderer.color = new Color(0.3608f, 0.3608f, 0.3608f);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void RestartStage(int stage)
    {
        if(stage == 1)
        {
            Debug.Log("restart stage1");
            foreach (string key in _puzzleKeys1)
            {
                UpdateStage1Field(key, 0);
                PlayerPrefs.SetInt(key, 0);
                PlayerPrefs.SetInt("Stage1_score", 0);
                PlayerPrefs.Save();
            }
            Time.timeScale = 1f;
            _isPaused = false;
            SceneManager.LoadScene("Stage1");
        }
        else if(stage == 2)
        {
            Debug.Log("restart stage2");
            foreach (string key in _puzzleKeys2)
            {
                UpdateStage2Field(key, 0);
                PlayerPrefs.SetInt(key, 0);
                PlayerPrefs.SetInt("Stage2_score", 0);
                PlayerPrefs.Save();
            }
            Time.timeScale = 1f;
            _isPaused = false;
            SceneManager.LoadScene("Stage2");
        }
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    
    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 Stage1 = PlayerList[PlayerID].Stage1;

        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

        // Check if the field exists
        if (fieldInfo != null)
        {
            // Set the value of the field
            fieldInfo.SetValue(Stage1, value);
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, "PlayerData.json");
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }
    void UpdateStage2Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage2 Stage2 = PlayerList[PlayerID].Stage2;

        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage2).GetField(fieldName);

        // Check if the field exists
        if (fieldInfo != null)
        {
            // Set the value of the field
            fieldInfo.SetValue(Stage2, value);
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, "PlayerData.json");
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }

}

