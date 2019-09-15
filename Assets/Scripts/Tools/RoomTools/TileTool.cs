using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTool : MonoBehaviour {
    [Header("Size")]
    [Tooltip("The width in unity units for creating rows")]
    public float width;

    [Tooltip("The height in unity units for creating columns")]
    public float height;

    [Header("BG Tile Info")]
    public int gridWidth;
    public int gridHeight;
    public float gridUnit = 0.25f;

    [Header("Colors")]
    public List<Color> colors = new List<Color>();



    [Header("Location")]
    public Transform startPosition;
    public Transform tileContainer;


    [Header("Tile Pool")]
    public TileDatabase tileDatabase;
    public TileSet.TyleStyle tileStyle;
    public TileSet.TileLocation tileLocaiton;


    private TileCollection currentTileCollection;
    private TileSet currentTileSet;

    [Header("Currently Selected Sprites")]
    public List<SpriteRenderer> currentSprites = new List<SpriteRenderer>();


    private GameObject GetRandomTile() {
        GetTargetSet();
        int randomIndex = Random.Range(0, currentTileCollection.tiles.Count);
        return currentTileCollection.tiles[randomIndex];
    }

    private SpriteRenderer GetRandomSprite() {
        return GetRandomTile().GetComponentInChildren<SpriteRenderer>();
    }

    private void GetTargetSet() {

        if (currentTileSet == null || currentTileSet.style != tileStyle || currentTileSet.tileCollections.Count < 1) {
            currentTileSet = tileDatabase.GetTilesetByStyle(tileStyle);
        }

        if (currentTileCollection == null || currentTileCollection.location != tileLocaiton || currentTileCollection.tiles.Count < 1) {
            currentTileCollection = currentTileSet.GetTileCollectionByLocation(tileLocaiton);
        }
    }

    public void ColorizeTiles() {
        int count = currentSprites.Count;
        for (int i = 0; i < count; i++) {

            if (currentSprites[i] == null)
                continue;

            int randomColorIndex = Random.Range(0, colors.Count);

            currentSprites[i].color = colors[randomColorIndex];
        }
    }

    public void CreateRow() {
        currentSprites.Clear();
        float totalWidth = 0f;
        Transform container = tileContainer == null ? transform : tileContainer;

        while (totalWidth < width) {
            GameObject currentTile = Instantiate(GetRandomTile()) as GameObject;
            SpriteRenderer currentSprite = currentTile.GetComponentInChildren<SpriteRenderer>();
            currentSprites.Add(currentSprite);

            float spriteWidth = GetSpriteWidth(currentSprite);

            //Debug.Log(spriteWidth + " is the width of " + currentTile);

            Vector2 desiredPosition = new Vector2(startPosition.position.x + totalWidth, startPosition.position.y);

            currentTile.transform.localPosition = desiredPosition;
            currentTile.transform.SetParent(container, true);

            totalWidth += spriteWidth;
        }
    }

    public void CreateColumn() {
        currentSprites.Clear();
        float totalHeight = 0f;
        Transform container = tileContainer == null ? transform : tileContainer;

        while (totalHeight < height) {
            GameObject currentTile = Instantiate(GetRandomTile()) as GameObject;
            SpriteRenderer currentSprite = currentTile.GetComponentInChildren<SpriteRenderer>();
            currentSprites.Add(currentSprite);

            float spriteHeight = GetSpriteHeight(currentSprite);

            //Debug.Log(spriteHeight + " is the height of " + currentTile);

            Vector2 desiredPosition = new Vector2(startPosition.position.x, startPosition.position.y + totalHeight);

            currentTile.transform.localPosition = desiredPosition;
            currentTile.transform.SetParent(container, true);

            totalHeight += spriteHeight;
        }
    }

    public void CreateBackgroundArea() {
        currentSprites.Clear();
        float totalHeight = 0f;
        float totalWidth = 0f;

        Transform container = tileContainer == null ? transform : tileContainer;

        Dictionary<Vector2, bool> filledSpaces = new Dictionary<Vector2, bool>();

        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {

                TileInfo tile = CreateTile();

                float spriteHeight = GetSpriteHeight(tile.sprite);
                float spriteWidth = GetSpriteWidth(tile.sprite);

                Vector2 tileSize = new Vector2(spriteWidth * (1/gridUnit), spriteHeight * (1/gridUnit));


                Vector2 desiredPosition = new Vector2(startPosition.position.x + (gridUnit * x), startPosition.position.y + (gridUnit * y));

            }
        }
        

        






        


    }

    private TileInfo CreateTile() {
        GameObject currentTile = Instantiate(GetRandomTile()) as GameObject;
        SpriteRenderer currentSprite = currentTile.GetComponentInChildren<SpriteRenderer>();
        currentSprites.Add(currentSprite);

        TileInfo result = new TileInfo(currentTile, currentSprite);

        return result;
    }


    public void DeleteRecent() {
        int count = currentSprites.Count;
        for (int i = count - 1; i >= 0; i--) {
            DestroyImmediate(currentSprites[i].transform.parent.gameObject);
            currentSprites.RemoveAt(i);
        }
    }

    public void FlipSpritesX() {
        int count = currentSprites.Count;
        for (int i = count - 1; i >= 0; i--) {
            currentSprites[i].flipX = !currentSprites[i].flipX;
        }
    }

    public void FlipSpritesY() {
        int count = currentSprites.Count;
        for (int i = count - 1; i >= 0; i--) {
            currentSprites[i].flipY = !currentSprites[i].flipY;
        }
    }

    public float GetSpriteWidth(SpriteRenderer sprite) {
        return sprite.bounds.size.x;
    }

    public float GetSpriteHeight(SpriteRenderer sprite) {
        return sprite.bounds.size.y;
    }

}


public class TileInfo {
    public GameObject root;
    public SpriteRenderer sprite;

    public TileInfo(GameObject root, SpriteRenderer sprite) {
        this.root = root;
        this.sprite = sprite;
    }
}
