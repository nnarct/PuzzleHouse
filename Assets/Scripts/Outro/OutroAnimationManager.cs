using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutroAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]

    GameObject Cloud, Princess, Cat;

    void Start()
    {
        LeanTween.moveLocal(Cloud, new Vector3(0f, 1122f, 0f), 0f).setDelay(.5f).setEase(LeanTweenType.easeInOutQuint);
        LeanTween.moveLocal(Princess, new Vector3(749f, -307f, 0f), .5f).setDelay(.5f).setEase(LeanTweenType.linear);
        LeanTween.moveLocal(Cat, new Vector3(-800f, -452f, 0f), .8f).setDelay(.5f).setEase(LeanTweenType.linear);
        MoveForthAndBack();
    }

    private void MoveForthAndBack()
    { 
        // Move forth
        LeanTween.moveY(Cloud, 1100f, 2f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(MoveBack);
    }

    private void MoveBack()
    { 
        // Move back
        LeanTween.moveY(Cloud, 1122f, 2f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() => MoveForthAndBack());
    }

}
