using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class posteffectcamerascript : MonoBehaviour
{
    [SerializeField]
    private Material mat;
    void OnRenderImage( RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }

}
