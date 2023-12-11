using UnityEngine;

public class JigsawArea : MonoBehaviour
{
    public bool IsPiecePlaced { get; private set; } // Flag indicating whether a puzzle piece is placed in the puzzle area

    // Called when a puzzle piece enters the puzzle area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if a puzzle piece entered the puzzle area
        if (other.CompareTag("JigsawPiece"))
        {
            // Set the flag to indicate that a puzzle piece is placed
            IsPiecePlaced = true;
        }
    }

    // Called when a puzzle piece exits the puzzle area
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if a puzzle piece exited the puzzle area
        if (other.CompareTag("JigsawPiece"))
        {
            // Set the flag to indicate that a puzzle piece is not placed
            IsPiecePlaced = false;
        }
    }
}
