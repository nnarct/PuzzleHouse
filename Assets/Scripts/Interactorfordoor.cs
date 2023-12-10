using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorForDoor : MonoBehaviour
{
    [SerializeField] GameObject InteractText;
    public bool IsInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            InteractText.gameObject.SetActive(true);
            IsInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            InteractText.gameObject.SetActive(false);
            IsInRange = false;
        }
    }
}
