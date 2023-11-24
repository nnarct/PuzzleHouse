using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject CorrectPanel;
    public GameObject[] Options;
    public TMP_Text QuestionTxt;
    public int CurrentQuestion;
    public string PuzzleKey;
    public TMP_Text ScoreText;

    [SerializeField] private AudioSource _source;

    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject WrongPanel;
    private Interactor _interactorScript;
    private int _isScore;

    private void Start()
    {
        
        _isScore = PlayerPrefs.GetInt("Stage1-score", 0);
        CorrectPanel.SetActive(false);
        generateQuestion();
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    public void Correct()
    {
        GamePanel.SetActive(false);
        CorrectPanel.SetActive(true);
        Button correctButton = GameObject.Find("CorrectKeyButton").GetComponent<Button>();
        correctButton.onClick.AddListener(OnCorrectButtonClick);
      //  GamePanel.SetActive(false);
      //  CorrectPanel.SetActive(true);
       // score = PlayerPrefs.GetInt("Stage1-score", 0);
        // Check if the player had won this puzzle already or not
      //  if (PlayerPrefs.GetInt(puzzleKey, 0) == 0)
      //  {
            // If the player hasn't won the puzzle , increment score
      //      score++;
      //  }
      //  PlayerPrefs.SetInt("Stage1-score", score);
      //  scoreText.text = score.ToString() + "/5";
      ///  PlayerPrefs.SetInt(puzzleKey, 1); // 1 passed 0 not passed
      //  PlayerPrefs.Save();

       // UpdateStage1Field(puzzleKey, 1);
        
       
    }

    void OnCorrectButtonClick()
    {
        CorrectPanel.SetActive(false);
        _interactorScript.EndInteraction();
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0)
        {
            _isScore++;
        }
        PlayerPrefs.SetInt("Stage1-score", _isScore);
        Debug.Log("score = " + _isScore.ToString());
        ScoreText.text = _isScore.ToString() + "/5";

        PlayerPrefs.SetInt(PuzzleKey, 1);
        PlayerPrefs.Save();
        UpdateStage1Field(PuzzleKey, 1);
    }
   
    public void Wrong()
    {
        _source.Play();
        WrongPanel.SetActive(true);
        Invoke("DeactiveWrongPanel", 1f);
        QnA.RemoveAt(CurrentQuestion);
        generateQuestion();

    }

    void SetAnswers()
    {
        for (int i = 0; i < Options.Length; i++)
        {
            Options[i].GetComponent<AnswerScript>().IsCorrect = false;
            Options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[CurrentQuestion].Answers[i];

            if (QnA[CurrentQuestion].CorrentAnswer == i + 1)
            {
                Options[i].GetComponent<AnswerScript>().IsCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        CurrentQuestion = UnityEngine.Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[CurrentQuestion].Question;
        SetAnswers();
    }

    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 Stage1 = PlayerList[PlayerID].Stage1;

        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

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

    public void DeactiveWrongPanel()
    {
        WrongPanel.SetActive(false);
    }
}
