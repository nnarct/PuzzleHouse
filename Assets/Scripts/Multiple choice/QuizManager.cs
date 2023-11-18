using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        score = score + 1;
        PlayerPrefs.SetInt("Stage1-score", score);
        scoreText.text = score.ToString() + "/5";
        PlayerPrefs.SetInt(puzzleKey, 1); // 1 passed 0 not passed
        PlayerPrefs.Save();
        CorrectPanel.SetActive(true);
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrentAnswer == i+1)
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
}
