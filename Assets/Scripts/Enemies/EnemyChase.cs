using UnityEngine;

public class EnemyChase : MonoBehaviour, IEnemyBehavior
{
    [SerializeField]
    private float speed = 3f; // Speed at which the enemy chases the player

    private Transform player; // Reference to the player's Transform

    void Start()
    {
        // Attempt to find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Cache the player's Transform
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} could not find a Player tagged object.");
        }
    }

    public void PerformBehavior()
    {
        // Perform chasing behavior only if the player reference exists
        if (player != null)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        // Calculate the normalized direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position += direction * speed * Time.deltaTime;
    }
}
