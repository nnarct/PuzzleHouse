using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPiece : MonoBehaviour
{
    public Vector2 correctAnchoredPosition;

    public float snapThreshold = 20f; // Adjust as needed

    private Vector2 initialAnchoredPosition;

    private RectTransform rectTransform;

    private bool isCorrect = false;

    [SerializeField] private AudioSource _source;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialAnchoredPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        Vector2 currentAnchoredPosition = rectTransform.anchoredPosition;

        if (IsWithinSnapThreshold(currentAnchoredPosition, correctAnchoredPosition, snapThreshold))
        {
            SnapToCorrectPosition();
            isCorrect = true;
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
        rectTransform.anchoredPosition = correctAnchoredPosition;
    }

    public bool IsPieceCorrect()
    {
        return isCorrect;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = initialAnchoredPosition;
        isCorrect = false; // Reset correctness status
    }
}
