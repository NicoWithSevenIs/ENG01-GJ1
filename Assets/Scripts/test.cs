using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] internal ComponentBuilder builder;

    [Header("Input")]
    [SerializeField] internal string componentName;

}

#if UNITY_EDITOR
[CustomEditor(typeof(test))]
public class testEditor: Editor
{
    public override void OnInspectorGUI()
    {
        var script = (test)target;

        base.OnInspectorGUI();

        // EditorGUI.BeginChangeCheck();

        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        if(GUILayout.Button("Create Component"))
            script.builder.createComponent(script.componentName, null);
        EditorGUI.EndDisabledGroup();

    }
}
#endif