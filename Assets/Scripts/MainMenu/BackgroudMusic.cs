using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroudMusic : MonoBehaviour
{
    private static BackgroudMusic backgroudMusic;

    private void Awake()
    {
        if (backgroudMusic == null)
        {
            backgroudMusic = this;
            DontDestroyOnLoad(backgroudMusic);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
