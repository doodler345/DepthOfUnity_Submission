using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlinkingCanvasGroup))]
public class BlinkingCanvasGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BlinkingCanvasGroup targetScript = (BlinkingCanvasGroup)target;
        Progressbar(targetScript.BlinkSpeed);
    }

    private void Progressbar(float blinkSpeed)
    {
        float timer = (float)EditorApplication.timeSinceStartup;
        float progress = (Mathf.Sin(timer * blinkSpeed) + 1) / 2;
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, progress, "Blink Speed");
    }
}
