using System.Collections;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Player player;
    public Camera mainCamera;
    private Vector2 _mousePosition;
    [Header("Set speed to 20.0f for WebGL export!")]
    [SerializeField] private float _rotationSpeed = 40.0f;
    private float _pitch = 0.0f;
    private readonly float _friction = 0.96f;
    private readonly int _defaultFov = 60; // The FOV when not moving
    private readonly int _maxFov = 95; // the max FOV value
    private readonly float _zoomFactor = 0.8f; // controls how much the FOV will change when the player moves
    private readonly float _cameraShakeMagnitude = 0.2f;
    private float _shakeTimer;
    private float _dampenMouse = 0.5f;
    

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        // the difference between the camera and the player when the scene is loaded
        SetOffset();
        ResetShakeTimer();
    }

    // LateUpdate() is called after Update() each frame
    // this ensures the player moves THEN the camera position is recalculated
    // https://www.maxester.com/blog/2020/02/24/how-do-you-make-the-camera-follow-the-player-in-unity-3d/
    private void LateUpdate()
    {
        // Get the mouse in a similar way to buttons/keys and set the pitch relevant to the movement of the mouse
        // Mouse sensitivity feels different in WebGL compared to editor, this is a long standing Unity bug:
        // https://forum.unity.com/threads/mouse-sensitivity-in-webgl-way-too-sensitive.411574/#post-3421405
        _pitch = Input.GetAxis("Mouse X");
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (Mathf.Abs(_pitch) > 1.0f) { _pitch = Mathf.Lerp(_pitch, Mathf.Sign (_pitch), _dampenMouse); }
        }
        _pitch *= _rotationSpeed;

        // Update the camera position to track the player, and rotate around the player
        transform.position = (player.transform.position + _offset);
        transform.RotateAround(player.transform.position, Vector3.up, _pitch * Time.deltaTime);
        
        // A rotate requires that _offset be re-calculated
        SetOffset();
        _pitch *= _friction; // slow down the camera rotation smoothly
        
        // at max FOV shake the camera slightly to stimulate very fast motion
        if (mainCamera.fieldOfView == _maxFov)
        {
            _shakeTimer -= Time.deltaTime;
            // A co-routine calls a function in a slightly different fashion
            if (_shakeTimer <= 0.0f) { StartCoroutine(ShakeCamera(_cameraShakeMagnitude)); }
        }
        else { ResetShakeTimer(); }
    }

    private void FixedUpdate()
    {
        UpdateFov();
    }

    private void UpdateFov()
    {
        // Gets the speed at which the player is moving (ignoring the Y speed of the player's RB)
        var playerVelocity = new Vector3(player.rb.velocity.x, 0.0f, player.rb.velocity.z).magnitude;
        // updates the FOV by the player's current speed * zoomfactor values
        // because velocity already takes into consideration acceleration, this feels smooth by default!
        // the if statement caps FOV at 95 so that it never feels TOO fast
        if (_defaultFov + (playerVelocity * _zoomFactor) > _maxFov)
        {
            mainCamera.fieldOfView = _maxFov;
        }
        else
        {
            mainCamera.fieldOfView = _defaultFov + (playerVelocity * _zoomFactor);
        }
    }

    // A co-routine that enables running multiple things in a parallel
    // This co-routine was created with the help of this tutorial: https://www.youtube.com/watch?v=9A9yj8KnM8c
    IEnumerator ShakeCamera(float shakeMag)
    {
        // Store the original value of the camera so that when shaking is done it can be restored
        Vector3 oldMainCamPosition = transform.localPosition;
        while (mainCamera.fieldOfView == _maxFov) // While the FOV is at maxFOV (i.e, the player is at max velocity)
        {
            // Get a random position at x and y, and adjust them to be relevant to the moving camera
                float x = (Random.Range(-1f, 1f) * shakeMag) + oldMainCamPosition.x;
                float y = (Random.Range(-1f, 1f) * shakeMag) + oldMainCamPosition.y;

                // Update the camera itself with the updated co-ordinates
                transform.localPosition = new Vector3(x, y, oldMainCamPosition.z);

                // Wait until the next frame is drawn to iterate the while loop
            yield return null;
        }

        transform.localPosition = oldMainCamPosition; // Set the camera back to what it was before the shake
    }

    private void SetOffset()
    {
        _offset = transform.position - player.transform.position;
    }
    
    private void ResetShakeTimer()
    {
        _shakeTimer = 1.2f;
    }
}