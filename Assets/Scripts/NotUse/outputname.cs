using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class outputname : MonoBehaviour
{
    private Text _obj_text;

     void Start()
    {
        _obj_text = GameObject.FindGameObjectWithTag("text").GetComponent<Text>();
        _obj_text.text = PlayerPrefs.GetString("user_name");

    }

}
