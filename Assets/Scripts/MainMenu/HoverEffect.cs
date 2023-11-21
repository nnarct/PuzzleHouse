using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _textRectTransform;
    private Vector2 _originalTextPosition;

    void Start()
    {
        _textRectTransform = GetComponent<Button>().GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>();
        // Save the original positions
        _originalTextPosition = _textRectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Adjust the positions when the button is hovered
        _textRectTransform.anchoredPosition = _originalTextPosition + new Vector2(0, -16f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the positions when the mouse exits the button
        _textRectTransform.anchoredPosition = _originalTextPosition;
    }

}