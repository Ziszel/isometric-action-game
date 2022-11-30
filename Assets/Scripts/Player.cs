using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Transform wall;
    private float _speed = 1000;
    private float _rotationSpeed = 5;

    // FixedUpdate() over Update as I am working with physics
    void FixedUpdate()
    {
        float zMovement = Input.GetAxisRaw("Vertical") * _speed;
        float hMovement = Input.GetAxisRaw("Horizontal") * _speed;

        if (zMovement != 0 || hMovement != 0)
        {
            MovePlayer(zMovement, hMovement);
            RotatePlayer(zMovement, hMovement);
        }
    }

    void MovePlayer(float zMovement, float hMovement)
    {
        // I have set constraints on the X and Z axis to stop the player from falling over when moving forward
        // https://forum.unity.com/threads/character-falling-over-problem.160027/
        if (zMovement > 0)
        {
            // I have previously adjusted the rb.velocity directly by using rb.velocity = transform.forward / left * speed
            // This was resulting in issues moving relative to the camera setup in the scene.
            // To fix this, I now add a force directly to the players vector along either the z (vertical) or x(horizontal) axis
            // This keeps the rotation and movement independant of one another
            rb.AddForce(0.0f, 0.0f, _speed); // move backwards
        }
        else if (zMovement < 0)
        {
            rb.AddForce(0.0f, 0.0f, -_speed); // move forward
        }
        
        if (hMovement > 0)
        {
            rb.AddForce(_speed, 0.0f, 0.0f); // move Right
        }
        else if (hMovement < 0)
        {
            rb.AddForce(-_speed, 0.0f, 0.0f); // move Left
        }
    }

    void RotatePlayer(float zMovement, float hMovement)
    {
        // Get the direction the player is moving in (using GetAxisRaw will always return a normalised value already)
        Vector3 movement = new Vector3(hMovement, 0.0f, zMovement);
        
        // spherical lerp from the current rotation to the angle the player is attempting to move to
        // slerp treats a vector like a direction rather than a position which is perfect for working with rotations
        // https://www.reddit.com/r/Unity3D/comments/6iskah/movetowards_vs_lerp_vs_slerp_vs_smoothdamp/
        // https://forum.unity.com/threads/how-quaternion-lookrotation-works.985800/
        // https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (movement), _rotationSpeed * Time.deltaTime);
    }
}
