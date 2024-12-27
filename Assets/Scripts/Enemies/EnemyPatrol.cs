using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IEnemyBehavior
{
    public float speed = 2f; // Speed of patrol
    public float patrolRangeX = 5f; // Horizontal patrol range
    public LayerMask WallLayer; // Layer to detect walls

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    public void PerformBehavior()
    {
        Patrol();
    }

    private void Patrol()
    {
        float targetX = movingRight ? startPosition.x + patrolRangeX : startPosition.x - patrolRangeX;

        // Detect walls in the direction of movement
        if (IsWallAhead())
        {
            movingRight = !movingRight; // Switch direction if a wall is detected
        }

        // Move the enemy horizontally
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), speed * Time.deltaTime);

        // Switch direction when the patrol range is reached
        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            movingRight = !movingRight;
        }
    }

    private bool IsWallAhead()
    {
        float direction = movingRight ? 1f : -1f; // Direction of movement
        Vector2 origin = transform.position;
        float rayDistance = 0.5f; // Distance to check for walls

        // Cast a ray to detect walls
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, rayDistance, WallLayer);
        return hit.collider != null; // Return true if a wall is detected
    }
}
