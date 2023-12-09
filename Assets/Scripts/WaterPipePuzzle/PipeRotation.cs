using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    private float[] RotationValue = { 0, 90, 180, 270 };

    public float[] CorrectRotation;
    
    [SerializeField]
    public bool isManyPossibleRots;

    int PossibleRots = 1;

    PipeManager pipeManager;

    [SerializeField] private AudioSource _source;

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
        pipeManager.CheckCorrect();

    }

    private void OnMouseDown()
    {
        _source.Play();

        if (!pipeManager.isRotating)
        {
            pipeManager.isRotating = true;
            transform.Rotate(new Vector3(0, 0, 90));
            Debug.Log("Pipe rotation: " + transform.rotation.z);

            pipeManager.CheckCorrect();

            pipeManager.isRotating = false;
        }


        
    }

}
