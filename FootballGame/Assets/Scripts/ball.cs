using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody ball_Rigidbody;
    public TextMeshProUGUI textMeshPro;

    public AudioSource audioSource;
    public AudioClip goalClip;

    //Defining the goals parameters
    //Own goal
    private float _xLimitHome = 20.4f;
    private float _zStartHome = -2.96f;
    private float _zEndHome = 0.77f;

    //Opponent's goal
    private float _xLimitOut = -21.2f;
    private float _zStartOut = -3.8f;
    private float _zEndOut = 0.61f;

    //Horizontal bounds
    private float _zLeft = -14.3f;
    private float _zRight = 10.65f;

    //bounds
    private bool _OutOfBounds = false;

    void Start()
    {
        ball_Rigidbody.AddForce(-transform.forward * 2500f);
        audioSource.clip = goalClip;
    }

    void Update()
    {
        if (!_OutOfBounds)
        {
            CheckGoalOrOutOfBounds(_xLimitHome, _zStartHome, _zEndHome, "You scored! For the other team, idiot!", true);
            CheckGoalOrOutOfBounds(_xLimitOut, _zStartOut, _zEndOut, "You scored!", false);
            CheckOutOfBounds();
        }
    }

    void CheckGoalOrOutOfBounds(float xLimit, float zStart, float zEnd, string message, bool isHomeGoal)
    {
        if (isHomeGoal ? ball_Rigidbody.position.x > xLimit : ball_Rigidbody.position.x < xLimit)
        {
            if (ball_Rigidbody.position.z > zStart && ball_Rigidbody.position.z < zEnd)
            {
                print(message);
                textMeshPro.text = message;
                audioSource.Play();
                StartCoroutine(ResetSceneAfterDelay(3));
            }
            else
            {
                OutOfBounds();
            }
        }
    }

    void CheckOutOfBounds()
    {
        if (ball_Rigidbody.position.z < _zLeft || ball_Rigidbody.position.z > _zRight)
        {
            OutOfBounds();
        }
    }

    void OutOfBounds()
    {
        print("Ball out of bounds");
        textMeshPro.text = "Ball out of bounds";
        _OutOfBounds = true;
        StartCoroutine(ResetSceneAfterDelay(2));
    }

    IEnumerator ResetSceneAfterDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            print("Player in contact with ball!");
        }
    }
}
