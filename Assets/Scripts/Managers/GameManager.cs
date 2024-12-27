using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0; // Current score
    public int lives = 3; // Player's lives
    public int winScore = 100; // Score required to win the game

    public delegate void OnGameDataChanged(int score, int lives); // Delegate for data change
    public static event OnGameDataChanged GameDataChanged;

    public delegate void OnGameOver(); // Delegate for Game Over
    public static event OnGameOver GameOverEvent;

    public delegate void OnGameWin(); // Delegate for Game Win
    public static event OnGameWin GameWinEvent;

    private bool isGameOver = false;
    private bool isGameWon = false;

    public void AddScore(int points)
    {
        if (isGameOver || isGameWon) return;

        score += points;
        Debug.Log("Score: " + score);

        // Check if the player has won
        if (score >= winScore)
        {
            TriggerGameWin();
        }

        // Notify listeners about the change
        GameDataChanged?.Invoke(score, lives);
    }

    public void LoseLife()
    {
        if (isGameOver || isGameWon) return;

        lives--;
        Debug.Log("Lives: " + lives);

        // Notify listeners about the change
        GameDataChanged?.Invoke(score, lives);

        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        GameOverEvent?.Invoke(); // Notify listeners that the game is over
    }

    private void TriggerGameWin()
    {
        isGameWon = true;
        Debug.Log("You Win!");
        GameWinEvent?.Invoke(); // Notify listeners that the game is won
    }
}
