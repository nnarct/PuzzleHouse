using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    [SerializeField] GameObject InteractText;
    [SerializeField] GameObject InteractText2;
    [SerializeField] GameObject InteractText3;
    [SerializeField] GameObject InteractText4;

    private GameObject CurrentWarp;

    public bool isInRange;


    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CurrentWarp != null)
                {
                    transform.position = CurrentWarp.GetComponent<StairFloor1>().GetDestination().position;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {
            CurrentWarp = collision.gameObject;
            InteractText.gameObject.SetActive(true);
            isInRange = true;
        }
        if (collision.CompareTag("Stair2"))
        {
            CurrentWarp = collision.gameObject;
            InteractText2.gameObject.SetActive(true);
            isInRange = true;
        }
        if (collision.CompareTag("Stair3"))
        {
            CurrentWarp = collision.gameObject;
            InteractText3.gameObject.SetActive(true);
            isInRange = true;
        }
        if (collision.CompareTag("Stair4"))
        {
            CurrentWarp = collision.gameObject;
            InteractText4.gameObject.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {

            InteractText.gameObject.SetActive(false);
            if (collision.gameObject == CurrentWarp)
            {
                isInRange = false;
                CurrentWarp = null;
            }
        }
        if (collision.CompareTag("Stair2"))
        {

            InteractText2.gameObject.SetActive(false);
            if (collision.gameObject == CurrentWarp)
            {
                isInRange = false;
                CurrentWarp = null;
            }
        }
        if (collision.CompareTag("Stair3"))
        {

            InteractText3.gameObject.SetActive(false);
            if (collision.gameObject == CurrentWarp)
            {
                isInRange = false;
                CurrentWarp = null;
            }
        }
        if (collision.CompareTag("Stair4"))
        {

            InteractText4.gameObject.SetActive(false);
            if (collision.gameObject == CurrentWarp)
            {
                isInRange = false;
                CurrentWarp = null;
            }
        }
    }


}
