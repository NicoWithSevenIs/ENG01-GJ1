using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

       


        List<string> impurities = budgetedInstantiate(blueprint, Random.Range(difficulty, difficulty + 3));
        //adds wrong components
        newList.AddRange(impurities);


        string s = " ";
        foreach (var str in impurities)
        {
            s += str + " ";
        }
        Debug.Log("Impurities:" + s);

        newList = rngDivide(blueprint, newList, 40);
        newList = rngMerge(blueprint, newList, 50);


        return newList;
    }

    private List<string> rngDivide(ComponentBlueprint blueprint, List<string> original, int chance)
    {
        
        List<string> newList = new List<string>();

        chance = Mathf.Clamp(chance, 0, 100);

        foreach(var component in original)
        {
            int rng = Random.Range(0, 100);

            if(rng < chance)
            {
                ComponentData componentData = blueprint.getComponentData(component);
                if(getBaseComponentCount(componentData) > 1) {
                    newList.Add(componentData.ComponentA.ComponentName);
                    newList.Add(componentData.ComponentB.ComponentName);
                }
                else newList.Add(component);
                
            }
            else newList.Add(component);
            
        }

        return newList;
    }


    private List<string> rngMerge(ComponentBlueprint blueprint, List<string> original, int chance)
    {

        List<string> copy = new List<string>(original);
        List<string> newList = new List<string>();

        System.Func<int, bool> willMerge = (int chance) =>
        {
            chance = Mathf.Clamp(chance, 0, 100);
            return Random.Range(0, 100) < chance;
        };

        while (copy.Count > 0)
        {

            if (copy.Count == 1)
            {
                newList.Add(copy[0]);
                copy.RemoveAt(0);
                continue;
            }

            //ermm, randomly selects 2 random indices
            List<int> random = new List<int>();
            for (int i = 0; i < copy.Count; i++)
                random.Add(i);

            int IndexA = Random.Range(0, random.Count-1);
            int A = random[IndexA];
            random.RemoveAt(IndexA);

            int indexB = Random.Range(0, random.Count - 1);
            int B = random[indexB];

            //selects 2 random entries in the copied array and attempts to merge them
            ComponentData componentA = blueprint.getComponentData(copy[A]);
            ComponentData componentB = blueprint.getComponentData(copy[B]);

            if (componentA != null && componentB != null)
            {
                ComponentData result = blueprint.getComponentFromRecipe(componentA, componentB);
                if (result != null && willMerge(chance))
                    newList.Add(result.ComponentName);   
            }

            newList.RemoveAt(A);
            newList.RemoveAt(B);

        }

        Debug.Log(newList);
        return newList;
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
