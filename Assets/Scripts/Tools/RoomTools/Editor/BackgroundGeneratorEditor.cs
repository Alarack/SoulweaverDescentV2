using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackgroundGenerator))]
public class BackgroundGeneratorEditor : Editor {

    private BackgroundGenerator _backgroundGenerator;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        _backgroundGenerator = (BackgroundGenerator)target;

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Creating Background Grid", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create BG")) {

            _backgroundGenerator.MakeBG();
        }

        if(GUILayout.Button("Reset Root")) {
            _backgroundGenerator.ResetRoot();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Colorize")) {
            _backgroundGenerator.ColorizeTiles();
        }

        EditorGUILayout.EndHorizontal();

    }


}
