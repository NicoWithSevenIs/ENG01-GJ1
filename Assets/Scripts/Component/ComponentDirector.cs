using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDirector : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] private int Difficulty = 5;
    [SerializeField] private GameObject dropOrigin;

    [SerializeField] private ComponentBuilder builder;


    private List<GameObject> list;
    private void Start()
    {
        list = new List<GameObject>();
    }



    public void MakeRecipe()
    {

        foreach (var o in list)
            Destroy(o);
        list.Clear();

        
        List<string> recipe = RecipeGenerator.Instance.CreateRecipe(builder.Blueprint, Difficulty);


        foreach (var componentName in recipe)
        {
            Vector3 pos = dropOrigin.transform.position;
            pos.x += Random.Range(-3f, 3f);
            pos.z += Random.Range(-3f, 3f);
            list.Add(builder.createComponent(componentName, pos));
        }

        int childCount = dropOrigin.transform.childCount;

        int totalBudget = 0;
        foreach (var component in list)
        {
            ComponentData data = component.GetComponent<ComponentScript>().Data;
            totalBudget += RecipeGenerator.Instance.getBaseComponentCount(data);

        }
        Debug.Log("Base Count: " + totalBudget);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeRecipe();
        }
    }






    #region singleton

    private static ComponentDirector instance = null;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else Destroy(this.gameObject);

    }

    #endregion
}
