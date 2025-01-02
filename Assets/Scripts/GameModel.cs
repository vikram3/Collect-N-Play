using UnityEngine;
using System.Collections.Generic;

public class GameModel : MonoBehaviour
{
    public float TimeRemaining { get; private set; } = 120f;
    public int CurrentLevel { get; private set; } = 1;
    public bool IsGameOver { get; private set; }
    public List<CollectibleObjectModel> AvailableObjects { get; private set; }
    public List<CollectibleObjectModel> TargetObjects { get; private set; }

    public GameModel(List<Sprite> objectSprites)
    {
        AvailableObjects = new List<CollectibleObjectModel>();
        for (int i = 0; i < objectSprites.Count; i++)
        {
            AvailableObjects.Add(new CollectibleObjectModel(i, objectSprites[i], 12));
        }
        RandomizeTargetObjects();
    }

    public void UpdateTimer(float deltaTime)
    {
        if (!IsGameOver)
        {
            TimeRemaining -= deltaTime;
            if (TimeRemaining <= 0)
            {
                IsGameOver = true;
                TimeRemaining = 0;
            }
        }
    }

    public void RandomizeTargetObjects()
    {
        TargetObjects = new List<CollectibleObjectModel>();
        List<CollectibleObjectModel> shuffled = new List<CollectibleObjectModel>(AvailableObjects);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = temp;
        }

        TargetObjects = shuffled.GetRange(0, 5);
        foreach (var obj in TargetObjects)
        {
            obj.ResetCount();
        }
    }

    public void NextLevel()
    {
        CurrentLevel++;
        TimeRemaining = 120f;
        RandomizeTargetObjects();
        IsGameOver = false;
    }

    public void RestartGame()
    {
        CurrentLevel = 1;
        TimeRemaining = 120f;
        RandomizeTargetObjects();
        IsGameOver = false;
    }
}
