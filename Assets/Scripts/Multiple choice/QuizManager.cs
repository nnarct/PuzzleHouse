using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System.Diagnostics;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject CorrectPanel;
    public Text QuestionTxt;
    public string puzzleKey;
    private int score;
    public TMP_Text scoreText;
    string Filename = "PlayerData.json";

    List<PlayerEntry> PlayerList = new List<PlayerEntry>();
    private void Start()
    {
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(Filename);
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        CorrectPanel.SetActive(false);
        generateQuestion();
    }

    public void correct()
    {
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        score = score + 1;
        PlayerPrefs.SetInt("Stage1-score", score);
        scoreText.text = score.ToString() + "/5";
        //PlayerPrefs.SetInt(puzzleKey, 1); // 1 passed 0 not passed
        PlayerPrefs.Save();

        int PlayerID = PlayerPrefs.GetInt("PlayerID");
        //PlayerList[PlayerID].stage1[puzzleKey] = 2;
        UpdateStage1Field(PlayerList[PlayerID].stage1, puzzleKey, 1);
        //PlayerList[PlayerID].stage1.time = 4;
        // PlayerList[PlayerID] = new PlayerList[PlayerID].stage1[puzzleKey] = 2;
        // PlayerList[PlayerID].stage1.GetType().GetProperty(puzzleKey, BindingFlags.Instance | BindingFlags.Public).SetValue(puzzleKey, 5);
        FileHandler.SaveToJSON<PlayerEntry>(PlayerList, Filename);
        CorrectPanel.SetActive(true);
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrentAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswers();
    }

    private void UpdateStage1Field(Stage1 stage1, string fieldName, int value)
    {
        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);
        UnityEngine.Debug.Log("typeof(Stage1) stage1" + typeof(Stage1));
        // Check if the field exists
        if (fieldInfo != null)
        {
            UnityEngine.Debug.Log(fieldInfo);
            // Set the value of the field
            fieldInfo.SetValue(stage1, value);
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }

}
