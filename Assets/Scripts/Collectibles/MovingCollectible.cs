using UnityEngine;

public class MovingCollectible : MonoBehaviour, ICollectibleBehavior, IScoringCollectible
{
    [Header("Oscillation Settings")]
    [SerializeField] private float speed = 2f; // Speed of oscillation
    [SerializeField] private float range = 1f; // Oscillation range


    private int scoreValue = Constants.CommonCollectiblePoints; // Points awarded for collecting this item

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the collectible
        startPosition = transform.position;
    }

    public void PerformBehavior()
    {
        // Oscillate vertically within the specified range and speed
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * range;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public int GetScoreValue()
    {
        // Return the score value for this collectible
        return scoreValue;
    }
}
