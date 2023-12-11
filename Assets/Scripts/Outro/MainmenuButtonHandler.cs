using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuButtonHandler : MonoBehaviour
{
    public GameObject MainmenuButton;

    // Attach this script to a GameObject manager, and assign the MainmenuButton in the Unity Editor.
    /*
     * To connect this method to the main menu button, select the GameObject manager in the Unity Editor,
     * find the OnClick event in the Inspector, click the '+' button, drag the GameObject manager to the Object field,
     * and choose "MainmenuButtonHandler" -> "GoToMainmenuScene" from the drop-down menu.
     */
    public void GoToMainmenuScene()
    {
        // Loads the main menu scene in the scene index 0
        SceneManager.LoadScene(0);
    }

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Animates the local position of MainmenuButton to (0f, -565f, 0f) over 0.7 seconds with a 0.5-second delay.
        // Uses easeInOutCubic for a smooth acceleration and deceleration.
        LeanTween.moveLocal(MainmenuButton, new Vector3(0f, -565f, 0f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInOutCubic);
    }
}
