using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SaveObject))]
public class SaveObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveObject targetScript = (SaveObject)target;

        if (targetScript.SaveFileName == "")
        {
            EditorGUILayout.HelpBox("Save File Name missing", MessageType.Warning);
        }

        SaveObjectData saveObjectData = targetScript.saveObjectData;

        if (!targetScript.IsEveryDataNamed())
        {
            EditorGUILayout.HelpBox("At lease one Value Name missing", MessageType.Warning);
        }
    }
}
