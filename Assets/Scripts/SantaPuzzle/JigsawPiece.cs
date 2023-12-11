using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPiece : MonoBehaviour
{
    public Vector2 correctAnchoredPosition; // Store the correct anchored position for the piece
    public float snapThreshold = 20f; // The threshold within which the piece snaps to the correct position
    private Vector2 initialAnchoredPosition; // Initial anchored position of the piece
    private RectTransform rectTransform; // Reference to the RectTransform component of the piece
    private bool isCorrect = false; // Flag indicating whether the piece is in the correct position
    [SerializeField] private AudioSource _source; // Flag indicating whether the piece is in the correct position

    void Start()
    {
        // Get the RectTransform component of the piece and store its initial position
        rectTransform = GetComponent<RectTransform>();
        initialAnchoredPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Get the current anchored position of the piece
        Vector2 currentAnchoredPosition = rectTransform.anchoredPosition;

        // Check if the piece is within the snap threshold of the correct position
        if (IsWithinSnapThreshold(currentAnchoredPosition, correctAnchoredPosition, snapThreshold))
        {
            // Snap the piece to the correct position if it's within the threshold
            SnapToCorrectPosition();

            // Mark the piece as correct
            isCorrect = true;
        }
        else
        {
            // Mark the piece as incorrect
            isCorrect = false;
        }
    }

    // Check if the piece is within the snap threshold of the correct position
    bool IsWithinSnapThreshold(Vector2 pos1, Vector2 pos2, float threshold)
    {
        float deltaX = Mathf.Abs(pos1.x - pos2.x);
        float deltaY = Mathf.Abs(pos1.y - pos2.y);

        return deltaX < threshold && deltaY < threshold;
    }

    // Snap the piece to the correct position
    void SnapToCorrectPosition()
    {
        rectTransform.anchoredPosition = correctAnchoredPosition;
    }

    // Check if the piece is in the correct position
    public bool IsPieceCorrect()
    {
        return isCorrect;
    }

    // Reset the position of the piece to its initial position
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = initialAnchoredPosition;

        // Reset the correctness status
        isCorrect = false;
    }
}
