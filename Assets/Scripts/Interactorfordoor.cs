using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorForDoor : MonoBehaviour
{
    [SerializeField] GameObject InteractText; // Serialized GameObjects reference to the UI text for interaction 
    
    public bool IsInRange; // Flag indicating whether the player is in the interaction range

    // Called when another Collider2D enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.gameObject.tag.Equals("Player"))
        {
            // Activate the interaction text and set IsInRange to true
            InteractText.gameObject.SetActive(true);
            IsInRange = true;
        }

    }

    // Called when another Collider2D exits the trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.gameObject.tag.Equals("Player"))
        {
            // Deactivate the interaction text and set IsInRange to false
            InteractText.gameObject.SetActive(false);
            IsInRange = false;
        }
    }
}
