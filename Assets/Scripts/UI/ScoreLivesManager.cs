using UnityEngine;
using TMPro;

public class ScoreLivesManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip loseLifeSound;
    [SerializeField] private AudioClip scoreCollectSound;

    [Header("Animation Settings")]
    [SerializeField] private Color livesFlashColor = Color.red;
    [SerializeField] private Color scoreFlashColor = Color.green;

    private AudioSource audioSource;
    private int previousLives = 0;
    private int previousScore = 0;

    private void Start()
    {
        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component is missing on the GameObject.");
        }
    }

    public void UpdateUI(int score, int lives)
    {
        Debug.Log($"Updating UI -> Score: {score}, Lives: {lives}");

        if (scoreText != null) scoreText.text = $"Score: {score}";
        if (livesText != null) livesText.text = $"Lives: {lives}";

        if (lives < previousLives) AnimateLivesText();
        if (score > previousScore) AnimateScoreText();

        previousLives = lives;
        previousScore = score;
    }


    private void AnimateLivesText()
    {
        // Play lose life sound
        PlaySound(loseLifeSound);

        // Flash the lives text
        if (livesText != null)
        {
            StartCoroutine(FlashText(livesText, livesFlashColor));
        }
    }

    private void AnimateScoreText()
    {
        // Play score collection sound
        PlaySound(scoreCollectSound);

        // Flash the score text
        if (scoreText != null)
        {
            StartCoroutine(FlashText(scoreText, scoreFlashColor));
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private System.Collections.IEnumerator FlashText(TMP_Text text, Color flashColor)
    {
        if (text == null) yield break;

        // Store the original color and apply flash color
        var originalColor = text.color;
        text.color = flashColor;

        // Wait for the flash duration
        yield return new WaitForSeconds(Constants.FlashDuration);

        // Revert to the original color
        text.color = originalColor;
    }
}
