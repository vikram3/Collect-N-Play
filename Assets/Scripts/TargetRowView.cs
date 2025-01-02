using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TargetRowView : MonoBehaviour
{
    [SerializeField] private GameObject targetObjectPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private float spacing = 100f; // Space between targets
    [SerializeField] private float startX = 0f;    // Starting X position
    [SerializeField] private float targetSize = 80f; // Size of each target

    private Dictionary<int, TextMeshProUGUI> countTexts = new Dictionary<int, TextMeshProUGUI>();

    public void DisplayObjects(List<CollectibleObjectModel> targetObjects)
    {
        if (targetObjects == null || targetObjectPrefab == null || container == null)
            return;

        ClearTargets();

        // Calculate total width needed
        float totalWidth = (targetObjects.Count * targetSize) + ((targetObjects.Count - 1) * spacing);
        // Calculate start position to center everything
        float currentX = -totalWidth / 2 + (targetSize / 2);

        foreach (var obj in targetObjects)
        {
            GameObject targetObj = Instantiate(targetObjectPrefab, container);

            // Set position
            RectTransform rectTransform = targetObj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(currentX, 0);

            // Set other components
            Image image = targetObj.GetComponent<Image>();
            if (image != null)
            {
                image.sprite = obj.Sprite;
            }

            TextMeshProUGUI countText = targetObj.GetComponentInChildren<TextMeshProUGUI>();
            if (countText != null)
            {
                countText.text = obj.CurrentCount.ToString();
                countTexts[obj.ID] = countText;
            }

            // Move to next position
            currentX += targetSize + spacing;
        }
    }

    public void UpdateObjectCount(int objectId, int count)
    {
        if (countTexts.ContainsKey(objectId))
        {
            countTexts[objectId].text = count.ToString();
        }
    }

    private void ClearTargets()
    {
        if (container == null) return;

        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        countTexts.Clear();
    }

    // Optional: Helper method to adjust layout in editor
    private void OnValidate()
    {
        // Update layout if we change values in inspector
        if (Application.isPlaying)
        {
            DisplayObjects(new List<CollectibleObjectModel>(countTexts.Count));
        }
    }
}