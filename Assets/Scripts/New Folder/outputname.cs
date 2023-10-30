using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class outputname : MonoBehaviour
{
    private Text obj_text;

     void Start()
    {
        obj_text = GameObject.FindGameObjectWithTag("text").GetComponent<Text>();
        obj_text.text = PlayerPrefs.GetString("user_name");

    }

}
