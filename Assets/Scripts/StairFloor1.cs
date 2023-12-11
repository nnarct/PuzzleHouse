using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairFloor1 : MonoBehaviour
{
    [SerializeField] private Transform _destination; // Reference to the destination 
    
    [SerializeField] private AudioSource _source; // Reference to the AudioSource component

    // Method to get the destination transform and play the AudioSource
    public Transform GetDestination()
    {
        _source.Play();
        return _destination;
    }

}
