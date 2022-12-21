using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Player Player;
    public Camera mainCamera;
    private Vector2 mousePosition;
    private float pitch = 0.0f;
    private float rotationSpeed = 5.0f;
    private readonly float _friction = 0.96f;
    private int defaultFov = 60; // The FOV when not moving
    private int maxFov = 95; // the max FOV value
    private float zoomFactor = 0.8f; // controls how much the FOV will change when the player moves
    

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        // the difference between the camera and the player when the scene is loaded
        SetOffset();
    }

    // LateUpdate() is called after Update() each frame
    // this ensures the player moves THEN the camera position is recalculated
    // https://www.maxester.com/blog/2020/02/24/how-do-you-make-the-camera-follow-the-player-in-unity-3d/
    private void LateUpdate()
    {
        // Get the mouse in a similar way to buttons/keys and set the pitch relevant to the movement of the mouse
        // Mouse sensitivity feels different in WebGL compared to editor, this is a long standing Unity bug:
        // https://forum.unity.com/threads/mouse-sensitivity-in-webgl-way-too-sensitive.411574/#post-3421405
        // NOT IMPLEMENTED CURRENTLY, Mouse slows too fast, feels bad. Need to look into this later.
        pitch += rotationSpeed * Input.GetAxis("Mouse X");

        // Update the camera position to track the player, and rotate around the player
        transform.position = (Player.transform.position + _offset);
        transform.RotateAround(Player.transform.position, Vector3.up, pitch * Time.deltaTime);
        
        // A rotate requires that _offset be re-calculated
        SetOffset();
        pitch *= _friction; // slow down the camera rotation smoothly
        
        //if ()
    }

    private void FixedUpdate()
    {
        // Gets the speed at which the player is moving (ignoring the Y speed of the player's RB)
        var playerVelocity = new Vector3(Player.rb.velocity.x, 0.0f, Player.rb.velocity.z).magnitude;
        // updates the FOV by the player's current speed * zoomfactor values
        // because velocity already takes into consideration acceleration, this feels smooth by default!
        // the if statement caps FOV at 95 so that it never feels TOO fast
        if (defaultFov + (playerVelocity * zoomFactor) > maxFov)
        {
            mainCamera.fieldOfView = maxFov;
        }
        else
        {
            mainCamera.fieldOfView = defaultFov + (playerVelocity * zoomFactor);
        }
    }

    private void SetOffset()
    {
        _offset = transform.position - Player.transform.position;
    }
}