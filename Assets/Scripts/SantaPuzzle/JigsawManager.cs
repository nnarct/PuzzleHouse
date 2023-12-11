using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{
    public ScoreManager scoreManager; // Reference to the ScoreManager for handling puzzle completion
    public string PuzzleKey = "Santa"; // Unique identifier for this puzzle
    public List<JigsawPiece> jigsawPieces; // List of JigsawPiece components representing the puzzle pieces
    [SerializeField] GameObject GamePanel; // Reference to the game panel for displaying puzzle completion
    private List<Vector2> initialPositions; // List to store initial positions of the puzzle pieces

    private void Start()
    {
        // Start a coroutine to periodically check if all puzzle pieces are in correct positions
        StartCoroutine(CheckPiecesAfterDelay());
    }

    // Coroutine to check the puzzle pieces after a delay
    IEnumerator CheckPiecesAfterDelay()
    {
        bool allCorrect = false;

        // Continue checking until all pieces are in correct positions
        while (!allCorrect)
        {
            allCorrect = true;

            // Check each piece's correctness
            foreach (JigsawPiece piece in jigsawPieces)
            {
                if (!piece.IsPieceCorrect())
                {
                    allCorrect = false;
                }
            }

            // Add a delay before the next check
            yield return new WaitForSeconds(1f);
        }

        // When all pieces are in correct positions, call the Correct method
        Correct();
    }

    // Add a delay before the next check
    public void Correct()
    {
        scoreManager.HandleCorrectAnswer(PuzzleKey, GamePanel);
    }

    // Method to reset the positions of all puzzle pieces
    public void ResetPieces()
    {
        // Reset each piece's position to its initial position
        for (int i = 0; i < jigsawPieces.Count; i++)
        {
            jigsawPieces[i].ResetPosition();
        }
    }
}
