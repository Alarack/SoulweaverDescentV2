using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {



    public TileSettings tileSettings;

    [Header("Transforms")]
    public Transform root;
    public Vector2 rootResetPoint;
    public Transform holder;

    public float xOffSet = 0.25f;


    [Header("Sizes")]

    public int backgroundWidth;
    public int backgroundHeight;
    public int cellWidth = 5;
    public int cellHeight = 5;

    private int[,] filledSpaces;



    [Header("Colors")]
    //public float colorIncrament = 5f;
    public List<Color> colors = new List<Color>();

    [Header("Currently Selected Sprites")]
    public List<SpriteRenderer> currentSprites = new List<SpriteRenderer>();


    private Vector2 startPos;

    [SerializeField]
    private List<GameObject> tileContainers = new List<GameObject>();

    private void Start() {
        startPos = root.localPosition - (Vector3)Vector2.one;
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

    public void ResetRoot() {
        root.localPosition = rootResetPoint;
    }

    private void ClearTiles() {
        int count = tileContainers.Count;
        for (int i = 0; i < count; i++) {
            if (tileContainers[i] == null) {
                tileContainers.Remove(tileContainers[i]);
                continue;
            }
            DestroyImmediate(tileContainers[i].gameObject);
        }

        currentSprites.Clear();
        tileContainers.Clear();
        //ResetRoot();
    }

    public void MakeBG() {
        ResetRoot();
        startPos = root.localPosition - (Vector3)Vector2.one;
        ClearTiles();
        float startY = startPos.y;
        float startX = startPos.x;

        for (int x = 0; x <= backgroundWidth; x++) {
            startPos = new Vector2(startPos.x + (cellWidth / 4f), startPos.y);

            for (int y = 0; y <= backgroundHeight; y++) {

                startPos = new Vector2(startPos.x /*- 0.2085f*/, startPos.y + (cellHeight / 4f));
                GameObject container = FillGrid(startPos);



                if (y.IsOdd())
                    container.transform.localPosition = new Vector3(x * (float)(cellWidth / 4f) + xOffSet, y * (float)(cellHeight / 4f));
                else
                    container.transform.localPosition = new Vector3(x * (float)(cellWidth / 4f), y * (float)(cellHeight / 4f));

            }

            startPos = new Vector2(startPos.x, startY);
        }
    }

    private GameObject FillGrid(Vector2 rootPos) {
        filledSpaces = new int[cellWidth, cellHeight];

        GameObject container = new GameObject();
        container.name = "Tile";
        container.transform.SetParent(root);
        //container.transform.localPosition = rootPos;
        tileContainers.Add(container);
        //root.transform.localPosition = (Vector3)rootPos;

        for (int x = 0; x < cellWidth; x++) {
            for (int y = 0; y < cellHeight; y++) {

                if (CheckGridSpace(x, y) == true) {
                    SpriteProperties targetSprite = GetFittingShape(x, y);

                    if (targetSprite == null) {
                        Debug.LogError("timed out");
                        continue;
                    }

                    FillInSpaces(x, y, (int)(targetSprite.width * 4), (int)(targetSprite.height * 4));
                    GameObject tile = Instantiate(targetSprite.spritePrefab, container.transform) as GameObject;

                    currentSprites.Add(tile.GetComponentInChildren<SpriteRenderer>());
                    Vector2 targetLocation = new Vector2(x / 4f, y / 4f);

                    tile.transform.localPosition = targetLocation;
                    //tile.transform.SetParent(holder, true);

                }
            }
        }

        return container;
    }

    private void FillInSpaces(int xPos, int yPos, int width, int height) {
        for (int x = xPos; x <= xPos + width - 1; x++) {
            for (int y = yPos; y <= yPos + height - 1; y++) {
                if (filledSpaces[x, y] == 1) {
                    Debug.Log(x + "," + y + " is already full");
                }

                filledSpaces[x, y] = 1;
            }
        }
    }

    private SpriteProperties GetFittingShape(int x, int y) {

        SpriteProperties testSprite = null;
        SpriteProperties currentSprite = null;
        int count = 0;
        while (testSprite == null) {

            currentSprite = tileSettings.GetRandomTile();

            if (DoesShapeFitInGrid(x, y, (int)(currentSprite.width * 4), (int)(currentSprite.height * 4)) == true) {
                if (DoesShapeOverlap(x, y, (int)(currentSprite.width * 4), (int)(currentSprite.height * 4)) == false) {
                    testSprite = currentSprite;
                }
                else {
                    count++;
                }

            }
            else {
                count++;
            }

            if (count > 50)
                break;
        }

        return testSprite;
    }

    private bool CheckGridSpace(int x, int y) {
        bool free = filledSpaces[x, y] == 0;

        return free;
    }

    private bool DoesShapeFitInGrid(int xPos, int yPos, int width, int height) {

        for (int x = xPos; x < xPos + width; x++) {
            for (int y = yPos; y < yPos + height; y++) {
                if (x >= this.cellWidth)
                    return false;

                if (y >= this.cellHeight)
                    return false;
            }
        }

        return true;
    }

    private bool DoesShapeOverlap(int xPos, int yPos, int width, int height) {

        for (int x = xPos; x <= xPos + width - 1; x++) {
            for (int y = yPos; y <= yPos + height - 1; y++) {
                if (filledSpaces[x, y] == 1)
                    return true;
            }
        }


        return false;
    }


}











