using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject patrolEnemyPrefab; // Prefab for patrol enemies
    [SerializeField] private GameObject chaseEnemyPrefab; // Prefab for chase enemies
    [SerializeField] private GameObject[] collectiblePrefabs; // Array of collectible prefabs

    [SerializeField] private float enemySpawnInterval = 5f; // Time interval for enemy spawns
    [SerializeField] private float collectibleSpawnInterval = 2f; // Time interval for collectible spawns
    [SerializeField] private int maxCollectibles = 5; // Maximum number of collectibles in the scene

    [Header("Enemy Probability")]
    [SerializeField, Range(0f, 1f)] private float patrolEnemyProbability = 0.5f; // Probability of spawning a patrol enemy

    [Header("Spawn Area Settings")]
    [SerializeField] private float spawnAreaWidth = 20f; // Total width of the spawn area
    [SerializeField] private float spawnAreaHeight = 10f; // Total height of the spawn area
    [SerializeField] private float marginX = 2f; // Horizontal margin from the edges
    [SerializeField] private float marginY = 1f; // Vertical margin from the edges

    private float halfSpawnAreaWidth; // Half of the spawn area width
    private float halfSpawnAreaHeight; // Half of the spawn area height

    void Start()
    {
        // Calculate half-dimensions for the spawn area
        halfSpawnAreaWidth = spawnAreaWidth * 0.5f;
        halfSpawnAreaHeight = spawnAreaHeight * 0.5f;

        // Start spawning enemies and collectibles
        InvokeRepeating(nameof(SpawnEnemy), 0, enemySpawnInterval);
        InvokeRepeating(nameof(SpawnCollectible), 0, collectibleSpawnInterval);
    }

    void SpawnEnemy()
    {
        // Use the defined probability to select the enemy type
        GameObject enemyPrefab = Random.value < patrolEnemyProbability ? patrolEnemyPrefab : chaseEnemyPrefab;

        // Instantiate the selected enemy at a random position
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
        // Calculate random position within the spawn area, accounting for margins
        float x = Random.Range(-halfSpawnAreaWidth + marginX, halfSpawnAreaWidth - marginX);
        float y = Random.Range(-halfSpawnAreaHeight + marginY, halfSpawnAreaHeight - marginY);
        return new Vector3(x, y, 0);
    }
}
