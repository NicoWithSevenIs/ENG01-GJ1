using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint", menuName = "Component/Component Blueprint", order = 2)]
public class ComponentBlueprint : ScriptableObject
{
    [SerializeField] private List<ComponentBlueprintData> blueprints;
    public List<ComponentBlueprintData> Blueprints { get { return blueprints; } }
}


[Serializable]
public class ComponentBlueprintData
{
    [SerializeField] private GameObject model;
    public GameObject Model { get { return model; } }

    [SerializeField] private ComponentData[] dataList;
    public ComponentData[] DataList { get { return dataList;} }

}
