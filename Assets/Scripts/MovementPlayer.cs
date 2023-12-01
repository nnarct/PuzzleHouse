using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private float MoveX;
    private float Speed = 500;

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
        else
        {
            _rb.velocity = Vector2.zero;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
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
