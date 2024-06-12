using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker : MonoBehaviour
{
    // as there could be more than one component in the list of gameobjects or potion contents, we have to make sure the count matches that of the correct recipe.
    public static RecipeChecker Instance = null;

    [SerializeField] private Dictionary<string, int> potionCount;
    [SerializeField] private Dictionary<string, int> recipeCount;
    [SerializeField] private List<string> componentNames;

    private List<GameObject> potionContents;

    public List<GameObject> PotionContents
    {
        get { return this.potionContents; }
        set { this.potionContents = value; }
    }

    private List<string> recipe;
    public List<string> Recipe
    {
        get { return this.recipe; }
        set { this.recipe = value; }
    }

    private bool completeMatch;


    public void countPotionComponents()
    {
        //this.resetCounts();
        if (this.recipe.Count > 0 && this.potionContents.Count > 0)
        {
            for (int i = 0; i < recipe.Count; i++)
            {
                Debug.Log("Count: " + i + " " + this.recipe[i]);
                Debug.Log("num: " + this.recipeCount["Cyan"]);

            }

            //for (int i = 0; i < potionContents.Count; i++)
            //{
            //    this.potionCount[potionContents[i].GetComponent<ComponentScript>().Data.ComponentName] += 1;
            //}
        }

        //this.checkIfMatch();
        //this.processPotionComplete();
    }

    private void resetCounts()
    {
        for (int i = 0; i < this.componentNames.Count; i++)
        {
            this.recipeCount[this.componentNames[i]] = 0;
            this.potionCount[this.componentNames[i]] = 0;
        }
    }

    private void checkIfMatch()
    {
        int nIndex = -1;

        for (int i = 0; i < this.componentNames.Count; i++)
        {
            if (this.recipeCount[this.componentNames[i]] != this.potionCount[this.componentNames[i]])
            {
                nIndex = i;
            }
        }


        if (nIndex != -1)
        {
            this.completeMatch = false;
        }
        else
        {
            this.completeMatch = true;
        }
    }

    private void Start()
    {
        List<GameObject> components = RecipeComponentPool.Instance.RecipeComponents;
        for (int i = 0; i < components.Count; i++)
        {
            this.componentNames.Add(components[i].GetComponent<ComponentScript>().Data.ComponentName);
        }
    }

    private void processPotionComplete()
    {
        if (this.completeMatch)
        {
            Debug.Log("potion fixed.");
        }
        else
        {
            Debug.Log("impurities found.");
        }
    }

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
