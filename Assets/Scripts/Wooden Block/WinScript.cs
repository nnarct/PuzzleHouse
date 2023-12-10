using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class WinScript : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject Block;
    public string PuzzleKey = "Wooden";
    [SerializeField] GameObject GamePanel;
   
    private int _pointToWin;
    private int _currentPoint;
    private int _score;
    private Interactor _interactorScript;

    void Start()
    {
        _score = PlayerPrefs.GetInt("Stage1-score", 0);
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
        _pointToWin = Block.transform.childCount;
    }

    public void AddPoint()
    {
        _currentPoint++;
        CheckWin();
    } 

    private void CheckWin()
    {
        if (_currentPoint >= _pointToWin)
        {
            //Win
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("Correct", .7f);
        }
    }

    public void Correct()
    {
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }



}
