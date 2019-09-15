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

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Creating Foreground Rows and Columns", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Create Row")) {

            _tileTool.CreateRow();

        }

        if (GUILayout.Button("Create Column")) {

            _tileTool.CreateColumn();

        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Recent Creation Tools", EditorStyles.boldLabel);

        if (GUILayout.Button("Delete")) {

            _tileTool.DeleteRecent();
        }

        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Colorize")) {
            _tileTool.ColorizeTiles();
        }

        if (GUILayout.Button("FlipX")) {
            _tileTool.FlipSpritesX();
        }

        if (GUILayout.Button("FlipY")) {
            _tileTool.FlipSpritesY();
        }

        EditorGUILayout.EndHorizontal();


    }


}
