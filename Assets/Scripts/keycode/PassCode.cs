using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassCode : MonoBehaviour
{
    string Code;
    string Number = null;
    int NumberIndex = 0;
    public Text UiText = null;

    public void CodeFunction(string Numbers)
    {
        if(NumberIndex < 7)
        {
            NumberIndex++;
            Number = Number + Numbers;
            UiText.text = Number;
        }
    }

    public void Enter()
    {
        Code = PlayerPrefs.GetString("Passcode");
        Debug.Log("Number"+ Number);
        Debug.Log("Passcode"+Code);
        if (Number == Code)
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
        if(NumberIndex!=0)
        {
            NumberIndex--;
            Number = Number.Substring(0, Number.Length - 1);
            UiText.text = Number;
        }
       
    }

}
