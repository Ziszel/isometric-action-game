using System;
using UnityEngine;

public enum PlatformDirection
{
    left = 0,
    right = 1,
    forwards = 2,
    back = 3
}

public class MovingPlatform : MonoBehaviour
{
    // Serialize the moveSpeed and maxDistance as these will dependant on where the platform
    // is on certain levels
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private PlatformDirection dir; 

    // Store the dir as an enum so that we can extend this class to move the platform in different
    // directions if we require
    public Transform platformTransform;
    private Vector3 initialPosition;

    private void Awake()
    {
        // We need to specify where the platform starts
        initialPosition = platformTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (dir == PlatformDirection.left)
        {
            // move the platform to the left
            MovePlatform(moveSpeed);
        }
        else if (dir == PlatformDirection.right)
        {
            // move the platform to the right
            MovePlatform(-moveSpeed);
        }

        // if the platform is further than the max distance, change direction
        if (platformTransform.position.x > maxDistance)
        {
            dir = PlatformDirection.right;
        }

        // if the platform has reached its initial position then change direction
        else if (platformTransform.position.x < initialPosition.x)
        {
            dir = PlatformDirection.left;
        }
    }

    private void MovePlatform(float moveSpeed)
    {
        // move the platform by a unit vector * our moveSpeed to world space right
        // Because no physics are involved we MUST use Time.deltaTime
        platformTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player hits the platform, set its parent to the transform of this object (child of moving platform,
        // which automatically moves with its parent)
        // Made the 'empty' top level game object have the Box Collider instead as its not intended to pass collisions UP
        // the hierarchy (https://stackoverflow.com/questions/41926890/unity-how-to-detect-collision-on-a-child-object-from-the-parent-gameobject) 
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            // Make the Player a top level object once again
            other.collider.transform.SetParent(null);
        }
    }
}
