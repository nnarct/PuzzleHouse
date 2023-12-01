using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairFloor1 : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private AudioSource _source;

    public Transform GetDestination()
    {
        _source.Play();
        return _destination;
    }

}
