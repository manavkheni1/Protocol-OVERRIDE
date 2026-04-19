using UnityEngine;

public class DroneController : MonoBehaviour
{
    [Header("Drone Settings")]
    public float moveSpeed = 3f;

    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        // Grab the Animator component we just set up
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 1. Get input from WASD or Arrow Keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Prevent the drone from moving faster diagonally
        movement.Normalize(); 

        // 2. Tell the Animator which way we are looking
        if (movement != Vector2.zero) 
        {
            animator.SetFloat("DirX", movement.x);
            animator.SetFloat("DirY", movement.y);
        }

        // 3. Actually move the drone in the game world
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}