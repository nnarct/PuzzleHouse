using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

    // [SerializeField] public GameObject InteractText;
    [SerializeField] GameObject Puzzle;
    public bool isInRange;
    public bool isInPuzzle;

    private void Start()
    {
        Puzzle.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("Player"))
        {
            //InteractText.gameObject.SetActive(true);
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            //InteractText.gameObject.SetActive(false);
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                 playPuzzle();
            }
        }
    }

    public void playPuzzle()
    {
        Puzzle.SetActive(true);
    }
}
