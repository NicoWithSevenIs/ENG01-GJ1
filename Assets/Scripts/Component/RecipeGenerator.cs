using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        difficulty = Mathf.Clamp(difficulty, 1, 10);

        //the total amount of components needed
        int componentBudget = 5 + Mathf.CeilToInt(1.5f * difficulty);

        int bonusOffset = (int)Mathf.Floor(difficulty / 3);
        int budgetBonus = Random.Range(0, 3 + bonusOffset);

        componentBudget += budgetBonus + bonusOffset;

        //Debug.Log("Budget is: " + componentBudget);

        return budgetedInstantiate(blueprint, componentBudget);
      
    }

    public List<string> budgetedInstantiate(ComponentBlueprint blueprint, int componentBudget)
    {
        List<string> recipe = new List<string>();
        //4 is the highest component budget, will try to find a way to make this not hardcoded if time allows it
        while (componentBudget >= 4)
        {
            ComponentData data = blueprint.getRandomComponentData();
            recipe.Add(data.name);
            componentBudget -= this.getBaseComponentCount(data);
        }

        //if there's remaning budget, roll for a specific tier that amounts to the remaining value
        //Some Level 2 components are size 3. Won't be making a catch case for that.

        if (componentBudget >= 1) //if this conditon is satisfied then the componentBudget must be between 1 and 3
            recipe.Add(blueprint.getRandomComponentDataFromTier(componentBudget - 1).ComponentName);

        return recipe;
    }



    public List<string> createIncorrectRecipe(ComponentBlueprint blueprint, List<string> correctRecipe, int difficulty)
    {
        List<string> newList = new List<string>(correctRecipe);

        newList = rngDivide(blueprint, newList, 40);

        //adds wrong components
        newList.AddRange(
            budgetedInstantiate(blueprint,  Random.Range(1, difficulty + 3)  ) 
        );

        newList = rngMerge(blueprint, newList);


        return newList;
    }

    private List<string> rngDivide(ComponentBlueprint blueprint, List<string> original, int chance)
    {
        
        List<string> newList = new List<string>();

        chance = Mathf.Clamp(chance, 0, 100);

        foreach(var component in original)
        {
            int rng = Random.Range(0, chance);

            if(rng < chance)
            {
                ComponentData componentData = blueprint.getComponentData(component);
                newList.Add(componentData.ComponentA.componentName);
                newList.Add(componentData.ComponentB.componentName);
            }
            else
            {
                newList.Add(component);
            }
        }

        return newList;
    }

    private List<string> rngMerge(ComponentBlueprint blueprint, List<string> original)
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
