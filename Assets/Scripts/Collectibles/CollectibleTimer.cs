using UnityEngine;

public class CollectibleTimer : MonoBehaviour
{
    public float lifetime = 5f; // Time in seconds before the collectible disappears

    void Start()
    {
        // Destroy the collectible after the lifetime has passed
        Destroy(gameObject, lifetime);
    }
}
