using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;


[CreateAssetMenu(fileName = "Component Data", menuName = "Component/Component Data", order = 1)]
public class ComponentData : ScriptableObject
{

    [SerializeField] private string componentName;
    public string ComponentName { get { return this.componentName; } }

    [SerializeField] private Color bodyColor = Color.black;
    public Color BodyColor { get { return this.bodyColor; } } 

    [SerializeField] private Color borderColor = Color.black;
    public Color BorderColor { get { return this.borderColor; } }

    internal Color accentColor = new Color(53f / 255f, 53f / 255f, 53f / 255f);
    public Color AccentColor { get { return this.accentColor; } }

    [SerializeField] internal bool isBuildable = false;

    public bool IsBuildable { get { return this.isBuildable; } }

    internal ComponentData componentA;
    public ComponentData ComponentA { get { return this.componentA; } }

    internal ComponentData componentB;
    public ComponentData ComponentB { get { return this.componentB; } }


  
   
 

}

[CustomEditor(typeof(ComponentData))]
public class ComponentData_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        var script = (ComponentData)target;

        base.OnInspectorGUI();


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
            AssetDatabase.SaveAssets();
        

    }
}

