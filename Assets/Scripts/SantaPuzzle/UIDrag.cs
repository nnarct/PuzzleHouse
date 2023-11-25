using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector3 startPosition;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        startPosition = rectTransform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        // Snap the piece to a grid or check if it's in the correct position
        // Implement logic to check if this piece fits with adjacent pieces
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPoint);
            rectTransform.position = worldPoint;
        }
    }
}
