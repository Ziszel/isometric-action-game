using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/SerializeField.html
    // [SerializeField] allows Unity to serialize private fields
    // Serializing allows editing in the inspector among other things
    [Header("PlayerProperties")]
    [SerializeField]private float speed = 1000.0f;
    [SerializeField]private float rotationSpeed = 5.0f;
    [SerializeField]private float jumpForce = 500.0f;
    [SerializeField]private float fallMultiplier = 1.5f;
    [SerializeField]private float lowJumpMultiplier = 1.2f;
    [SerializeField]private float doubleJumpTimer;

    private bool _onGround = true;
    private int _jumpCount;
    private bool _jumpPressed;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        _jumpCount = 0; // set _jumpCount to 0 here just in-case player was mid-air at end of previous level.
        _jumpPressed = false;
        ResetJumpTimer();
    }

    private void Update()
    {
        //https://stackoverflow.com/questions/51958042/c-sharp-unity-getkeydown-not-being-detected-properly
        // GetKeyDown is detected every frame, therefore FixedUpdate (frame independent due to physics)
        // will not be detected correctly, and is thus called here.
        // HOWEVER, jumping is done through physics, so this is called first to capture input and then that is used
        // inside of FixedUpdate, phew!
        //HandleJumping();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _jumpPressed = true;
        }

    }

    // FixedUpdate() over Update as I am working with physics
    private void FixedUpdate()
    {
        float zMovement = Input.GetAxisRaw("Vertical") * speed;
        float hMovement = Input.GetAxisRaw("Horizontal") * speed;

        // if the player is trying to move (axis down), update the velocity on X and Z axis
        if (zMovement != 0 || hMovement != 0)
        {
            MovePlayer(-zMovement, -hMovement);
            RotatePlayer(-zMovement, -hMovement);
        }
    
        HandleJumping(_jumpPressed);
        _jumpPressed = false;
    }

    private void MovePlayer(float zMovement, float hMovement)
    {
        // I have set constraints on the X and Z axis to stop the player from falling over when moving forward
        // https://forum.unity.com/threads/character-falling-over-problem.160027/
        if (zMovement > 0)
        {
            // I have previously adjusted the rb.velocity directly by using rb.velocity = transform.forward / left * speed
            // This was resulting in issues moving relative to the camera setup in the scene.
            // To fix this, I now add a force directly to the players vector along either the z (vertical) or x(horizontal) axis
            // This keeps the rotation and movement independent of one another
            rb.AddForce(0.0f, 0.0f, speed); // move backwards
        }
        else if (zMovement < 0)
        {
            rb.AddForce(0.0f, 0.0f, -speed); // move forward
        }
        
        if (hMovement > 0)
        {
            rb.AddForce(speed, 0.0f, 0.0f); // move Right
        }
        else if (hMovement < 0)
        {
            rb.AddForce(-speed, 0.0f, 0.0f); // move Left
        }
    }

    private void RotatePlayer(float zMovement, float hMovement)
    {
        // Get the direction the player is moving in (using GetAxisRaw will always return a normalised value already)
        Vector3 movement = new Vector3(hMovement, 0.0f, zMovement);
        
        // spherical lerp from the current rotation to the angle the player is attempting to move to
        // slerp treats a vector like a direction rather than a position which is perfect for working with rotations
        // https://www.reddit.com/r/Unity3D/comments/6iskah/movetowards_vs_lerp_vs_slerp_vs_smoothdamp/
        // https://forum.unity.com/threads/how-quaternion-lookrotation-works.985800/
        // https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (movement), rotationSpeed * Time.deltaTime);
    }

    private void PlayerJump()
    {
        // https://docs.unity3d.com/ScriptReference/Vector3-up.html
        // rb.Addforce(Vector3.up * _jumpForce, ForceMode.Impulse) was my initial attempt for the jump. Whilst this jump
        // is physics accurate, it feels bad and so I searched for a better way to implement this
        // https://www.youtube.com/watch?v=7KiK0Aqtmzc
        if (_jumpCount < 2)
        {
            _onGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _jumpCount += 1;
            Debug.Log("jumping");
        }
        else
        {
            Debug.Log("max jumps reached");
            return;
        }
    }
    
    private void HandleJumping(bool _jumpPressed)
    {
        // Triggering inconsistently
        if (_jumpPressed)
        {
            // onGround is not the problem
            PlayerJump();
        }

        // If the player is falling, apply the multiplier to make them fall faster
        if (rb.velocity.y < 0 && !_onGround)
        {
            // The first code snippet below is in-efficient due to order of multiplication:
            // https://manuelotheo.com/on-optimization-order-of-multiplication-operations-is-inefficient/
            // rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1);
            // The extra parenthesis ensures the order is correct
            rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1));
        }

        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1));
        }

        if(!_onGround && doubleJumpTimer != 0)
        {
            doubleJumpTimer -= Time.deltaTime;
            if (doubleJumpTimer < 0) { doubleJumpTimer = 0.0f; }
        }
    }

    // called once per frame for each collider or RB that touches another
    // therefore if the player is not in the air or has hit a wall, set onGround to true allowing them to jump again
    private void OnCollisionStay(Collision collisionInfo)
    {
        _onGround = true;
        _jumpCount = 0;
    }
    
    // Used as supplemental to above, just in-case the player falls off a ledge instead of jumps make sure they can't
    // jump in air
    private void OnCollisionExit(Collision collisionInfo)
    {
        _onGround = false;
    }

    private void ResetJumpTimer()
    {
        doubleJumpTimer = 0.5f;
    }
}
