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
    public bool IsInRange;
    public bool IsInPuzzle;
    public string PuzzleKey;

    private MovementPlayer _movementPlayer;
    private Rigidbody2D _rigidBodyPlayer;
    private void Start()
    {
        Puzzle.SetActive(false);
        _movementPlayer = GameObject.Find("Player").GetComponent<MovementPlayer>();
        _rigidBodyPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0)
        {
             if (collision.gameObject.name.Equals("Player"))
             {
                 InteractText.gameObject.SetActive(true);
                 IsInRange = true;
             }
        }
       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            InteractText.gameObject.SetActive(false);
            IsInRange = false;
        }
    }

    private void Update()
    {
        if (IsInRange) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playPuzzle();
               _rigidBodyPlayer.velocity = Vector2.zero;
               _movementPlayer.FreezeMovement();
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

        IsInPuzzle = true;
        Debug.Log("Player entered the puzzle.");
    }

    public void EndInteraction()
    {
        Puzzle.SetActive(false);
        _movementPlayer.UnfreezeMovement();
        IsInPuzzle = false;
        Debug.Log("Player exited the puzzle.");
    }
}
