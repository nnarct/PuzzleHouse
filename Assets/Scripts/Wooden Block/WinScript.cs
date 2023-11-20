using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class WinScript : MonoBehaviour
{
    private int PointToWin;
    private int CurrentPoint;
    public GameObject Block;
    [SerializeField] GameObject GamePanel;
    public GameObject CorrectPanel;
    public string puzzleKey = "wooden";
    private Interactor interactorScript;
    private int score;

    void Start()
    {
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
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
            Correct();
        }
    }

    public void Correct()
    {
        GamePanel.SetActive(false);
        CorrectPanel.SetActive(true);
        Button correctButton = GameObject.Find("CorrectKeyButton").GetComponent<Button>();
        correctButton.onClick.AddListener(OnCorrectButtonClick);
    }

    void OnCorrectButtonClick()
    {
        CorrectPanel.SetActive(false);
        if (PlayerPrefs.GetInt(puzzleKey, 0) == 0)
        {
            score++;
        }
        TMP_Text scoreText = GameObject.Find("score text").GetComponent<TMP_Text>();
        scoreText.text = score.ToString() + "/5";
        PlayerPrefs.SetInt("Stage1-score", score);
        PlayerPrefs.SetInt(puzzleKey, 1);
        PlayerPrefs.Save();
        interactorScript.EndInteraction();
        UpdateStage1Field(puzzleKey, 1);
    }

    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 stage1 = PlayerList[PlayerID].stage1;

        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

        if (fieldInfo != null)
        {
            fieldInfo.SetValue(stage1, value);
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, "PlayerData.json");
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }


}
