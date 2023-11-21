using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    public float MoveX;
    public float Speed = 456;

    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _anim;

    private bool _isFrozen = false;
    private bool _isMoveable = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isFrozen && _isMoveable)
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
        _isMoveable = false;
    }

    public void UnfreezeMovement()
    {
        _isFrozen = false;
        _isMoveable = true;
    }
}
