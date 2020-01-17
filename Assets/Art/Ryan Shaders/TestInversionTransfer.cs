using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInversionTransfer : MonoBehaviour
{
    [SerializeField]
    private Material postprocessMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postprocessMaterial);
        Debug.Log("TestLog");
    }

}
