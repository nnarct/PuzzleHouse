using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    float[] RotationValue = { 0, 90, 180, 270 };


    public float[] CorrectRotation;
    
    [SerializeField]
    bool isPlace = false;

    int PossibleRots = 1;

    PipeManager pipeManager;

    private void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();

        if (pipeManager == null)
        {
            Debug.LogError("PipeManager not found or PipeManager component missing.");
        }
    }
    private void Start()
    {
    

        PossibleRots = CorrectRotation.Length;
        int RotationIndex = Random.Range(0, RotationValue.Length);
        transform.eulerAngles = new Vector3(0, 0, RotationValue[RotationIndex]);

        if(PossibleRots > 1)
        {
            if (transform.eulerAngles.z == CorrectRotation[0] || transform.eulerAngles.z == CorrectRotation[1])
            {
                isPlace = true;
                pipeManager.CorrectMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == CorrectRotation[0])
            {
                isPlace = true;
                pipeManager.CorrectMove();
            }
        }
        
        
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        if (PossibleRots > 1)
        {
            if (transform.eulerAngles.z == CorrectRotation[0] || transform.eulerAngles.z == CorrectRotation[1] && isPlace == false)
            {
                isPlace = true;
                pipeManager.CorrectMove();
            }
            else if (isPlace == true)
            {
                isPlace = false;
                pipeManager.WrongMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == CorrectRotation[0] && isPlace == false)
            {
                isPlace = true;
                pipeManager.CorrectMove();
            }
            else if (isPlace == true)
            {
                isPlace = false;
                pipeManager.WrongMove();
            }
        }

        
    }
}
