using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Image soundOnIcon;

    [SerializeField] Image soundOffIcon;
    
    private bool _isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            PlayerPrefs.SetInt("isMuted", 0);
            _getSoundStatus();
        }

        else
        {
            _getSoundStatus();
        }

        _updateButtonIcon();
        AudioListener.pause = _isMuted;
    }

    public void OnButtonPress()
    {
        if (_isMuted == false)
        {
            _isMuted = true;
            AudioListener.pause = true;
        }

        else
        {
            _isMuted = false;
            AudioListener.pause = false;
        }

        _saveSoundStatus();
        _updateButtonIcon();
    }

    private void _updateButtonIcon()
    {
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

    // if isMuted == 1  set isMuted = true
    // if isMuted == 0  set isMuted = false
    private void _getSoundStatus()
    {
        _isMuted = PlayerPrefs.GetInt("isMuted") == 1;
    }

    // if isMuted == true  save 1
    // if isMuted == false save 0
    private void _saveSoundStatus()
    {
        PlayerPrefs.SetInt("isMuted", _isMuted ? 1 : 0);
    }
    
}
