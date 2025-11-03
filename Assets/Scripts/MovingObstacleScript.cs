using UnityEngine;
using UnityEngine.AI;  // Required for NavMesh

public class RandomNavMeshMover : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
    }

    void Update()
    {
        // When close to destination, choose a new one
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewRandomDestination();
        }
    }

    void SetNewRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f; // 10 = roaming distance
        randomDirection.y = 0; // Stay on ground

        Vector3 targetPosition = transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}