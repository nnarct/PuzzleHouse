using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDragAndDrop : MonoBehaviour
{
    public GameObject CorrectForm;
    private bool isMoving;
    private bool isFinish;

    private float StartPosX;
    private float StartPosY;

    private Vector3 ResetPosition;

    void Start()
    {
        ResetPosition = this.transform.localPosition;
    }

    void Update()
    {
        if (isFinish == false)
        {
            if (isMoving)
            {
                Vector3 MousePos;
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                this.gameObject.transform.localPosition = new Vector3(MousePos.x - StartPosX, MousePos.y - StartPosY, this.transform.localPosition.z);
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

            StartPosX = MousePos.x - this.transform.localPosition.x;
            StartPosY = MousePos.y - this.transform.localPosition.y;

            isMoving = true;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;

        if (Mathf.Abs(this.transform.localPosition.x - CorrectForm.transform.localPosition.x) <= 100 &&
            Mathf.Abs(this.transform.localPosition.y - CorrectForm.transform.localPosition.y) <= 100)
        {
            this.transform.localPosition = new Vector3(CorrectForm.transform.localPosition.x, CorrectForm.transform.localPosition.y, CorrectForm.transform.localPosition.z);
            isFinish = true;

            GameObject.Find("PointHandler").GetComponent<WinScript>().AddPoint();
        }
        else
        {
            this.transform.localPosition = new Vector3(ResetPosition.x,ResetPosition.y,ResetPosition.z);
        }
    }
}
