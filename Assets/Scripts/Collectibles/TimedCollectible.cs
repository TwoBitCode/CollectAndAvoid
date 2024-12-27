using UnityEngine;

public class TimedCollectible : MonoBehaviour, ICollectibleBehavior
{
    public float lifetime = 5f; // Time in seconds before disappearing

    void Start()
    {
        // Destroy the collectible after its lifetime
        Destroy(gameObject, lifetime);
    }

    public void PerformBehavior()
    {
        // Timed collectible doesn't need additional behavior during gameplay
    }
}
