using UnityEngine;

public class ResolutionLocker : MonoBehaviour
{
    // Target resolution
    public int targetWidth = 1080;  // Width in pixels
    public int targetHeight = 1920; // Height in pixels
    public bool fullscreen = false; // Set to true if you want fullscreen

    void Start()
    {
        LockResolution();
    }

    void LockResolution()
    {
        // Set the screen resolution
        Screen.SetResolution(targetWidth, targetHeight, fullscreen);
    }
}
