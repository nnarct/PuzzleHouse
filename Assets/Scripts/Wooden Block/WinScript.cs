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

//void Update()
   // {
     //   if (CurrentPoint >= PointToWin)
      //  {
            //Win
       //     transform.GetChild(0).gameObject.SetActive(true);
       //     score++;
        //    PlayerPrefs.SetInt("Stage1-score", score);
        //    PlayerPrefs.SetInt(puzzleKey, 1);
        //    PlayerPrefs.Save();
       // }
   // }

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
            Correct();
        }
    }

    public void Correct()
    {
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }



}
