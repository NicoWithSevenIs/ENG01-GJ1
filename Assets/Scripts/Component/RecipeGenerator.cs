using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RecipeGenerator {

    //private List<string> _recipes;


    public int getBaseComponentCount(ComponentData component)
    {
        if (component.ComponentA == null || component.ComponentB == null)
            return 1;
        return getBaseComponentCount(component.ComponentA) + getBaseComponentCount(component.ComponentB);
    }


    /*
       Balancing Spreadsheet:
        https://docs.google.com/spreadsheets/d/1UnIKWTCbTy17ZQssfgBspnh3ofPz7Q9hEi2mbXQHXw0/edit?usp=sharing
     */
    public List<string> CreateRecipe(ComponentBlueprint blueprint, int difficulty)
    {

        List<string> recipe = new List<string>();

        difficulty = Mathf.Clamp(difficulty, 1, 10);

        //the total amount of components needed
        int componentBudget = Mathf.CeilToInt(1.5f * difficulty);

        int bonusOffset = (int)Mathf.Floor(difficulty / 3);
        int budgetBonus = Random.Range(0, 3 + bonusOffset);

        componentBudget += budgetBonus + bonusOffset;

        Debug.Log("Budget is:" + componentBudget);

        while(componentBudget >= 4)
        {
            ComponentData data = blueprint.getRandomComponentData();
            recipe.Add(data.name);
            componentBudget -= this.getBaseComponentCount(data);
        }

         //if there's remaning budget, roll for a specific tier that amounts to the remaining value
         //Some Level 2 components are size 3. Won't be making a catch case for that.

        if(componentBudget >= 1) //if this conditon is satisfied then the componentBudget must be between 1 and 3
            recipe.Add(blueprint.getRandomComponentDataFromTier(componentBudget - 1).ComponentName);
        //post processing

        return recipe;
    }


    public List<string> getIncorrectRecipe()
    {
        return null;
    }

    #region Singleton
    private static RecipeGenerator instance = null;

    public static RecipeGenerator Instance
    {
        get
        {
            if(instance == null)
                instance = new RecipeGenerator();
            return instance;
        }
    }

    private RecipeGenerator() { }
    private RecipeGenerator(RecipeGenerator instance) { }

    #endregion


}
