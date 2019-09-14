using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile Settings")]
public class TileSettings : ScriptableObject
{
    public List<SpriteProperties> sprites = new List<SpriteProperties>();




    public SpriteProperties GetRandomFittingSprite(CellContainer cellContainer) {
        SpriteProperties result = null;

        //List<SpriteProperties> potentialSprites = new List<SpriteProperties>();
        //int count = sprites.Count;
        //for (int i = 0; i < count; i++) {
        //    if (sprites[i].width <= freeWidth || sprites[i].height <= freeHeight)
        //        potentialSprites.Add(sprites[i]);
        //}

        //if (potentialSprites.Count > 0) {
        //    int randomIndex = Random.Range(0, potentialSprites.Count);
        //    result = potentialSprites[randomIndex];
        //}



        return result;


    }


    public SpriteProperties GetRandomFittingSprite(float freeWidth, float freeHeight) {
        SpriteProperties result = null;

        List<SpriteProperties> potentialSprites = new List<SpriteProperties>();
        int count = sprites.Count;
        for (int i = 0; i < count; i++) {
            if (sprites[i].width <= freeWidth || sprites[i].height <= freeHeight)
                potentialSprites.Add(sprites[i]);
        }

        if(potentialSprites.Count > 0) {
            int randomIndex = Random.Range(0, potentialSprites.Count);
            result = potentialSprites[randomIndex];
        }



        return result;
    }

    public SpriteProperties GetRandomTile() {
        int randomIndex = Random.Range(0, sprites.Count);
        return sprites[randomIndex];
    }
}



[System.Serializable]
public class SpriteProperties {

    public GameObject spritePrefab;
    public string tileName;

    [Header("Size")]
    public float height;
    public float width;


}
