using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Reflection;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text ScoreText;
    public int Stage;
    public List<PuzzleKeysData> PuzzleKeys;
    public GameObject CorrectPanel;
    private int _maxScore;
    private bool _isOpenCorrectPanel = false ;
    private string _puzzleKey;
    // Start is called before the first frame update
    void Start()
    {
        _maxScore = PuzzleKeys.Count;
        LoadFileToPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_isOpenCorrectPanel)
        {
            if (Input.anyKeyDown)
            {
                ManageScore();
                _isOpenCorrectPanel = false;
            }
        }
    }

    private void OnEnable()
    {
        // Subscribe to the CorrectAnswerEvent when the ScoreManager is enabled
        QuizManager.CorrectAnswerEvent += HandleCorrectAnswer;
    }

    private void OnDisable()
    {
        // Unsubscribe from the CorrectAnswerEvent when the ScoreManager is disabled
        QuizManager.CorrectAnswerEvent -= HandleCorrectAnswer;
    }

    private void HandleCorrectAnswer(string puzzleKey, GameObject puzzlePanel)
    {

        if(!string.IsNullOrEmpty(puzzleKey))
        {
            _puzzleKey = puzzleKey;
            // This method will be called when the CorrectAnswerEvent is invoked
            // Add your scoring logic here
            puzzlePanel.SetActive(false);
            CorrectPanel.SetActive(true);
            _isOpenCorrectPanel = true;
        }
        else
        {
            Debug.Log("Error! Puzzle Key not found.");
        }
                   
    }

    private void ManageScore()
    {
        CorrectPanel.SetActive(false);
        Debug.Log(_puzzleKey + " : Correct answer! Incrementing score.");
        PlayerPrefs.SetInt(_puzzleKey, 1);
        PlayerPrefs.Save();
        UpdateStage1Field(_puzzleKey, 1);
        UpdateScoreDisplay();

    }

    private void UpdateScoreDisplay()
    {
        int currentScore = 0;
        for (int i = 0; i < _maxScore; i++)
        {
            if (string.IsNullOrEmpty(PuzzleKeys[i].Key))
            {
                Debug.Log("Error! Puzzle Key not found.");
                return;
            }
            int point = PlayerPrefs.GetInt(PuzzleKeys[i].Key, 0);
            Debug.Log("Currect point for " + PuzzleKeys[i].Key + ": " + point);
            currentScore += point;
        }
        Debug.Log("Currect Score : " + currentScore);
        ScoreText.text = currentScore + "/" + _maxScore;
    }

    void LoadFileToPlayerPrefs()
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");
        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        for (int i = 0; i < _maxScore; i++)
        {
            FieldInfo fieldInfo = typeof(Stage1).GetField(PuzzleKeys[i].Key);
            int isSolved = (int)fieldInfo.GetValue(this);
            Debug.Log(PuzzleKeys[i].Key + " is " + isSolved);
            PlayerPrefs.SetInt(PuzzleKeys[i].Key, isSolved);
        }
     
        PlayerPrefs.Save();
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

[System.Serializable]
public class PuzzleKeysData
{
    public string Key;
}
