using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inputname : MonoBehaviour
{
    public Text Obj_text;
    public InputField Display;

    void Start()
    {
        Obj_text.text = PlayerPrefs.GetString("user_name");
    }


    public void Next()
    {
        Obj_text.text = Display.text;
        PlayerPrefs.SetString("user_name", Obj_text.text);
        PlayerPrefs.Save();

        SceneManager.LoadSceneAsync(2);
    }


}
