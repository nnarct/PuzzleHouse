using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutroAnimationManager : MonoBehaviour
{
   
    // Serialized GameObjects representing elements in the outro animation
    [SerializeField] GameObject Cloud, Princess, Cat;

    // Start is called before the first frame update
    void Start()
    {
        // Animates the Cloud GameObject's local position from an initial hidden position to (0f, 2f, 0f) within the game screen.
        LeanTween.moveLocal(Cloud, new Vector3(0f, 2f, 0f), 0f).setDelay(.5f).setEase(LeanTweenType.easeInOutQuint);

        // Animates the Princess GameObject's local position from an initial hidden position to (749f, -307f, 0f) within the game screen.
        LeanTween.moveLocal(Princess, new Vector3(749f, -307f, 0f), .5f).setDelay(.5f).setEase(LeanTweenType.linear);

        // Animates the Cat GameObject's local position from an initial hidden position to (-800f, -452f, 0f) within the game screen.
        LeanTween.moveLocal(Cat, new Vector3(-800f, -452f, 0f), .8f).setDelay(.5f).setEase(LeanTweenType.linear);

        // Start continuous forth and back movement for Cloud
        MoveForthAndBack();
    }

    // Initiates a continuous forth-and-back movement for the Cloud GameObject
    private void MoveForthAndBack()
    { 
        // Move Cloud up and call MoveBack on completion
        LeanTween.moveY(Cloud, 2.5f, 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(MoveBack);
    }

    // Moves Cloud back down and calls MoveForthAndBack on completion
    private void MoveBack()
    { 
        // Move back
        LeanTween.moveY(Cloud, 2f, 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => MoveForthAndBack());
    }

}
