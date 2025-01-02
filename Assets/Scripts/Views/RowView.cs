using UnityEngine;
using System.Collections.Generic;

public class RowView : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private float objectSpacing = 2f;
    [SerializeField] private float yOffset = 0f; // Vertical position of this row
    [SerializeField] private int numberOfObjects = 12;
    [SerializeField] private ParticleSystem moveParticles;

    private float speed;
    private bool moveRight;
    private float screenWidth;
    private float totalRowWidth;
    private List<GameObject> objects = new List<GameObject>();
    private float leftBoundary;
    private float rightBoundary;

    public void Initialize(float speed, bool moveRight, List<CollectibleObjectModel> availableObjects)
    {
        this.speed = speed;
        this.moveRight = moveRight;

        // Calculate screen boundaries
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        totalRowWidth = (numberOfObjects - 1) * objectSpacing;
        leftBoundary = -screenWidth / 2 - totalRowWidth / 2;
        rightBoundary = screenWidth / 2 + totalRowWidth / 2;

        SpawnObjects(availableObjects);
    }

    public void UpdateMovement()
    {
        float direction = moveRight ? 1 : -1;
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

        // Check if the row needs to wrap around
        foreach (var obj in objects)
        {
            float objWorldX = obj.transform.position.x;

            if (moveRight)
            {
                if (objWorldX > rightBoundary)
                {
                    // Move object to the left side
                    Vector3 newPos = obj.transform.position;
                    newPos.x = leftBoundary + (objWorldX - rightBoundary);
                    obj.transform.position = newPos;
                }
            }
            else
            {
                if (objWorldX < leftBoundary)
                {
                    // Move object to the right side
                    Vector3 newPos = obj.transform.position;
                    newPos.x = rightBoundary - (leftBoundary - objWorldX);
                    obj.transform.position = newPos;
                }
            }
        }
    }

    private void SpawnObjects(List<CollectibleObjectModel> availableObjects)
    {
        ClearObjects();

        // Create a shuffled list of available objects
        List<CollectibleObjectModel> shuffled = new List<CollectibleObjectModel>(availableObjects);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = temp;
        }

        // Calculate starting position
        float startX = moveRight ? leftBoundary : rightBoundary - ((numberOfObjects - 1) * objectSpacing);

        // Create objects
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 position = new Vector3(
                startX + (i * objectSpacing),
                yOffset,
                0
            );

            GameObject obj = Instantiate(collectiblePrefab, position, Quaternion.identity, transform);
            obj.GetComponent<CollectibleObjectView>().Initialize(shuffled[i % shuffled.Count]);
            objects.Add(obj);
        }
    }

    private void ClearObjects()
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }

    // Helper method to visualize boundaries in the editor
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(leftBoundary, yOffset - 0.5f, 0), new Vector3(leftBoundary, yOffset + 0.5f, 0));
            Gizmos.DrawLine(new Vector3(rightBoundary, yOffset - 0.5f, 0), new Vector3(rightBoundary, yOffset + 0.5f, 0));
        }
    }
}