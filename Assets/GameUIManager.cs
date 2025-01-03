using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Level Complete UI")]
    [SerializeField] private TextMeshProUGUI levelCompleteText;
    [SerializeField] private Button nextLevelButton;

    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;

    [Header("Timer & Level Info")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Audio")]
    [SerializeField] private AudioSource successSound;
    [SerializeField] private AudioSource failureSound;

    private GameViewModel gameViewModel;

    private void Start()
    {
        gameViewModel = FindObjectOfType<GameViewModel>();

        // Initialize UI
        nextLevelPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // Add listeners to buttons
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        }
    }

    public void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowNextLevelPanel(int currentLevel)
    {
        gameOverPanel.SetActive(false);
        nextLevelPanel.SetActive(true);

        levelCompleteText.text = $"Level {currentLevel} Complete!";

        if (successSound != null)
        {
            successSound.Play();
        }
    }

    public void ShowGameOverPanel(int currentLevel)
    {
        nextLevelPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        gameOverText.text = $"Game Over\nLevel {currentLevel}";

        if (failureSound != null)
        {
            failureSound.Play();
        }
    }

    public void UpdateLevel(int level)
    {
        levelText.text = $"Level {level}";
    }

    public void HideNextLevel()
    {
        nextLevelPanel.SetActive(false);
    }

    public void SetNextLevelCallback(UnityAction callback)
    {
        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(callback);
    }

    public void SetRestartCallback(UnityAction callback)
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(callback);
    }

    private void OnNextLevelClicked()
    {
        gameViewModel.RestartGame();
        nextLevelPanel.SetActive(false);
    }

    private void OnRestartClicked()
    {
        gameViewModel.RestartGame();
        gameOverPanel.SetActive(false);
    }
}
