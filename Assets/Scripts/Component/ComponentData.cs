using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;


[CreateAssetMenu(fileName = "Component Data", menuName = "Component/Component Data", order = 1)]
public class ComponentData : ScriptableObject
{

    [SerializeField] internal string componentName;
    public string ComponentName { get { return this.componentName; } }

    [SerializeField] internal Color bodyColor = Color.black;
    public Color BodyColor { get { return this.bodyColor; } } 

    [SerializeField] internal Color borderColor = Color.black;
    public Color BorderColor { get { return this.borderColor; } }

    [SerializeField] internal bool isBuildable = false;

    public bool IsBuildable { get { return this.isBuildable; } }

   
    [SerializeField] internal Color accentColor = new Color(53f / 255f, 53f / 255f, 53f / 255f);
    public Color AccentColor { get { return this.accentColor; } }


    [SerializeField] internal ComponentData componentA;
    public ComponentData ComponentA { get { return this.componentA; } }

    [SerializeField] internal ComponentData componentB;
    public ComponentData ComponentB { get { return this.componentB; } }

}

#if UNITY_EDITOR 
[CustomEditor(typeof(ComponentData))]
public class ComponentData_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        var script = (ComponentData)target;

        string path = UnityEditor.AssetDatabase.GetAssetPath(script);

        EditorGUI.BeginDisabledGroup(true);
        script.componentName = EditorGUILayout.TextField("Component Name", Path.GetFileNameWithoutExtension(path));
        EditorGUI.EndDisabledGroup();

        script.bodyColor = EditorGUILayout.ColorField("Body Color", script.bodyColor);
        script.borderColor = EditorGUILayout.ColorField("Border Color", script.borderColor);
        script.isBuildable = EditorGUILayout.Toggle("Is Buildable", script.isBuildable);

        if (script.isBuildable)
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
            GUILayout.Label("Recipe");

            script.accentColor = EditorGUILayout.ColorField("Accent Color", script.accentColor);
            script.componentA = EditorGUILayout.ObjectField("Component A", script.componentA, typeof(ComponentData), false) as ComponentData;
            script.componentB = EditorGUILayout.ObjectField("Component B", script.componentB, typeof(ComponentData), false) as ComponentData;
        }
        else
        {
            script.accentColor = new Color(53f / 255f, 53f / 255f, 53f / 255f);
            script.componentA = null;
            script.componentB = null;
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(script);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
