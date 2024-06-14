using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(GameTimer))]
public class GameLoopManager : MonoBehaviour
{


    [SerializeField] private GameObject potion;
    private Vector3 originalPosition;

    [SerializeField] private List<string> correctRecipe;

    public List<string> CorrectRecipe { get { return correctRecipe; } }

    [SerializeField] private List<string> incorrectRecipe;

    [Range(1,10)]
    [SerializeField] private int difficulty;

    [Header("Profits")]
    [SerializeField] private float quotaMultiplier = 1;
    [SerializeField] private float dailyProfit;
    [SerializeField] private float dailyQuota;
    [SerializeField] private float totalProfit;

    [Header("Time")]
    [SerializeField] private int currentHour;
    [SerializeField] private int maxWorkHour = 6;
    [SerializeField] private int daysWorking = 0;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_POTION_SUBMISSION, OnPotionSubmission);
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_STAGE_START, DeconstructPotion);
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_ENTRY_CAMERA_PAN_END, StartStage);
        this.totalProfit = 0f;
        this.currentHour = 0;

        this.originalPosition = potion.transform.position;
    }

    public void StartDay()
    {
        this.dailyProfit = 0f;
        this.dailyQuota = 500 * quotaMultiplier;
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_DAY_START);
     
    }

    private void DeconstructPotion()
    {

        correctRecipe.Clear();
        incorrectRecipe.Clear();

        this.correctRecipe = RecipeGenerator.CreateRecipe(ComponentDirector.Instance.getBlueprint(), this.difficulty);
        this.incorrectRecipe = RecipeGenerator.CreateIncorrectRecipe(ComponentDirector.Instance.getBlueprint(), correctRecipe, this.difficulty);

        string s = "";
        foreach(var c in correctRecipe)
        {
            s += c + " ";
        }
        Debug.Log("Correct Recipe: " + s);


        ComponentDirector.Instance.ProcessStage(this.difficulty, incorrectRecipe);
        ComponentDirector.Instance.setComponentsEditable(false);


        

    }

    private void StartStage()
    {
        ComponentDirector.Instance.setComponentsEditable(true);
        potion.SetActive(false);
        potion.transform.position = this.originalPosition;
    }

    private void OnPotionSubmission(Parameters p)
    {
        bool isForced = p.GetBoolExtra("IS_FORCED", false);

        float percentCompletion = 1f;

     
        bool isCorrect = RecipeGenerator.crosscheckRecipe(ComponentDirector.Instance.WorkAreaContainer, this.correctRecipe, out percentCompletion);

        print("Percent Completion: " + percentCompletion);

        if (!isForced && (!isCorrect || percentCompletion != 1f))
        {
            print("Recipe Wrong");
            return;
        }

        if(isForced && percentCompletion != 1f)
        {
            print("Time's Up");
        }
        
        if(isCorrect && percentCompletion == 1f)
        {
            print("Good Job!");
        }


        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_EXIT_CAMERA_PAN_START);

        foreach(Transform child in ComponentDirector.Instance.WorkAreaContainer.transform)
        {
            child.gameObject.AddComponent<LevitateComponent>();
        }

        StartCoroutine(delayedDeactivate());
       

    }

    //AHHH RUSH FUNCTION
    private  IEnumerator delayedDeactivate()
    {
        yield return new WaitForSeconds(1.5f);

        ComponentDirector.Instance.ClearBatch();

    }

    private void EndDay()
    {

        if(dailyProfit < dailyQuota)
        {
            //gameOver
            return;
        }

        totalProfit += dailyProfit;

        this.quotaMultiplier *= 1.1f + 1f / difficulty;
        //temp
        difficulty += Mathf.Clamp(UnityEngine.Random.Range(1/difficulty, 3), 1, 10);
        this.daysWorking++;

        //trigger next day sequence which calls start day
        
    }


    #region singleton
    private static GameLoopManager _instance;
    public static GameLoopManager Instance { get { return _instance; } }

    public void Awake()
    {
        if(_instance == null)
            _instance = this;
        else GameObject.Destroy(gameObject);
    }

  

    #endregion


}
