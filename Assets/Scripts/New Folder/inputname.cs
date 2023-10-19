using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inputname : MonoBehaviour
{
    public Text obj_text;
    public InputField display;

    void Start()
    {
        obj_text.text = PlayerPrefs.GetString("user_name");
    }


    public void next()
    {
        obj_text.text = display.text;
        PlayerPrefs.SetString("user_name", obj_text.text);
        PlayerPrefs.Save();

        SceneManager.LoadSceneAsync(2);
    }


}
