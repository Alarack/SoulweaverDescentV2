using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteTools))]
public class SpriteToolsEditor : Editor
{
    private SpriteTools _spriteTools;

    public override void OnInspectorGUI()
    {
        _spriteTools = (SpriteTools)target;

        //if(GUILayout.Button("Randomize Sprites"))
        //{
        //    _spriteTools.RandomizeSprites();
        //}

        //if (GUILayout.Button("Reset Sprites")) {
        //    _spriteTools.ResetSprites();
        //}

        if (GUILayout.Button("Randomzie")) {
            _spriteTools.RandomizeSprites();
        }

        if (GUILayout.Button("Colorize")) {
            _spriteTools.GetAndColorizeSprites();
        }

        if (GUILayout.Button("Shift Value")) {
            _spriteTools.GetAndShiftSpriteValues();
        }

        //if (GUILayout.Button("Shift Cached Value")) {
        //    _spriteTools.ShiftCachedColorsValue();
        //}

        if (GUILayout.Button("Set Order In Layer")) {
            _spriteTools.GetAndSetSpriteOrder();
        }

        if (GUILayout.Button("Set Layer")) {
            _spriteTools.GetAndSetSpriteLayer();
        }



        base.OnInspectorGUI();



    }


}
