using UnityEngine;
using UnityEditor;
using SimpleSaveSystem;

[CustomEditor(typeof(SaveObject))]
public class SaveObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawButton(target as UnityEngine.Object);

        SaveObject targetScript = (SaveObject)target;
        if (targetScript.SaveFileName == "")
        {
            EditorGUILayout.HelpBox("Save File Name missing", MessageType.Warning);
        }
        if (!targetScript.IsEveryDataNamed())
        {
            EditorGUILayout.HelpBox("At lease one Value Name missing", MessageType.Warning);
        }
    }

    private void DrawButton(UnityEngine.Object target)
    {
        var methodInfos = target.GetType().GetMethods(System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

        foreach (var methodInfo in methodInfos)
        {
            var attributes = methodInfo.GetCustomAttributes(typeof(ButtonAttribute), true);
            if (attributes.Length > 0)
            {
                var buttonAttribute = attributes[0] as ButtonAttribute;
                string label = string.IsNullOrEmpty(buttonAttribute.Label) ?
                    methodInfo.Name : buttonAttribute.Label;

                if (GUILayout.Button(label))
                {
                    methodInfo.Invoke(target, null);
                }
            }
        }
    }
}
