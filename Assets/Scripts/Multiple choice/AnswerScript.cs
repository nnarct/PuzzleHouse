using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool IsCorrect = false;
    public QuizManager quizManager;


    public void Answer()
    {
        if (IsCorrect)
        {
            // Debug.Log("Correct Answer");
            quizManager.Correct();
        }
        else
        {
            // Debug.Log("Wrong Answer");
            quizManager.Wrong();
        }

    }
}
