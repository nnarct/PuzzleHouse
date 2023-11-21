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
    public string puzzleKey;

    private MovementPlayer movementPlayer;
    private Rigidbody2D rigidBodyPlayer;
    private void Start()
    {
        Puzzle.SetActive(false);
        movementPlayer = GameObject.Find("Player").GetComponent<MovementPlayer>();
        rigidBodyPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt(puzzleKey, 0) == 0)
        {
             if (collision.gameObject.name.Equals("Player"))
             {
                 InteractText.gameObject.SetActive(true);
                 isInRange = true;
             }
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
                rigidBodyPlayer.velocity = Vector2.zero;
                movementPlayer.FreezeMovement();
            }
        }

      /*if (isInPuzzle)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndInteraction();
            }
        }*/

    }

    public void playPuzzle()
    {
        Puzzle.SetActive(true);
        InteractText.gameObject.SetActive(false);

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
