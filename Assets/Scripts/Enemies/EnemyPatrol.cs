using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IEnemyBehavior
{
    [SerializeField]
    private float speed = 2f; // Speed of patrol

    [SerializeField]
    private float patrolRangeX = 5f; // Horizontal patrol range

    [SerializeField]
    private LayerMask wallLayer; // Layer to detect walls

    [SerializeField]
    private float wallDetectionDistance = 0.5f; // Distance to detect walls

    private const float RightDirection = 1f; // Constant for moving right
    private const float LeftDirection = -1f; // Constant for moving left

    private Vector3 startPosition; // Initial position of the enemy
    private bool movingRight = true; // Direction of movement

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

        if (IsWallAhead())
        {
            movingRight = !movingRight; // Switch direction if a wall is detected
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetX, transform.position.y, transform.position.z),
            speed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            movingRight = !movingRight;
        }
    }

    private bool IsWallAhead()
    {
        float direction = movingRight ? RightDirection : LeftDirection; // Horizontal direction of movement
        Vector2 origin = transform.position; // Origin of the raycast

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, wallDetectionDistance, wallLayer);
        return hit.collider != null;
    }
}
