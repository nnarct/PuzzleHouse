sddasfdfdfsad{name}sdfasdfhasdf
Adfasdfdsfdsfdsfasdfdsfadsfdasfsdfasdfsd

Thank you, Princess. It was an honor to help. I hope our paths cross again.


PC:
Thank you, noble[Player's Title]! Your wit and cunning have lifted the curse that bound me.
/*The princess approaches the player, gratitude in her eyes.*/
Your determination and intelligence have proven invaluable.I am forever grateful.

PY:
Your Highness, the honor was mine. I am grateful to have been of service. May our paths cross again in the future.

PC:
Your kindness and bravery will be remembered in the annals of our kingdom. Should you ever need aid, our doors will be open to you.

PY:
Thank you, Princess. It was an honor to help. I hope our paths cross again.

PC:
And in the future, I hope our paths cross again. Please, grace our kingdom with your presence whenever you can.

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
