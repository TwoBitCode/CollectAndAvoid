using UnityEngine;

public class RareCollectible : MonoBehaviour, ICollectibleBehavior, IScoringCollectible
{

    private int scoreValue = Constants.RareCollectiblePoints; // Points for collecting

    [Header("Visibility Settings")]
    [SerializeField] private float visibleDuration = 5f; // Time the collectible stays visible
    [SerializeField] private float tickInterval = 1f; // Interval for ticking before disappearing

    private float remainingTime; // Time left before disappearing
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer

    void Start()
    {
        // Initialize the remaining time
        remainingTime = visibleDuration;

        // Cache the sprite renderer to toggle visibility
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning($"{gameObject.name} is missing a SpriteRenderer component.");
        }

        // Start ticking effect before disappearing
        InvokeRepeating(nameof(TickEffect), visibleDuration - 3f, tickInterval);

        // Schedule destruction after the duration
        Destroy(gameObject, visibleDuration);
    }

    public void PerformBehavior()
    {
        TickEffect();
    }

    public int GetScoreValue()
    {
        Debug.Log($"RareCollectible Score Value: {scoreValue}");
        return scoreValue;
    }


    private void TickEffect()
    {
        // Toggle visibility to simulate ticking
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return; // Prevent double-triggering
        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            Debug.Log("Rare collectible picked up!");
            CancelInvoke(nameof(TickEffect));
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        // Ensure ticking is canceled to avoid invoking on a destroyed object
        CancelInvoke(nameof(TickEffect));
    }
}
