using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ShuffleWordQuizManager : MonoBehaviour
{
    public static ShuffleWordQuizManager instance;

    [SerializeField]
    private QuestionData question;

    [SerializeField]
    private WordData[] answerWordArray;
    [SerializeField]
    private WordData[] optionWordArray;
    [SerializeField]
    private GameObject CorrectPanel;

    private char[] charArray = new char[5];
    private int currentAnswerIndex = 0;
    private bool correctAnswer;
    private List<int> selectWordIndex;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
            Destroy(gameObject);

        selectWordIndex = new List<int>();
    }

    private void Start()
    {
        SetQuestion();
    }
    private void SetQuestion()
    {
        currentAnswerIndex = 0;
        selectWordIndex.Clear();

        ResetQuestion();

        for (int i = 0; i < question.answer.Length; i++) 
        {
            charArray[i] = char.ToUpper(question.answer[i]);
        }

        for (int i = question.answer.Length ;i < optionWordArray.Length; i++)
        {
            charArray[i] = (char)UnityEngine.Random.Range(65, 91);
        }
        
        charArray = ShuffleList.ShuffleListItems<char>(charArray.ToList()).ToArray();

        for (int i = 0; i < optionWordArray.Length; i++)
        {
            optionWordArray[i].SetChar(charArray[i]);
        }
    }

    public void SelectedOption(WordData wordData)
    {
        if(currentAnswerIndex >= question.answer.Length) return;

        selectWordIndex.Add(wordData.transform.GetSiblingIndex());
        answerWordArray[currentAnswerIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false);
        currentAnswerIndex++;

        if (currentAnswerIndex >= question.answer.Length)
        {
            correctAnswer = true;

            for (int i = 0;i < question.answer.Length;i++)
            {
                if (char.ToUpper(question.answer[i]) != char.ToUpper(answerWordArray[i].charValue))
                {
                    correctAnswer = false;
                    break;
                }
            }

            if (correctAnswer) 
            {
                CorrectPanel.SetActive(true);
                Debug.Log("Correct Answer");
            }
            else if (!correctAnswer)
            {
                Debug.Log("Wrong Answer");
            }
        }
    }

    public void ResetQuestion()
    {
        for (int i = 0; i < answerWordArray.Length; i++) 
        {
            answerWordArray[i].gameObject.SetActive(true);
            answerWordArray[i].SetChar('_');
        }

        for (int i = question.answer.Length; i < answerWordArray.Length; i++)
        {
            answerWordArray[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < optionWordArray.Length; i++)
        {
            optionWordArray[i].gameObject.SetActive(true);
        }

        currentAnswerIndex = 0;
    }

    public void ResetLastWord() 
    {
        if (selectWordIndex.Count > 0)
        {
            int index = selectWordIndex[selectWordIndex.Count - 1];
            optionWordArray[index].gameObject.SetActive(true);
            selectWordIndex.RemoveAt(selectWordIndex.Count - 1);
            currentAnswerIndex--;
            answerWordArray[currentAnswerIndex].SetChar('_');
        }
    }

}

[System.Serializable]
public class QuestionData
{
    public string answer;
}