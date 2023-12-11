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

    [SerializeField] public float DelayTime = 1f;

    public TMP_Text ScoreText;

    public ScoreManager scoreManager;

    [SerializeField] private AudioSource _source;

    [SerializeField] private QuestionData _question;

    [SerializeField] GameObject GamePanel;

    [SerializeField] private WordData[] _AnswerWordArray;

    [SerializeField] private WordData[] _optionWordArray;

    [SerializeField] private GameObject _correctPanel;

    [SerializeField] private GameObject _wrongPanel;

    private Interactor _interactorScript;

    private char[] _charArray = new char[5];

    private int _currentAnswerIndex = 0;

    private bool _correctAnswer;

    private List<int> _selectWordIndex;
    
    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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

        for (int i = 0; i < _question.Answer.Length; i++) 
        {
            _charArray[i] = char.ToUpper(_question.Answer[i]);
        }

        for (int i = _question.Answer.Length ;i < _optionWordArray.Length; i++)
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
        if(_currentAnswerIndex >= _question.Answer.Length) return;

        _selectWordIndex.Add(wordData.transform.GetSiblingIndex());
        _AnswerWordArray[_currentAnswerIndex].SetChar(wordData.CharValue);
        wordData.gameObject.SetActive(false);
        _currentAnswerIndex++;

        if (_currentAnswerIndex >= _question.Answer.Length)
        {
            _correctAnswer = true;

            for (int i = 0;i < _question.Answer.Length;i++)
            {
                if (char.ToUpper(_question.Answer[i]) != char.ToUpper(_AnswerWordArray[i].CharValue))
                {
                    _correctAnswer = false;
                    break;
                }
            }

            if (_correctAnswer) 
            {
                Invoke("Correct", .7f);
            }
            else if (!_correctAnswer)
            {
                _source.Play();
                _wrongPanel.SetActive(true);
                Invoke("DeactiveWrongPanel", DelayTime);
                Invoke("ResetQuestion", DelayTime);
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
        for (int i = 0; i < _AnswerWordArray.Length; i++) 
        {
            _AnswerWordArray[i].gameObject.SetActive(true);
            _AnswerWordArray[i].SetChar('_');
        }

        for (int i = _question.Answer.Length; i < _AnswerWordArray.Length; i++)
        {
            _AnswerWordArray[i].gameObject.SetActive(false);
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
            _AnswerWordArray[_currentAnswerIndex].SetChar('_');
        }
    }
}

[System.Serializable]
public class QuestionData
{
    public string Answer;
}