using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
{
    public Sprite OriginalSprite;
    public Sprite HoverSprite;
    private RectTransform _textRectTransform;
    private Vector2 _originalTextPosition;

    private bool _isDisable = false;
    private TMP_Text _buttonText;
    private Button _button;
    private bool _isClicked = false;
    void Start()
    {
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

        if (_isDisable)
        {
            _button.image.color = new Color(0.6f, 0.6f, 0.6f);
            _buttonText.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }
    void Update() {
        if(_button.interactable == false)
        {
            _isDisable = true;
            _button.image.color = new Color(0.6f, 0.6f, 0.6f);
            _buttonText.color = new Color(0.6f, 0.6f, 0.6f);
        }
        else
        {
            _isDisable = false;
            _button.image.color = new Color(255f , 255f , 255f );
            _buttonText.color = new Color(255f , 255f , 255f );
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isDisable)
        {
            TextDown();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_isDisable)
        {
            _isClicked = false;
            _button.image.color = new Color(255f , 255f , 255f );
            _buttonText.color = new Color(255f , 255f , 255f );
            TextUp();
        }
    }

    public void OnPointerDown(PointerEventData evenDate) {
        if(!_isDisable)
        {
            _isClicked = true;
            _button.image.color = new Color(0.8f, 0.8f, 0.8f);
            _buttonText.color = new Color(0.8f, 0.8f, 0.8f);
            TextDown();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_isDisable)
        {
            if(!_isClicked)
            {
                _button.image.color = new Color(255f , 255f , 255f );
                _buttonText.color = new Color(255f , 255f , 255f );
                TextUp();
            }
        }
        
    }

    void TextDown() {
        _button.image.sprite = HoverSprite;
        _textRectTransform.anchoredPosition = _originalTextPosition + new Vector2(0, -16f);
    }

    void TextUp() {
        _button.image.sprite = OriginalSprite;
        _textRectTransform.anchoredPosition = _originalTextPosition;
    }

}