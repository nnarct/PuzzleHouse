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

    [SerializeField] private AudioSource _source;

    private MovementPlayer _movementPlayer;
    private Rigidbody2D _rigidBodyPlayer;
    private void Start()
    {
        Puzzle.SetActive(false);
        _movementPlayer = GameObject.FindWithTag("Player").GetComponent<MovementPlayer>();
        _rigidBodyPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0 || PuzzleKey.Length == 0)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                InteractText.gameObject.SetActive(true);
                IsInRange = true;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            InteractText.gameObject.SetActive(false);
            IsInRange = false;
        }
    }

    private void Update()
    {
        if (IsInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && InteractText.gameObject.activeSelf)
            {
                PlayPuzzle();
                _rigidBodyPlayer.velocity = Vector2.zero;
                _movementPlayer.FreezeMovement();
            }
        }

        if (IsInPuzzle)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndInteraction();
                _source.Stop();
            }
        }

    }

    public void PlayPuzzle()
    {
        _source.Play();
        Puzzle.SetActive(true);
        InteractText.gameObject.SetActive(false);
        IsInPuzzle = true;
        // Debug.Log("Player entered the puzzle.");
    }

    public void EndInteraction()
    {
        _source.Play();
        // Debug.Log("sound on.");

        Puzzle.SetActive(false);
        _movementPlayer.UnfreezeMovement();
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0)
        {
            InteractText.gameObject.SetActive(true);
        }
        IsInPuzzle = false;
        // Debug.Log("Player exited the puzzle.");
    }
}
