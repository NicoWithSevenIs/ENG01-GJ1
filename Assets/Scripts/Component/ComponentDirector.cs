using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDirector : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] private int Difficulty = 5;
    [SerializeField] private GameObject dropOrigin;

    [SerializeField] private ComponentBuilder builder;
    public ComponentBuilder Builder { get { return builder; } }

    private List<GameObject> list;
    private void Start()
    {
        list = new List<GameObject>();
    }

    [SerializeField] private List<string> correctRecipe;

    [SerializeField] private List<string> incorrectRecipe;

    public void MakeRecipe()
    {

        foreach (var o in list)
            Destroy(o);
        list.Clear();

        correctRecipe.Clear();
        incorrectRecipe.Clear();

        correctRecipe = RecipeGenerator.Instance.CreateRecipe(builder.Blueprint, Difficulty);
        incorrectRecipe = RecipeGenerator.Instance.createIncorrectRecipe(builder.Blueprint, correctRecipe, Difficulty);

        foreach (var componentName in incorrectRecipe)
        {
            Vector3 pos = dropOrigin.transform.position;
            pos.x += Random.Range(-3f, 3f);
            pos.z += Random.Range(-3f, 3f);

            GameObject component = builder.createComponent(componentName, pos);
            component.transform.parent = dropOrigin.transform;
            list.Add(component);
        }

        int childCount = dropOrigin.transform.childCount;

        foreach (var component in list)
        {
            ComponentData data = component.GetComponent<ComponentScript>().Data;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeRecipe();
        }
    }


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
