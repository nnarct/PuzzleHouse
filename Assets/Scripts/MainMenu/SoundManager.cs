using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Image soundOnIcon; // Reference to the sound on icon image

    [SerializeField] Image soundOffIcon; // Reference to the sound off icon image
    
    private bool _isMuted = false; // Variable to check sound is mute

    // Start is called before the first frame update
    void Start()
    {
        // Check if "isMuted" key is not present in PlayerPrefs
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            // Set "isMuted" to 0 (false) and get the sound status
            PlayerPrefs.SetInt("isMuted", 0);
            _getSoundStatus();
        }
        else
        {
            // Get the sound status from PlayerPrefs
            _getSoundStatus();
        }

        // Update the button icon based on the sound status and set the AudioListener accordingly
        _updateButtonIcon();
        AudioListener.pause = _isMuted;
    }

    // Method called when the sound button is pressed
    public void OnButtonPress()
    {
        // Toggle the sound status and update the AudioListener accordingly
        if (_isMuted == false)
        {
            // If sound is not muted, mute it
            _isMuted = true;

            // Pause the AudioListener
            AudioListener.pause = true;
        }
        else
        {
            // If sound is muted, unmute it
            _isMuted = false;

            // Unpause the AudioListener
            AudioListener.pause = false;
        }

        // Save the sound status and update the button icon
        _saveSoundStatus();
        _updateButtonIcon();
    }

    // Method to update the sound button icon based on the sound status
    private void _updateButtonIcon()
    {
        // Enable the appropriate icon based on the sound status
        if (_isMuted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    // Method to get sound status from PlayerPrefs
    private void _getSoundStatus()
    {
        // Get the sound status from PlayerPrefs
        _isMuted = PlayerPrefs.GetInt("isMuted") == 1;
    }

    // Method to save sound status
    // If isMuted == true, save 1; if isMuted == false, save 0
    private void _saveSoundStatus()
    {
        // Save the sound status to PlayerPrefs
        PlayerPrefs.SetInt("isMuted", _isMuted ? 1 : 0);
    }
    
}
