using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeChecker
{
    private List<string> contentNames;

    public void checkPotionContents(List<GameObject> contents, List<string> correctRecipe)
    {
        this.updateContentNames(contents);

        if (this.contentNames.Count != correctRecipe.Count)
        {
            Debug.Log("lists are not of the same size.");
        }
        else
        {
            bool value = this.crosscheckRecipe(contents, correctRecipe);
            if (value)
            {
                Debug.Log("NO impurities found.");
            }
            else
            {
                Debug.Log("impurities FOUND.");
            }
        }
    }

    private bool crosscheckRecipe(List<GameObject> contents, List<string> correctRecipe)
    {
        List<string> temp_contentNames = new List<string>(this.contentNames);

        for (int i = 0; i < correctRecipe.Count; i++)
        {
            for (int j = 0; j < contents.Count; j++)
            {
                if (temp_contentNames[j] == correctRecipe[i])
                {
                    temp_contentNames.Remove(correctRecipe[i]);
                }
            }
        }

        if (temp_contentNames.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void updateContentNames(List<GameObject> contents)
    {
       this.contentNames.Clear();
        for (int i = 0; i < contents.Count; i++)
        {
            string componentName = contents[i].GetComponent<ComponentScript>().Data.ComponentName;
            this.contentNames.Add(componentName);
        }
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
        this.contentNames = new List<string>();
    }
    private RecipeChecker(RecipeChecker instance) { }

    #endregion
}
