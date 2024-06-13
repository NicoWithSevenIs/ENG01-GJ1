using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ComponentScript : MonoBehaviour
{
    [SerializeField] private ComponentData data = null;

    public ComponentData Data { get { return this.data; } set { data = value; } }

    public void SetAppearance()
    {

        if (this.data == null)
            return;

        if (this.data.BodyColor.a > 0)
            this.setMaterialColor("Body", this.data.BodyColor);

        if (this.data.BorderColor.a > 0)
            this.setMaterialColor("Border", this.data.BorderColor);

        if(this.data.AccentColor.a > 0)
            this.setMaterialColor("Border_Accent", this.data.AccentColor);
    }

    private void setMaterialColor(string name, Color c)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        foreach (var mat in meshRenderer.materials)
        {
            if (mat.name == name + " (Instance)")
            {
                mat.color = c;
                break;
            }
        }

    }

}
