using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteTools : MonoBehaviour {
    //[Header("Renderers")]
    //public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    [Header("Sprites")]
    public List<Sprite> spriteVariants = new List<Sprite>();

    [Header("Colors")]
    public List<Color> tints = new List<Color>();

    [Header("Layer Tweaks")]
    public int targetSortOrder;
    public string targetLayerName;

    [Header("Color Value Tools")]
    [Range(-0.5f, 0.5f)]
    public float colorIncrement;


    //public void RandomizeSprites() {

    //    int count = spriteRenderers.Count;
    //    for (int i = 0; i < count; i++) {
    //        if (spriteVariants.Count > 0) {
    //            int randomSpriteIndex = Random.Range(0, spriteVariants.Count);
    //            spriteRenderers[i].sprite = spriteVariants[randomSpriteIndex];
    //        }

    //        if (tints.Count > 0) {
    //            int randomColorIndex = Random.Range(0, tints.Count);
    //            spriteRenderers[i].color = tints[randomColorIndex];
    //        }
    //    }
    //}

    //public void ResetSprites() {
    //    int count = spriteRenderers.Count;
    //    for (int i = 0; i < count; i++) {

    //        int randomColorIndex = Random.Range(0, tints.Count);
    //        spriteRenderers[i].color = Color.white;

    //    }
    //}

    public void GetAndColorizeSprites() {
        SpriteToolsExtended.Colorize(GetRenderers(), tints);
    }

    public void GetAndShiftSpriteValues() {
        SpriteToolsExtended.ShiftColorValue(GetRenderers(), colorIncrement);
    }

    public void GetAndSetSpriteOrder() {
        SpriteToolsExtended.SetSpriteSortingOrder(GetRenderers(), targetSortOrder);
    }

    public void GetAndSetSpriteLayer() {
        SpriteToolsExtended.SetSpriteSortingLayer(GetRenderers(), targetLayerName);
    }

    public void ShiftCachedColorsValue() {
        SpriteToolsExtended.ShiftColorValue(tints, colorIncrement);
    }

    public void RandomizeSprites() {

        if(spriteVariants.Count == 0) {
            Debug.LogError("[Sprite Tools] Sprite Variants have not been set in the inspector");
            return;
        }

        SpriteToolsExtended.RandomizeSprites(GetRenderers(), spriteVariants);
    }



    private List<SpriteRenderer> GetRenderers() {
        return GetComponentsInChildren<SpriteRenderer>().ToList();
    }

}


public static class SpriteToolsExtended {


    public static void Colorize(List<SpriteRenderer> sprites, List<Color> colors) {
        int count = sprites.Count;
        for (int i = 0; i < count; i++) {

            if (sprites[i] == null)
                continue;

            int randomColorIndex = Random.Range(0, colors.Count);

            sprites[i].color = colors[randomColorIndex];
        }
    }

    public static void SetSpriteSortingOrder(List<SpriteRenderer> sprites, int orderInLayer) {
        int count = sprites.Count;
        for (int i = 0; i < count; i++) {
            if (sprites[i] == null)
                continue;

            sprites[i].sortingOrder = orderInLayer;
        }
    }

    public static void SetSpriteSortingLayer(List<SpriteRenderer> sprites, string sortingLanyerName) {
        int count = sprites.Count;
        for (int i = 0; i < count; i++) {
            if (sprites[i] == null)
                continue;

            sprites[i].sortingLayerName = sortingLanyerName;
        }
    }


    public static void ShiftColorValue(List<SpriteRenderer> sprites, float colorIncrement) {
        int count = sprites.Count;
        for (int i = 0; i < count; i++) {

            //if (sprites[i] == null)
            //    continue;

            //SpriteRenderer current = sprites[i];
            Color newColor = new Color(sprites[i].color.r + colorIncrement, sprites[i].color.g + colorIncrement, sprites[i].color.b + colorIncrement, sprites[i].color.a);

            sprites[i].color = newColor;
        }
    }

    public static void ShiftColorValue(List<Color> colors, float colorIncrement) {
        int count = colors.Count;
        for (int i = 0; i < count; i++) {

            Color newColor = new Color(colors[i].r + colorIncrement, colors[i].g + colorIncrement, colors[i].b + colorIncrement, colors[i].a);

            colors[i] = newColor;
        }
    }

    public static void RandomizeSprites(List<SpriteRenderer> renderers, List<Sprite> variants) {

        int count = renderers.Count;
        for (int i = 0; i < count; i++) {
            float width = renderers[i].sprite.bounds.size.x;
            float height = renderers[i].sprite.bounds.size.y;

            Sprite randomSprite = GetRandomSpriteOfSameSize(width, height, variants);

            if (randomSprite == null)
                continue;
            else {
                renderers[i].sprite = randomSprite;
            }


        }

    }

    public static Sprite GetRandomSpriteOfSameSize(float width, float height, List<Sprite> sprites) {

        sprites.Shuffle();

        int count = sprites.Count;
        for (int i = 0; i < count; i++) {
            float curWidth = sprites[i].bounds.size.x;
            float curHeight = sprites[i].bounds.size.y;

            if (curWidth == width && curHeight == height)
                return sprites[i];

        }

        return null;

    }


}
