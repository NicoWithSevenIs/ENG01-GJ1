using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    int componentCount = 0;

    [SerializeField]
    List<Sprite> sheetSizes = new List<Sprite>();

    [SerializeField]
    GameObject recipeTab;

    [SerializeField]
    GameObject recipe;

    [SerializeField]
    GameObject recipeSheet;

    [SerializeField]
    GameObject recipeCloseSheet;

    [SerializeField]
    List<GameObject> componentDisplays = new List<GameObject>();

    public void ShowRecipe()
    {
        UpdateSheetSize();
        HideRecipeTab();
        LeanTween
            .moveX(recipe, recipe.transform.position.x + 220.0f, 0.2f)
            .setOnComplete(ShowCloseTab);
    }

    public void hideRecipe()
    {
        ShowRecipeTab();
        LeanTween
            .moveX(recipe, recipe.transform.position.x - 220.0f, 0.2f)
            .setOnComplete(HideCloseTab);
    }

    void ShowCloseTab()
    {
        LeanTween.moveX(recipeCloseSheet, recipeCloseSheet.transform.position.x + 80.0f, 0.2f);
    }

    void HideCloseTab()
    {
        LeanTween.moveX(recipeCloseSheet, recipeCloseSheet.transform.position.x - 80.0f, 0.2f);
    }

    void ShowRecipeTab()
    {
        LeanTween.moveY(recipeTab, recipeTab.transform.position.y - 80.0f, 0.2f);
    }

    void HideRecipeTab()
    {
        LeanTween.moveY(recipeTab, recipeTab.transform.position.y + 80.0f, 0.2f);
    }

    void UpdateSheetSize()
    {
        foreach (GameObject componentDisplay in componentDisplays)
        {
            componentDisplay.SetActive(true);
        }
        if (componentCount <= 6)
        {
            recipeSheet.GetComponent<Image>().overrideSprite = sheetSizes[2];
            if (componentCount <= 4)
            {
                recipeSheet.GetComponent<Image>().overrideSprite = sheetSizes[1];
                componentDisplays[4].SetActive(false);
                componentDisplays[5].SetActive(false);
                if (componentCount <= 2)
                {
                    recipeSheet.GetComponent<Image>().overrideSprite = sheetSizes[0];
                    componentDisplays[2].SetActive(false);
                    componentDisplays[3].SetActive(false);
                }
            }
        }
        recipeSheet.GetComponent<Image>().SetNativeSize();
    }

    public void SetComponentCount(int newComponentCount)
    {
        componentCount = newComponentCount;
    }
}
