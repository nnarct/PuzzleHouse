using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Reflection;

public class SaveLoadManager : MonoBehaviour
{
    
    private int _sceneIndex; // Variable to hold scene index

    private int _playerID; // Current playerID 

    private string _filename = "PlayerData.json" ; // file name  player data

    private List<PlayerEntry> _playerList = new List<PlayerEntry>(); // List to store player data

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _playerList = FileHandler.ReadListFromJSON<PlayerEntry>(_filename);
    }
    
    // Method to new game
    public void NewGame()
    {
        // Get scene index and go to next scene index
        _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(_sceneIndex);
        
        // Save to PlayerPrefs
        PlayerPrefs.Save();
    }
    
    // Method to load game from player data
    public void LoadGame()
    {
        // Get playerID that is selected from the UI button's 
        _playerID = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        // PlayerPrefs PlayerID to selected playerID a
        PlayerPrefs.SetInt("PlayerID", _playerID);

        // Load the scene associated with the selected player's level 
        SceneManager.LoadSceneAsync(_playerList[_playerID].Level);
    }

    // Method to save game to player data JSON file
    public void SaveGame()
    {
        // get current playerID from PlayerPrefs
        _playerID = PlayerPrefs.GetInt("PlayerID");

        // get current scene index and set to current player level then save to file
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _playerList[_playerID].Level = _sceneIndex;
        FileHandler.SaveToJSON<PlayerEntry>(_playerList, _filename);
        SceneManager.LoadSceneAsync(0);
    }

    // Method to Delete player from file
    public void DelPlayer()
    {
        // Get playerID that is selected from the UI button's 
        _playerID = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        // Remove the selected player from the player list and save the update to file
        _playerList.RemoveAt(_playerID);
        FileHandler.SaveToJSON<PlayerEntry>(_playerList, _filename);

        // load current scene again
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    // Method to update stage field
    public static void UpdateStageField<T>(string fieldName, int value) where T : class
    {
        // Read the player list from JSON file
        List<PlayerEntry> playerList = FileHandler.ReadListFromJSON<PlayerEntry>("PlayerData.json");

        // Get the player ID from PlayerPrefs
        int playerID = PlayerPrefs.GetInt("PlayerID");

        // Attempt to retrieve the specific stage (e.g., Stage1 or Stage2) using reflection
        T stage = playerList[playerID]?.GetType()?.GetProperty(typeof(T).Name)?.GetValue(playerList[playerID]) as T;

        if (stage != null)
        {
            // Use reflection to get the field by name
            FieldInfo fieldInfo = typeof(T).GetField(fieldName);

            if (fieldInfo != null)
            {
                // Set the value of the field
                fieldInfo.SetValue(stage, value);

                // Save the updated player list to the JSON file
                FileHandler.SaveToJSON<PlayerEntry>(playerList, "PlayerData.json");
            }
            else
            {
                // Log an error if the field is not found or not writable
                UnityEngine.Debug.LogError($"Field '{fieldName}' not found or not writable in {typeof(T).Name}");
            }
        }
        else
        {
            // Log an error if the player or stage is not found for the given ID
            UnityEngine.Debug.LogError($"Player or Stage not found for ID: {playerID}");
        }
    }

}
