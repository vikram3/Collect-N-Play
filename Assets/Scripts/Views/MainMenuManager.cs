using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private GameObject instructionsPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource buttonClickSound;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        // Add listeners to buttons
        playButton.onClick.AddListener(OnPlayClick);
        quitButton.onClick.AddListener(OnQuitClick);
        instructionsButton.onClick.AddListener(OnInstructionsClick);

        if (backgroundMusic != null)
            backgroundMusic.Play();
    }

    private void OnPlayClick()
    {
        PlayButtonSound();
        SceneManager.LoadScene("Game"); // Your game scene name
    }

    private void OnQuitClick()
    {
        PlayButtonSound();
        Application.Quit();
    }

    private void OnInstructionsClick()
    {
        PlayButtonSound();
        instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }

    private void PlayButtonSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }
}