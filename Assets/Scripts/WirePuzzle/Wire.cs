using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour , IDragHandler , IBeginDragHandler , IEndDragHandler
{
    public bool IsLeftWire;

    public Color CustomColor;

    private Image _image;

    private LineRenderer _lineRenderer;

    private Canvas _canvas;

    private bool _isDragStarted = false;
    
    public bool IsCorrect = false;

    private WireTask _wireTask;

    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireTask = GetComponentInParent<WireTask>();
    }

    private void Update()
    {
        if (_isDragStarted)
        {
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
            //hide the line if not connect
            if (!IsCorrect)
            { 
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);

        if (isHovered) 
        {
            _wireTask.CurrentHoveredWire = this;
        }
    }

    public void SetColor(Color color)
    {
        _image.color = color;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        CustomColor = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Use for unity
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _source.Play();
        if (!IsLeftWire) { return; }
        //if it correct don't draw more line
        if (!IsCorrect)
        {
            _isDragStarted = true;
            _wireTask.CurrentDraggedWire = this;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _source.Play();
        if (_wireTask.CurrentHoveredWire != null) 
        {
            if (_wireTask.CurrentHoveredWire.CustomColor == CustomColor && !_wireTask.CurrentHoveredWire.IsLeftWire)
            {
                IsCorrect = true;
                _wireTask.CurrentHoveredWire.IsCorrect = true;
            }
        }

        _isDragStarted = false;
        _wireTask.CurrentDraggedWire = null;
    }
}
