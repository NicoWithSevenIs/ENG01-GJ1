using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker
{
    private bool completeMatch;

    public void crosscheckPotionContents(List<GameObject> contents, List<string> correctRecipe)
    {
        
    }


    #region Singleton
    private static RecipeChecker instance = null;

    public static RecipeChecker Instance
    {
        get
        {
            if (instance == null)
                instance = new RecipeChecker();
            return instance;
        }
    }

    private RecipeChecker() { 
        
    }
    private RecipeChecker(RecipeChecker instance) { }

    #endregion
}
