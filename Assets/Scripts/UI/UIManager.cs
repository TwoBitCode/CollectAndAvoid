using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText; // TextMeshPro for score
    public TMP_Text livesText; // TextMeshPro for lives
    public GameObject gameOverPanel; // Game over UI
    public GameObject gameWinPanel; // Win UI

    public AudioClip loseLifeSound; // Sound to play when losing a life
    public AudioClip scoreCollectSound; // Sound to play when score increases

    public Color livesFlashColor = Color.red; // Flash color for lives
    public Color scoreFlashColor = Color.green; // Flash color for score
    public float flashDuration = 0.5f; // Duration of the flash effect

    private AudioSource audioSource; // AudioSource for playing sounds
    private int previousLives; // To track changes in lives
    private int previousScore; // To track changes in score

    private void OnEnable()
    {
        GameManager.GameDataChanged += UpdateUI;
        GameManager.GameOverEvent += ShowGameOverScreen;
        GameManager.GameWinEvent += ShowGameWinScreen;
    }

    private void OnDisable()
    {
        GameManager.GameDataChanged -= UpdateUI;
        GameManager.GameOverEvent -= ShowGameOverScreen;
        GameManager.GameWinEvent -= ShowGameWinScreen;
    }

    private void Start()
    {
        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();

        // Initialize UI with current game data
        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            UpdateUI(gm.score, gm.lives);
            previousLives = gm.lives;
            previousScore = gm.score; // Set the initial score
        }
    }

    private void UpdateUI(int score, int lives)
    {
        // Update score and lives text
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {lives}";

        // Check if lives have decreased
        if (lives < previousLives)
        {
            AnimateLivesText();
        }

        // Check if score has increased
        if (score > previousScore)
        {
            AnimateScoreText();
        }

        // Update previous values
        previousLives = lives;
        previousScore = score;
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    private void ShowGameWinScreen()
    {
        gameWinPanel.SetActive(true);
    }

    private void AnimateLivesText()
    {
        // Play the lose life sound
        if (loseLifeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(loseLifeSound);
        }

        // Flash the lives text
        StopAllCoroutines(); // Stop any existing animations to prevent overlap
        StartCoroutine(FlashText(livesText, livesFlashColor));
    }

    private void AnimateScoreText()
    {
        // Play the score collect sound
        if (scoreCollectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scoreCollectSound);
        }

        // Flash the score text
        StopAllCoroutines(); // Stop any existing animations to prevent overlap
        StartCoroutine(FlashText(scoreText, scoreFlashColor));
    }

    private System.Collections.IEnumerator FlashText(TMP_Text text, Color flashColor)
    {
        // Store the original color
        Color originalColor = text.color;

        // Change text color to the flash color
        text.color = flashColor;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert to the original color
        text.color = originalColor;
    }
}
