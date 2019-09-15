using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileTool))]
public class TileToolEditor : Editor
{
    private TileTool _tileTool;


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        _tileTool = (TileTool)target;

        if(GUILayout.Button("Create Row")) {

            _tileTool.CreateRow();

        }

        if (GUILayout.Button("Colorize Tiles")) {

            _tileTool.ColorizeTiles();

        }


    }


}
