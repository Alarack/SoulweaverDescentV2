using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {
    public TileSettings tileSettings;


    public List<CellContainer> containers = new List<CellContainer>();




}


[System.Serializable]
public class CellContainer {

    public float cellWidth = 1f;
    public float cellHeight = 1f;

    public Dictionary<Vector2, BackgroundCell> cells = new Dictionary<Vector2, BackgroundCell>();

    public List<Vector2> filledSpace = new List<Vector2>();

    public BackgroundCell GetCellByPos(Vector2 pos) {
        BackgroundCell cell = null;

        cells.TryGetValue(pos, out cell);

        return cell;
    }

    public Vector2 FindFirstEmptySpace(BackgroundCell cell) {



        return Vector2.zero;
    }

    public bool CheckCellFit(BackgroundCell cell, Vector2 position) {


        foreach (KeyValuePair<Vector2, BackgroundCell> entry in cells) {

            //if(cell.position.x > entry.Value.position.x)

        }



        return false;
    }

    public bool IsCellIntersecting(BackgroundCell cellA, BackgroundCell cellB) {

        int aXMin = (int)cellA.position.x;
        int aXMax = (int)cellA.Area.x;

        int aYMin = (int)cellA.position.y;
        int aYMax = (int)cellA.Area.y;

        int bXMin = (int)cellB.position.x;
        int bXMax = (int)cellB.Area.x;

        int bYMin = (int)cellB.position.y;
        int bYMax = (int)cellB.Area.y;


        if (bXMin >= aXMin &&
            bYMin >= aYMin &&
            bXMin <= aXMax &&
            bYMin <= aYMax) {
            return true;
        }

        if(bXMax >= aXMin &&
            bYMax >= aYMin) {
            return true;
        }





        return false;
    }


    public void AddCell(BackgroundCell cell) {
        if (filledSpace.Contains(cell.position) || filledSpace.Contains(cell.Area))
            return;

        if (cell.Area.x > cellWidth)
            return;

        if (cell.Area.y > cellHeight)
            return;

        cells.Add(cell.position, cell);
        filledSpace.Add(cell.Area);
    }


    private void AddFilledSpaces(BackgroundCell cell) {
      


    }

}


[System.Serializable]
public class BackgroundCell {
    public Vector2 position;
    public int width;
    public int height;

    public BackgroundCell(Vector2 position, int width, int height) {
        this.position = position;
        this.width = width;
        this.height = height;
    }

    public Vector2 Area { get { return GetArea(); } }
    //public Rect RectArea { get { return GetRect(); } }

    //public RectInt RectIntArea { get { return GetRectInt(); } }

    private Vector2 GetArea() {
        return new Vector2(width, height);
    }

    //private Rect GetRect() {
    //    return new Rect(position, new Vector2(width, height));
    //}

    //private RectInt GetRectInt() {

    //    int x = Mathf.RoundToInt(position.x * 4);
    //    int y = Mathf.RoundToInt(position.y * 4);

    //    int width = Mathf.RoundToInt(this.width * 4);
    //    int height = Mathf.RoundToInt(this.height * 4);

    //    Vector2Int pos = new Vector2Int(x,y);
    //    Vector2Int size = new Vector2Int(width, height);
    //    RectInt rect = new RectInt(pos, size);

    //    return rect;
    //}


}
