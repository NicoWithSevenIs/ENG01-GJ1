using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameTimer))]
public class GameLoopManager : MonoBehaviour
{

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
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_CAMERA_PAN_END, StartStage);
        this.totalProfit = 0f;
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
    }

    private void OnPotionSubmission(Parameters p)
    {
        bool isForced = p.GetBoolExtra("IS_FORCED", false);

        float percentCompletion = 1f;
        RecipeGenerator.crosscheckRecipe(ComponentDirector.Instance.WorkAreaContainer, this.correctRecipe, out percentCompletion);

        if (!isForced && percentCompletion != 1f)
        {
            print("Recipe Wrong");
            return;
        }

        if(isForced && percentCompletion != 1f)
        {
            print("Time's Up");
        }
        else
        {
            print("Good Job!");
        }
           


        
        this.dailyProfit = 300 * (1 + 1/(10-difficulty)) * percentCompletion;

        if(currentHour < maxWorkHour)
        {
            //depth 1 chain
            Parameters param = new Parameters();
            p.PutExtra("IS_REVERSED", true);
            EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_CAMERA_PAN_START, param);

            currentHour++;

            //go through the next stage
            EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_STAGE_END);
        }
        else
        {
            currentHour = 0;
            this.EndDay();
        }
        
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
