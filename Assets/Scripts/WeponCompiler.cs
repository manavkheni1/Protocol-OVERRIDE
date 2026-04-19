using UnityEngine;

public class WeaponCompiler : MonoBehaviour
{
    [Header("Compiler Specs")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 8f; // 8 bolts per second
    public float ramCost = 2f;  // 2 RAM per shot

    private float nextFireTime = 0f;
    private ResourceManager ramManager;

    void Start()
    {
        ramManager = GetComponent<ResourceManager>();
    }

    void Update()
    {
        // Check for Left Click and if enough time has passed for the fire rate
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Ask the ResourceManager for 2 RAM. If it says yes, we shoot!
        if (ramManager != null && ramManager.TryConsumeRAM(ramCost))
        {
            // Set the timer for the next shot
            nextFireTime = Time.time + (1f / fireRate);
            
            // Spawn the bolt
            if (projectilePrefab != null && firePoint != null)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

                // --- NEW AUDIO TRIGGER ---
                // Tell the AudioManager to blast the shooting sound!
                AudioManager.instance.PlaySound(AudioManager.instance.playerShoot);
            }
        }
    }
}