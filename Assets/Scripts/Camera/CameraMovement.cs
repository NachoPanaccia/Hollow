using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            Follow();
        }
    }

    void Follow()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
