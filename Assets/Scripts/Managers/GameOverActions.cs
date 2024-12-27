using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverActions : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
        Debug.Log("Game Quit"); // Log for testing (this won't work in the editor)
    }
}
