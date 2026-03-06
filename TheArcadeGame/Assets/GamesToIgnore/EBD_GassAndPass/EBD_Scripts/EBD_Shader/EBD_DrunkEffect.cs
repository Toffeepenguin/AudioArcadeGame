using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_DrunkEffect : MonoBehaviour
{
    public Material material; // Material with the "Drunk" shader

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            // Apply the material (post-processing effect)
            Graphics.Blit(source, destination, material);
        }
        else
        {
            // If the material is missing, just pass through the image
            Graphics.Blit(source, destination);
        }
    }
}
