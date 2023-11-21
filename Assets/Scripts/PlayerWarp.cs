using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    [SerializeField] GameObject InteractText;
    [SerializeField] GameObject InteractText2;
    [SerializeField] GameObject InteractText3;
    [SerializeField] GameObject InteractText4;

    public bool IsInRange;

    private GameObject _currentWarp;


    void Update()
    {
        if (IsInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentWarp != null)
                {
                    transform.position = _currentWarp.GetComponent<StairFloor1>().GetDestination().position;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {
            _currentWarp = collision.gameObject;
            InteractText.gameObject.SetActive(true);
            IsInRange = true;
        }
        if (collision.CompareTag("Stair2"))
        {
            _currentWarp = collision.gameObject;
            InteractText2.gameObject.SetActive(true);
            IsInRange = true;
        }
        if (collision.CompareTag("Stair3"))
        {
            _currentWarp = collision.gameObject;
            InteractText3.gameObject.SetActive(true);
            IsInRange = true;
        }
        if (collision.CompareTag("Stair4"))
        {
            _currentWarp = collision.gameObject;
            InteractText4.gameObject.SetActive(true);
            IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stair"))
        {

            InteractText.gameObject.SetActive(false);
            if (collision.gameObject == _currentWarp)
            {
                IsInRange = false;
                _currentWarp = null;
            }
        }
        if (collision.CompareTag("Stair2"))
        {

            InteractText2.gameObject.SetActive(false);
            if (collision.gameObject == _currentWarp)
            {
                IsInRange = false;
                _currentWarp = null;
            }
        }
        if (collision.CompareTag("Stair3"))
        {

            InteractText3.gameObject.SetActive(false);
            if (collision.gameObject == _currentWarp)
            {
                IsInRange = false;
                _currentWarp = null;
            }
        }
        if (collision.CompareTag("Stair4"))
        {

            InteractText4.gameObject.SetActive(false);
            if (collision.gameObject == _currentWarp)
            {
                IsInRange = false;
                _currentWarp = null;
            }
        }
    }


}
