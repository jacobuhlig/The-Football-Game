using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float _speed;

    private Animator _animator;
    public Rigidbody _rigidbody;

    private bool _running;
    private float _horizontalInput;
    private float _verticalInput;

    //Restricting the confines of the playable area
    private float _xStart = 20;
    private float _xEnd = -21;
    private float _zStart = 10;
    private float _zEnd = -14;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _running = false;
    }


    void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(_horizontalInput) > 0.01f || Mathf.Abs(_verticalInput) > 0.01f)
        {
            // move character
            transform.rotation = Quaternion.LookRotation(new Vector3(
                -_horizontalInput, 0f, _verticalInput));

            Vector3 newPostion = _rigidbody.position - transform.forward * _speed * Time.fixedDeltaTime;
            newPostion.x = Mathf.Clamp(newPostion.x, _xEnd, _xStart);
            newPostion.z = Mathf.Clamp(newPostion.z, _zEnd, _zStart);
            _rigidbody.MovePosition(newPostion);

            // change animation
            if (!_running)
            {
                _running = true;
                _animator.SetBool("Running", true);
            }
        }
        else if (_running)
        {
            _running = false;
            _animator.SetBool("Running", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            print("You died!");
        }
    }

}
