using UnityEngine;

public class JigsawArea : MonoBehaviour
{
    public bool IsPiecePlaced { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if a puzzle piece entered the puzzle area
        if (other.CompareTag("JigsawPiece"))
        {
            IsPiecePlaced = true;
            // You can add more logic here if needed
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if a puzzle piece exited the puzzle area
        if (other.CompareTag("JigsawPiece"))
        {
            IsPiecePlaced = false;
            // You can add more logic here if needed
        }
    }
}
