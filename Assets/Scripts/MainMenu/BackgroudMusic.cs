using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudMusic : MonoBehaviour
{
    // Static reference to the BackgroudMusic instance
    private static BackgroudMusic _backgroudMusic;

    // Awake is called before the Start method
    private void Awake()
    {
        // Check if there is no existing instance of BackgroudMusic
        if (_backgroudMusic == null)
        {
            // If no exists, set this as the instance and mark it to persist between scenes
            _backgroudMusic = this;
            DontDestroyOnLoad(_backgroudMusic);
        }
        else
        {
            // If already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}
