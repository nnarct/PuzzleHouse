using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Unity class that provides hover effects for a UI button.
// Implements interface handlers for pointer enter, exit, down, and up events.
public class HoverEffect : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
{
    public Sprite OriginalSprite; // Sprite to be used when the button is in its original state

    public Sprite HoverSprite; // Sprite to be used when the button is hovered over

    private RectTransform _textRectTransform; // RectTransform for the text within the button

    private Vector2 _originalTextPosition; // Original position of the text

    private bool _isDisable = false; // Flag to determine if the button is disable

    private bool _isClicked = false; // Flag to determine if the button was clicked (button state will be 'selected')

    private TMP_Text _buttonText; // TMP_Text component for the button text

    private Button _button; // Button component for this GameObject
    
    void Start()
    {
         // Check for missing sprites
        if (OriginalSprite == null || HoverSprite == null)
        {
            Debug.LogError("OriginalSprite or HoverSprite is null on " + gameObject.name);
        }

        // Get the Button component attached to this GameObject
        _button = GetComponent<Button>();

        // Get the TMP_Text component in the Button's children
        _buttonText = _button.GetComponentInChildren<TMP_Text>();

        // Get the RectTransform of the text
        _textRectTransform = _buttonText.GetComponent<RectTransform>();

        // Save the original positions
        _originalTextPosition = _textRectTransform.anchoredPosition;

        // Apply color changes if the button is initially disabled
        if (_isDisable)
        {
            _button.image.color = new Color(0.6f, 0.6f, 0.6f);
            _buttonText.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }


    void Update() 
    {
        // Check if the button is interactable and update the color accordingly
        if (_button.interactable == false)
        {
            // Set the flag to indicate that the button is disabled
            _isDisable = true;

            // Set the button image color for a disabled state
            _button.image.color = new Color(0.6f, 0.6f, 0.6f); 

            // Set the button text color for a disabled state
            _buttonText.color = new Color(0.6f, 0.6f, 0.6f);
        }
        else
        {
            // Set the flag to indicate that the button is not disabled
            _isDisable = false;

            // Set the button image color for an enabled state
            _button.image.color = new Color(255f, 255f, 255f);

             // Set the button text color for an enabled state
            _buttonText.color = new Color(255f, 255f, 255f);
        }
    }

    // Called when the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if the button is not disabled
        if (!_isDisable)
        {
            // Shift the text down and change the button sprite when the button is hovered over
            TextDown();
        }
    }

    // Handle the button press event when the pointer is unpressed
    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the button is not disabled
        if(!_isDisable)
        {
            // Reset the flag to indicate that the button is no longer clicked
            _isClicked = false;

            // Set the button image color to its original state
            _button.image.color = new Color(255f , 255f , 255f );

            // Set the button text color to its original state
            _buttonText.color = new Color(255f , 255f , 255f );

            // Invoke the method to restore the original text position and button sprite when the pointer is released
            TextUp();
        }
    }

    // Handle the button press event when the pointer is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the button is not disabled
        if (!_isDisable)
        {
            _isClicked = true; // Set the flag to indicate that the button is currently clicked

            // Set the button image color to a darker shade, indicating a pressed state
            _button.image.color = new Color(0.8f, 0.8f, 0.8f);

            // Set the button text color to a darker shade, indicating a pressed state
            _buttonText.color = new Color(0.8f, 0.8f, 0.8f);

            TextDown(); // Invoke the method to shift the text down and change the button sprite when the button is pressed
        }
    }

    // Handle the button exit event when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Check if the button is not disabled
        if (!_isDisable)
        {
            // Check if the button is not clicked (to avoid changing appearance on exit after a click)
            if (!_isClicked)
            {
                // Restore the button's appearance to its original state when the pointer exits
                _button.image.color = new Color(255f, 255f, 255f); // Set the button image color to its original state
                _buttonText.color = new Color(255f, 255f, 255f); // Set the button text color to its original state

                TextUp(); // Invoke the method to restore the original text position and button sprite when the pointer exits
            }
        }
    }

    // Shifts the button text down and changes the button sprite to indicate a hover or pressed state
    void TextDown()
    {
        _button.image.sprite = HoverSprite; // Set the button sprite to the hover state sprite
        _textRectTransform.anchoredPosition = _originalTextPosition + new Vector2(0, -16f); // Shift the text down
    }

    // Restores the button text position and changes the button sprite to its original state
    void TextUp()
    {
        _button.image.sprite = OriginalSprite; // Set the button sprite to the original state sprite
        _textRectTransform.anchoredPosition = _originalTextPosition; // Restore the text position
    }

}