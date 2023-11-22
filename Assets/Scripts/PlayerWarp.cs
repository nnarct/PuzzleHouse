using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{

    private GameObject CurrentWarp;

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CurrentWarp != null)
                {
                    transform.position = CurrentWarp.GetComponent<StairFloor1>().GetDestination().position;
                }
            }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {
            CurrentWarp = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {
            if (collision.gameObject == CurrentWarp)
            {
                CurrentWarp = null;
            }
        }
    }
}
