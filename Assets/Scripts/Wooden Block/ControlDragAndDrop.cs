using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDragAndDrop : MonoBehaviour
{
    public GameObject CorrectForm;
    private bool _isMoving;
    private bool _isFinish;

    private float _startPosX;
    private float _startPosY;

    private Vector3 _resetPosition;

    void Start()
    {
        _resetPosition = this.transform.localPosition;
    }

    void Update()
    {
        if (_isFinish == false)
        {
            if (_isMoving)
            {
                Vector3 MousePos;
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                this.gameObject.transform.localPosition = new Vector3(MousePos.x - _startPosX, MousePos.y - _startPosY, this.transform.localPosition.z);
            }
        }
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos;
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            _startPosX = MousePos.x - this.transform.localPosition.x;
            _startPosY = MousePos.y - this.transform.localPosition.y;

            _isMoving = true;
        }
    }

    private void OnMouseUp()
    {
        _isMoving = false;

        if (Mathf.Abs(this.transform.localPosition.x - CorrectForm.transform.localPosition.x) <= 100 &&
            Mathf.Abs(this.transform.localPosition.y - CorrectForm.transform.localPosition.y) <= 100)
        {
            this.transform.localPosition = new Vector3(CorrectForm.transform.localPosition.x, CorrectForm.transform.localPosition.y, CorrectForm.transform.localPosition.z);
            _isFinish = true;

            GameObject.Find("WoodenPuzzle").GetComponent<WinScript>().AddPoint();
        }
        else
        {
            this.transform.localPosition = new Vector3(_resetPosition.x,_resetPosition.y,_resetPosition.z);
        }
    }
}
