using UnityEngine;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{
    public char CharValue; // Character value associated with this word

    [SerializeField] private Text _charText; // Text component to display the character

    [HideInInspector] private Button _buttonObj; // Button component for user interaction

    private void Awake()
    {
        // Get the Button component
        _buttonObj = GetComponent<Button>();

        // Add a listener to the button click event
        if (_buttonObj)
        {
            _buttonObj.onClick.AddListener(() => CharSelected());
        }
    }

    // Method to set the character value and update the displayed text
    public void SetChar(char value)
    {
        // Update the text to display the character value
        _charText.text = value + "";
        
        // Set the character value
        CharValue = value;
    }

    // Method called when the word is selected
    private void CharSelected()
    {
        // Notify the ShuffleWordQuizManager that this option word is selected
        ShuffleWordQuizManager.Instance.SelectedOption(this);
    }
}
