using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    
    public void LoadGameProper()
    {
        Debug.Log("called load game proper.");
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

    public void LoadWin()
    {
        SceneManager.LoadScene("Win");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }


}
