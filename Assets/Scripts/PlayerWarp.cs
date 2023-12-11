using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{

    private GameObject _currentWarp;

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentWarp != null)
                {
                    transform.position = _currentWarp.GetComponent<StairFloor1>().GetDestination().position;
                }
            }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {
           _currentWarp = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {
            if (collision.gameObject == _currentWarp)
            {
                _currentWarp = null;
            }
        }
    }
}
