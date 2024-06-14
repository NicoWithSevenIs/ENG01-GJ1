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

        
        Parameters parameters = new Parameters();
        parameters.PutExtra("DIFFICULTY", this.difficulty);
        parameters.PutExtra("INCORRECT_RECIPE", new ArrayList(this.incorrectRecipe));

        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_STAGE_START, parameters);

    }

    private void OnPotionSubmission(Parameters p)
    {
        bool isForced = p.GetBoolExtra("IS_FORCED", false);


        //each potion's base price is 300 * (1 + 1/difficulty)
        //check for potion accuracy
        //reduce the price accordingly
        //addItToDailyProfits


        if(currentHour < maxWorkHour)
        {
            currentHour++;
            //this.DeconstructPotion();
        }
        else
        {
            currentHour = 0;
            this.OnDayEnd();
        }
        
    }

    private void OnDayEnd()
    {

        if(dailyProfit < dailyQuota)
        {
            //gameOver
            return;
        }

        this.quotaMultiplier *= 1.1f + 1f / difficulty;
        this.daysWorking++;
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
