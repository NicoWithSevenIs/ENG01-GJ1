using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class ComponentBuilder 
{

    [SerializeField] private ComponentBlueprint componentBlueprint = null;

    public ComponentBlueprint Blueprint { get { return componentBlueprint; } }


    public ComponentBuilder(ComponentBlueprint blueprint) { this.AssignBlueprint(blueprint); }
    public void AssignBlueprint(ComponentBlueprint blueprint) { this.componentBlueprint = blueprint; }


    public GameObject createComponent(string componentName, Vector3? position)
    {

        if (componentBlueprint == null)
            return null;

        GameObject component = null;

        
        foreach (var b in componentBlueprint.Blueprints)
        {
            foreach (var componentData in b.DataList)
            {
                if(componentData.ComponentName == componentName)
                {
        
                    component = GameObject.Instantiate(b.Model);
                    component.name = componentData.componentName;

                    if(position != null)
                        component.transform.position = position.Value;


                    //attach the necessary scripts here
                    ComponentScript script = component.AddComponent<ComponentScript>();
                    script.Data = componentData;
                    script.SetAppearance();


                    component.AddComponent<BoxCollider>();
                    component.AddComponent<Rigidbody>();

                    component.AddComponent<ComponentDragDrop>();
                 
              
                    break;
                }
            }
        }


        return component;
    }




}
