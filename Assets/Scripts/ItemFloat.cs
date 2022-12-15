using System;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    private void Update()
    {
        ItemSway();
    }
    
    private void ItemSway()
    {
        // do nothing
        // Make the bottle sway in the air
        // transform.Translate();
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<LevelManager>().playerHasFlower = true;
        Destroy(gameObject);
    }
}
