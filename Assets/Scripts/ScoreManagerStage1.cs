using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManagerStage1 : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text ParchmentText;
    public GameObject chestBox;
    public GameObject chestPuzzleButton;

    private int score;
    private string[] puzzleKeys = { "wooden", "genetic", "time", "moon", "earth" };

    void Start()
    {
        LoadFileToPlayerPrefs();
        chestPuzzleButton.SetActive(false);
        CheckChest(score);
        ParchmentText.text = GenerateRandomNumericPassword(3, 6) + "#";
    }
    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        scoreText.text = score.ToString() + "/5";
        CheckChest(score);
        UpdateScoreText(score);
    }

    private void CheckChest(int score)
    {
        if (score == 5)
        {
            chestPuzzleButton.SetActive(true);
            chestBox.SetActive(true);
        }
        else if (score < 5)
        {
            chestPuzzleButton.SetActive(false);
            chestBox.SetActive(false);
        }
    }

    void LoadFileToPlayerPrefs()
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");
        int PlayerID = PlayerPrefs.GetInt("PlayerID");
        PlayerPrefs.SetInt("earth", PlayerList[PlayerID].stage1.earth);
        PlayerPrefs.SetInt("moon", PlayerList[PlayerID].stage1.moon);
        PlayerPrefs.SetInt("wooden", PlayerList[PlayerID].stage1.wooden);
        PlayerPrefs.SetInt("genetic", PlayerList[PlayerID].stage1.genetic);
        PlayerPrefs.SetInt("time", PlayerList[PlayerID].stage1.time);
        foreach (string key in puzzleKeys)
        {
            score = score + PlayerPrefs.GetInt(key, 0);
        }
        PlayerPrefs.SetInt("Stage1-score", score);
        PlayerPrefs.Save();
    }

    void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString() + "/5";
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
