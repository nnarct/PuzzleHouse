using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private int PointToWin;
    private int CurrentPoint;
    public GameObject Block;
    public string puzzleKey = "wooden";
    private int score;
    void Start()
    {
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        PointToWin = Block.transform.childCount;
    }

//void Update()
   // {
     //   if (CurrentPoint >= PointToWin)
      //  {
            //Win
       //     transform.GetChild(0).gameObject.SetActive(true);
       //     score++;
        //    PlayerPrefs.SetInt("Stage1-score", score);
        //    PlayerPrefs.SetInt(puzzleKey, 1);
        //    PlayerPrefs.Save();
       // }
   // }

    public void AddPoint()
    {
        CurrentPoint++;
        CheckWin();
    }

    private void CheckWin()
    {
        if (CurrentPoint >= PointToWin)
        {
            //Win
            transform.GetChild(0).gameObject.SetActive(true);
            score++;
            PlayerPrefs.SetInt("Stage1-score", score);
            PlayerPrefs.SetInt(puzzleKey, 1);
            PlayerPrefs.Save();
        }
    }
}
