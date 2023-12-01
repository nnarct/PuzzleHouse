using UnityEngine;

public class JigsawPiece : MonoBehaviour
{
    private bool isBeingDragged = false;
    private bool isPlacedCorrectly = false;
    private Vector3 startPosition;
    //private SantaManager jigsawPuzzle;

    private void Start()
    {
        // Get reference to the main puzzle script
        //jigsawPuzzle = FindObjectOfType<SantaManager>();
    }

    private void OnMouseDown()
    {
        if (!isPlacedCorrectly)
        {
            isBeingDragged = true;
            startPosition = transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (isBeingDragged)
        {
            // Move the puzzle piece with the mouse cursor
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    /*private void OnMouseUp()
    {
        isBeingDragged = false;

        // Check if the puzzle piece is over a puzzle area
        JigsawArea[] puzzleAreas = GameObject.FindObjectsOfType<JigsawArea>();
        bool isOverPuzzleArea = false;

        foreach (JigsawArea puzzleArea in puzzleAreas)
        {
            if (puzzleArea.IsPiecePlaced)
            {
                // Snap the piece to the center of the puzzle area
                transform.position = puzzleArea.transform.position;
                isOverPuzzleArea = true;
                // You can add more logic here if needed
                break; // Exit the loop after finding the first matching puzzle area
            }
        }

        // If the piece is not over any puzzle area, return it to the start position
        if (!isOverPuzzleArea)
        {
            transform.position = startPosition;
        }
    }
    private Vector3 GetCorrectPosition()
    {
        // You need to implement logic to determine the correct position for each piece
        // This can be based on the structure of your puzzle
        // For simplicity, this example returns the original starting position
        return startPosition;
    }*/

}
