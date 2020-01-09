using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessBlurTestC : MonoBehaviour
{
    [SerializeField]
    public Material postProcessMaterial;

    //method which is automatically called by unity after the camera is done rendering
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //draws the pixels from the source texture to the destination texture
        var temporaryTexture = RenderTexture.GetTemporary(source.width, source.height);
        Graphics.Blit(source, temporaryTexture, postProcessMaterial, 0);
        Graphics.Blit(temporaryTexture, destination, postProcessMaterial, 1);
        RenderTexture.ReleaseTemporary(temporaryTexture);
        Debug.Log("PostProcessBlurTest Confirmed");
    }
}
