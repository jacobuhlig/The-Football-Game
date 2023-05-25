using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float minZ, maxZ; // Set these values according to the width of your field

    void Update()
    {
        Vector3 newPosition = target.position + offset;

        // Clamp the X value within the bounds of the field
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
}
