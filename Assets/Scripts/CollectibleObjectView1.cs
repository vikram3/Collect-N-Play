using UnityEngine;

[System.Serializable]
public class CollectibleObjectModel
{
    public int ID { get; private set; }
    public Sprite Sprite { get; private set; }
    public int RequiredCount { get; private set; }
    public int CurrentCount { get; private set; }

    public CollectibleObjectModel(int id, Sprite sprite, int requiredCount)
    {
        ID = id;
        Sprite = sprite;
        RequiredCount = requiredCount;
        ResetCount();
    }

    public void ResetCount()
    {
        CurrentCount = RequiredCount;
    }

    public bool Collect()
    {
        if (CurrentCount > 0)
        {
            CurrentCount--;
            return true;
        }
        return false;
    }
}
