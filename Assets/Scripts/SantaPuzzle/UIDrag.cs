using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false; // Flag to determine if the UI element is being dragged
    private Vector3 startPosition; // Initial position of the UI element
    private RectTransform rectTransform; // Reference to the RectTransform component of the UI element
    [SerializeField] private AudioSource _source; // Audio source for drag sound feedback

    void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();
    }

    // Called when a pointer is pressed down on the UI element
    public void OnPointerDown(PointerEventData eventData)
    {
        // Play a sound when the UI element is pressed
        _source.Play();

        // Set dragging flag to true and store the initial position
        isDragging = true;
        startPosition = rectTransform.position;
    }

    // Called when a pointer is released from the UI element
    public void OnPointerUp(PointerEventData eventData)
    {
        // Set dragging flag to false
        isDragging = false;

        // Logic for snapping the piece to a grid or checking its position
        // This could involve checking if the piece fits with adjacent pieces
    }

    // Called while the UI element is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Convert screen point to world point within the RectTransform's space
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPoint);

            // Update the position of the UI element to the calculated world point
            rectTransform.position = worldPoint;
        }
    }
}
