using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    
    [SerializeField] public bool isManyPossibleRots; // flag to indicate if the pipe has many possible rotations

    public float[] CorrectRotation; // Array of correct rotations for the pipe
    
    [SerializeField] private AudioSource _source; // Audio source for the pipe rotation sound
    
    private PipeManager pipeManager; // Reference to the PipeManager script
    
    private int PossibleRots = 1; // Number of possible rotations for the pipe

    private float[] RotationValue = { 0, 90, 180, 270 }; // Array of possible rotation values for the pipe

    // Start is called before the first frame update
    void Start()
    {
        // Set the number of possible rotations based on the CorrectRotation array length
        PossibleRots = CorrectRotation.Length;

        // Randomly set the initial rotation of the pipe
        int rotationIndex = Random.Range(0, RotationValue.Length);
        transform.eulerAngles = new Vector3(0, 0, RotationValue[rotationIndex]);

        // Check correct rotation based on the initial rotation
        pipeManager.CheckCorrect();
    }

    // Awake is called when the script instance is being loaded
    // Get component PipeManager from GameObject  
    void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();

        if (pipeManager == null)
        {
            Debug.LogError("PipeManager not found or PipeManager component missing.");
        }
    }

    // Method to called when the mouse button is pressed
    private void OnMouseDown()
    {
        // play the sound
        _source.Play();

        // Check if the pipe is not currently rotating in the PipeManager
        if (!pipeManager.isRotating)
        {
            // Set the flag to indicate that the pipe is rotating
            pipeManager.isRotating = true;

            // Rotate the pipe by 90 degrees when clicked and check correct position
            transform.Rotate(new Vector3(0, 0, 90));
            pipeManager.CheckCorrect();

            // Set the flag to indicate that the pipe has finished rotating
            pipeManager.isRotating = false;
        }  
    }
}
