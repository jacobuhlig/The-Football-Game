using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpponentController : MonoBehaviour
{
    public Rigidbody opponent_Rigidbody;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Icosphere")
        {
            print("Player died!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
