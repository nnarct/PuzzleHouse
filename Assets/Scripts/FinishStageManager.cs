using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FinishStageAnimation : MonoBehaviour
{

    [SerializeField]

    GameObject PanelBackground, EndGamePanel, Star, UiText, NextButton, ReplayButton, LightWheel;

    private Image _lightWheelImage;
    // Start is called before the first frame update
    void Awake()
    {
        _lightWheelImage = LightWheel.GetComponent<Image>();
        StartCoroutine(RandomColorChange());
        LeanTween.rotateAround(LightWheel, Vector3.forward, -360, 10f).setLoopClamp();
        LeanTween.scale(PanelBackground, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Star, new Vector3(2f, 2f, 2f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(ShowText);
        LeanTween.scale(LightWheel, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(Star, new Vector3(0f, 240f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.moveLocal(LightWheel, new Vector3(0f, 240f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.scale(Star, new Vector3(.8f, .8f, .8f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(LightWheel, new Vector3(.7f, .7f, .7f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
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

    IEnumerator RandomColorChange()
    {
        while (true) // Infinite loop for continuous color changes
        {
            int randomIndex = Random.Range(0, 7);

            // Define the seven possible colors
            Color[] possibleColors =
            {
                new Color(1, 0, Random.value),
                new Color(0, 1, Random.value),
                new Color(Random.value, 0, 1),
                new Color(Random.value, 1, 0),
                new Color(0, Random.value, 1),
                new Color(1, Random.value, 0),
                Color.white // (1, 1, 1)
            };

            // Assign the color based on the random index
            _lightWheelImage.color = possibleColors[randomIndex];

            // Wait for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
        }
    }


}
