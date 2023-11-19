using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
            // Check if the player had won this puzzle already or not
            if(PlayerPrefs.GetInt(puzzleKey, 0) == 0 )
            {
                // If the player hasn't won the puzzle , increment score
                score++;
            }
            PlayerPrefs.SetInt("Stage1-score", score);
            PlayerPrefs.SetInt(puzzleKey, 1);
            PlayerPrefs.Save();
            UpdateStage1Field(puzzleKey, 1);
        }
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
