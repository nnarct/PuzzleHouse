using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool IsLeftWire; // Indicates if this wire is on the left side
    public bool IsCorrect = false; // Indicates if the wire is in the correct position
    private bool _isDragStarted = false; // Flag to check if dragging has started
    public Color CustomColor; // Custom color for the wire
    private Image _image; // Reference to the Image component
    private LineRenderer _lineRenderer; // Reference to the LineRenderer component
    private Canvas _canvas; // Reference to the Canvas
    private WireTask _wireTask; // Reference to the WireTask script
    [SerializeField] private AudioSource _source; // AudioSource for sound feedback


    private void Awake()
    {
        // Get necessary component references
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireTask = GetComponentInParent<WireTask>();
    }

    private void Update()
    {
        if (_isDragStarted)
        {
            // Calculate the position of the wire during dragging
            Vector2 movePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePosition);

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePosition));

        }
        else
        {
            // Hide the line if the wire is not connect in the correct position
            if (!IsCorrect)
            {
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }

        // Check if the wire is being hovered over
        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);

        if (isHovered)
        {
            _wireTask.CurrentHoveredWire = this;
        }
    }

    // Set the color of the wire
    public void SetColor(Color color)
    {
        _image.color = color;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        CustomColor = color;
    }

    // Unity's OnDrag method implementation
    public void OnDrag(PointerEventData eventData)
    {
        // This method is required by the interface but not used
    }

    // Event when dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Play audio feedback
        _source.Play();

        //check that is it the wires on the left side
        if (!IsLeftWire)
        {
            return;
        }

        // If the wire is not connect in the correct position, allow dragging
        if (!IsCorrect)
        {
            // Set the drag started flag
            _isDragStarted = true;
            _wireTask.CurrentDraggedWire = this;
        }
    }

    // Event when dragging ends
    public void OnEndDrag(PointerEventData eventData)
    {
        // Play audio feedback
        _source.Play();

        //check if the hovered wire is not null
        if (_wireTask.CurrentHoveredWire != null)
        {
            // Check if the hovered wire matches the color and is on the right side
            if (_wireTask.CurrentHoveredWire.CustomColor == CustomColor && !_wireTask.CurrentHoveredWire.IsLeftWire)
            {
                // Set this wire as correct
                IsCorrect = true;

                // Set the hovered wire as correct
                _wireTask.CurrentHoveredWire.IsCorrect = true;
            }
        }

        // Reset the drag started flag
        _isDragStarted = false;

        // Reset the current dragged wire
        _wireTask.CurrentDraggedWire = null;
    }
}
