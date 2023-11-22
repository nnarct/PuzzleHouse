using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShuffleWordQuizManager : MonoBehaviour
{
    public static ShuffleWordQuizManager Instance;
    public string PuzzleKey;
    [SerializeField]
     public float DelayTime = 1f;
    public TMP_Text ScoreText;

    private QuestionData _question;
    [SerializeField] GameObject GamePanel;
    [SerializeField]
    private WordData[] _answerWordArray;
    [SerializeField]
    private WordData[] _optionWordArray;
    [SerializeField]
    private GameObject _correctPanel;
    [SerializeField]
    private GameObject _wrongPanel;
    private Interactor _interactorScript;
    private char[] _charArray = new char[5];
    private int _currentAnswerIndex = 0;
    private bool _correctAnswer;
    private List<int> _selectWordIndex;
   

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
            Destroy(gameObject);

        _selectWordIndex = new List<int>();
    }

    private void Start()
    {
        SetQuestion();
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }
    private void SetQuestion()
    {
        _currentAnswerIndex = 0;
        _selectWordIndex.Clear();

        ResetQuestion();

        for (int i = 0; i < _question.answer.Length; i++) 
        {
            _charArray[i] = char.ToUpper(_question.answer[i]);
        }

        for (int i = _question.answer.Length ;i < _optionWordArray.Length; i++)
        {
            _charArray[i] = (char)UnityEngine.Random.Range(65, 91);
        }
        
        _charArray = ShuffleList.ShuffleListItems<char>(_charArray.ToList()).ToArray();

        for (int i = 0; i < _optionWordArray.Length; i++)
        {
            _optionWordArray[i].SetChar(_charArray[i]);
        }
    }

    public void SelectedOption(WordData wordData)
    {
        if(_currentAnswerIndex >= _question.answer.Length) return;

        _selectWordIndex.Add(wordData.transform.GetSiblingIndex());
        _answerWordArray[_currentAnswerIndex].SetChar(wordData.CharValue);
        wordData.gameObject.SetActive(false);
        _currentAnswerIndex++;

        if (_currentAnswerIndex >= _question.answer.Length)
        {
            _correctAnswer = true;

            for (int i = 0;i < _question.answer.Length;i++)
            {
                if (char.ToUpper(_question.answer[i]) != char.ToUpper(_answerWordArray[i].CharValue))
                {
                    _correctAnswer = false;
                    break;
                }
            }

            if (_correctAnswer) 
            {
                UnityEngine.Debug.Log("Correct Answer");
                Correct();
            }
            else if (!_correctAnswer)
            {
                _wrongPanel.SetActive(true);
                Invoke("DeactiveWrongPanel", DelayTime);
                Invoke("ResetQuestion", DelayTime);
                //Debug.Log("Wrong Answer");
            }
        }
    }


    public void Correct()
    {
        GamePanel.SetActive(false);
        _correctPanel.SetActive(true);
        Button correctButton = GameObject.Find("CorrectKeyButton").GetComponent<Button>();
        correctButton.onClick.AddListener(OnCorrectButtonClick);
    }

    public void DeactiveWrongPanel()
    {
        _wrongPanel.SetActive(false);
    }

    void OnCorrectButtonClick()
    {
        Debug.Log("Key is clicked!");
        _correctPanel.SetActive(false);
        _interactorScript.EndInteraction();
        Debug.Log("Correct Panel is closed!");
        int score = PlayerPrefs.GetInt("Stage1-score", 0);
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0)
        {
            score++;
        }
        PlayerPrefs.SetInt("Stage1-score", score);
        ScoreText.text = score.ToString() + "/5";
        PlayerPrefs.SetInt(PuzzleKey, 1);
        PlayerPrefs.Save();
        UpdateStage1Field(PuzzleKey, 1);
    }

    public void ResetQuestion()
    {
        for (int i = 0; i < _answerWordArray.Length; i++) 
        {
            _answerWordArray[i].gameObject.SetActive(true);
            _answerWordArray[i].SetChar('_');
        }

        for (int i = _question.answer.Length; i < _answerWordArray.Length; i++)
        {
            _answerWordArray[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _optionWordArray.Length; i++)
        {
            _optionWordArray[i].gameObject.SetActive(true);
        }

        _currentAnswerIndex = 0;
    }

    public void ResetLastWord() 
    {
        if (_selectWordIndex.Count > 0)
        {
            int index = _selectWordIndex[_selectWordIndex.Count - 1];
            _optionWordArray[index].gameObject.SetActive(true);
            _selectWordIndex.RemoveAt(_selectWordIndex.Count - 1);
            _currentAnswerIndex--;
            _answerWordArray[_currentAnswerIndex].SetChar('_');
        }
    }

    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 Stage1 = PlayerList[PlayerID].Stage1;

        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

        //UnityEngine.Debug.Log("typeof(Stage1) stage1" + typeof(Stage1));
        // Check if the field exists
        if (fieldInfo != null)
        {
            // Set the value of the field
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
public class QuestionData
{
    public string answer;
}