using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTools : MonoBehaviour {
    [Header("Renderers")]
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    [Header("Sprites")]
    public List<Sprite> spriteVariants = new List<Sprite>();
    public List<Color> tints = new List<Color>();



    public void RandomizeSprites() {

        int count = spriteRenderers.Count;
        for (int i = 0; i < count; i++) {
            if (spriteVariants.Count > 0) {
                int randomSpriteIndex = Random.Range(0, spriteVariants.Count);
                spriteRenderers[i].sprite = spriteVariants[randomSpriteIndex];
            }

            if (tints.Count > 0) {
                int randomColorIndex = Random.Range(0, tints.Count);
                spriteRenderers[i].color = tints[randomColorIndex];
            }
        }
    }

    public void ResetSprites() {
        int count = spriteRenderers.Count;
        for (int i = 0; i < count; i++) {

            int randomColorIndex = Random.Range(0, tints.Count);
            spriteRenderers[i].color = Color.white;

        }
    }

}
