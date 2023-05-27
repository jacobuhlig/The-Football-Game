# Models
https://www.cgtrader.com/items/2617987/download-page
https://www.cgtrader.com/items/4022877/download-page
https://www.mixamo.com/#/

# Articles
https://dotnetfiddle.net/UIqGOB
https://medium.com/codex/how-to-animate-and-move-a-simple-player-with-mixamo-and-unity-c-721470b54551
https://docs.unity3d.com/Manual/index.html

# Tools
https://imagetostl.com/convert/file/blend/to/fbx
https://www.sharetextures.com/assets

# Videos
https://www.youtube.com/watch?v=2_Hn5ZsUIXM&list=PL1eFi-QGU9EHvoUOhAg3JnUUZIn6mNqmr&index=2&t=164s
https://www.youtube.com/watch?v=76Lh0UApjNI&t=393s
https://www.youtube.com/watch?v=8Pk7FI629O8


# ChatGPT
## Me
I'm working on a project in Unity currently (3d project):
There are really five elements in the scene.

1. A plane (football field)
2. A character (player)
3. A sphere (football)
4. A camera
5. A light source

I'd like to position these through the transform component for each.
I'd like for the player to use wasd in the natural directions, when starting the scene (w=up the field, a=left on the field).
When moving the character up, the direction on the field, should be down toward one of the goals (so they're aligned properly).
The ball should be positioned in the middle of the field.
The camera should follow the ball at all times (this also if the starting position of the ball should change). The camera should look down on the field, and only move along the length of the field, never leaving the bounds of the side (unlike a regular football match where the match is filmed from the side with the goals on the right and left of the view, this should film on the vertical path from goal, in the direction of the other goal. It should be angled downward, but not looking down completely.

As I mentioned the ball should be the center of attention for the camera, however it should no necessarily center the ball at all times, but rather center the ball on the vertical plane, but not the horizontal, so as to allow the ball and player to go to either the left or the right side of the screen

The light source should be indicate bright day light in relation to its position.

Keep in mind that the values should be for both position, rotation and scale for each element.


## ChatGPT
To accomplish what you are asking for, we need to first understand the positioning of your game objects in the Unity scene. Note that the directions in Unity are: up/down is the Y axis, left/right is the X axis, and forward/back is the Z axis.

A plane (football field):

Position: (0,0,0)
Scale: This will depend on how large you want your football field to be. For this example, we will assume a scale of (1,1,1).
Rotation: (0,0,0)
A character (player):

Position: Depending on the field's size and assuming you want your player to start in the middle of one end of the field, you might set this to something like (0,1,-40). This will position the player just above the field (Y = 1) and towards one end of the field (Z = -40).
Rotation: To have the player face down the field, you should rotate them around the Y axis. The amount of rotation depends on the initial orientation of your player model, but let's assume the rotation should be (0,0,0).
Scale: This will depend on the size of your player model. Assuming the model is appropriately scaled, you might use (1,1,1).
A sphere (football):

Position: To position the ball in the middle of the field, you could use something like (0,1,0). This places it just above the field (Y = 1) and in the center (X = 0, Z = 0).
Rotation: (0,0,0)
Scale: This will depend on how large you want the football to be. Let's assume a scale of (1,1,1).
A camera:

Position: This will depend on how high above the field you want the camera to be. You might use something like (0,30,0) to place it high above the field, centered along the width of the field (X = 0) and in the middle of the field lengthwise (Z = 0).
Rotation: To point the camera down towards the field, you would rotate it around the X axis. A value of (-45,0,0) would make the camera look 45 degrees downward.
Scale: The scale of the camera doesn't affect its projection and is typically left as (1,1,1).
To have the camera follow the ball, you will need to write a script. You could create a new script in C#, add it to your camera, and use something like this:

csharp
Copy code
public GameObject ball; // Assign this in the inspector
private Vector3 offset; // The offset between the camera and the ball

void Start() {
    offset = transform.position - ball.transform.position;
}

void LateUpdate() {
    Vector3 newPosition = ball.transform.position + offset;
    newPosition.x = 0; // Ensure the camera does not move horizontally
    transform.position = newPosition;
}
This script keeps the camera a fixed distance above the ball and prevents it from moving horizontally.

A light source:
Position: This can be anywhere above the field, e.g. (0,50,0).
Rotation: To make the light shine down onto the field, rotate it around the X axis by -90 degrees, e.g. (-90,0,0).
Scale: The scale of the light source does not affect its illumination and is typically left as (1,1,1).
You should adjust the light's type...



## Me
This is my current file:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private static float _SPEED = 5f;

    private Animator _animator;
    private Rigidbody _rigidbody;

    private bool _running;
    private float _horizontalInput;
    private float _verticalInput;
    private float _xRange = 20;
    private float _zRange;

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
            _rigidbody.MovePosition(
                _rigidbody.position - transform.forward * _SPEED * Time.fixedDeltaTime);

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

        if (transform.position.x > _xRange)
        {
            transform.position = new Vector3(_xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -_xRange)
        {
            transform.position = new Vector3(-_xRange, transform.position.y, transform.position.z);
        }
    }
}

I didn't implement the mathf.clam yet, (don't know if I want to if I can get what I wrote initially to work). The problem right now, is that in the negative part of the x-axis (-20), I'm, still getting stuck, which I find odd to be honest...?


## ChatGPT
If you're getting stuck at the negative part of the x-axis, it may still be due to the conflict between Update() and FixedUpdate(). Even if you've moved the boundary checks into FixedUpdate(), FixedUpdate() can still run less frequently than Update(), which could still lead to the same problem.

Using Mathf.Clamp is recommended, as it eliminates the need for two separate if checks and can be more performant. However, if you wish to stick to your original approach, I would recommend you to manage the player movement and boundary checks all in the Update() method, which could solve the issue. Here's how you could modify your code:

```csharp
void Update()
{
    _horizontalInput = Input.GetAxis("Horizontal");
    _verticalInput = Input.GetAxis("Vertical");

    if (Mathf.Abs(_horizontalInput) > 0.01f || Mathf.Abs(_verticalInput) > 0.01f)
    {
        // move character
        transform.rotation = Quaternion.LookRotation(new Vector3(
            -_horizontalInput, 0f, _verticalInput));
        transform.position = transform.position - transform.forward * _SPEED * Time.deltaTime;

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

    if (transform.position.x > _xRange)
    {
        transform.position = new Vector3(_xRange, transform.position.y, transform.position.z);
    }

    if (transform.position.x < -_xRange)
    {
        transform.position = new Vector3(-_xRange, transform.position.y, transform.position.z);
    }
}
```

This should solve the getting stuck issue. However, using Update() for movement may lead to inconsistent movement speed on different machines, depending on the framerate. FixedUpdate() is generally recommended for consistent physics/movement calculations. If you continue to experience issues, I would suggest reconsidering the use of Mathf.Clamp(), as it is the most straightforward solution.