using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            PlayerPrefs.SetInt("isMuted", 0);
            Load();
        }

        else
        {
            Load();
        }

        UpdateButtonIcon();
        AudioListener.pause = isMuted;
    }

    public void OnButtonPress()
    {
        if (isMuted == false)
        {
            isMuted = true;
            AudioListener.pause = true;
        }

        else
        {
            isMuted = false;
            AudioListener.pause = false;
        }

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (isMuted == false)
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
    private void Load()
    {
        isMuted = PlayerPrefs.GetInt("isMuted") == 1;
    }

    // if isMuted == true  save 1
    // if isMuted == false save 0
    private void Save()
    {
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }
    
}
