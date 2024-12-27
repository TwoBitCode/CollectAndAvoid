using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Prefabs for enemies and collectibles
    public GameObject patrolEnemyPrefab;
    public GameObject chaseEnemyPrefab;
    public GameObject[] collectiblePrefabs;

    // Spawn intervals
    public float enemySpawnInterval = 5f; // Time interval for enemy spawns
    public float collectibleSpawnInterval = 2f; // Time interval for collectible spawns

    // Maximum number of collectibles allowed in the scene
    public int maxCollectibles = 5;

    void Start()
    {
        // Start spawning enemies and collectibles
        InvokeRepeating(nameof(SpawnEnemy), 0, enemySpawnInterval);
        InvokeRepeating(nameof(SpawnCollectible), 0, collectibleSpawnInterval);
    }

    void SpawnEnemy()
    {
        // Randomly select an enemy type
        GameObject enemyPrefab = Random.value > 0.5f ? patrolEnemyPrefab : chaseEnemyPrefab;

        // Instantiate the enemy at a random position
        Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
    }

    void SpawnCollectible()
    {
        // Check if the number of collectibles in the scene is below the limit
        if (GameObject.FindGameObjectsWithTag("Collectible").Length < maxCollectibles)
        {
            // Randomly select a collectible type
            int index = Random.Range(0, collectiblePrefabs.Length);
            GameObject collectiblePrefab = collectiblePrefabs[index];

            // Instantiate the collectible at a random position
            Instantiate(collectiblePrefab, GetRandomPosition(), Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Generate a random position within bounds
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-5f, 5f);
        return new Vector3(x, y, 0);
    }
}
