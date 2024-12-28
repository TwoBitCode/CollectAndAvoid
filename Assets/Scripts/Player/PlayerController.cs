using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; // Movement speed of the player

    private GameManager gm; // Reference to the GameManager
    private bool isGameOver = false; // Tracks if the game is over

    private void Start()
    {
        // Cache the GameManager reference at the start
        gm = Object.FindFirstObjectByType<GameManager>();
        if (gm == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    private void Update()
    {
        if (!isGameOver)
        {
            MovePlayer(); // Handle player movement
        }
    }

    private void MovePlayer()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create a movement vector and move the player
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGameOver) return;

        // Check if the player collides with a collectible
        if (collision.CompareTag("Collectible"))
        {
            HandleCollectibleCollision(collision);
        }
        // Check if the player collides with an enemy
        else if (collision.CompareTag("Enemy"))
        {
            HandleEnemyCollision();
        }
    }

    private void HandleCollectibleCollision(Collider2D collision)
    {
        // Handle behavior
        ICollectibleBehavior collectibleBehavior = collision.GetComponent<ICollectibleBehavior>();
        collectibleBehavior?.PerformBehavior(); // Execute behavior if it exists

        // Handle scoring
        IScoringCollectible scoringCollectible = collision.GetComponent<IScoringCollectible>();
        if (scoringCollectible != null && gm != null)
        {
            gm.AddScore(scoringCollectible.GetScoreValue()); // Add points from the collectible
        }

        // Destroy the collectible after interaction
        Destroy(collision.gameObject);
    }

    private void HandleEnemyCollision()
    {
        if (gm != null)
        {
            gm.LoseLife(); // Reduce player's lives
            if (gm.Lives <= 0) // Check if the player has no lives left
            {
                OnGameOver(); // Trigger game-over logic
            }
        }
    }

    private void OnGameOver()
    {
        isGameOver = true; // Set the game-over state
        Debug.Log("Game Over! Player movement disabled.");
    }
}
