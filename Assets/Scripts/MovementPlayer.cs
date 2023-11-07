using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    public float MoveX;
    private Rigidbody2D rb;
    public float speed;

    private bool isFrozen = false;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    private void Update()
    {
        if (!isFrozen && canMove)
        {
            MoveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(MoveX * speed, rb.velocity.y);
        }
    }

    public void FreezeMovement()
    {
        isFrozen = true;
        canMove = false;
    }

    public void UnfreezeMovement()
    {
        isFrozen = false;
        canMove= true;
    }
}
