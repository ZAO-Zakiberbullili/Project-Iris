using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    private GameObject Player;

    private Camera camera;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    public float horizontalMargin = 0.3f;
    public float verticalMargin = 0.4f;
    public float depth = -10;

    public float smoothTime = 0.25f;
    Vector3 target;
    Vector3 lastPosition;
    Vector3 currentVelocity;

    void Start() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        camera = GetComponent<Camera>();
    }

    void LateUpdate() 
    {
        Vector3 movementDelta = Player.transform.position - lastPosition;
        Vector3 screenPos = camera.WorldToScreenPoint(Player.transform.position);
        Vector3 bottomLeft = camera.ViewportToScreenPoint(new Vector3(horizontalMargin, verticalMargin, 0));
        Vector3 topRight = camera.ViewportToScreenPoint(new Vector3(1 - horizontalMargin, 1 - verticalMargin, 0));
        if (screenPos.x < bottomLeft.x || screenPos.x > topRight.x)
        {
            target.x += movementDelta.x;
        }
        if (screenPos.y < bottomLeft.y || screenPos.y > topRight.y)
        {
            target.y += movementDelta.y;
        }
        target.z = depth;

        lastPosition = Player.transform.position;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
    }
}
