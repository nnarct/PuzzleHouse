using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShuffleWordQuizManager : MonoBehaviour
{
    
    [SerializeField] public float DelayTime = 1f; // Delay time for certain actions

    [SerializeField] public GameObject GamePanel; // Game panel for UI interaction

    public static ShuffleWordQuizManager Instance; // Singleton instance

    public string PuzzleKey; // Puzzle key identifier
    
    public TMP_Text ScoreText; // Text component for displaying the score

    public ScoreManager scoreManager; // Score manager reference

    [SerializeField] private AudioSource _source; // Audio source for playing sounds

    [SerializeField] private QuestionData _question; // Question data

    [SerializeField] private WordData[] _AnswerWordArray; // Array for answer word data

    [SerializeField] private WordData[] _optionWordArray; // Array for option word data

    [SerializeField] private GameObject _correctPanel; // Panels for displaying correct answer

    [SerializeField] private GameObject _wrongPanel; // Panels for displaying wrong answer

    private Interactor _interactorScript; // Interactor script reference

    private char[] _charArray = new char[5]; // Array to hold characters

    private bool _correctAnswer; // Flag to check if the answer is correct

    private int _currentAnswerIndex = 0; // Index to track the current answer position

    private List<int> _selectWordIndex; // List to store selected word indices

    // Start is called before the first frame update
    private void Start()
    {
        // Set up the initial question
        SetQuestion();

        // Get the Interactor script component
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    // Awake is called when the script instance is activated
    private void Awake()
    {
        // Ensure only one instance of ShuffleWordQuizManager exists
        if (Instance == null)
        {
            // Set the instance to the current instance
            Instance = this;
        }
        // Check if the instance is not the current instance
        else
        {
            // Destroy the current instance
            Destroy(gameObject);
        }

        // Initialize the list for selected word indices
        _selectWordIndex = new List<int>();
    }

    

    //Set up the question
    private void SetQuestion()
    {
        _currentAnswerIndex = 0;
        _selectWordIndex.Clear();

        // Reset the question state
        ResetQuestion();

        // Convert answer to uppercase characters
        for (int i = 0; i < _question.Answer.Length; i++)
        {
            _charArray[i] = char.ToUpper(_question.Answer[i]);
        }

        // Fill remaining slots with random characters
        for (int i = _question.Answer.Length; i < _optionWordArray.Length; i++)
        {
            _charArray[i] = (char)UnityEngine.Random.Range(65, 91);
        }

        // Shuffle the characters in the array
        _charArray = ShuffleList.ShuffleListItems<char>(_charArray.ToList()).ToArray();

        // Set characters to WordData objects
        for (int i = 0; i < _optionWordArray.Length; i++)
        {
            _optionWordArray[i].SetChar(_charArray[i]);
        }
    }

    // Handle selected option
    public void SelectedOption(WordData wordData)
    {
        // Check if the answer has been fully filled
        if (_currentAnswerIndex >= _question.Answer.Length) return;

        // Add the selected word index to the list
        _selectWordIndex.Add(wordData.transform.GetSiblingIndex());

        // Set the character in the answer word array
        _AnswerWordArray[_currentAnswerIndex].SetChar(wordData.CharValue);

        // Hide the selected word
        wordData.gameObject.SetActive(false);

        _currentAnswerIndex++;

        // Check if the answer is complete
        if (_currentAnswerIndex >= _question.Answer.Length)
        {
            _correctAnswer = true;

            // Check each character in the answer
            for (int i = 0; i < _question.Answer.Length; i++)
            {
                // If any character does not match, set the correct answer flag to false
                if (char.ToUpper(_question.Answer[i]) != char.ToUpper(_AnswerWordArray[i].CharValue))
                {
                    _correctAnswer = false;
                    break;
                }
            }

            // Invoke appropriate functions based on correctness
            if (_correctAnswer)
            {
                // If the answer is correct, play the correct sound and display the correct panel
                Invoke("Correct", .7f);
            }
            else if (!_correctAnswer)
            {
                // If the answer is wrong, play the wrong sound and display the wrong panel
                _source.Play();
                _wrongPanel.SetActive(true);
                Invoke("DeactiveWrongPanel", DelayTime);
                Invoke("ResetQuestion", DelayTime);
            }
        }
    }

    // Function called for correct answer
    public void Correct()
    {
        // Call HandleCorrectAnswer() from the ScoreManager class to handle the correct answer
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }

    // Deactivate the wrong panel after a delay
    public void DeactiveWrongPanel()
    {
        _wrongPanel.SetActive(false);
    }

    // Reset the question
    public void ResetQuestion()
    {
        // Reset the answer word array
        for (int i = 0; i < _AnswerWordArray.Length; i++)
        {
            _AnswerWordArray[i].gameObject.SetActive(true);
            _AnswerWordArray[i].SetChar('_');
        }

        // Hide additional answer word slots
        for (int i = _question.Answer.Length; i < _AnswerWordArray.Length; i++)
        {
            _AnswerWordArray[i].gameObject.SetActive(false);
        }

        // Show all option word slots
        for (int i = 0; i < _optionWordArray.Length; i++)
        {
            _optionWordArray[i].gameObject.SetActive(true);
        }

        // Reset the current answer index
        _currentAnswerIndex = 0;
    }

    // Reset the last selected word
    public void ResetLastWord()
    {
        // Check if there is a selected word to reset
        if (_selectWordIndex.Count > 0 && _currentAnswerIndex != 0)
        {
            int index = _selectWordIndex[_selectWordIndex.Count - 1];
            // Show the hidden option word
            _optionWordArray[index].gameObject.SetActive(true);

            // Remove the index from the list
            _selectWordIndex.RemoveAt(_selectWordIndex.Count - 1);

            // Decrease the current answer index
            _currentAnswerIndex--;

            // Reset the corresponding answer word character
            _AnswerWordArray[_currentAnswerIndex].SetChar('_');
        }
    }
}

// Serializable class to hold question data
[System.Serializable]
public class QuestionData
{
    public string Answer;
}