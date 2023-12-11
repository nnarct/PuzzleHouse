using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public delegate void OnCorrectAnswer(string puzzleKey, GameObject puzzlePanel); // Define an event to notify other scripts when a question is answered correctly

    public static event OnCorrectAnswer CorrectAnswerEvent; // Event to handle correct answers

    public List<QuestionAndAnswers> QnA; // List of questions and answers

    public GameObject[] Options; // Array to store answer options

    public GameObject PuzzlePanel; // Reference to the puzzle panel GameObject
    
    public GameObject WrongPanel; // Reference to the wrong panel GameObject

    public TMP_Text QuestionTxt; // Text component to display the question

    public string PuzzleKey; // Key to identify the puzzle

    public int CurrentQuestion; // Index of the current question

    [SerializeField] private AudioSource _source; // Audio source for correct sound

     // Called when the script starts
    private void Start()
    {
        // Generate the first question when the game starts
        GenerateQuestion();
    }

    // Method called when the answer is correct
    public void Correct()
    {
        // Invoke the CorrectAnswerEvent, notifying other scripts about the correct answer
        CorrectAnswerEvent(PuzzleKey, PuzzlePanel);
    }

    // Method called when the answer is wrong
    public void Wrong()
    {
        // Play the wrong sound
        _source.Play();

        // Display the wrong panel temporarily
        WrongPanel.SetActive(true);

        // Cancel any previous invokes and schedule the deactivation of the wrong panel
        CancelInvoke("DeactiveWrongPanel");
        Invoke("DeactiveWrongPanel", 2f);
    
        // Remove the current question from the list and generate a new question
        QnA.RemoveAt(CurrentQuestion);
        GenerateQuestion();
    }

    // Method to set up answer options for the current question
    void SetAnswers()
    {
        for(int i = 0; i < Options.Length; i++)
        {
            // Initialize IsCorrect to false for each option
            Options[i].GetComponent<AnswerScript>().IsCorrect = false;
            
            // Set the text of the option based on the current question's answers
            Options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[CurrentQuestion].Answers[i];
            
            // If the option is the correct answer, set IsCorrect to true
            if(QnA[CurrentQuestion].CorrectAnswer == i+1)
            {
                Options[i].GetComponent<AnswerScript>().IsCorrect = true;
            }
        }
    }

    // Method to generate a new question
    void GenerateQuestion()
    {
        // Randomly select a question from the list
        CurrentQuestion = Random.Range(0, QnA.Count);

        // Display the selected question
        QuestionTxt.text = QnA[CurrentQuestion].Question;

        // Set up answer options for the current question
        SetAnswers();
    }

    // Method to deactivate the wrong panel
    public void DeactiveWrongPanel()
    {
        WrongPanel.SetActive(false);
    }
}