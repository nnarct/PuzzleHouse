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

    [SerializeField] private AudioSource _openCorrectPanelSound;
    [SerializeField] private AudioSource _clickKeySound;

    void Awake()
    {
        LoadFileToPlayerPrefs();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(ScoreText == null)
        {
            Debug.LogError("Error! Cannot find reference of ScoreText.");
        } 
        if(Stage == 0)
        {
            Debug.LogError("Error! Cannot find reference of Stage.");
        } 
        if(PuzzleKeys.Count == 0)
        {
            Debug.LogError("Error! Cannot find reference of PuzzleKeys.");
        }
        if(CorrectPanel == null)
        {
            Debug.LogError("Error! Cannot find reference of CorrectPanel.");
        }
        _maxScore = PuzzleKeys.Count;
        LoadFileToPlayerPrefs();
        //Debug.Log("Max Score : " + _maxScore);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreDisplay();
        if (_isOpenCorrectPanel)
        {
            if (Input.anyKeyDown)
            {
                //_clickKeySound.Play();
                //Debug.Log("click key");
                Invoke("ManageScore", 0.5f);
                _isOpenCorrectPanel = false;
            }
            Invoke("ManageScore", 4f);
            _isOpenCorrectPanel = false;

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

    public void HandleCorrectAnswer(string puzzleKey, GameObject puzzlePanel)
    {


        Debug.Log("handle correct manager");
        if(puzzlePanel == null)
        {
            Debug.LogError("Parameter puzzlePanel cannot be null.");
            return;
        }
        if(!string.IsNullOrEmpty(puzzleKey))
        {
            _puzzleKey = puzzleKey;
            Debug.Log("diable puzzle panel");
            puzzlePanel.SetActive(false);
            CorrectPanel.SetActive(true);
            _isOpenCorrectPanel = true;
            _openCorrectPanelSound.Play();
            Invoke("DelayCorrectPanelStatus", 2f);
           // Debug.Log("correct panel" + CorrectPanel.activeSelf);
        }
        else
        {
            Debug.Log("Error! Puzzle Key not found.");
        }
    }

    private void ManageScore()
    {
        CorrectPanel.SetActive(false);

        // Debug.Log(_puzzleKey + " : Correct answer! Incrementing score.");

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
            //Debug.Log("Currect point for " + PuzzleKeys[i].Key + ": " + point);
            currentScore += point;
        }
        // Debug.Log("Currect Score : " + currentScore);
        ScoreText.text = currentScore.ToString() + "/" + _maxScore.ToString();
        if( Stage == 1)
            PlayerPrefs.SetInt("Stage1_score", currentScore);
        else if(Stage == 2)
            PlayerPrefs.SetInt("Stage2_score", currentScore);
        PlayerPrefs.Save();
    }

    public void DelayCorrectPanelStatus()
    {
        _isOpenCorrectPanel = true;
    }

    void LoadFileToPlayerPrefs()
    {
        List<PlayerEntry> playerList = new List<PlayerEntry>();

        playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int playerId = PlayerPrefs.GetInt("PlayerID");
        PlayerPrefs.SetString("Character", playerList[playerId].CharacterChoose);

        foreach (var puzzleData in PuzzleKeys)
        {
            string puzzleKey = puzzleData.Key;
            int isSolved = 0;
            if(Stage == 1)
            {
                FieldInfo fieldInfo = typeof(Stage1).GetField(puzzleKey);
                isSolved = (int)fieldInfo.GetValue(playerList[playerId].Stage1);
            }
            else if(Stage == 2)
            {
                FieldInfo fieldInfo = typeof(Stage2).GetField(puzzleKey);
                isSolved = (int)fieldInfo.GetValue(playerList[playerId].Stage2);
            }

            PlayerPrefs.SetInt(puzzleKey, isSolved);

        }
        Debug.Log("LoadFileToPlayerPrefs");
        PlayerPrefs.Save();
    }


    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> playerList = new List<PlayerEntry>();

        playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int playerId = PlayerPrefs.GetInt("PlayerID");

        Stage1 stage1 = playerList[playerId].Stage1;
        Stage2 stage2 = playerList[playerId].Stage2;

        FieldInfo fieldInfo;

        if(Stage == 1)
        {
            fieldInfo = typeof(Stage1).GetField(fieldName);
            fieldInfo.SetValue(stage1, value);
        }
        else if(Stage == 2)
        {
            fieldInfo = typeof(Stage2).GetField(fieldName);
            fieldInfo.SetValue(stage2, value);
        }

        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }

        FileHandler.SaveToJSON<PlayerEntry>(playerList, "PlayerData.json");
    }

}

[System.Serializable]
public class PuzzleKeysData
{
    public string Key;
}
