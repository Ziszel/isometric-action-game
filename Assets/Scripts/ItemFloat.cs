using System;
using UnityEngine;

public enum BottleDirection
{
    Up = 0,
    Down = 1,
}
public class ItemFloat : MonoBehaviour
{
    public Transform itemTransform;
    
    private BottleDirection bottleDir;
    [SerializeField]private float moveSpeed = 0.8f;
    [SerializeField]private float maxDistance = 0.8f;
    [SerializeField]private float yRotationValue = 35.0f;
    private Vector3 startingPosition;
    private Vector3 itemRotation;

    private void Awake()
    {
        startingPosition = itemTransform.position;
        bottleDir = BottleDirection.Up;
        itemRotation = new Vector3(0.0f, yRotationValue, 0.0f);
    }

    private void Update()
    {
        ItemSway();
        ItemRotate();
    }
    
    private void ItemSway()
    {
        if (bottleDir == BottleDirection.Up)
        {
            itemTransform.Translate((Vector3.up * moveSpeed) * Time.deltaTime);
        }
        else if (bottleDir == BottleDirection.Down)
        {
            itemTransform.Translate((Vector3.up * -moveSpeed) * Time.deltaTime);
        }

        // if the platform is further than the max distance, change direction
        if (itemTransform.position.y > startingPosition.y + maxDistance)
        {
            bottleDir = BottleDirection.Down;
        }
        // if the platform has reached its initial position then change direction
        else if (itemTransform.position.y < startingPosition.y - maxDistance)
        {
            bottleDir = BottleDirection.Up;
        }
    }

    private void ItemRotate()
    {
        // https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
        // RotateAround has been depreciated and 'Rotate()' is the method to use going forward.
        transform.Rotate(0.0f, (itemRotation.y * Time.deltaTime), 0.0f, Space.World);
        //itemTransform.rotation *= Quaternion.Euler(itemRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<LevelManager>().playerHasFlower = true;
        Destroy(gameObject);
    }
}
