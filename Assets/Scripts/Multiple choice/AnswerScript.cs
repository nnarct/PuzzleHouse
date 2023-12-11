using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool IsCorrect = false; // Flag indicating whether this answer is correct or not

    public QuizManager quizManager; // Reference to the QuizManager script to communicate the correctness of the answer

    // Called when the answer is selected
    public void Answer()
    {
        // Check if the selected answer is correct
        if (IsCorrect)
        {
            // Inform the QuizManager that the answer is correct
            quizManager.Correct();
        }
        else
        {
            // Inform the QuizManager that the answer is wrong
            quizManager.Wrong();
        }
    }
}
