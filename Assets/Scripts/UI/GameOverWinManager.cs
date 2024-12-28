using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWinManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over UI panel
    [SerializeField] private GameObject gameWinPanel; // Reference to the Win UI panel

    [Header("Game Settings")]
    [SerializeField] private float defaultTimeScale = 1f; // Default time scale for resuming the game

    // Displays the Game Over screen
    public void ShowGameOverScreen()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Activate the Game Over panel
        }
        else
        {
            Debug.LogWarning("Game Over Panel is not assigned in the Inspector!");
        }
    }

    // Displays the Game Win screen
    public void ShowGameWinScreen()
    {
        if (gameWinPanel != null)
        {
            gameWinPanel.SetActive(true); // Activate the Win panel
        }
        else
        {
            Debug.LogWarning("Game Win Panel is not assigned in the Inspector!");
        }
    }

    // Restarts the current game level
    public void RestartGame()
    {
        // Reset time scale in case the game was paused
        Time.timeScale = defaultTimeScale;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
