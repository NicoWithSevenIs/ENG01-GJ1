using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeComponentPool : MonoBehaviour
{





    /*
    public GameObject findRecipeComponent(string name)
    {
        for (int i = 0; i < this.recipeComponents.Count; i++)
        {
            if (this.recipeComponents[i].GetComponent<ComponentScript>().Data.ComponentName == name)
            {
                Debug.Log(name + " component has been found");
                return this.recipeComponents[i];
            }
        }

        return null;
    }

    */



    private void Start()
    {
        
    }

    #region singleton

    private static RecipeComponentPool _instance;
    public static RecipeComponentPool Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else Destroy(this);

    }

    #endregion

}
