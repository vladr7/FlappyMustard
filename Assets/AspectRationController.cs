using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRationController : MonoBehaviour
{
    public float targetAspect = 16.0f / 9.0f; // Set your target aspect ratio here

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        // Calculate the current aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Current viewport height should be scaled by this amount
        float scaleHeight = windowAspect / targetAspect;

        // If scaled height is less than current height, add letterbox
        if (scaleHeight < 1.0f)
        {  
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else // Add pillarbox
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
