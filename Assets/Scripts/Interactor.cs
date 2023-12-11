using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

    [SerializeField] GameObject InteractText; // Reference to the InteractText GameObject

    [SerializeField] GameObject Puzzle; // Reference to the Puzzle GameObject
    
    public bool IsInRange; // Boolean to check if the player is in range

    public bool IsInPuzzle; // Boolean to check if the player is in the puzzle

    public string PuzzleKey; // Key of this Puzzle

    [SerializeField] private AudioSource _source; // Serialized AudioSource variable

    private MovementPlayer _movementPlayer; // Reference to MovementPlayer script

    private Rigidbody2D _rigidBodyPlayer; // Reference to Rigidbody2D component of the player

    // Start is called before the first frame update
    private void Start()
    {
        // Deactivate the Puzzle
        Puzzle.SetActive(false);

        // Find the player GameObject with the "Player" tag and get the MovementPlayer and Rigidbody2D components
        _movementPlayer = GameObject.FindWithTag("Player").GetComponent<MovementPlayer>();
        _rigidBodyPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Method to activate the InteractText GameObject when the player is in range
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

    // Method to deactivate the InteractText GameObject when the player is out of range
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

    // Update is called once per frame
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

    // Method to run all the necessary code to start the puzzle
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

    // Method to run all the necessary code to end the puzzle
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
