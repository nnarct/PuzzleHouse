using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

    [SerializeField] GameObject InteractText;
    [SerializeField] GameObject Puzzle;
    public bool isInRange;
    public bool isInPuzzle;

    private MovementPlayer movementPlayer;

    private void Start()
    {
        Puzzle.SetActive(false);
        movementPlayer = GameObject.Find("Player").GetComponent<MovementPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("Player"))
        {
            InteractText.gameObject.SetActive(true);
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            InteractText.gameObject.SetActive(false);
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
                movementPlayer.FreezeMovement();

            }
        }

        if (isInPuzzle)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndInteraction();
            }
        }
    }

    public void playPuzzle()
    {
        Puzzle.SetActive(true);
        isInPuzzle = true;
        Debug.Log("Player entered the puzzle.");
    }

    public void EndInteraction()
    {
        Puzzle.SetActive(false);
        movementPlayer.UnfreezeMovement();
        isInPuzzle = false;
        Debug.Log("Player exited the puzzle.");
    }
}
