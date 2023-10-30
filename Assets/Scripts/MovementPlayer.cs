using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    public float MoveX;
    private Rigidbody2D rb;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    private void Update()
    {
        MoveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveX*speed, rb.velocity.y);
    }

    // Interaction
    [SerializeField] InputAction MOUSE;
    Vector2 mousePositionInput;
    Camera myCamera;
    [SerializeField] InputAction INTERACTION;
    [SerializeField] LayerMask interactLayer;

    private void Awake()
    {
        INTERACTION.performed += Interact;
    }

    private void OnEnable()
    {
        MOUSE.Enable();
        INTERACTION.Enable();
    }

    private void OnDisable()
    {
        MOUSE.Disable();
        INTERACTION.Disable();
    }

    void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            RaycastHit Hit;
            Ray ray = myCamera.ScreenPointToRay(mousePositionInput);
            if (Physics.Raycast(ray, out Hit, interactLayer))
            {
                if (Hit.transform.tag == "Interactable")
                {
                    if (!Hit.transform.GetChild(0).gameObject.activeInHierarchy)
                        return;
                    Interactor temp = Hit.transform.GetComponent<Interactor>();
                    temp.playPuzzle();
                }
            }
        }
    }
}
