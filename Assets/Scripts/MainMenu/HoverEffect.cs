using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private RectTransform _textRectTransform;
    private Vector2 _originalTextPosition;

    void Start()
    {
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();
        // Get the TMP_Text component in the Button's children
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        // Get the RectTransform of the text
        _textRectTransform = buttonText.GetComponent<RectTransform>();
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

    // public void OnPointerClick(PointerEventData evenData)
    //{
    // Reset the positions when the mouse click the button
    //_textRectTransform.anchoredPosition = _originalTextPosition;
    //}
}