using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class ScoreManagerStage1 : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject chestBox;
    private int score;
    private string[] puzzleKeys = { "wooden", "genetic", "time", "moon", "earth" };

    string Filename = "PlayerData.json";
    List<PlayerEntry> PlayerList = new List<PlayerEntry>();
    void Start()
    {
        //score = PlayerPrefs.GetInt("Stage1-score", 0);
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(Filename);
        int PlayerID = PlayerPrefs.GetInt("PlayerID");
        PlayerPrefs.SetInt("moon", PlayerList[PlayerID].stage1.moon);
        PlayerPrefs.SetInt("wooden", PlayerList[PlayerID].stage1.wooden);
        PlayerPrefs.SetInt("genetic", PlayerList[PlayerID].stage1.genetic);
        PlayerPrefs.SetInt("time", PlayerList[PlayerID].stage1.time);

        score = 0;
        foreach (string key in puzzleKeys)
        {
            score = score + PlayerPrefs.GetInt(key, 0);
        }
        PlayerPrefs.SetInt("Stage1-score", score);
        PlayerPrefs.Save();
        scoreText.text = score.ToString() + "/5";
        CheckChest(score);
    }

    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        scoreText.text = score.ToString() + "/5";
        CheckChest(score);
    }

    private void CheckChest(int score)
    {
        if (score == 5)
        {
            chestBox.SetActive(true);
        }
        else if (score < 5)
        {
            chestBox.SetActive(false);
        }
       
    }

}
