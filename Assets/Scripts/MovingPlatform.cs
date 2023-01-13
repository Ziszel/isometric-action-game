using System;
using UnityEngine;

public enum PlatformDirection
{
    left = 0,
    right = 1,
    forward = 2,
    back = 3
}

public class MovingPlatform : MonoBehaviour
{
    // Serialize the moveSpeed and maxDistance as these will dependant on where the platform
    // is on certain levels
    [Header("Platform movement controls")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistanceValue;
    [SerializeField] private PlatformDirection dir; 

    // Store the dir as an enum so that we can extend this class to move the platform in different
    // directions if we require
    public Transform platformTransform;
    private Vector3 initialPosition;
    private Vector3 maxDistance;

    private void Awake()
    {
        // We need to specify where the platform starts
        initialPosition = platformTransform.position;
        maxDistance = initialPosition + new Vector3(maxDistanceValue, maxDistanceValue, maxDistanceValue);
    }

    // Update is called once per frame
    private void Update()
    {
        if (dir == PlatformDirection.left)
        {
            platformTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.x > maxDistance.x) { dir = PlatformDirection.right; }
        }
        else if (dir == PlatformDirection.right)
        {
            platformTransform.Translate(Vector3.right * -moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.x < initialPosition.x) { dir = PlatformDirection.left; }
        }

        if (dir == PlatformDirection.forward)
        {
            platformTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.z > maxDistance.z) { dir = PlatformDirection.back; }
        }
        else if (dir == PlatformDirection.back)
        {
            platformTransform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.z < initialPosition.z) { dir = PlatformDirection.forward; }
        }
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
