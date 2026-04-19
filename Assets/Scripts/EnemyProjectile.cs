using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 15f; 
    public float damage = 5f; 
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Fly straight forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with ANY enemies
        if (other.GetComponent<EnemyHealth>() != null)
        {
            return; 
        }

        // Check if the object we hit has the PlayerHealth script attached
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        
        if (player != null)
        {
            // Deal 5 damage to the player
            player.TakeDamage(damage);
        }

        // Destroy the red bolt regardless of what it hit
        Destroy(gameObject); 
    }
}