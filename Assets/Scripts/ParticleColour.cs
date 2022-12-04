using UnityEngine;

public class ParticleColour : MonoBehaviour
{
    public ParticleSystem ps;

    // does not has flower colours
    private float noFlowerR = 255.0f;
    private float noFlowerG = 0.0f;
    private float noFlowerB = 0.0f;
    private float noFlowerA = 255.0f;
    
    // Has flower colours
    private float hasFlowerR = 0.0f;
    private float hasFlowerG = 255.0f;
    private float hasFlowerB = 0.0f;
    private float hasFlowerA = 255.0f;
    
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<LevelManager>().playerHasFlower)
        {
            var main = ps.main;
            main.startColor = new Color(noFlowerR, noFlowerG, noFlowerB, noFlowerA);
        }
        else
        {
            var main = ps.main;
            main.startColor = new Color(hasFlowerR, hasFlowerG, hasFlowerB, hasFlowerA);
        }
        
    }
}
