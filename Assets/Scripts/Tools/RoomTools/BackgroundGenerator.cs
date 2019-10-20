using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {



    public TileSettings tileSettings;

    [Header("Transforms")]
    public Transform root;
    public Transform holder;


    [Header("Sizes")]

    public int backgroundWidth;
    public int backgroundHeight;
    public int cellWidth = 4;
    public int cellHeight = 4;

    private int[,] filledSpaces;



    [Header("Colors")]
    //public float colorIncrament = 5f;
    public List<Color> colors = new List<Color>();

    [Header("Currently Selected Sprites")]
    public List<SpriteRenderer> currentSprites = new List<SpriteRenderer>();


    private Vector2 startPos;

    private void Start() {
        startPos = root.localPosition - (Vector3)Vector2.one;
    }


    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.G)) {
    //        //FillGrid();
    //        //root.transform.localPosition += (Vector3)Vector2.right;      

    //        MakeBG();
    //    }
    //}

    public void ColorizeTiles() {
        int count = currentSprites.Count;
        for (int i = 0; i < count; i++) {

            if (currentSprites[i] == null)
                continue;

            int randomColorIndex = Random.Range(0, colors.Count);

            currentSprites[i].color = colors[randomColorIndex];
        }
    }


    public void MakeBG() {
        startPos = root.localPosition - (Vector3)Vector2.one;
        currentSprites.Clear();
        float startY = startPos.y;

        for (int x = 0; x <= backgroundWidth; x++) {
            startPos = new Vector2(startPos.x + 1, startPos.y);

            for (int y = 0; y <= backgroundHeight; y++) {
                startPos = new Vector2(startPos.x, startPos.y + 1);

                FillGrid(startPos);
            }

            startPos = new Vector2(startPos.x, startY);
        }
    }

    private void FillGrid(Vector2 rootPos) {
        filledSpaces = new int[cellWidth, cellHeight];

        root.transform.localPosition = (Vector3)rootPos;

        for (int x = 0; x < cellWidth; x++) {
            for (int y = 0; y < cellHeight; y++) {

                if (CheckGridSpace(x, y) == true) {
                    SpriteProperties targetSprite = GetFittingShape(x, y);

                    if (targetSprite == null) {
                        Debug.LogError("timed out");
                        continue;
                    }

                    FillInSpaces(x, y, (int)(targetSprite.width * 4), (int)(targetSprite.height * 4));
                    GameObject tile = Instantiate(targetSprite.spritePrefab, root) as GameObject;

                    currentSprites.Add(tile.GetComponentInChildren<SpriteRenderer>());
                    Vector2 targetLocation = new Vector2(x / 4f, y / 4f);

                    tile.transform.localPosition = targetLocation;
                    tile.transform.SetParent(holder, true);

                }
            }
        }

        
    }

    private void FillInSpaces(int xPos, int yPos, int width, int height) {
        for (int x = xPos; x <= xPos + width - 1; x++) {
            for (int y = yPos; y <= yPos + height - 1; y++) {
                if(filledSpaces[x,y] == 1) {
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











