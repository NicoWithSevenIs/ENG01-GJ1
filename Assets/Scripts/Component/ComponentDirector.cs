using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDirector : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] private int Difficulty = 5;
    [SerializeField] private GameObject dropOrigin;


    [SerializeField] private ComponentBuilder builder;

    [SerializeField] private List<string> correctRecipe;
    [SerializeField] private List<string> incorrectRecipe;


    [Header("Object Pooling")]

    [SerializeField] private List<GameObject> componentPool;
    [SerializeField] private Transform poolableContainer;


    private void Start()
    {
        componentPool = new List<GameObject>();
        if (this.poolableContainer == null)
            throw new System.Exception("Object Pooling needs a container");

    }

    public GameObject MakeComponent(string componentName)
    {
        return builder.createComponent(componentName, null);
    }

    public ComponentBlueprint getBlueprint()
    {
        return builder.Blueprint;
    }

    public void ProcessStage(int difficulty)
    {

        foreach (var component in componentPool)
            Destroy(component);
        componentPool.Clear();

        correctRecipe.Clear();
        incorrectRecipe.Clear();

        correctRecipe = RecipeGenerator.CreateRecipe(builder.Blueprint, difficulty);
        incorrectRecipe = RecipeGenerator.createIncorrectRecipe(builder.Blueprint, correctRecipe, difficulty);

        string s = "";
        foreach(var c in correctRecipe)
        {
            s += c + " ";
        }
        Debug.Log("Correct Recipe: " + s);



        foreach (var componentName in incorrectRecipe)
        {
            Vector3 pos = poolableContainer.transform.position;
            pos.x += Random.Range(-3f, 3f);
            pos.z += Random.Range(-3f, 3f);

            GameObject component = this.getPoolableInstance(componentName, pos);
          
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProcessStage(this.Difficulty);
        }

        if (this.correctRecipe != null)
        {
            RecipeGenerator.checkPotionContents(this.dropOrigin, this.correctRecipe);
        }
    }

    #region ObjectPooling
    public GameObject getPoolableInstance(string componentName)
    {
        GameObject poolable = null;


        foreach (var componentInstance in componentPool)
        {

            ComponentData data = componentInstance.GetComponent<ComponentScript>()?.Data;

            //ignores element if it doesn't have ComponentData attached to it
            if (data == null)
                continue;


            if (data.ComponentName == componentName && !componentInstance.activeInHierarchy)
            {
                poolable = componentInstance;

            }

        }

        if (poolable == null)
        {
            poolable = builder.createComponent(componentName, null);
    
           
            if (poolable != null)
            {
                poolable.transform.parent = poolableContainer;
                componentPool.Add(poolable);
            }
      
        }

        if (poolable != null)
        {
            poolable.SetActive(true);
            poolable.transform.parent = dropOrigin.transform;
        }
            

        return poolable;
    }

    public GameObject getPoolableInstance(string componentName, Vector3 position)
    {
        GameObject poolable = getPoolableInstance(componentName);
        poolable.transform.position = position;
        return poolable;
    }


    public void setPoolableInactive(GameObject instance)
    {
        if (this.componentPool.Contains(instance))
        {
            
            instance.transform.parent = poolableContainer;
            instance.transform.position = poolableContainer.position;
            instance.SetActive(false);
        }
        else
            print("component not found");
           
    }

    public bool isObjectInPool(GameObject instance)
    {
        foreach (var poolable in componentPool)
            if (poolable == instance)
                return true;
        return false;
    }

    #endregion
    #region singleton

    public static ComponentDirector Instance = null;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else Destroy(this.gameObject);

    }

    #endregion
}
