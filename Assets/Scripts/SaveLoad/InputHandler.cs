using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField NameInput;

    private Button _confirmButton;

    private string _fileName = "PlayerData.json";

    private List<PlayerEntry> _playerList = new List<PlayerEntry>();

    private int _sceneIndex;


    void Start()
    {
        _confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
        _confirmButton.interactable = false;
        _playerList = FileHandler.ReadListFromJSON<PlayerEntry>(_fileName);
    }

    void Update()
    {
        _confirmButton.interactable = ValidateInput(NameInput.text);
    }

    public void AddNameToList()
    {
        if(ValidateInput(NameInput.text))
        {
            int PlayerID = _playerList.Count;
            PlayerPrefs.SetInt("PlayerID", PlayerID);
            _sceneIndex = SceneManager.GetActiveScene().buildIndex +1;
            _playerList.Add(new PlayerEntry(NameInput.text, _sceneIndex));
            FileHandler.SaveToJSON<PlayerEntry>(_playerList, _fileName);
            SceneManager.LoadSceneAsync(_sceneIndex);
        }
    }

    public void BackScene()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex ;
        SceneManager.LoadSceneAsync(_sceneIndex - 1);
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
