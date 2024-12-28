using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Managers")]
    [SerializeField] private ScoreLivesManager scoreLivesManager; // Reference to UI manager for score and lives
    [SerializeField] private GameOverWinManager gameOverWinManager; // Reference to UI manager for game-over and win screens

    private int score = 0; // Current score, not exposed in the Inspector
    private int lives; // Current number of lives
    public int Lives => lives; // Read-only property to access the current lives

    private bool isGameOver = false; // Tracks whether the game is over

    private void Start()
    {
        // Initialize lives using Constants.MaxLives
        lives = Constants.MaxLives;

        // Update the UI with the initial score and lives
        scoreLivesManager.UpdateUI(score, lives);
    }
    public void AddScore(int points)
    {
        Debug.Log($"AddScore called with {points} points. Current score before adding: {score}");

        if (isGameOver) return;

        score += points;
        scoreLivesManager.UpdateUI(score, lives);

        if (score >= Constants.MaxScore)
        {
            TriggerWin();
        }
    }


    public void LoseLife()
    {
        if (isGameOver) return;

        // Decrement the number of lives
        lives--;
        scoreLivesManager.UpdateUI(score, lives);

        // Check if the player has lost all lives
        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        if (isGameOver) return;

        // Mark the game as over and pause
        isGameOver = true;
        Debug.Log("Game Over!");
        Time.timeScale = Constants.GamePausedTimeScale; // Use constant for pause
        gameOverWinManager.ShowGameOverScreen();
    }

    private void TriggerWin()
    {
        if (isGameOver) return;

        // Mark the game as won and pause
        isGameOver = true;
        Debug.Log("You Win!");
        Time.timeScale = Constants.GamePausedTimeScale; // Use constant for pause
        gameOverWinManager.ShowGameWinScreen();
    }

}
