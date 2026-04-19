using UnityEngine;
using System.Collections; // Required for Coroutines

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Specs")]
    public float maxSpeed = 5f; 
    public float accelerationTime = 0.15f; 
    public float decelerationTime = 0.1f; 

    [Header("Phase Shift Specs")]
    public float dashDistance = 3f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 2f;

    private CharacterController controller;
    private Vector3 currentVelocity;
    private Vector3 targetVelocity;
    
    private float velocityXSmoothing;
    private float velocityZSmoothing;

    private Transform cameraTransform;

    // Dash State Variables
    private bool isDashing = false;
    private float lastDashTime = -2f; // Starts at -2 so we can dash immediately

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 1. If we are currently dashing, ignore normal movement input
        if (isDashing) return;

        // 2. Normal Keyboard Movement Logic
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(inputX, inputY).normalized;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * input.y + right * input.x).normalized;

        // 3. Phase Shift Trigger (Spacebar)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            // If the player isn't pressing a direction, dash forward based on where the robot is facing
            Vector3 dashDirection = moveDirection == Vector3.zero ? transform.forward : moveDirection;
            StartCoroutine(DashRoutine(dashDirection));
            return; // Skip the rest of the update this frame
        }

        targetVelocity = moveDirection * maxSpeed;

        float smoothTimeX = (Mathf.Abs(moveDirection.x) > 0) ? accelerationTime : decelerationTime;
        float smoothTimeZ = (Mathf.Abs(moveDirection.z) > 0) ? accelerationTime : decelerationTime;

        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, targetVelocity.x, ref velocityXSmoothing, smoothTimeX);
        currentVelocity.z = Mathf.SmoothDamp(currentVelocity.z, targetVelocity.z, ref velocityZSmoothing, smoothTimeZ);

        if (controller.isGrounded)
        {
            currentVelocity.y = -2f; 
        }
        else
        {
            currentVelocity.y -= 9.81f * Time.deltaTime; 
        }

        controller.Move(currentVelocity * Time.deltaTime);

        // Make the robot face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        }
    }

    // 4. The Phase Shift Execution
    private IEnumerator DashRoutine(Vector3 dashDir)
    {
        isDashing = true;
        lastDashTime = Time.time;
        float startTime = Time.time;

        // Calculate the speed required to travel 3 meters in 0.25 seconds
        float dashSpeed = dashDistance / dashDuration;

        while (Time.time < startTime + dashDuration)
        {
            // Move the controller at the high dash speed
            controller.Move(dashDir * dashSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        isDashing = false;
        
        // Reset current velocity so we don't slide wildly after the dash ends
        currentVelocity = Vector3.zero; 
    }
}