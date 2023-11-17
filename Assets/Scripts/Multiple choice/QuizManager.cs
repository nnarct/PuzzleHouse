using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject CorrectPanel;
    public Text QuestionTxt;

    private void Start()
    {
        CorrectPanel.SetActive(false);
        generateQuestion();
    }

    public void correct()
    {
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
