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
        LeanTween.moveLocal(Princess, new Vector3(749f, -307f, 0f), .7f).setDelay(.5f).setEase(LeanTweenType.linear);
        LoopCloud();
    }
    void LoopCloud()
    {
        // Create a sequence with the two movements
        LTSeq sequence = LeanTween.sequence();
        sequence.append(LeanTween.moveLocal(Cloud, new Vector3(0f, 370f, 0f), 1f).setEase(LeanTweenType.linear));
        sequence.append(LeanTween.moveLocal(Cloud, new Vector3(0f, 384f, 0f), 1f).setEase(LeanTweenType.linear));

        // Loop the sequence with a ping-pong effect
        //LeanTween.setLoopPingPong(sequence.id);
    }

// Update is called once per frame
void Update()
    {
        
    }
}
