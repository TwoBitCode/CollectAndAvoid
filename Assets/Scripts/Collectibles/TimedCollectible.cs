using UnityEngine;

public class TimedCollectible : MonoBehaviour, ICollectibleBehavior, IScoringCollectible
{
    [Header("Lifetime Settings")]
    [SerializeField] private float lifetime = 5f; // Time before the collectible disappears

    [Header("Circular Motion Settings")]
    [SerializeField] private float circleRadius = 1f; // Radius of the circular motion
    [SerializeField] private float circleSpeed = 2f; // Speed of the circular motion

    private int scoreValue = Constants.CommonCollectiblePoints; // Points awarded for collecting this item

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position
        startPosition = transform.position;

        // Destroy the collectible after its lifetime
        Destroy(gameObject, lifetime);
    }

    public void PerformBehavior()
    {
        // Move the collectible in a circular pattern
        float x = Mathf.Cos(Time.time * circleSpeed) * circleRadius;
        float y = Mathf.Sin(Time.time * circleSpeed) * circleRadius;
        transform.position = startPosition + new Vector3(x, y, 0);
    }

    public int GetScoreValue()
    {
        // Return the score value for this collectible
        return scoreValue;
    }
}
