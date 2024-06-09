using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeComponentPool : MonoBehaviour
{
    public static RecipeComponentPool instance;

    [SerializeField] private List<GameObject> recipeComponents;
    public List<GameObject> RecipeComponents
    {
        get { return recipeComponents; }
    }

    private void createComponentsLevel0 ()
    {
        GameObject CyanComponent = ComponentDirector.instance.Builder.createComponent("Cyan", null);
        GameObject GrayComponent = ComponentDirector.instance.Builder.createComponent("Gray", null);
        GameObject MagentaComponent = ComponentDirector.instance.Builder.createComponent("Magenta", null);
        GameObject YellowComponent = ComponentDirector.instance.Builder.createComponent("Yellow", null);

        this.recipeComponents.Add(CyanComponent);
        this.recipeComponents.Add(GrayComponent);
        this.recipeComponents.Add(MagentaComponent);
        this.recipeComponents.Add(YellowComponent);
        
    }
    
    private void createComponentsLevel1()
    {
        GameObject AegeanComponent = ComponentDirector.instance.Builder.createComponent("Aegean", null);
        GameObject BoysenberryComponent = ComponentDirector.instance.Builder.createComponent("Boysenberry", null);
        GameObject ButterscotchComponent = ComponentDirector.instance.Builder.createComponent("Butterscotch", null);
        GameObject LimeComponent = ComponentDirector.instance.Builder.createComponent("Lime", null);
        GameObject ScarletComponent = ComponentDirector.instance.Builder.createComponent("Scarlet", null);
        GameObject LavenderComponent = ComponentDirector.instance.Builder.createComponent("Lavender", null);

        this.recipeComponents.Add(AegeanComponent);
        this.recipeComponents.Add(BoysenberryComponent);
        this.recipeComponents.Add(ButterscotchComponent);
        this.recipeComponents.Add(LimeComponent);
        this.recipeComponents.Add(ScarletComponent);
        this.recipeComponents.Add(LavenderComponent);
    }
    
    private void createComponentsLevel2()
    {
        GameObject ArcticComponent = ComponentDirector.instance.Builder.createComponent("Arctic", null);
        GameObject GoldComponent = ComponentDirector.instance.Builder.createComponent("Gold", null);
        GameObject DarkGreenComponent = ComponentDirector.instance.Builder.createComponent("Dark Green", null);
        GameObject BubblegumPinkComponent = ComponentDirector.instance.Builder.createComponent("Bubblegum Pink", null);
        GameObject VioletComponent = ComponentDirector.instance.Builder.createComponent("Violet", null);
        GameObject SpiceComponent = ComponentDirector.instance.Builder.createComponent("Spice", null);
        GameObject CharcoalComponent = ComponentDirector.instance.Builder.createComponent("Charcoal", null);
        GameObject ScotchMistComponent = ComponentDirector.instance.Builder.createComponent("Scotch Mist", null);

        this.recipeComponents.Add(ArcticComponent);
        this.recipeComponents.Add(GoldComponent);
        this.recipeComponents.Add(DarkGreenComponent);
        this.recipeComponents.Add(BubblegumPinkComponent);
        this.recipeComponents.Add(VioletComponent);
        this.recipeComponents.Add(SpiceComponent);
        this.recipeComponents.Add(CharcoalComponent);
        this.recipeComponents.Add(ScotchMistComponent);
    }

    private void disableRecipeComponents()
    {
        for (int i = 0; i < this.recipeComponents.Count; i++)
        {
            this.recipeComponents[i].SetActive(false);
        }
    }

    public GameObject findRecipeComponent(string name)
    {
        for (int i = 0; i < this.recipeComponents.Count; i++)
        {
            if(this.recipeComponents[i].GetComponent<ComponentScript>().Data.ComponentName == name)
            {
                Debug.Log(name + " component has been found");
                return this.recipeComponents[i];
            }
        }

        return null;
    }

    public void clone(string name, Vector3 position)
    {
        GameObject cloneObject = Instantiate(this.findRecipeComponent(name));
        cloneObject.transform.position = position;
        cloneObject.SetActive(true);
    }

    void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.createComponentsLevel0();
        this.createComponentsLevel1();
        this.createComponentsLevel2();
        this.disableRecipeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
