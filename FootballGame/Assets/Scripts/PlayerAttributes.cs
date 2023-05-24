using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public Rigidbody my_Rigidbody;

    void Start()
    {
        my_Rigidbody = GetComponent<Rigidbody>();

        // To prevent the player from falling over
        my_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Making player's mass much higher than the ball (you may need to adjust this value)
        my_Rigidbody.mass = 100f;
    }

    
    void Update()
    {
        
    }
}
