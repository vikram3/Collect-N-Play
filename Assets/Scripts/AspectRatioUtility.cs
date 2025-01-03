using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioUtility : MonoBehaviour
{
    void Start()
    {
        Adjust();
    }

    public void Adjust()
    {
        // Target aspect ratio for portrait (1080x1920 or 9:16)
        float targetAspect = 9.0f / 16.0f;

        // Current screen aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Scale factor to adjust the height or width
        float scaleWidth = windowAspect / targetAspect;

        // Get the camera component
        Camera camera = GetComponent<Camera>();

        if (scaleWidth < 1.0f)
        {
            // Add black bars on the sides
            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
        else
        {
            // Add black bars on the top and bottom
            float scaleHeight = 1.0f / scaleWidth;

            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
    }
}
