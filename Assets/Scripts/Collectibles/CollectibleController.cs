using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private ICollectibleBehavior collectibleBehavior;

    void Start()
    {
        // Detect and assign the correct behavior
        if (TryGetComponent<TimedCollectible>(out var timed))
        {
            collectibleBehavior = timed;
        }
        else if (TryGetComponent<MovingCollectible>(out var moving))
        {
            collectibleBehavior = moving;
        }
        else if (TryGetComponent<RareCollectible>(out var rare))
        {
            collectibleBehavior = rare;
        }
    }

    void Update()
    {
        collectibleBehavior?.PerformBehavior();
    }
}
