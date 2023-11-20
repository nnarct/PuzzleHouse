using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform textRectTransform;
    private Vector2 originalTextPosition;

    void Start()
    {
        textRectTransform = GetComponent<Button>().GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>();
        // Save the original positions
        originalTextPosition = textRectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Adjust the positions when the button is hovered
        textRectTransform.anchoredPosition = originalTextPosition + new Vector2(0, -16f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the positions when the mouse exits the button
        textRectTransform.anchoredPosition = originalTextPosition;
    }

}