using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody ball_Rigidbody;
    void Start()
    {
        ball_Rigidbody.AddForce(-transform.forward * 2500f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cylinder")
        {
            print("You lose!");
        }
    }
}
