using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTool : MonoBehaviour
{
    [Header("Size")]
    [Tooltip("The width in unity units for creating rows")]
    public float width;

    [Tooltip("The height in unity units for creating columns")]
    public float height;


    [Header("Colors")]
    public List<Color> colors = new List<Color>();
    


    [Header("Location")]
    public Transform startPosition;


    [Header("Tile Pool")]
    public List<GameObject> tiles = new List<GameObject>();

    [Header("Currently Selected Sprites")]
    public List<SpriteRenderer> currentSprites = new List<SpriteRenderer>();


    private GameObject GetRandomTile() {
        int randomIndex = Random.Range(0, tiles.Count);

        return tiles[randomIndex];
    }


    public void ColorizeTiles() {
        int count = currentSprites.Count;
        for (int i = 0; i < count; i++) {
            int randomColorIndex = Random.Range(0, colors.Count);

            currentSprites[i].color = colors[randomColorIndex];
        }
    }

    public void CreateRow() {
        currentSprites.Clear();

        float totalWidth = 0;

        while (totalWidth < width) {

            GameObject currentTile = Instantiate(GetRandomTile(), transform) as GameObject;
            SpriteRenderer currentSprite = currentTile.GetComponentInChildren<SpriteRenderer>();
            currentSprites.Add(currentSprite);

            float spriteWidth = GetSpriteWidth(currentSprite);

            Vector2 desiredPosition = new Vector2(startPosition.position.x + totalWidth, startPosition.position.y);

            currentTile.transform.localPosition = desiredPosition;
            totalWidth += spriteWidth;

        }


    }




    public float GetSpriteWidth(SpriteRenderer sprite) {
        return sprite.bounds.size.x;
    }

    public float GetSpriteHeight(SpriteRenderer sprite) {
        return sprite.bounds.size.y;
    }

}
