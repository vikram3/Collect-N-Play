using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool moveRight = true;
    [SerializeField] private SpriteRenderer[] backgrounds;  // Array of background sprites

    private float backgroundWidth;
    private float leftBoundary;
    private float rightBoundary;

    private void Start()
    {
        if (backgrounds.Length == 0) return;

        // Get the width of a single background
        backgroundWidth = backgrounds[0].bounds.size.x;

        // Calculate boundaries
        float totalWidth = backgroundWidth * backgrounds.Length;
        leftBoundary = -totalWidth / 2;
        rightBoundary = totalWidth / 2;

        // Position backgrounds side by side
        PositionBackgrounds();
    }

    private void PositionBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float xPosition = leftBoundary + (backgroundWidth * i);
            backgrounds[i].transform.position = new Vector3(xPosition, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
        }
    }

    private void Update()
    {
        float direction = moveRight ? 1 : -1;
        float movement = speed * Time.deltaTime * direction;

        // Move all backgrounds
        foreach (var bg in backgrounds)
        {
            bg.transform.Translate(new Vector3(movement, 0, 0));

            // Check if background needs to wrap around
            if (moveRight)
            {
                if (bg.transform.position.x >= rightBoundary)
                {
                    // Move to the left side
                    float excess = bg.transform.position.x - rightBoundary;
                    bg.transform.position = new Vector3(leftBoundary + excess, bg.transform.position.y, bg.transform.position.z);
                }
            }
            else
            {
                if (bg.transform.position.x <= leftBoundary)
                {
                    // Move to the right side
                    float excess = leftBoundary - bg.transform.position.x;
                    bg.transform.position = new Vector3(rightBoundary - excess, bg.transform.position.y, bg.transform.position.z);
                }
            }
        }
    }
}