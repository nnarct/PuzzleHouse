using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairFloor1 : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public Transform GetDestination()
    {
        return destination;
    }

}
