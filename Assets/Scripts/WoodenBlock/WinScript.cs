using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private int PointToWin;
    private int CurrentPoint;
    public GameObject Block;
    void Start()
    {
        PointToWin = Block.transform.childCount;
    }

    void Update()
    {
        if (CurrentPoint >= PointToWin)
        {
            //Win
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void AddPoint()
    {
        CurrentPoint++;
    }
}
