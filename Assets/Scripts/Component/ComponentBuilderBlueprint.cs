using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint", menuName = "Component/Component Builder Blueprint", order = 2)]
public class ComponentBuilderBlueprint : ScriptableObject
{
    [SerializeField] private List<ComponentBlueprint> blueprints;
}


[Serializable]
public class ComponentBlueprint
{
    [SerializeField] private GameObject model;
    public GameObject Model { get { return model; } }

    [SerializeField] private ComponentData[] dataList;
    public ComponentData[] DataList { get { return dataList;} }

}
