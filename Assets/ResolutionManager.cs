using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Start()
    {
        // Set the resolution to 1080x1920 in portrait mode
        Screen.SetResolution(1080, 1920, true);

        // Ensure the aspect ratio is maintained
        if (Screen.height < Screen.width)
        {
            Screen.SetResolution(1920, 1080, true); // Force portrait orientation
        }
    }
}
