using UnityEngine;

public enum Direction
{
    up = 0,
    down = 1,
    left = 2,
    right = 3
}

public class MovingPlatform : MonoBehaviour
{
    // Serialize the moveSpeed and maxDistance as these will dependant on where the platform
    // is on certain levels
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistance;

    // Store the dir as an enum so that we can extend this class to move the platform in different
    // directions if we require
    private Direction dir = Direction.left;
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
        if (dir == Direction.left)
        {
            // move the platform to the left
            MovePlatform(moveSpeed);
        }
        else if (dir == Direction.right)
        {
            // move the platform to the right
            MovePlatform(-moveSpeed);
        }

        // if the platform is further than the max distance, change direction
        if (platformTransform.position.x > maxDistance)
        {
            dir = Direction.right;
        }

        // if the platform has reached its initial position then change direction
        else if (platformTransform.position.x < initialPosition.x)
        {
            dir = Direction.left;
        }
    }

    private void MovePlatform(float moveSpeed)
    {
        // move the platform by a unit vector * our moveSpeed to world space right
        // Because no physics are involved we MUST use Time.deltaTime
        platformTransform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
