using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;    // The Spam Bot blueprint
    public float spawnInterval = 3f;  // Seconds between each spawn
    public int maxEnemies = 15;       // Don't crash the computer!

    [Header("Spawn Area")]
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 20f); // The X and Z size of your floor

    void Start()
    {
        // Start the endless spawning loop
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Only spawn if we haven't hit the max limit
            int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            
            if (currentEnemies < maxEnemies && enemyPrefab != null)
            {
                SpawnEnemy();
            }

            // Wait a few seconds before looping again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Pick a random X and Z coordinate inside our box
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

        // Combine those with the Spawner's position
        Vector3 randomPosition = new Vector3(
            transform.position.x + randomX, 
            transform.position.y, 
            transform.position.z + randomZ
        );

        // Spawn the bot!
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    // This magically draws a red box in the Scene view to help you visualize the spawn area!
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f); // Transparent red
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }
}