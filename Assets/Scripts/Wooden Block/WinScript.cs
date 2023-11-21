using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class WinScript : MonoBehaviour
{  
    public GameObject Block;
    [SerializeField] GameObject GamePanel;
    public GameObject CorrectPanel;
    public string PuzzleKey = "Wooden";
   
    private int _pointToWin;
    private int _currentPoint;
    private Interactor _interactorScript;
    private int _score;

    void Start()
    {
        _score = PlayerPrefs.GetInt("Stage1-score", 0);
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
        _pointToWin = Block.transform.childCount;
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
        _currentPoint++;
        CheckWin();
    }

    private void CheckWin()
    {
        if (_currentPoint >= _pointToWin)
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
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0)
        {
            _score++;
        }
        TMP_Text scoreText = GameObject.Find("score text").GetComponent<TMP_Text>();
        scoreText.text = _score.ToString() + "/5";
        PlayerPrefs.SetInt("Stage1-score", _score);
        PlayerPrefs.SetInt(PuzzleKey, 1);
        PlayerPrefs.Save();
        _interactorScript.EndInteraction();
        UpdateStage1Field(PuzzleKey, 1);
    }

    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 Stage1 = PlayerList[PlayerID].Stage1;

        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

        if (fieldInfo != null)
        {
            fieldInfo.SetValue(Stage1, value);
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, "PlayerData.json");
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }


}
