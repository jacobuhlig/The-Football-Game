using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        Jogging,
        Idle,
        Ragdoll
    }

    private bool isJogging = false;

    private Rigidbody[] _ragdollRigidbodies;
    private PlayerState _currentState = PlayerState.Jogging;
    private Animator _animator;
    private CharacterController _characterController;

    void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        DisableRagdoll();
    }

    void Update()
    {
        switch (_currentState)
        {
            case PlayerState.Jogging:
                joggingBehavior();
                break;
            case PlayerState.Idle:
                IdleBehavior();
                break;
        }
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        _animator.enabled = true;
        _characterController.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        _animator.enabled = false;
        _characterController.enabled = false;
    }

    private void joggingBehavior()
    {
        // Determine the direction based on user input
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // If we have some input
        if (direction.magnitude >= 0.1f)
        {
            direction = direction.normalized;

            // Calculate the target angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Smoothly rotate towards the target
            Quaternion toRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime);

            // Move the character controller
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDirection.normalized * Time.deltaTime * 5f);

            // Set jogging animation
            _animator.SetBool("IsJogging", true);
        }
        else
        {
            // No input, stop jogging animation
            _animator.SetBool("IsJogging", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableRagdoll();
            _currentState = PlayerState.Ragdoll;
        }
    }

    private void IdleBehavior()
    {
        if (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s"))
        {
            _currentState = PlayerState.Jogging;
        }
    }
}
