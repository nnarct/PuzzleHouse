using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    // Define an event to notify other scripts when a question is answered correctly
    public delegate void OnCorrectAnswer(string puzzleKey, GameObject puzzlePanel);
    public static event OnCorrectAnswer CorrectAnswerEvent;

    public List<QuestionAndAnswers> QnA;
    public GameObject[] Options;
    public int CurrentQuestion;
    public GameObject PuzzlePanel;
    public TMP_Text QuestionTxt;
    public string PuzzleKey;

    private void Start()
    {
        GenerateQuestion();
    }

    public void Correct()
    {
        if (CorrectAnswerEvent != null)
        {
            CorrectAnswerEvent(PuzzleKey, PuzzlePanel);
        }
    }

    public void Wrong()
    {
        QnA.RemoveAt(CurrentQuestion);
        GenerateQuestion();
    }
    void SetAnswers()
    {
        for(int i = 0; i < Options.Length; i++)
        {
            Options[i].GetComponent<AnswerScript>().IsCorrect = false;
            Options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[CurrentQuestion].Answers[i];
            if(QnA[CurrentQuestion].CorrectAnswer == i+1)
            {
                Options[i].GetComponent<AnswerScript>().IsCorrect = true;
            }
        }
    }
    void GenerateQuestion()
    {
        CurrentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[CurrentQuestion].Question;
        SetAnswers();
    }
}