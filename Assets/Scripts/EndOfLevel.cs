using System;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.Log("you are not the player");  
        }
        else if (FindObjectOfType<LevelManager>().playerHasFlower)
        {
            Debug.Log("you have won");
            FindObjectOfType<LevelManager>().Reset();
        }
    }
}
