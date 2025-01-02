using UnityEngine;
using System.Collections.Generic;

public class GameViewModel : MonoBehaviour
{
    [SerializeField] private List<Sprite> objectSprites;
    [SerializeField] private RowView[] rowViews;
    [SerializeField] private TargetRowView targetRowView;
    [SerializeField] private UIView uiView;
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
        uiView.UpdateLevel(gameModel.CurrentLevel);
    }

    private void Update()
    {
        if (!gameModel.IsGameOver)
        {
            gameModel.UpdateTimer(Time.deltaTime);
            UpdateRows();
            uiView.UpdateTimer(gameModel.TimeRemaining);

            if (gameModel.TimeRemaining <= 0)
            {
                uiView.ShowGameOver();
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
                uiView.ShowNextLevel();
            }
        }
    }

    private bool CheckWinCondition()
    {
        return gameModel.TargetObjects.TrueForAll(x => x.CurrentCount == 0);
    }

    public void StartNextLevel()
    {
        gameModel.NextLevel();
        InitializeRows();
        UpdateTargetRow();
        uiView.HideNextLevel();
        uiView.UpdateLevel(gameModel.CurrentLevel);
    }

    public void RestartGame()
    {
        gameModel.RestartGame();
        InitializeRows();
        UpdateTargetRow();
        uiView.UpdateLevel(gameModel.CurrentLevel);
    }
}