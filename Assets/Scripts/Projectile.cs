using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f; 
    public float damage = 5f; 
    public float lifeTime = 3f; // Destroys itself after 3 seconds so we don't clutter the game

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Fly straight forward at 15 m/s
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // SAFETY CHECK: Ignore Unit 734 so the player doesn't block their own shots!
        if (other.name == "Unit 734")
        {
            return; 
        }

        // Try to find the EnemyHealth script on the object we just hit
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        
        // If we found it, tell it to take our damage amount (5)
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destroy the bolt regardless of what it hit (wall or enemy)
        Destroy(gameObject); 
    }
}