using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;
    private Interactor interactorScript;

    private void Start()
    { 
        interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
    }

    public void ButtonOrderPanelClose()
    {
        GamePanel.SetActive(false);
        interactorScript.EndInteraction();
        Debug.Log("Exit button pressed. Calling EndInteraction.");
    }
}
