using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuButtonHandler : MonoBehaviour
{
    public GameObject MainmenuButton;

    public void GoToMainmenuScene()
    {
        SceneManager.LoadScene(0);
    }

    void Awake()
    {
        LeanTween.moveLocal(MainmenuButton, new Vector3(0f, -565f, 0f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInOutCubic);
    }
}
