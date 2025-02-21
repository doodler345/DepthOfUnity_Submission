using UnityEditor;
using UnityEngine;
using GizmoDebug;

[CustomEditor(typeof(GizmoDebugger))]
public class GizmoDebuggerEditor : Editor
{
    SerializedProperty gizmoType;
    SerializedProperty gizmoColor;
    SerializedProperty onlyDrawWhenSelected;
    SerializedProperty originPositionOffset;
    SerializedProperty endPositionOffset;
    SerializedProperty direction;
    SerializedProperty size;
    SerializedProperty radius;

    private void OnEnable()
    {
        gizmoType = serializedObject.FindProperty("gizmoType");
        onlyDrawWhenSelected = serializedObject.FindProperty("onlyDrawWhenSelected");
        gizmoColor = serializedObject.FindProperty("gizmoColor");
        originPositionOffset = serializedObject.FindProperty("originPositionOffset");
        endPositionOffset = serializedObject.FindProperty("endPositionOffset");
        direction = serializedObject.FindProperty("direction");
        size = serializedObject.FindProperty("size");
        radius = serializedObject.FindProperty("radius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(gizmoType, new GUIContent("Gizmo Type"));
        EditorGUILayout.PropertyField(onlyDrawWhenSelected, new GUIContent("Only Draw When Selected"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Gizmo Properties", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(gizmoColor, new GUIContent("Gizmo Color"));
        EditorGUILayout.PropertyField(originPositionOffset, new GUIContent("Origin Offset"));

        if (gizmoType.enumValueIndex == (int)GizmoDebugger.GizmoTypes.Line)
            EditorGUILayout.PropertyField(endPositionOffset, new GUIContent("End Position Offset"));
        
        if (gizmoType.enumValueIndex == (int)GizmoDebugger.GizmoTypes.Ray)
            EditorGUILayout.PropertyField(direction, new GUIContent("Direction"));

        if (gizmoType.enumValueIndex == (int)GizmoDebugger.GizmoTypes.Cube || gizmoType.enumValueIndex == (int)GizmoDebugger.GizmoTypes.WireCube)
            EditorGUILayout.PropertyField(size, new GUIContent("Size"));

        if (gizmoType.enumValueIndex == (int)GizmoDebugger.GizmoTypes.Sphere || gizmoType.enumValueIndex == (int)GizmoDebugger.GizmoTypes.WireSphere)
            EditorGUILayout.PropertyField(radius, new GUIContent("Radius"));

        serializedObject.ApplyModifiedProperties();
    }
}
