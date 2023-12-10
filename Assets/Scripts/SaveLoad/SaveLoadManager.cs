using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    
    private int _sceneIndex;
    private int _playerID;
    private string _filename = "PlayerData.json" ;
    private List<PlayerEntry> _playerList = new List<PlayerEntry>();

    private void Awake()
    {
        _playerList = FileHandler.ReadListFromJSON<PlayerEntry>(_filename);
    }
    public void NewGame()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(_sceneIndex);
        
        PlayerPrefs.Save();
    }
    
    public void LoadGame()
    {
        _playerID = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        PlayerPrefs.SetInt("PlayerID", _playerID);
        SceneManager.LoadSceneAsync(_playerList[_playerID].Level);
    }

    public void SaveGame()
    {
        _playerID = PlayerPrefs.GetInt("PlayerID");
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _playerList[_playerID].Level = _sceneIndex;
        FileHandler.SaveToJSON<PlayerEntry>(_playerList, _filename);
        SceneManager.LoadSceneAsync(0);
    }

    public void DelPlayer()
    {
        _playerID = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        _playerList.RemoveAt(_playerID);
        FileHandler.SaveToJSON<PlayerEntry>(_playerList, _filename);
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }


}
