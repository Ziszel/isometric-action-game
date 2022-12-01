using UnityEngine;
using UnityEngine.SceneManagement;

// Cannot call class 'SceneManager' as that is a built-in Unity class
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // static ensures there can be only one
    
    private void Awake()
    {
        // If we already have an instance, we don't want another gameObject.
        // This stops duplication on reloading of the scene
        if (instance != null)
        {
            // We already have an instance so destroy the newly created one.
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // don't destroy the empty game object when loading a new scene
        
    }

    // Update is called once per frame
    private void Update()
    {
        // In here, use a FSM to manage the states of the scene: explore, in-battle, dead, etc...
        
    }

    // public as will need to be called by Player when they die, and a collision zone trigger at end of level
    // will be split out into 'PlayerDead' & 'LevelComplete' methods eventually
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
