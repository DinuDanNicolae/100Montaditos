using UnityEngine;
using UnityEngine.AI;

public class WifeController : MonoBehaviour
{
    [SerializeField]
    private float startWaitTime; // time to wait at each spot

    [SerializeField]
    private Transform moveSpot; // Patrol move spot
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    [SerializeField]
    private Transform player; // Reference to the player's transform
    [SerializeField]
    private float chaseRange; // The range at which the enemy will start to chase the player

    private float waitTime; // time to wait at each spot
    private bool isChasing; // Whether the enemy is currently chasing the player

    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        agent.SetDestination(moveSpot.position);
    }

    void Update()
    {
        Debug.Log("Player position: " + player.position);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log(distanceToPlayer);

        // Check if the player is within chase range
        if (distanceToPlayer < chaseRange)
        {
            // Start chasing the player
            isChasing = true;
            agent.SetDestination(player.position);
        }
        else if (isChasing && distanceToPlayer >= chaseRange)
        {
            // Stop chasing the player and return to patrolling
            isChasing = false;
        }

        // If not chasing, patrol behavior
        if (!isChasing)
        {
            // Check if the agent has reached the destination (moveSpot)
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (waitTime <= 0)
                {
                    // Assign a new random patrol spot after waiting is done
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    agent.SetDestination(moveSpot.position);
                    waitTime = startWaitTime;
                }
                else
                {
                    // Decrease wait time
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}