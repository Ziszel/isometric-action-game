using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform Player;

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        // the difference between the camera and the player when the scene is loaded
        _offset = transform.position - Player.transform.position;
    }
    
    

    // LateUpdate() is called after Update() each frame
    // this ensures the player moves THEN the camera position is recalculated
    // https://www.maxester.com/blog/2020/02/24/how-do-you-make-the-camera-follow-the-player-in-unity-3d/
    void LateUpdate()
    {
        transform.position = Player.transform.position + _offset;
    }
}
