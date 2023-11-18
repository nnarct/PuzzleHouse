using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    public float MoveX;
    private Rigidbody2D rb;
    public float speed;
    private SpriteRenderer sprite;

    private Animator anim;

    private bool isFrozen = false;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (!isFrozen && canMove)
        {
            MoveX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(MoveX * speed, rb.velocity.y);
        }

        UpdateAnimationUpdate();

    }

    private void UpdateAnimationUpdate()
    {
        if (MoveX > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (MoveX < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
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
