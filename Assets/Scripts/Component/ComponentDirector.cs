using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDirector : MonoBehaviour
{
    [Range(0,10)]
    [SerializeField] private int Difficulty = 5;
    [SerializeField] private GameObject dropOrigin;

    [SerializeField] private ComponentBuilder builder;

    private void Start()
    {
        List<string> recipe = RecipeGenerator.Instance.CreateRecipe(builder.Blueprint, Difficulty);


        foreach(var componentName in recipe)
        {
            Vector3 pos = dropOrigin.transform.position;
            pos.x += Random.Range(-3f, 3f);
            pos.z += Random.Range(-3f, 3f);
            builder.createComponent(componentName, pos);
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
