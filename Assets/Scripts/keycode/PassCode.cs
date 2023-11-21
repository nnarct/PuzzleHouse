using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PassCode : MonoBehaviour
{ 
    public TMP_Text UiText = null;

    private string _code;
    private string _answer = null;
    private int _answerIndex = 0;
   

    public void AddNumber(string number)
    {
        if(_answerIndex < 7)
        {
            _answerIndex++;
            _answer = _answer + number;
            UiText.text = _answer;
        }
    }

    public void OnEnter()
    {
        _code = PlayerPrefs.GetString("Passcode");
        //Debug.Log("Number"+ Number);
        //Debug.Log("Passcode"+Code);
        if (_answer == _code)
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

    public void OnDelete()
    {
        if(_answerIndex!=0)
        {
            _answerIndex--;
            _answer = _answer.Substring(0, _answer.Length - 1);
            UiText.text = _answer;
        }
       
    }

}
