using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudMusic : MonoBehaviour
{
    private static BackgroudMusic _backgroudMusic;

    private void Awake()
    {
        if (_backgroudMusic == null)
        {
            _backgroudMusic = this;
            DontDestroyOnLoad(_backgroudMusic);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
