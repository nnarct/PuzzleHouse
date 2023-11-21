using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerStage1 : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text ParchmentText;
    public GameObject ChestBox;
    public GameObject ChestPuzzleButton;

    private int _score;
    private string[] _puzzleKeys = { "Wooden", "Genetic", "Time", "Moon", "Earth" };

    void Start()
    {
        LoadFileToPlayerPrefs();
        ChestPuzzleButton.SetActive(false);
        CheckChest(_score);
        ParchmentText.text = GenerateRandomNumericPassword(3, 6) + "#";
    }
    // Update is called once per frame
    void Update()
    {
        _score = PlayerPrefs.GetInt("Stage1-score", 0);
        ScoreText.text = _score.ToString() + "/5";
        CheckChest(_score);
        UpdateScoreText(_score);
    }

    private void CheckChest(int score)
    {
        if (score == 5)
        {
            ChestPuzzleButton.SetActive(true);
            ChestBox.SetActive(true);
        }
        else if (score < 5)
        {
            ChestPuzzleButton.SetActive(false);
            ChestBox.SetActive(false);
        }
    }

    void LoadFileToPlayerPrefs()
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");
        int PlayerID = PlayerPrefs.GetInt("PlayerID");
        PlayerPrefs.SetInt("Earth", PlayerList[PlayerID].Stage1.Earth);
        PlayerPrefs.SetInt("Moon", PlayerList[PlayerID].Stage1.Moon);
        PlayerPrefs.SetInt("Wooden", PlayerList[PlayerID].Stage1.Wooden);
        PlayerPrefs.SetInt("Genetic", PlayerList[PlayerID].Stage1.Genetic);
        PlayerPrefs.SetInt("Time", PlayerList[PlayerID].Stage1.Time);
        foreach (string key in _puzzleKeys)
        {
            _score = _score + PlayerPrefs.GetInt(key, 0);
        }
        PlayerPrefs.SetInt("Stage1-score", _score);
        PlayerPrefs.Save();
    }

    void UpdateScoreText(int score)
    {
        ScoreText.text = _score.ToString() + "/5";
    }

    string GenerateRandomNumericPassword(int minLength, int maxLength)
    {
        int passwordLength = UnityEngine.Random.Range(minLength, maxLength + 1); // +1 to make it inclusive
        System.Text.StringBuilder password = new System.Text.StringBuilder();

        for (int i = 0; i < passwordLength; i++)
        {
            // Generate a random digit and append it to the password
            password.Append(UnityEngine.Random.Range(0, 10));
        }
        PlayerPrefs.SetString("Passcode", password.ToString());
        // Debug.Log("True passcode = " + password.ToString());
        return password.ToString();
    }
}
