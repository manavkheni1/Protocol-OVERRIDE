using UnityEngine;
using UnityEngine.AI; // Required for the NavMeshAgent

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Find Unit 734 when the game starts
        GameObject player = GameObject.Find("Unit 734");
        if (player != null)
        {
            target = GameObject.Find("Unit 734").transform;
        }
    }

    void Update()
    {
        // Chase the player ONLY if they exist and are currently active (alive)
        if (target != null && target.gameObject.activeInHierarchy)
        {
            agent.SetDestination(target.position);
        }
        else if (agent.hasPath)
        {
            // If the player vanishes, stop walking
            agent.ResetPath();
        }
    }
}