using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDragAndDrop : MonoBehaviour
{
    public GameObject CorrectForm; // Reference to the correct form GameObject

    [SerializeField] private AudioSource _source; // Reference to the AudioSource component
    
    [SerializeField] private AudioClip _pickUpClip; // AudioClip to play when the object is picked up

    private bool _isMoving; // Flag to check object is being moved

    private bool _isFinish; // Flag to check puzzle is in correct position

    private float _startPosX; // The starting position x of the object 

    private float _startPosY; // The starting position y of the object 

    private Vector3 _resetPosition; // The original position of the object

    // Start is called before the first frame update
    void Start()
    {
        // Save the original position of the object for resetting
        _resetPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the puzzle piece is not in the correct position
        if (_isFinish == false)
        {
            // Check if the object is currently being moved
            if (_isMoving)
            {
                // Get the mouse position and convert it to world coordinates
                Vector3 MousePos;
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                // Update the position of the object based on mouse movement
                this.gameObject.transform.localPosition = new Vector3(MousePos.x - _startPosX, MousePos.y - _startPosY, this.transform.localPosition.z);
            }
        }
        
    }

    // Called when the mouse button is pressed while over the collider
    private void OnMouseDown()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Play the pick-up sound
            _source.PlayOneShot(_pickUpClip);

            // Get the mouse position and convert it to world coordinates
            Vector3 MousePos;
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            // Calculate the offset from the current position
            _startPosX = MousePos.x - this.transform.localPosition.x;
            _startPosY = MousePos.y - this.transform.localPosition.y;

            // Set the flag to indicate that the object is being moved
            _isMoving = true;
        }
    }

    // Called when the mouse button is released
    private void OnMouseUp()
    {
        // Set the flag to indicate that the object is no longer being moved
        _isMoving = false;

        // Check if the object is close enough to the correct position
        if (Mathf.Abs(this.transform.localPosition.x - CorrectForm.transform.localPosition.x) <= 100 &&
            Mathf.Abs(this.transform.localPosition.y - CorrectForm.transform.localPosition.y) <= 100)
        {
            // Snap the object to the correct position
            this.transform.localPosition = new Vector3(CorrectForm.transform.localPosition.x, CorrectForm.transform.localPosition.y, CorrectForm.transform.localPosition.z);

            // Set the flag to indicate that the puzzle piece is in the correct position
            _isFinish = true;

            // Call the AddPoint method in the WinScript of the WoodenPuzzle GameObject
            GameObject.Find("WoodenPuzzle").GetComponent<WinScript>().AddPoint();
        }
        else
        {
            // If not in the correct position, reset the object to its original position
            this.transform.localPosition = new Vector3(_resetPosition.x, _resetPosition.y, _resetPosition.z);
        }
    }
}
