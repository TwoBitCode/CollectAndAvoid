using UnityEngine;

public class MovingCollectible : MonoBehaviour, ICollectibleBehavior
{
    public float speed = 2f; // Speed of oscillation
    public float range = 1f; // Oscillation range

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store initial position
    }

    public void PerformBehavior()
    {
        // Oscillate vertically
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * range;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
