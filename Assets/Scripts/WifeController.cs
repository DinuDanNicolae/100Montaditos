using UnityEngine;
using UnityEngine.AI;

public class WifeController : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float startWaitTime = 3f; // Time to wait at each spot
    [SerializeField] private Transform moveSpot; // Patrol move spot
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;

    [Header("Chase Settings")]
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private float chaseRange = 5f; // The range at which the wife will start to chase the player

    [Header("Appearance Settings")]
    [SerializeField] private float maxAppearanceInterval = 10f; // Maximum time before disappearing
    [SerializeField] private Vector3 startPoint = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] private Vector3 endPoint = new Vector3(0.0f, 0.0f, 0.0f);
    private float waitTime; // Current wait time
    private bool isChasing; // Whether the wife is currently chasing the player
    private bool isActive; // Whether the wife is currently active
    private float nextAppearanceTime; // Time for next appearance or disappearance
    private bool firstApperance;
    private float patrolTimer = 0f;

    private NavMeshAgent agent;
    private Animator animator;

    private Renderer wifeRenderer; // Renderer for the wife

    void Start()
    {
        waitTime = startWaitTime;
        isActive = false; // Start inactive

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>() ?? gameObject.AddComponent<Animator>();

        wifeRenderer = GetComponent<Renderer>(); // Get the Renderer component
        wifeRenderer.enabled = false; // Make wife invisible at start

        SetNextAppearanceTime();
    }

    void Update()
    {
        if (!isActive && Time.time >= nextAppearanceTime)
        {
            ReturnFromOffscreen();
        }

        if (isActive)
        {
            PatrolOrChasePlayer();

            // Update the patrol timer
            patrolTimer += Time.deltaTime;

            // Check if 20 seconds have passed
            if (patrolTimer >= 20f)
            {
                MoveToEndPoint();
            }
        }
    }
    private void MoveToEndPoint()
    {
        isChasing = false;
        isActive = false;
        agent.SetDestination(endPoint); // Set destination to endPoint
        animator.SetBool("Walk", true);

        // Check if the wife has reached the endPoint
        //!agent.pathPending && 
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            wifeRenderer.enabled = false;
            animator.SetBool("Walk", false);
            Destroy(gameObject); // Destroy the object
        }
    }
    private void SetNextAppearanceTime()
    {
        nextAppearanceTime = Time.time + Random.Range(0f, 40f); // Random time between 10 and 20 seconds
    }

    private void ReturnFromOffscreen()
    {
        Debug.Log("Wife starts patrolling!");
        isActive = true;
        wifeRenderer.enabled = true; // Make wife visible
        SetNewPatrolDestination();
    }
    private void PatrolOrChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            isChasing = true;
            agent.SetDestination(player.position);
            animator.SetBool("Walk", true);
        }
        else if (isChasing && distanceToPlayer >= chaseRange)
        {
            isChasing = false;
        }

        if (!isChasing && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (waitTime <= 0)
            {
                SetNewPatrolDestination();
                waitTime = startWaitTime;
            }
            else
            {
                animator.SetBool("Walk", false);
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void SetNewPatrolDestination()
    {
        Vector2 newMoveSpotPosition = GetRandomPositionThatIsNotObstacle();
        moveSpot.position = newMoveSpotPosition;
        agent.SetDestination(newMoveSpotPosition);
        animator.SetBool("Walk", true);
    }

    private Vector2 GetRandomPositionThatIsNotObstacle()
    {
        Vector2 randomPosition;
        bool isObstacle;

        do
        {
            randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            isObstacle = Physics2D.OverlapCircle(randomPosition, 0.1f, LayerMask.GetMask("Obstacles")) != null;
        }
        while (isObstacle);

        return randomPosition;
    }
}
