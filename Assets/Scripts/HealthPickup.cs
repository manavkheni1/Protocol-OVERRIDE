using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float healAmount = 20f;
    public float lifeTime = 5f;      // Disappears after 5 seconds
    public float spinSpeed = 100f;   // Makes it look like a classic video game item

    void Start()
    {
        // Unity's built-in timer: Destroy THIS object after 'lifeTime' seconds!
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Slowly spin the object to make it visually interesting
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Did the player touch us?
        if (other.CompareTag("Player"))
        {
            // Find the player's health script and trigger the heal
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject); // Delete the pickup instantly once collected
            }
        }
    }
}