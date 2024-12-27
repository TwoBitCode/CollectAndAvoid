using UnityEngine;

public class EnemyChase : MonoBehaviour, IEnemyBehavior
{
    private Transform player; // Reference to the player's Transform
    public float speed = 3f; // Speed of the chase

    void Start()
    {
        // Automatically find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Assign the player's Transform
        }
    }

    public void PerformBehavior()
    {
        if (player != null)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
