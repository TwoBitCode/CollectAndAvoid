using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroductionManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject introductionPanel; // Panel to show introduction
    [SerializeField] private TMP_Text rulesText; // Text for rules and introduction
    [SerializeField] private Button startButton; // Button to start the game

    [Header("Game Settings")]
    private string introductionMessage =
        $"Welcome to the Game!\n\n" +
        $"You have {Constants.MaxLives} lives. Collect {Constants.MaxScore} points to win and avoid enemies!\n" +
        "There are two types of donuts:\n" +
        $"- Common: Worth {Constants.CommonCollectiblePoints} points.\n" +
        $"- Rare: Worth {Constants.RareCollectiblePoints} points.\n\n" +
        "Good luck and have fun!";

    private void Start()
    {
        // Pause the game and show the introduction panel
        PauseGame();
        DisplayIntroduction();

        // Hook up the start button
        startButton.onClick.AddListener(StartGame);
    }

    private void PauseGame()
    {
        Time.timeScale = Constants.GamePausedTimeScale; // Pause the game
    }

    private void ResumeGame()
    {
        Time.timeScale = Constants.GameRunningTimeScale; // Resume the game
    }

    private void DisplayIntroduction()
    {
        introductionPanel.SetActive(true); // Show the introduction panel
        if (rulesText != null)
        {
            rulesText.text = introductionMessage; // Set the introduction message
        }
    }

    private void StartGame()
    {
        // Hide the introduction panel and resume the game
        introductionPanel.SetActive(false);
        ResumeGame();
    }
}
