using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class ShuffleWordQuizManager : MonoBehaviour
{
    public static ShuffleWordQuizManager instance;
    public string puzzleKey;
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
    public float delayTime = 1f;

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
                int score = PlayerPrefs.GetInt("Stage1-score", 0);
                if (PlayerPrefs.GetInt(puzzleKey, 0) == 0)
                {
                    // If the player hasn't won the puzzle , increment score
                    score++;
                }
                PlayerPrefs.SetInt("Stage1-score", score);
                PlayerPrefs.SetInt(puzzleKey, 1);
                PlayerPrefs.Save();
                UpdateStage1Field(puzzleKey, 1);
            }
            else if (!correctAnswer)
            {

                Invoke("ResetLastWord", delayTime);
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

    void UpdateStage1Field(string fieldName, int value)
    {
        List<PlayerEntry> PlayerList = new List<PlayerEntry>();

        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        int PlayerID = PlayerPrefs.GetInt("PlayerID");

        Stage1 stage1 = PlayerList[PlayerID].stage1;

        // Use reflection to get the field by name
        FieldInfo fieldInfo = typeof(Stage1).GetField(fieldName);

        //UnityEngine.Debug.Log("typeof(Stage1) stage1" + typeof(Stage1));
        // Check if the field exists
        if (fieldInfo != null)
        {
            // Set the value of the field
            fieldInfo.SetValue(stage1, value);
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, "PlayerData.json");
        }
        else
        {
            UnityEngine.Debug.LogError($"Field not found or not writable: {fieldName}");
        }
    }


}

[System.Serializable]
public class QuestionData
{
    public string answer;
}