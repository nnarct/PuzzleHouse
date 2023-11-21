using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;
    private Interactor _interactorScript;

    private void Start()
    { 
        _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    public void ButtonOrderPanelClose()
    {
        GamePanel.SetActive(false);
        _interactorScript.EndInteraction();
        Debug.Log("Exit button pressed. Calling EndInteraction.");
    }
}
