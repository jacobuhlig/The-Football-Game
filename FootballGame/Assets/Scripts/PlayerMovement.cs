using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody my_Rigidbody;
    void Start()
    {
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s"))
        {
            Vector3 my_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            my_Rigidbody.MovePosition(transform.position + my_Input * Time.deltaTime * 5f);
        }
    }
}
