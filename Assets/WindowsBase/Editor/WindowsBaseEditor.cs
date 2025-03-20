using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WindowsBase))]
public class WindowsBaseEditor : Editor
{
    WindowsBase root;
    private void OnEnable()
    {
        root = (WindowsBase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10);
        if (!root.windowsDefolt.canvasGroup)
        {
            EditorGUILayout.HelpBox("Требуется ссылка на canvasGroup", MessageType.Error);
            if (GUILayout.Button("Setup Windows"))
            {
                root.SetupWindows();
            }
            return;
        }

        if (root.windowsDefolt.canvasGroup.alpha == 0)
        {
            if (GUILayout.Button("SHOW"))
            {
                root.ShowWindows(false);
            }

        }
        else
        {
            if(GUILayout.Button("HIDE"))
            {
                root.HideWindows(false);
            }
        }
    }
}
