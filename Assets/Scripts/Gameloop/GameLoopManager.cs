using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameTimer))]
public class GameLoopManager : MonoBehaviour
{

    [SerializeField] private List<string> correctRecipe;
    [SerializeField] private List<string> incorrectRecipe;

    [Range(1,10)]
    [SerializeField] private int difficulty;

    [Header("Profits")]
    [SerializeField] private float quotaMultiplier = 1;
    [SerializeField] private float dailyProfit;
    [SerializeField] private float dailyQuota;

    [Header("Time")]
    [SerializeField] private int currentHour;
    [SerializeField] private int maxWorkHour = 6;
    [SerializeField] private int daysWorking = 0;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_POTION_SUBMISSION, OnPotionSubmission);
    }




    public void StartDay()
    {
        this.dailyProfit = 0f;
        this.dailyQuota = 500 * quotaMultiplier;
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_DAY_START);
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_STAGE_START);
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

        //call camera transition here

    }

    private void StartStage()
    {
        ComponentDirector.Instance.setComponentsEditable(true);
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_STAGE_START);
    }

    private void OnPotionSubmission(Parameters p)
    {
        bool isForced = p.GetBoolExtra("IS_FORCED", false);

        float percentCompletion = 1f;
        RecipeGenerator.crosscheckRecipe(ComponentDirector.Instance.WorkAreaContainer, this.correctRecipe, out percentCompletion);

        if (!isForced && percentCompletion != 1f)
            return;


        
        this.dailyProfit = 300 * (1 + 1/(10-difficulty)) * percentCompletion;

        if(currentHour < maxWorkHour)
        {
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
