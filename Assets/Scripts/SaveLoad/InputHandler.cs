using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField NameInput;

    public GameObject ErrorText;

    private Button _confirmButton;

    private string _fileName = "PlayerData.json";

    private List<PlayerEntry> _playerList = new List<PlayerEntry>();

    private int _sceneIndex;

    private string[] _existName;

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
        if(ValidateInput(NameInput.text) && ValidateName())
        {
            int playerID = _playerList.Count;
            string characterName = PlayerPrefs.GetString("Character");
            PlayerPrefs.SetInt("PlayerID", playerID);
            _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            _playerList.Add(new PlayerEntry(NameInput.text, _sceneIndex+1, characterName));
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

    Boolean ValidateName()
    {
        foreach(var player in _playerList)
        {
            if(player.PlayerName == NameInput.text)
            {
                ErrorText.SetActive(true);
                LeanTween.scale(ErrorText, new Vector3(1f, 1f, 1f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInOutElastic);
                Invoke("CloseErrorText", 3f);
                return false;
            }
        }
        return true;
    }

    void CloseErrorText()
    {
        ErrorText.SetActive(false);
        LeanTween.scale(ErrorText, new Vector3(0f, 0f, 0f), .7f).setEase(LeanTweenType.linear);
    }

}
