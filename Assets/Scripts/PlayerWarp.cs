using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    private GameObject _currentWarp; // Reference to the current warp/stair the player is near

    // Update is called once per frame
    void Update()
    {       
        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if there is a current warp object
            if (_currentWarp != null)
            {
                // Move the player to the destination of the current warp
                transform.position = _currentWarp.GetComponent<StairFloor1>().GetDestination().position;
            }
        }
    }

    // Called when another Collider2D enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "Stair" tag
        if (collision.CompareTag("Stair"))
        {
            // Set the current warp object to the colliding stair object
            _currentWarp = collision.gameObject;
        }
    }

    // Called when another Collider2D exits the trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object has the "Stair" tag
        if (collision.CompareTag("Stair"))
        {
            // Check if the colliding stair object is the current warp object
            if (collision.gameObject == _currentWarp)
            {
                // Clear the current warp object since the player is no longer near it
                _currentWarp = null;
            }
        }
    }
}
