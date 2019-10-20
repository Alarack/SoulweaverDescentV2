using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile Settings")]
public class TileSettings : ScriptableObject
{
    public List<SpriteProperties> sprites = new List<SpriteProperties>();


    public SpriteProperties GetRandomTile() {

        List<SpriteProperties> possibleTiles = new List<SpriteProperties>();

        int count = sprites.Count;
        for (int i = 0; i < count; i++) {
            if (sprites[i].disable == false)
                possibleTiles.Add(sprites[i]);
        }


        int randomIndex = Random.Range(0, possibleTiles.Count);
        return sprites[randomIndex];
    }
}



[System.Serializable]
public class SpriteProperties {

    public GameObject spritePrefab;
    public string tileName;
    public bool disable;

    [Header("Size")]
    public float height;
    public float width;


}
