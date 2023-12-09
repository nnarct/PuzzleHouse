using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStageAnimation : MonoBehaviour
{

    [SerializeField]

    GameObject EndGamePanel, Star, UiText, NextButton, ReplayButton, Sparkling;
    // Start is called before the first frame update
    void Awake()
    {
        LeanTween.rotateAround(Sparkling, Vector3.forward, -360, 10f).setLoopClamp();
        LeanTween.scale(Star, new Vector3(2f, 2f, 2f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(ShowText);
        LeanTween.scale(Sparkling, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(Star, new Vector3(0f, 240f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.moveLocal(Sparkling, new Vector3(0f, 240f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(Star, new Vector3(.8f, .8f, .8f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Sparkling, new Vector3(.7f, .7f, .7f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }

    void ShowText()
    {
        LeanTween.scale(UiText, new Vector3(1.7f, 1.7f, 1.7f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(UiText, new Vector3(0f, -118f, 0f), .7f).setDelay(1.6f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.scale(ReplayButton, new Vector3(1f, 1f, 1f), 2f).setDelay(1.9f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(NextButton, new Vector3(1f, 1f, 1f), 2f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(UiText, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }


    public void GoToNextScene()
    {
        EndGamePanel.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }


}
