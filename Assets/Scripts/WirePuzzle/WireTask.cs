using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePanel;

    public ScoreManager scoreManager;

    public string PuzzleKey = "Wire";
    public List<Color> WireColors = new List<Color>();

    public List<Wire> LeftWires = new List<Wire>();
    public List<Wire> RightWires = new List<Wire>();

    public Wire CurrentDraggedWire;
    public Wire CurrentHoveredWire;

    private List<Color> _availableColors;
    private List<int> _availableLeftWireIndex;
    private List<int> _availableRightWireIndex;

    public bool IsTaskComplete = false;
    private bool _isCoroutineStarted = false;

    private void Start()
    {
        ClosePanel();
        
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

    private IEnumerator CheckTaskCompletion()
    {
        while(!IsTaskComplete)
        {
            int correctWire = 0;
            for (int i = 0; i < RightWires.Count; i++)
            {
                if (RightWires[i].IsCorrect) { correctWire++; }
            }

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

    private void ClosePanel()
    {
        gamePanel.SetActive(false);
    }
}
