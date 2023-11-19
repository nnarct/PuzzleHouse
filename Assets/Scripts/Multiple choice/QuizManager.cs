using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System.Diagnostics;
using System;

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

  
    private void Start()
    {
        
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        CorrectPanel.SetActive(false);
        generateQuestion();
    }

    public void correct()
    {
        score = PlayerPrefs.GetInt("Stage1-score", 0);
        // Check if the player had won this puzzle already or not
        if (PlayerPrefs.GetInt(puzzleKey, 0) == 0)
        {
            // If the player hasn't won the puzzle , increment score
            score++;
        }
        PlayerPrefs.SetInt("Stage1-score", score);
        scoreText.text = score.ToString() + "/5";
        PlayerPrefs.SetInt(puzzleKey, 1); // 1 passed 0 not passed
        PlayerPrefs.Save();

        UpdateStage1Field(puzzleKey, 1);
        
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
        currentQuestion = UnityEngine.Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswers();
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
