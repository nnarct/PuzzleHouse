using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;
    // private Interactor _interactorScript;
    private MovementPlayer _movementPlayer;

    private void Start()
    {
        // _interactorScript = GameObject.FindWithTag("Interactable").GetComponent<Interactor>();
        _movementPlayer = GameObject.Find("Player").GetComponent<MovementPlayer>();

    }

        public void ClosePanel()
    {
        GamePanel.SetActive(false);
        _movementPlayer.UnfreezeMovement();
        //_interactorScript.EndInteraction();
        //Debug.Log("Exit button pressed. Calling EndInteraction.");
    }
}
