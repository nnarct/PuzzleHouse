using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveName : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public TMP_Text NameDisplayText;
    public Button ConfirmButton;
    private const string playerNameKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        // Attach the button click listener
        // Button saveButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
        Button saveButton = ConfirmButton;
        saveButton.onClick.AddListener(GetPlayerName);
    }

    void GetPlayerName()
    {
        // Get the name from the TMP input field and display it
        string playerName = NameInputField.text;
        NameDisplayText.text =  playerName;
        NameInputField.text = null;
        // Adjust the font size based on the length of the text
        SavePlayerName(playerName);
    }
    void SavePlayerName(string playerName)
    {
        // Save the player name to web storage (alternative to PlayerPrefs)
        PlayerPrefs.SetString(playerNameKey, playerName);
    }

}
