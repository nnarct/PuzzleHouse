using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField NameInput;
    string Filename = "PlayerData.json";

    List<PlayerEntry> PlayerList = new List<PlayerEntry>();

    int Scene_index;

    private void Start()
    {
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(Filename);
        
    }

    public void AddNameToList()
    {
        PlayerPrefs.SetInt("PlayerID", PlayerList.Count);
        Scene_index = SceneManager.GetActiveScene().buildIndex +1;
        PlayerList.Add(new PlayerEntry(NameInput.text, Scene_index));
        //Debug.Log(PlayerList.Count);
        NameInput.text = "";
        FileHandler.SaveToJSON<PlayerEntry>(PlayerList, Filename);
        SceneManager.LoadSceneAsync(Scene_index);
    }

    public void BackScene()
    {
        Scene_index = SceneManager.GetActiveScene().buildIndex ;
        SceneManager.LoadSceneAsync(Scene_index - 1);
    }


   
}
