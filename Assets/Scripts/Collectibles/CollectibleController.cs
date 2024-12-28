using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    // Interface for the collectible's specific behavior
    private ICollectibleBehavior collectibleBehavior;

    void Start()
    {
        // Detect and assign the appropriate behavior component
        // Check if the collectible has a TimedCollectible behavior
        if (TryGetComponent<TimedCollectible>(out var timed))
        {
            collectibleBehavior = timed;
        }
        // Check if the collectible has a MovingCollectible behavior
        else if (TryGetComponent<MovingCollectible>(out var moving))
        {
            collectibleBehavior = moving;
        }
        // Check if the collectible has a RareCollectible behavior
        else if (TryGetComponent<RareCollectible>(out var rare))
        {
            collectibleBehavior = rare;
        }
    }

    void Update()
    {
        // Execute the assigned behavior, if any
        collectibleBehavior?.PerformBehavior();
    }
}
