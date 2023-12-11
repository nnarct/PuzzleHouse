using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{
    // Public variables accessible in the Inspector
    public bool IsTaskComplete = false; // Indicates if the task is complete
    public ScoreManager scoreManager; // Reference to the score manager
    public string PuzzleKey = "Wire"; // Key for identifying the puzzle
    public List<Color> WireColors = new List<Color>(); // List of available wire colors
    public List<Wire> LeftWires = new List<Wire>(); // List of wires on the left side
    public List<Wire> RightWires = new List<Wire>(); // List of wires on the right side
    public Wire CurrentDraggedWire; // Reference to the currently dragged wire
    public Wire CurrentHoveredWire; // Reference to the wire currently being hovered over

    [SerializeField] private GameObject gamePanel; // Reference to the game panel UI
    private bool _isCoroutineStarted = false; // Indicates if the coroutine has started
    private List<Color> _availableColors; // List of available colors for wires
    private List<int> _availableLeftWireIndex; // List of available indices for left wires
    private List<int> _availableRightWireIndex; // List of available indices for right wires
    private void Start()
    {
        //Hide the game panel
        ClosePanel();

        // Initialize lists and assign indices for wires
        _availableColors = new List<Color>(WireColors);
        _availableLeftWireIndex = new List<int>();
        _availableRightWireIndex = new List<int>();

        for (int i = 0; i < LeftWires.Count; i++)
        {
            _availableLeftWireIndex.Add(i);
        }
        for (int i = 0; i < RightWires.Count; i++)
        {
            _availableRightWireIndex.Add(i);
        }

        // Assign random colors to pairs of left and right wires
        while (_availableColors.Count > 0 && _availableLeftWireIndex.Count > 0 && _availableRightWireIndex.Count > 0)
        {
            Color pickedColor = _availableColors[Random.Range(0, _availableColors.Count)];
            int pickedLeftWireIndex = Random.Range(0, _availableLeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range(0, _availableRightWireIndex.Count);

            LeftWires[_availableLeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor);
            RightWires[_availableRightWireIndex[pickedRightWireIndex]].SetColor(pickedColor);

            _availableColors.Remove(pickedColor);
            _availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            _availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }

    }

    private void Update()
    {
        // Check if the gamePanel is inactive and the coroutine hasn't been started
        if (!_isCoroutineStarted)
        {
            StartCoroutine(CheckTaskCompletion());
            _isCoroutineStarted = true;
        }
    }

    // Coroutine to check task completion
    private IEnumerator CheckTaskCompletion()
    {
        while (!IsTaskComplete)
        {
            int correctWire = 0;
            for (int i = 0; i < RightWires.Count; i++)
            {
                if (RightWires[i].IsCorrect) { correctWire++; }
            }

            // Check if all right wires are in correct positions
            if (correctWire >= RightWires.Count)
            {
                Debug.Log("Task Complete");
                scoreManager.HandleCorrectAnswer(PuzzleKey, gamePanel);
            }
            else
            {
                Debug.Log("Task Incomplete");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // Method to close the game panel
    private void ClosePanel()
    {
        gamePanel.SetActive(false);
    }
}
