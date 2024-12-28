using UnityEngine;

public class RareCollectible : MonoBehaviour, ICollectibleBehavior, IScoringCollectible
{
    private int scoreValue = Constants.RareCollectiblePoints;

    [Header("Pulsating Settings")]
    [SerializeField] private float visibleDuration = 10f; // Total time the collectible stays visible
    [SerializeField] private float pulseDuration = 5f; // Time to start pulsating
    [SerializeField] private float minScale = 0.8f; // Minimum scale for pulsating
    [SerializeField] private float maxScale = 1.2f; // Maximum scale for pulsating
    [SerializeField] private float pulseSpeed = 2f; // Speed of pulsation

    private const float SinWaveMin = -1f; // Minimum value of Mathf.Sin()
    private const float SinWaveMax = 1f;  // Maximum value of Mathf.Sin()
    private const float NormalizedMin = 0f; // Normalized range minimum
    private const float NormalizedMax = 1f; // Normalized range maximum

    private float elapsedTime;

    void Start()
    {
        // Schedule destruction
        Destroy(gameObject, visibleDuration);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Start pulsating effect during the last few seconds of the collectible's lifespan
        if (elapsedTime >= visibleDuration - pulseDuration)
        {
            ApplyPulsatingEffect();
        }
    }

    private void ApplyPulsatingEffect()
    {
        float sinWave = Mathf.Sin(Time.time * pulseSpeed); // Oscillates between SinWaveMin and SinWaveMax
        float normalizedSinWave = Mathf.InverseLerp(SinWaveMin, SinWaveMax, sinWave); // Normalized to 0–1
        float scale = Mathf.Lerp(minScale, maxScale, normalizedSinWave); // Map normalized value to scale range
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public int GetScoreValue()
    {
        return scoreValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Required method from ICollectibleBehavior
    public void PerformBehavior()
    {
        // Define any custom behavior here, for example:
        Debug.Log("Rare collectible is performing its behavior!");
        ApplyPulsatingEffect();
    }
}
