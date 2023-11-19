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
        int PlayerID = PlayerList.Count;
        PlayerPrefs.SetInt("PlayerID", PlayerID);
        Scene_index = SceneManager.GetActiveScene().buildIndex +1;
        PlayerList.Add(new PlayerEntry(NameInput.text, Scene_index));

        PlayerPrefs.SetInt("moon", 0);
        PlayerPrefs.SetInt("wooden", 0);
        PlayerPrefs.SetInt("genetic", 0);
        PlayerPrefs.SetInt("time", 0);
        PlayerPrefs.SetInt("earth", 0);
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
