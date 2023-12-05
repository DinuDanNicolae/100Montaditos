using UnityEngine;

public class Angry_Wife : MonoBehaviour
{
    public Transform player;
    public float walkPointRange;
    public float sightRange;
    public float moveSpeed;

    private Vector3 walkPoint;
    private bool walkPointSet;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));

        if (!playerInSightRange)
        {
            Patroling();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkPoint, moveSpeed * Time.deltaTime);
        }

        float distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);

        if (distanceToWalkPoint < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, LayerMask.GetMask("Ground")))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
