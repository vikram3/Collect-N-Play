using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private TextMeshProUGUI levelText;

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
}
