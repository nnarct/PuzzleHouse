using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassCode : MonoBehaviour
{
    string Code = "6969";
    string Number = null;
    int NumberIndex = 0;
    public Text UiText = null;

    public void CodeFunction(string Numbers)
    {
        if(NumberIndex < 4)
        {
            NumberIndex++;
            Number = Number + Numbers;
            UiText.text = Number;
        }
    }

    public void Enter()
    {
        if (Number == Code)
        {
            Debug.Log("It's Work!!!");
            //SceneManager.LoadScene("stage2");
        }
        else
        {
            Debug.Log("It's Wrong!!!");
            Debug.Log("Try again");
        }
    }

    public void Delete()
    {
        NumberIndex = 0;
        Number = null;
        UiText.text = Number;
    }

}
