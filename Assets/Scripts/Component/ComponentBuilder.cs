using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class ComponentBuilder 
{

    [SerializeField] private ComponentBlueprint componentBlueprint = null;
    [SerializeField] private GameObject container;

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

                    //attach the necessary scripts here
                    ComponentScript script = component.AddComponent<ComponentScript>();
                    script.Data= componentData;
                    script.SetAppearance();


                    script.AddComponent<BoxCollider>();
                    script.AddComponent<Rigidbody>();

                    if(container != null)
                        component.transform.parent = container.transform;

                    break;
                }
            }
        }


        return component;
    }




}
