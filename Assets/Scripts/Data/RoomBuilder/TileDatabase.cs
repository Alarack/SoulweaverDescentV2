using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Tile Database")]
public class TileDatabase : ScriptableObject
{

    public List<TileSet> tileSets = new List<TileSet>();


    public List<GameObject> GetTilesByStyleAndLocation(TileSet.TyleStyle style, TileSet.TileLocation location) {
        int count = tileSets.Count;
        for (int i = 0; i < count; i++) {
            if(tileSets[i].style == style) {
                return tileSets[i].GetTilesByLocation(location);
            }
        }

        return null;
    }
    
    public TileCollection GetTileCollectionByStyleAndLocation(TileSet.TyleStyle style, TileSet.TileLocation location) {
        int count = tileSets.Count;
        for (int i = 0; i < count; i++) {
            if (tileSets[i].style == style) {
                return tileSets[i].GetTileCollectionByLocation(location);
            }
        }

        return null;
    }

    public TileSet GetTilesetByStyle(TileSet.TyleStyle style) {
        int count = tileSets.Count;
        for (int i = 0; i < count; i++) {
            if (tileSets[i].style == style) {
                return tileSets[i];
            }
        }

        return null;
    }


}


[System.Serializable]
public class TileSet {

    public enum TyleStyle {
        Temple
    }

    public enum TileLocation {
        Floor,
        Wall,
        Ceiling,
        Background
    }

    public TyleStyle style;
    public List<TileCollection> tileCollections = new List<TileCollection>();


    public List<GameObject> GetTilesByLocation(TileLocation location) {
        int count = tileCollections.Count;
        for (int i = 0; i < count; i++) {
            if (tileCollections[i].location == location)
                return tileCollections[i].tiles;
        }

        return null;
    }

    public TileCollection GetTileCollectionByLocation(TileLocation location) {
        int count = tileCollections.Count;
        for (int i = 0; i < count; i++) {
            if (tileCollections[i].location == location)
                return tileCollections[i];
        }

        return null;
    }


}

[System.Serializable]
public class TileCollection {

    public TileSet.TileLocation location;
    public List<GameObject> tiles = new List<GameObject>();

}
