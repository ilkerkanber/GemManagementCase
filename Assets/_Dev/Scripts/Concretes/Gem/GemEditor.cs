using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GemSpawner))]
public class GemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var _gemSpawner = (GemSpawner)target;
        //For new gems
        if (GUILayout.Button("UPDATE GEMS"))
        {
            EventManager.SaveData();
        }
    }
}
