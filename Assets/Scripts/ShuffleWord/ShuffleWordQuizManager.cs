using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShuffleWordQuizManager : MonoBehaviour
{
    public static ShuffleWordQuizManager Instance;
    public string PuzzleKey;
    [SerializeField]
    public float DelayTime = 1f;
    public TMP_Text ScoreText;
    public ScoreManager scoreManager;

    [SerializeField] private AudioSource _source;

    [SerializeField]
    private QuestionData _question;
    [SerializeField] GameObject GamePanel;
    [SerializeField]
    private WordData[] _answerWordArray;
    [SerializeField]
    private WordData[] _optionWordArray;
    [SerializeField]
    private GameObject _correctPanel;
    [SerializeField]
    private GameObject _wrongPanel;
    private Interactor _interactorScript;
    private char[] _charArray = new char[5];
    private int _currentAnswerIndex = 0;
    private bool _correctAnswer;
    private List<int> _selectWordIndex;
   
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
            Destroy(gameObject);

        _selectWordIndex = new List<int>();
    }

    private void Start()
    {
        SetQuestion();
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }
    private void SetQuestion()
    {
        _currentAnswerIndex = 0;
        _selectWordIndex.Clear();

        ResetQuestion();

        for (int i = 0; i < _question.answer.Length; i++) 
        {
            _charArray[i] = char.ToUpper(_question.answer[i]);
        }

        for (int i = _question.answer.Length ;i < _optionWordArray.Length; i++)
        {
            _charArray[i] = (char)UnityEngine.Random.Range(65, 91);
        }
        
        _charArray = ShuffleList.ShuffleListItems<char>(_charArray.ToList()).ToArray();

        for (int i = 0; i < _optionWordArray.Length; i++)
        {
            _optionWordArray[i].SetChar(_charArray[i]);
        }
    }

    public void SelectedOption(WordData wordData)
    {
        if(_currentAnswerIndex >= _question.answer.Length) return;

        _selectWordIndex.Add(wordData.transform.GetSiblingIndex());
        _answerWordArray[_currentAnswerIndex].SetChar(wordData.CharValue);
        wordData.gameObject.SetActive(false);
        _currentAnswerIndex++;

        if (_currentAnswerIndex >= _question.answer.Length)
        {
            _correctAnswer = true;

            for (int i = 0;i < _question.answer.Length;i++)
            {
                if (char.ToUpper(_question.answer[i]) != char.ToUpper(_answerWordArray[i].CharValue))
                {
                    _correctAnswer = false;
                    break;
                }
            }

            if (_correctAnswer) 
            {
                // UnityEngine.Debug.Log("Correct Answer");
                Correct();
            }
            else if (!_correctAnswer)
            {
                _source.Play();
                _wrongPanel.SetActive(true);
                Invoke("DeactiveWrongPanel", DelayTime);
                Invoke("ResetQuestion", DelayTime);
                //Debug.Log("Wrong Answer");
            }
        }
    }


    public void Correct()
    { 
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }

    public void DeactiveWrongPanel()
    {
        _wrongPanel.SetActive(false);
    }


    public void ResetQuestion()
    {
        for (int i = 0; i < _answerWordArray.Length; i++) 
        {
            _answerWordArray[i].gameObject.SetActive(true);
            _answerWordArray[i].SetChar('_');
        }

        for (int i = _question.answer.Length; i < _answerWordArray.Length; i++)
        {
            _answerWordArray[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _optionWordArray.Length; i++)
        {
            _optionWordArray[i].gameObject.SetActive(true);
        }

        _currentAnswerIndex = 0;
    }

    public void ResetLastWord() 
    {
        if (_selectWordIndex.Count > 0 && _currentAnswerIndex != 0)
        {
            int index = _selectWordIndex[_selectWordIndex.Count - 1];
            _optionWordArray[index].gameObject.SetActive(true);
            _selectWordIndex.RemoveAt(_selectWordIndex.Count - 1);
            _currentAnswerIndex--;
            _answerWordArray[_currentAnswerIndex].SetChar('_');
        }
    }

}

[System.Serializable]
public class QuestionData
{
    public string answer;
}