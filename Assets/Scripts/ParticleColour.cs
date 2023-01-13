using UnityEngine;

public class ParticleColour : MonoBehaviour
{
    // this script will be attached to the flower (end of level).
    // the particle will change colour depending on whether or not the LevelManager
    // variable 'playerHasFlower' is true;
    public ParticleSystem ps;

    // end level particle colours when the player does not have the flower.
    private float _noFlowerR = 255.0f;
    private float _noFlowerG = 0.0f;
    private float _noFlowerB = 0.0f;
    private float _noFlowerA = 255.0f;
    
    // end level particle colours when the player DOES have the flower.
    private float _hasFlowerR = 0.0f;
    private float _hasFlowerG = 255.0f;
    private float _hasFlowerB = 0.0f;
    private float _hasFlowerA = 255.0f;
    
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<LevelManager>().playerHasFlower)
        {
            var main = ps.main;
            main.startColor = new Color(_noFlowerR, _noFlowerG, _noFlowerB, _noFlowerA);
        }
        else
        {
            var main = ps.main;
            main.startColor = new Color(_hasFlowerR, _hasFlowerG, _hasFlowerB, _hasFlowerA);
        }
        
    }
}
