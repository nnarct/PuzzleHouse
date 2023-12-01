using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{
    [SerializeField]
    GameObject GamePanel;

    public ScoreManager scoreManager;

    public string PuzzleKey = "Wire";
    public List<Color> _wireColors = new List<Color>();

    public List<Wire> _leftWires = new List<Wire>();
    public List<Wire> _rightWires = new List<Wire>();

    public Wire CurrentDraggedWire;
    public Wire CurrentHoveredWire;

    private List<Color> _availableColors;
    private List<int> _availableLeftWireIndex;
    private List<int> _availableRightWireIndex;

    public bool _isTaskComplete = false;
    private bool _isCoroutineStarted = false;

    private void Start()
    {
        ClosePanel();
        
        _availableColors = new List<Color>(_wireColors);
        _availableLeftWireIndex = new List<int>();
        _availableRightWireIndex = new List<int>();

        for (int i = 0; i < _leftWires.Count; i++)
        {
            _availableLeftWireIndex.Add(i);
        }
        for (int i = 0; i < _rightWires.Count; i++)
        {
            _availableRightWireIndex.Add(i);
        }

        while (_availableColors.Count > 0 && _availableLeftWireIndex.Count > 0 && _availableRightWireIndex.Count > 0)
        {
            Color pickedColor = _availableColors[Random.Range(0, _availableColors.Count)];
            int pickedLeftWireIndex = Random.Range(0, _availableLeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range(0, _availableRightWireIndex.Count);

            _leftWires[_availableLeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor);
            _rightWires[_availableRightWireIndex[pickedRightWireIndex]].SetColor(pickedColor);

            _availableColors.Remove(pickedColor);
            _availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            _availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }

    }

    private void Update()
    {
        // Check if the GamePanel is inactive and the coroutine hasn't been started
        if (!_isCoroutineStarted)
        {
            StartCoroutine(CheckTaskCompletion());
            _isCoroutineStarted = true;
        }
    }

    private IEnumerator CheckTaskCompletion()
    {
        while(!_isTaskComplete)
        {
            int correctWire = 0;
            for (int i = 0; i < _rightWires.Count; i++)
            {
                if (_rightWires[i]._isCorrect) { correctWire++; }
            }

            if (correctWire >= _rightWires.Count)
            {
                Debug.Log("Task Complete");
                scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
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
        GamePanel.SetActive(false);
    }
}
