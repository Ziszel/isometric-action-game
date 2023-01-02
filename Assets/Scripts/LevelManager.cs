using UnityEngine;
using UnityEngine.SceneManagement;

// every ten seconds change the state of the game
public enum Cycle {
    Day = 0,
    Night = 1
}

// Cannot call class 'SceneManager' as that is a built-in Unity class
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // static ensures there can be only one
    public bool playerHasFlower;
    public Cycle cycle;
    public float tenSeconds;
    private float timer; // seconds

    private void Awake()
    {
        // Cursor comes from UnityEngine
        // Make the mouse invisible and lock its position to the centre of the screen
        // https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        tenSeconds = 10.0f;
        timer = tenSeconds;
        cycle = Cycle.Day;
        playerHasFlower = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer <= 0.0f)
        {
            cycle = UpdateState(cycle);
            timer = tenSeconds;
        }
        
        timer -= Time.deltaTime;
        // In here, use a FSM to manage the states of the scene: explore, in-battle, dead, etc...

    }

    // public as will need to be called by Player when they die, and a collision zone trigger at end of level
    // will be split out into 'PlayerDead' & 'LevelComplete' methods eventually
    public void Reset()
    {
        playerHasFlower = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private Cycle UpdateState(Cycle cycle)
    {
        if (cycle == Cycle.Day)
        {
            cycle = Cycle.Night;
        }
        else if (cycle == Cycle.Night)
        {
            cycle = Cycle.Day;
        }

        return cycle;
    }
    
}
