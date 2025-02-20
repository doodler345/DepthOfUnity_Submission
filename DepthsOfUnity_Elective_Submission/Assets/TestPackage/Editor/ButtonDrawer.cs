#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using SimpleSaveSystem;

[CustomEditor(typeof(Object), true)]
public class ButtonDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var monoTarget = target as MonoBehaviour;
        if (monoTarget != null) DrawButton(monoTarget);

        var scriptableTarget = target as ScriptableObject;
        if (scriptableTarget != null) DrawButton(scriptableTarget);
    }

    private void DrawButton(Object target)
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
#endif