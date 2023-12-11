using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    private float MoveX; // Horizontal input for movement

    private float Speed = 500; // Movement speed multiplier

    private Rigidbody2D _rb; // Rigidbody component for physics interactions

    private SpriteRenderer _sprite; // SpriteRenderer component for sprite manipulation

    private Animator _anim; // Animator component for character animations

    private bool _isFrozen = false; // Flag indicating if the player's movement is frozen
    
    private bool _isMoveable = true; // Flag indicating if the player is allowed to move

    // Called when the script starts
    private void Start()
    {
        // Get references to necessary components
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Called every frame
    private void Update()
    {
        // Check if the player is not frozen and is allowed to move
        if (!_isFrozen && _isMoveable)
        {
            // Get horizontal input for movement
            MoveX = Input.GetAxisRaw("Horizontal");

            // Set the player's velocity based on input and speed
            _rb.velocity = new Vector2(MoveX * Speed, _rb.velocity.y);
        }

        // Update character animations
        UpdateAnimationUpdate();
    }

    // Update character animations based on movement direction
    private void UpdateAnimationUpdate()
    {
        if (MoveX > 0f)
        {
            // Set "running" animation parameter to true when moving right
            _anim.SetBool("running", true);

            // Flip the sprite to face right
            _sprite.flipX = false;
        }
        else if (MoveX < 0f)
        {
            // Set "running" animation parameter to true when moving left
            _anim.SetBool("running", true);

            // Flip the sprite to face left
            _sprite.flipX = true;
        }
        else
        {
            // Set "running" animation parameter to false when not moving
            _anim.SetBool("running", false);
        }
    }

    // Method to freeze player movement
    public void FreezeMovement()
    {
        _isFrozen = true;
        _isMoveable = false;
    }

    // Method to unfreeze player movement
    public void UnfreezeMovement()
    {
        _isFrozen = false;
        _isMoveable = true;
    }
}
