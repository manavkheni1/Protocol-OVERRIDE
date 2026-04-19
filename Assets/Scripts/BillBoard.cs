using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        // Find the main camera automatically
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Force the health bar to look exactly the same direction the camera is looking
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}