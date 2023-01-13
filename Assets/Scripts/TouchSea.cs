using UnityEngine;

public class TouchSea : MonoBehaviour
{
    private LevelManager lm;

    private void Start()
    {
        lm = (LevelManager)FindObjectOfType(typeof(LevelManager));
    }
    private void OnCollisionEnter(Collision other)
    {
        lm.Reset();
    }
}
