using UnityEngine;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{
    public char CharValue;

    [SerializeField] private Text _charText;
    
    [HideInInspector]
    private Button _buttonObj;

    private void Awake()
    {
        _buttonObj = GetComponent<Button>();

        if (_buttonObj) 
        {
            _buttonObj.onClick.AddListener(() => CharSelected());
        }
    }

    public void SetChar(char value)
    {
        _charText.text = value + "";
        CharValue = value;
    }

    private void CharSelected() 
    {
        ShuffleWordQuizManager.Instance.SelectedOption(this);
    }
}
