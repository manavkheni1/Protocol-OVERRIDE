using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        // Calculate the exact distance and angle between the camera and Unit 734 right when the game starts
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        // Follow the player by adding that exact offset to their new position every frame
        // (We use LateUpdate instead of Update so the camera moves AFTER the player finishes moving, preventing jitter)
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}