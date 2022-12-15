using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform Player;
    private Vector2 mousePosition;
    private float pitch = 0.0f;
    private float rotationSpeed = 10.0f;
    private readonly float _friction = 0.96f;

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
        pitch += rotationSpeed * Input.GetAxis("Mouse X");

        // Update the camera position to track the player, and rotate around the player
        transform.position = (Player.transform.position + _offset);
        transform.RotateAround(Player.position, Vector3.up, pitch * Time.deltaTime);
        
        // A rotate requires that _offset be re-calculated
        SetOffset();
        
        pitch *= _friction;
    }

    private void SetOffset()
    {
        _offset = transform.position - Player.transform.position;
    }
}
