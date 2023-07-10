using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableObjectPRO), true)]
public class SoPRODrawer : OdinEditor
{

    public override void OnInspectorGUI()
    {
        try
        {
            DrawDefaultInspector();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }



        var so = (ScriptableObjectPRO)target;

        if (!so.IsSOPro)
        {
            GUILayout.Space(15);
            EditorGUILayout.HelpBox("Please convert to Scriptable Object PRO !", MessageType.Error);
        }

        if (EditorApplication.isPlaying)
            return;
        
        if (so.IsSOPro) return;
        
        if (GUILayout.Button("Convert to Scriptable Object PRO ✓"))
        {
            so.ConvertToSOPRO();
        }
    }
}