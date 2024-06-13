using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint", menuName = "Component/Component Blueprint", order = 2)]
public class ComponentBlueprint : ScriptableObject
{
    [SerializeField] private List<ComponentBlueprintData> blueprints;
    public List<ComponentBlueprintData> Blueprints { get { return blueprints; } }

   

    public ComponentData getRandomComponentData() 
    {
        Func<int> getTierRandom = () => {

            /*
                Chances:
                    Level 0: 40%
                    Level 1: 35%
                    Level 2: 25%
             */

            int[] breakpoints = {0, 40, 75, 100};
            int rng = UnityEngine.Random.Range(breakpoints[0], breakpoints[breakpoints.Length - 1]);

            int currentTier = 0;
            for(int i = 1; i < breakpoints.Length; i++)
            {
                if (rng >= breakpoints[i - 1] && rng < breakpoints[i])
                    //this could be i-1 but for readability purposes....
                    return currentTier; 
                currentTier++;
            }


            return 0;
        };


        ComponentData[] datalist = blueprints[getTierRandom()].DataList;
        return datalist[UnityEngine.Random.Range(0, datalist.Length)];
    }

    public ComponentData getRandomComponentDataFromTier(int tier)
    {
        return blueprints[tier].DataList[UnityEngine.Random.Range(0, blueprints[tier].DataList.Length)];
    }

    public ComponentData getComponentData(string componentName)
    {
        foreach (var b in this.blueprints)
            foreach (var componentData in b.DataList)
                if (componentData.ComponentName == componentName)
                    return componentData;
            
        return null;
    }

    public ComponentData getComponentFromRecipe(ComponentData ComponentA, ComponentData ComponentB)
    {
        foreach (var b in this.blueprints)
            foreach (var componentData in b.DataList)
            {
                if (ComponentUtilities.getBaseComponentCount(componentData) == 1)
                    continue;

                if (componentData.ComponentA.ComponentName == ComponentA.ComponentName && componentData.ComponentB.ComponentName == ComponentB.ComponentName)
                    return componentData;
            }
                
                

        return null;
    }

    

}


[Serializable]
public class ComponentBlueprintData
{
    [SerializeField] private GameObject model;
    public GameObject Model { get { return model; } }

    [SerializeField] private ComponentData[] dataList;
    public ComponentData[] DataList { get { return dataList;} }

}
