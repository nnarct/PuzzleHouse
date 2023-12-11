using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour 
{
    // Reference to the button triggering the scene change
    public Button BackButton;

    void Start()
    {
        // Attach the GoBack method to the button's click event
        BackButton.onClick.AddListener(GoBack);
    }

    // Method to go back to the previous scene
    /*
     * This method is intended to be associated with a button click event in the Unity Inspector.
     * To use it, drag the GameObject with this script onto the button's OnClick event,
     * and select this method ("GoBack") from the dropdown.
     */
    public void GoBack()
    {
        // Load the scene with an index one less than the current scene index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
