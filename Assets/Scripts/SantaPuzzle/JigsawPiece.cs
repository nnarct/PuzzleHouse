using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPiece : MonoBehaviour
{
    public Vector2 correctAnchoredPosition;
    public float snapThreshold = 20f; // Adjust as needed

    private RectTransform rectTransform;
    private bool isCorrect = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 currentAnchoredPosition = rectTransform.anchoredPosition;

        //Debug.Log("Anchored Position: " + currentAnchoredPosition);

        if (IsWithinSnapThreshold(currentAnchoredPosition, correctAnchoredPosition, snapThreshold))
        {
            SnapToCorrectPosition();
            isCorrect = true;
            //Debug.Log("Piece correct");
        }
        else
        {
            isCorrect = false;
        }
    }

    bool IsWithinSnapThreshold(Vector2 pos1, Vector2 pos2, float threshold)
    {
        float deltaX = Mathf.Abs(pos1.x - pos2.x);
        float deltaY = Mathf.Abs(pos1.y - pos2.y);

        return deltaX < threshold && deltaY < threshold;
    }

    void SnapToCorrectPosition()
    {
        //Debug.Log("Snapping to Position: " + correctAnchoredPosition);

        rectTransform.anchoredPosition = correctAnchoredPosition;
        // Implement logic for snapping action
    }

    public bool IsPieceCorrect()
    {
        return isCorrect;
    }
}
