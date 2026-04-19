using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Specs")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireInterval = 2f; // Fires exactly every 2 seconds

    private float nextFireTime;
    private Transform target;

    void Start()
    {
        // Add a small random delay so if you have 10 Spam Bots, they don't all shoot on the exact same frame
        nextFireTime = Time.time + fireInterval + Random.Range(0f, 0.5f);
        
        GameObject player = GameObject.Find("Unit 734");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        // Only shoot if the target exists AND is currently active in the game
        if (target != null && target.gameObject.activeInHierarchy && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Calculate the direction to the player, keeping the bolt level with the floor (Y = 0)
            Vector3 aimDirection = (target.position - firePoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(aimDirection.x, 0, aimDirection.z));

            Instantiate(projectilePrefab, firePoint.position, rotation);
        }
    }
}