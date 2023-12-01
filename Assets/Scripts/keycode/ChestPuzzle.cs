using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class ChestPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ChestPuzzleButton;
    public GameObject ChestBoxUI;
    public TMP_Text ParchmentText;

    private int _score;
    private Scene _currentScene;

    void Start()
    {
        //ChestPuzzleButton.SetActive(false);
        ParchmentText.text = GenerateRandomNumericPassword(3, 6) + "#";
        _currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentScene.name == "Stage1")
        {
            _score = PlayerPrefs.GetInt("Stage1_score", 0);
        }
        else if (_currentScene.name == "Stage2") {
            _score = PlayerPrefs.GetInt("Stage2_score", 0);
        }
        CheckChest(_score);
    }

    private void CheckChest(int score)
    {
        if (score == 5)
        {
          //  Debug.Log("should be active");
            ChestPuzzleButton.SetActive(true);
            ChestBoxUI.SetActive(true);
        }
        else if (score < 5)
        {
            ChestPuzzleButton.SetActive(false);
            ChestBoxUI.SetActive(false);
        }
    }

    private string GenerateRandomNumericPassword(int minLength, int maxLength)
    {
        int passwordLength = UnityEngine.Random.Range(minLength, maxLength + 1); // +1 to make it inclusive
        System.Text.StringBuilder password = new System.Text.StringBuilder();
        for (int i = 0; i < passwordLength; i++)
        {
            // Generate a random digit and append it to the password
            password.Append(UnityEngine.Random.Range(0, 10));
        }
        PlayerPrefs.SetString("Passcode", password.ToString());
        PlayerPrefs.Save();
        return password.ToString();
    }

}
