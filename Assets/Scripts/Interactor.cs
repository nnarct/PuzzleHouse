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

    [SerializeField] private AudioSource _source; // Serialized AudioSource variable
    private MovementPlayer _movementPlayer; // Reference to MovementPlayer script
    private Rigidbody2D _rigidBodyPlayer; // Reference to Rigidbody2D component of the player
    private void Start()
    {
        // Deactivate the Puzzle
        Puzzle.SetActive(false);

        // Find the player GameObject with the "Player" tag and get the MovementPlayer and Rigidbody2D components
        _movementPlayer = GameObject.FindWithTag("Player").GetComponent<MovementPlayer>();
        _rigidBodyPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the PuzzleKey is not set or if it's set to 0
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0 || PuzzleKey.Length == 0)
        {
            // Check if the collision is with the player GameObject
            if (collision.gameObject.tag.Equals("Player"))
            {
                // Activate the InteractText GameObject
                InteractText.gameObject.SetActive(true);

                // Set IsInRange to true
                IsInRange = true;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the collision is with the player GameObject
        if (collision.gameObject.tag.Equals("Player"))
        {
            // Deactivate the InteractText GameObject
            InteractText.gameObject.SetActive(false);

            // Set IsInRange to false
            IsInRange = false;
        }
    }

    private void Update()
    {
        // Check if the player is in range
        if (IsInRange)
        {
            // Check for the "E" key press and if the InteractText GameObject is active
            if (Input.GetKeyDown(KeyCode.E) && InteractText.gameObject.activeSelf)
            {
                // Call the playPuzzle method
                PlayPuzzle();

                // Set the player's velocity to zero
                _rigidBodyPlayer.velocity = Vector2.zero;

                // Freeze the player's movement
                _movementPlayer.FreezeMovement();
            }
        }

        // Check if the player is in the puzzle
        if (IsInPuzzle)
        {
            // Check for the "Escape" key press
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Call the EndInteraction method
                EndInteraction();

                // Stop the AudioSource
                _source.Stop();
            }
        }

    }

    public void PlayPuzzle()
    {
        // Play the AudioSource
        _source.Play();

        // Activate the Puzzle GameObject
        Puzzle.SetActive(true);

        // Deactivate the InteractText GameObject
        InteractText.gameObject.SetActive(false);

        // Set IsInPuzzle to true
        IsInPuzzle = true;
    }

    public void EndInteraction()
    {
        // Play the AudioSource
        _source.Play();

        // Deactivate the Puzzle GameObject
        Puzzle.SetActive(false);

        // Unfreeze the player's movement
        _movementPlayer.UnfreezeMovement();

        // Check if the PuzzleKey is not set to 0 and activate the InteractText GameObject
        if (PlayerPrefs.GetInt(PuzzleKey, 0) == 0)
        {
            // Activate the InteractText GameObject
            InteractText.gameObject.SetActive(true);
        }
        // Set IsInPuzzle to false
        IsInPuzzle = false;

    }
}
