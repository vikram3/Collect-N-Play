using UnityEngine;
using System.Collections.Generic;

public class GameViewModel : MonoBehaviour
{
    [SerializeField] private List<Sprite> objectSprites;
    [SerializeField] private RowView[] rowViews;
    [SerializeField] private TargetRowView targetRowView;
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private float[] rowSpeeds = { 1f, 2f, 3f, 4f };
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource levelCompleteSound;

    private GameModel gameModel;

    private void Start()
    {
        gameModel = gameObject.AddComponent<GameModel>();
        InitializeGame();
    }

    private void InitializeGame()
    {
        gameModel = new GameModel(objectSprites);
        InitializeRows();
        UpdateTargetRow();
        gameUIManager.UpdateLevel(gameModel.CurrentLevel);
    }

    private void Update()
    {
        if (!gameModel.IsGameOver)
        {
            gameModel.UpdateTimer(Time.deltaTime);
            UpdateRows();
            gameUIManager.UpdateTimer(gameModel.TimeRemaining);

            if (gameModel.TimeRemaining <= 0)
            {
                gameUIManager.ShowGameOverPanel(gameModel.CurrentLevel);
            }
        }
    }

    private void InitializeRows()
    {
        for (int i = 0; i < rowViews.Length; i++)
        {
            rowViews[i].Initialize(rowSpeeds[i], i % 2 == 0, gameModel.AvailableObjects);
        }
    }

    private void UpdateRows()
    {
        foreach (var row in rowViews)
        {
            row.UpdateMovement();
        }
    }

    private void UpdateTargetRow()
    {
        targetRowView.DisplayObjects(gameModel.TargetObjects);
    }

    public void OnObjectCollected(int objectId)
    {
        var targetObject = gameModel.TargetObjects.Find(x => x.ID == objectId);
        if (targetObject != null && targetObject.Collect())
        {
            if (collectSound != null) collectSound.Play();
            targetRowView.UpdateObjectCount(objectId, targetObject.CurrentCount);

            if (CheckWinCondition())
            {
                if (levelCompleteSound != null) levelCompleteSound.Play();
                gameUIManager.ShowNextLevelPanel(gameModel.CurrentLevel);
            }
        }
    }

    private bool CheckWinCondition()
    {
        return gameModel.TargetObjects.TrueForAll(x => x.CurrentCount == 0);
    }

    public bool IsTargetObject(int objectId)
    {
        return gameModel.TargetObjects.Exists(x => x.ID == objectId);
    }

    public void StartNextLevel()
    {
        RestartGame(); // Call the RestartGame method to reset the game.
    }

    public void RestartGame()
    {
        gameModel.RestartGame();
        InitializeRows();
        UpdateTargetRow();
        gameUIManager.UpdateLevel(gameModel.CurrentLevel);
        gameUIManager.HideNextLevel();
    }

    public void MuteAudio(bool mute)
    {
        AudioListener.pause = mute;
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }
}
