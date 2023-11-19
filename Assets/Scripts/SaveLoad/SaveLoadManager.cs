using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    
    int SceneIndex;
    int PlayerID;
    string Filename = "PlayerData.json" ;
    List<PlayerEntry> PlayerList = new List<PlayerEntry>();

    private void Awake()
    {
        PlayerList = FileHandler.ReadListFromJSON<PlayerEntry>(Filename);
    }
    public void NewGame()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(SceneIndex);
        
        PlayerPrefs.Save();
    }
    
    public void LoadGame()
    {
        PlayerID = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        PlayerPrefs.SetInt("PlayerID", PlayerID);
        
        //PlayerPrefs.SetInt("Stage1-score", 0);
        PlayerPrefs.SetInt("moon", PlayerList[PlayerID].stage1.moon);
        PlayerPrefs.SetInt("wooden", PlayerList[PlayerID].stage1.wooden);
        PlayerPrefs.SetInt("genetic", PlayerList[PlayerID].stage1.genetic);
        PlayerPrefs.SetInt("time", PlayerList[PlayerID].stage1.time);
        SceneManager.LoadSceneAsync(PlayerList[PlayerID].Level);
    }

    public void SaveGame()
    {
        PlayerID = PlayerPrefs.GetInt("PlayerID");
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerList[PlayerID].Level = SceneIndex;
        FileHandler.SaveToJSON<PlayerEntry>(PlayerList, Filename);
        SceneManager.LoadSceneAsync(0);
    }

    public void DelPlayer()
    {
        PlayerID = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        PlayerList.RemoveAt(PlayerID);
        FileHandler.SaveToJSON<PlayerEntry>(PlayerList, Filename);
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }


}
