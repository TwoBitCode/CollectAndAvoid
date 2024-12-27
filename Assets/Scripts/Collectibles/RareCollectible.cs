using UnityEngine;

public class RareCollectible : MonoBehaviour, ICollectibleBehavior
{
    public int bonusPoints = 50; // Extra points for this collectible

    public void PerformBehavior()
    {
        // Rare collectible doesn't have a specific in-game behavior
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Rare collectible picked up!");
            GameManager gm = Object.FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.AddScore(bonusPoints);
            }
            Destroy(gameObject);
        }
    }
}
