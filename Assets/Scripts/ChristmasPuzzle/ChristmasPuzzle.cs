using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChristmasPuzzle : MonoBehaviour
{
    public ScoreManager scoreManager;

    public string PuzzleKey = "Christmas";

    public GameObject GamePanel;

    private Button _lastClickedButton;

    public GameObject[] correctPositions;

    private Button[] _buttons;

    public int _totalScore;

    public int _currentScore;

    // Start is called before the first frame update
    void Start()
    {
        _buttons = GetComponentsInChildren<Button>();

        _totalScore = _buttons.Length;

        SwapButtonsRandomly(_buttons); // Randomize button positions
        
        foreach (Button button in _buttons)
        {
            // Attach a listener to each button
            button.onClick.AddListener(() => OnButtonClick(button));
            
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        // Check if this is the first button clicked
        if (_lastClickedButton == null)
        {
            _lastClickedButton = clickedButton;
        }
        else
        {

            // Swap positions if a button was already clicked
            Vector3 tempPosition = _lastClickedButton.transform.position;
            _lastClickedButton.transform.position = clickedButton.transform.position;
            clickedButton.transform.position = tempPosition;

            // Reset the last clicked button
            _lastClickedButton = null;


            
            // Check correct positions after swapping
            CheckCorrectPosition(correctPositions, _buttons);
        }
    }

    void SwapButtonsRandomly(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, buttons.Length);

            // Swap the positions of buttons[i] and buttons[randomIndex]
            Vector3 tempPosition = buttons[i].transform.position;
            buttons[i].transform.position = buttons[randomIndex].transform.position;
            buttons[randomIndex].transform.position = tempPosition;

            // Reset any visual indication (e.g., color)
            buttons[i].image.color = Color.white;
        }
    }

    void CheckCorrectPosition(GameObject[] correctPositions, Button[] buttons)
    {
        _currentScore = 0;
        int positionThreshold = 1; // Adjust this threshold as needed

        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log("Button : " +  i);
            //Debug.Log("correctPosition : " + correctPositions[i].transform.position);
            //Debug.Log("ButtonPosition : " + buttons[i].transform.position);
            Debug.Log(Vector3.Distance(correctPositions[i].transform.position, buttons[i].transform.position));

            // Check if the positions are close enough
            if (Vector3.Distance(correctPositions[i].transform.position, buttons[i].transform.position) <= positionThreshold)
            {
                _currentScore += 1;
                Debug.Log("Current Score: " + _currentScore);

            }
        }

        if (_currentScore == _totalScore)
        {
            Correct();
            Debug.Log("_totalScore Score: " + _totalScore);


        }

    }

    public void Correct()
    {
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }

}
