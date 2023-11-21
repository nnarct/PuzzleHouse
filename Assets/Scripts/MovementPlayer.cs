using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    public float MoveX;

    private Rigidbody2D _rb;
    public float Speed;
    private SpriteRenderer _sprite;

    private Animator _anim;

    private bool _isFrozen = false;
    private bool _canMove = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (!_isFrozen && _canMove)
        {
            MoveX = Input.GetAxisRaw("Horizontal");
            _rb.velocity = new Vector2(MoveX * Speed, _rb.velocity.y);
        }

        UpdateAnimationUpdate();

    }

    private void UpdateAnimationUpdate()
    {
        if (MoveX > 0f)
        {
            _anim.SetBool("running", true);
            _sprite.flipX = false;
        }
        else if (MoveX < 0f)
        {
            _anim.SetBool("running", true);
            _sprite.flipX = true;
        }
        else
        {
            _anim.SetBool("running", false);
        }
    }

    public void FreezeMovement()
    {
        _isFrozen = true;
        _canMove = false;
    }

    public void UnfreezeMovement()
    {
        _isFrozen = false;
        _canMove= true;
    }
}
