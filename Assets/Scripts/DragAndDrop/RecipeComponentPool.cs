using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeComponentPool : MonoBehaviour
{
    public static RecipeComponentPool Instance;

    [SerializeField] private List<GameObject> recipeComponents;
    public List<GameObject> RecipeComponents
    {
        get { return recipeComponents; }
    }

    private void createComponentsLevel0 ()
    {
        GameObject CyanComponent = ComponentDirector.Instance.Builder.createComponent("Cyan", null);
        GameObject GrayComponent = ComponentDirector.Instance.Builder.createComponent("Gray", null);
        GameObject MagentaComponent = ComponentDirector.Instance.Builder.createComponent("Magenta", null);
        GameObject YellowComponent = ComponentDirector.Instance.Builder.createComponent("Yellow", null);

        this.recipeComponents.Add(CyanComponent);
        this.recipeComponents.Add(GrayComponent);
        this.recipeComponents.Add(MagentaComponent);
        this.recipeComponents.Add(YellowComponent);
        
    }
    
    private void createComponentsLevel1()
    {
        GameObject AegeanComponent = ComponentDirector.Instance.Builder.createComponent("Aegean", null);
        GameObject BoysenberryComponent = ComponentDirector.Instance.Builder.createComponent("Boysenberry", null);
        GameObject ButterscotchComponent = ComponentDirector.Instance.Builder.createComponent("Butterscotch", null);
        GameObject LimeComponent = ComponentDirector.Instance.Builder.createComponent("Lime", null);
        GameObject ScarletComponent = ComponentDirector.Instance.Builder.createComponent("Scarlet", null);
        GameObject LavenderComponent = ComponentDirector.Instance.Builder.createComponent("Lavender", null);

        this.recipeComponents.Add(AegeanComponent);
        this.recipeComponents.Add(BoysenberryComponent);
        this.recipeComponents.Add(ButterscotchComponent);
        this.recipeComponents.Add(LimeComponent);
        this.recipeComponents.Add(ScarletComponent);
        this.recipeComponents.Add(LavenderComponent);
    }
    
    private void createComponentsLevel2()
    {
        GameObject ArcticComponent = ComponentDirector.Instance.Builder.createComponent("Arctic", null);
        GameObject GoldComponent = ComponentDirector.Instance.Builder.createComponent("Gold", null);
        GameObject DarkGreenComponent = ComponentDirector.Instance.Builder.createComponent("Dark Green", null);
        GameObject BubblegumPinkComponent = ComponentDirector.Instance.Builder.createComponent("Bubblegum Pink", null);
        GameObject VioletComponent = ComponentDirector.Instance.Builder.createComponent("Violet", null);
        GameObject SpiceComponent = ComponentDirector.Instance.Builder.createComponent("Spice", null);
        GameObject CharcoalComponent = ComponentDirector.Instance.Builder.createComponent("Charcoal", null);
        GameObject ScotchMistComponent = ComponentDirector.Instance.Builder.createComponent("Scotch Mist", null);

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

    public void processMergeComponents(GameObject currentObject, GameObject targetObject, string mergedComponentName, Vector3 position)
    {

        string currentComponentName = currentObject.GetComponent<ComponentScript>().Data.ComponentName;
        string targetComponentName = targetObject.GetComponent<ComponentScript>().Data.ComponentName;

        int nIndex = -1;
        for (int i = 0; i < this.recipeComponents.Count; i++)
        {
            if (this.recipeComponents[i].GetComponent<ComponentScript>().Data.ComponentName == mergedComponentName)
            {
               nIndex = i;
            }
        }

        if (nIndex != -1)
        {   
            string componentAName = this.recipeComponents[nIndex].GetComponent<ComponentScript>().Data.ComponentA.ComponentName;
            string componentBName = this.recipeComponents[nIndex].GetComponent<ComponentScript>().Data.ComponentB.ComponentName;
            if ((componentAName == currentComponentName && componentBName == targetComponentName) || (componentBName == currentComponentName && componentAName == targetComponentName))
            {
                currentObject.SetActive(false);
                targetObject.SetActive(false);
                this.clone(mergedComponentName, position);
            }
        }
    }

    public void processDivideComponents(GameObject currentObject, Vector3 position)
    {
        string currentComponentName = currentObject.GetComponent<ComponentScript>().Data.ComponentName;
        int nIndex = -1;
        for (int i = 0; i < this.recipeComponents.Count; i++)
        {
            if (this.recipeComponents[i].GetComponent<ComponentScript>().Data.ComponentName == currentComponentName)
            {
                nIndex = i;
            }
        }

        string componentAName = this.recipeComponents[nIndex].GetComponent<ComponentScript>().Data.ComponentA.ComponentName;
        string componentBName = this.recipeComponents[nIndex].GetComponent<ComponentScript>().Data.ComponentB.ComponentName;

        if (componentAName != currentComponentName && componentBName != currentComponentName)
        {
            currentObject.SetActive(false);
            this.clone(componentAName, position);
            this.clone(componentBName, position);

        }
    }


    public void clone(string name, Vector3 position)
    {
        GameObject cloneObject = Instantiate(this.findRecipeComponent(name));
        cloneObject.transform.position = position;
        cloneObject.SetActive(true);
    }

    void Awake()
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
