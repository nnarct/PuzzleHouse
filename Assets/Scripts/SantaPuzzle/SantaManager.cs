using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaManager : MonoBehaviour
{
    public List<GameObject> puzzlePieces; // List to store all puzzle pieces
    private int correctPlacementsRequired; // Number of correct placements required to consider the puzzle solved
    private int correctPlacementCount; // Counter to keep track of correct placements

    private void Start()
    {
        // Initialize puzzlePieces list with puzzle pieces
        // Add all puzzle pieces to the list manually or use GameObject.FindWithTag, GameObject.FindObjectsOfType, etc.
        // For this example, it assumes you've set up the puzzle pieces in the Inspector
        correctPlacementsRequired = puzzlePieces.Count;

        // Initialize correct placement count
        correctPlacementCount = 0;
    }

    // Called when a puzzle piece is correctly placed
    public void PiecePlacedCorrectly()
    {
        correctPlacementCount++;

        // Check if all pieces are placed correctly
        if (correctPlacementCount == correctPlacementsRequired)
        {
            PuzzleComplete();
        }
    }

    // Called when the puzzle is complete
    private void PuzzleComplete()
    {
        Debug.Log("Congratulations! Puzzle complete!");
    }
}
