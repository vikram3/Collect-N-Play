using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private TextMeshProUGUI levelText;

    // Add button references
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;

    private GameViewModel gameViewModel;

    private void Start()
    {
        gameViewModel = FindObjectOfType<GameViewModel>();

        // Add button listeners
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClick);
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelClick);
        }
    }

    public void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowNextLevel()
    {
        nextLevelPanel.SetActive(true);
    }

    public void HideNextLevel()
    {
        nextLevelPanel.SetActive(false);
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level " + level.ToString();
    }

    // Button click handlers
    private void OnRestartClick()
    {
        gameViewModel.RestartGame();
        gameOverPanel.SetActive(false);
    }

    private void OnNextLevelClick()
    {
        gameViewModel.RestartGame();
        nextLevelPanel.SetActive(false);
    }
}