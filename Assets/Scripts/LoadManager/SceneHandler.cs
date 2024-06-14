using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    
    public void LoadGameProper()
    {
        SceneManager.LoadScene("PotionFloat");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadNextDay()
    {
        SceneManager.LoadScene("NextDay");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
