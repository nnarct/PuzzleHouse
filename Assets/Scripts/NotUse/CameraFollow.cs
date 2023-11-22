using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;

    void Update()
    {
        transform.position = new Vector3(
            Player.position.x + Offset.x,
            Player.position.y + Offset.y,
            Offset.z
        );
    }
}