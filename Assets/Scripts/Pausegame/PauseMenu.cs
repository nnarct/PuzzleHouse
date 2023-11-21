using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Player;
    public GameObject PausePanel;
    public Button PauseButton;
    
    private bool _isPaused = false;

    private Interactor _interactorScript;
    private SpriteRenderer _playerSpriteRenderer;
    private string[] _puzzleKeys = { "genetic", "wooden", "earth", "moon", "time" };

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

    public void RestartGame(int stage)
    {
        if(stage == 1)
        {
            foreach (string key in _puzzleKeys)
            {
                UpdateStage1Field(key, 0);
                PlayerPrefs.SetInt(key, 0);
                PlayerPrefs.SetInt("Stage1-score", 0);
                PlayerPrefs.Save();
            }
            Time.timeScale = 1f;
            _isPaused = false;
            SceneManager.LoadScene("Stage1");
        }
    }

    public void ResumeGame()
    {
       // playerSpriteRenderer.color = new Color(1f, 1f, 1f);
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

}

