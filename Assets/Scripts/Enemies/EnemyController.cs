using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IEnemyBehavior enemyBehavior;

    void Start()
    {
        // Try to get a component that implements IEnemyBehavior
        enemyBehavior = GetComponent<IEnemyBehavior>();

        if (enemyBehavior == null)
        {
            Debug.LogError("No behavior found for this enemy!");
        }
    }

    void Update()
    {
        enemyBehavior?.PerformBehavior(); // Execute the behavior if it exists
    }
}
