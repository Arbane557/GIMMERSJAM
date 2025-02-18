using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (target.position.y > transform.position.y)
        {
            Vector3 newpos = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = newpos;
        }
    }
}
