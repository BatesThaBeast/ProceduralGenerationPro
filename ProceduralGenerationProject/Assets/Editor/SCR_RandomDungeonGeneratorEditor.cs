using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SCR_AbstractDungeonGenerator), true)]
public class SCR_RandomDungeonGeneratorEditor : Editor
{
    SCR_AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (SCR_AbstractDungeonGenerator)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
        if (GUILayout.Button("Erase Dungeon"))
        {
            generator.ClearDungeon();
        }
    }
}
