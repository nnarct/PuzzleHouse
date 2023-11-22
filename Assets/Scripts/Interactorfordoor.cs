using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactorfordoor : MonoBehaviour
{

    [SerializeField] GameObject InteractText;
    public bool isInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            InteractText.gameObject.SetActive(true);
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            InteractText.gameObject.SetActive(false);
            isInRange = false;
        }
    }

}
