using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField NameInput;
    string Filename = "PlayerData.json";

    List<PlayerEntry> PlayerList = new List<PlayerEntry>();

    int Scene_index;

    private void _start()
    {
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(Filename);
    }

    public void AddNameToList()
    {
        if(ValidateInput(NameInput.text))
        {
            int PlayerID = PlayerList.Count;
            PlayerPrefs.SetInt("PlayerID", PlayerID);
            Scene_index = SceneManager.GetActiveScene().buildIndex +1;
            PlayerList.Add(new PlayerEntry(NameInput.text, Scene_index));
            FileHandler.SaveToJSON<PlayerEntry>(PlayerList, Filename);
            SceneManager.LoadSceneAsync(Scene_index);
        }
    }

    public void BackScene()
    {
        Scene_index = SceneManager.GetActiveScene().buildIndex ;
        SceneManager.LoadSceneAsync(Scene_index - 1);
    }

    Boolean ValidateInput(string text)
    {
        if(text == null || text.Length == 0)
        {
            return false;
        }
        return true;
    }
   
}
