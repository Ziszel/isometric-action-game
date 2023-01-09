using System;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<LevelManager>().playerHasFlower)
        {
            FindObjectOfType<LevelManager>().CompleteLevel();
        }
    }
}
