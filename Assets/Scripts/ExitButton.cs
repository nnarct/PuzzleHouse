using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] GameObject GamePanel; // Reference to the game panel in the Inspector
    private MovementPlayer _movementPlayer; // MovementPlayer script reference

    private void Start()
    {
        // Find and assign the MovementPlayer script in the scene
        _movementPlayer = FindObjectOfType<MovementPlayer>();
    }

    // Method to close the game panel and unfreeze the player's movement
    public void ClosePanel()
    {
        // Deactivate the game panel
        GamePanel.SetActive(false);

        // Unfreeze the movement of the player
        _movementPlayer.UnfreezeMovement();
    }
}
