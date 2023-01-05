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
    private Cycle cycle;
    public Light LightSource;
    private float cycleTimer; // seconds
    private float lengthOfDay;
    private float visualCycleTime = 10.0f;
    private float sunRotationX;
    private Color lightColour;
    private Color lightestColour;
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
        
        lightestColour = new Color32(190, 190, 190, 1);
        sunRotationX = 90.0f;
        cycle = Cycle.Day;
        playerHasFlower = false;
        lengthOfDay = 10.0f;
        visualCycleTime = 20.0f;
        cycleTimer = 10.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // taking a full day / night cycle to switch over, investigate!
        // set whether it's day or night based on the sun (directional light) rotation axis X
        if (cycleTimer <= 0.0f)
        {
            if (cycle == Cycle.Day)
            {
                cycle = Cycle.Night;
            }
            else
            {
                cycle = Cycle.Day;
            }
            cycleTimer = lengthOfDay;
        }

        /* In here, use a FSM to manage the states of the scene: explore, in-battle, dead, etc...
        if (cycle == Cycle.Day)
        {
            do day things
        } */
        Debug.Log(cycle);
        
        RotateSun();
        //TintSkyBox();
        cycleTimer -= Time.deltaTime;

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
        sunRotationX = visualCycleTime * Time.deltaTime;
        LightSource.transform.Rotate(new Vector3(sunRotationX, 0.0f, 0.0f), Space.World);
    }

    private void TintSkyBox()
    {

        // work out what colour to return between black and the lightest colour of the skybox
        // then set the skybox to that colour, this is based off of the day night cycle


        // These parts work but require a method to work out the right colour that is
        // not yet implemented
        //lightColour = Color.Lerp(Color.black, lightestColour, 1);
        //RenderSettings.skybox.SetColor("_Tint", lightColour);
        //LightSource.color = lightColour;
        //RenderSettings.ambientLight = lightColour;

    }

}