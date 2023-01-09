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
    public Light LightSource;
    private Cycle _cycle;
    private float _cycleTimer; // seconds
    private float _lengthOfDay;
    private float _visualCycleTime = 10.0f;
    private float _sunRotationX;
    private Color _lightColour;
    //private Color _lightestColour;
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
        
        //_lightestColour = new Color32(190, 190, 190, 1);
        _sunRotationX = 90.0f;
        _cycle = Cycle.Day;
        playerHasFlower = false;
        _lengthOfDay = 10.0f;
        _visualCycleTime = 20.0f;
        _cycleTimer = 10.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // taking a full day / night cycle to switch over, investigate!
        // set whether it's day or night based on the sun (directional light) rotation axis X
        if (_cycleTimer <= 0.0f)
        {
            if (_cycle == Cycle.Day)
            {
                _cycle = Cycle.Night;
            }
            else
            {
                _cycle = Cycle.Day;
            }
            _cycleTimer = _lengthOfDay;
        }

        RotateSun();
        //TintSkyBox();
        _cycleTimer -= Time.deltaTime;

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
        _sunRotationX = _visualCycleTime * Time.deltaTime;
        LightSource.transform.Rotate(new Vector3(_sunRotationX, 0.0f, 0.0f), Space.World);
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