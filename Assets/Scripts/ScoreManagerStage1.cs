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
    void Start()
    {
        //score = PlayerPrefs.GetInt("Stage1-score", 0);
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
