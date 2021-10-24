using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuadTreeTest))]
public class QuadTreeEditor : Editor
{
    private QuadTreeTest quadTree;

    private void OnEnable()
    {
        quadTree = (QuadTreeTest)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            quadTree.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            quadTree.Clear();
        }
    }
}
