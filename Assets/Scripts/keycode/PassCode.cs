using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PassCode : MonoBehaviour
{
    string Code;
    string Answer = null;
    int AnswerIndex = 0;
    public TMP_Text UiText = null;

    public void CodeFunction(string Number)
    {
        if(AnswerIndex < 7)
        {
            AnswerIndex++;
            Answer = Answer + Number;
            UiText.text = Answer;
        }
    }

    public void Enter()
    {
        Code = PlayerPrefs.GetString("Passcode");
        //Debug.Log("Number"+ Number);
        //Debug.Log("Passcode"+Code);
        if (Answer == Code)
        {
            Debug.Log("It's Work!!!");
            //SceneManager.LoadScene("stage2");
        }
        else
        {
            Debug.Log("It's Wrong!!!");
            //Debug.Log("Try again");
        }
    }

    public void Delete()
    {
        if(AnswerIndex!=0)
        {
            AnswerIndex--;
            Answer = Answer.Substring(0, Answer.Length - 1);
            UiText.text = Answer;
        }
       
    }

}
