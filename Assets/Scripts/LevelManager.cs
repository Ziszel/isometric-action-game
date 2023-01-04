using System.Collections;
using System.Collections.Generic;
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
    public Light LightSource;
    private float cycleTimer; // seconds
    private float cycleTime = 45.0f;
    private Vector3 sunRotation = Vector3.zero;
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
        
        cycle = Cycle.Day;
        playerHasFlower = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // taking a full day / night cycle to switch over, investigate!
        // set whether it's day or night based on the sun (directional light) rotation axis X
        if (LightSource.transform.rotation.x < 0.0f)
        {
            cycle = Cycle.Night;
        }
        else if (LightSource.transform.rotation.x > 0.0f)
        {
            cycle = Cycle.Day;
        }
        /* In here, use a FSM to manage the states of the scene: explore, in-battle, dead, etc...
        if (cycle == Cycle.Day)
        {
            do day things
        } */
        Debug.Log(cycle);
        
        RotateSun();

    }

    // public as will need to be called by Player when they die, and a collision zone trigger at end of level
    // will be split out into 'PlayerDead' & 'LevelComplete' methods eventually
    public void Reset()
    {
        playerHasFlower = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // https://vionixstudio.com/2022/04/25/creating-a-day-and-night-cycle-in-unity/
    // We can use the X axis of the light to change how it looks & to track it as a status
    public void RotateSun()
    {
        sunRotation.x = cycleTime * Time.deltaTime;
        LightSource.transform.Rotate(sunRotation, Space.World);
    }

}