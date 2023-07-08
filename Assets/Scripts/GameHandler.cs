using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] string sceneName;
    
    private bool isGameStarted = false;
    private bool isGamePlaying = true;

    private string[] allSceneNames = { "Canopus", "TheFootofCanopus", "Vega",  "VegaPuzzle", "Capella", "FinalBoss" };
    
    public void StartTheGame()
    {
        isGameStarted = true;
    }

    public bool GetTheGameIsStarted()
    {
        return isGameStarted;
    }

    public void StopTheGame()
    {
        isGamePlaying = false;
        Time.timeScale = 0;
    }

    public bool GetTheGameIsPlaying()
    {
        return isGamePlaying;
    }

    public string GetTheSceneName()
    {
        return sceneName;
    }

    public int GetCurrentSceneIndex()
    {
        int index = 0;
        for(int i=0;i< allSceneNames.Length; i++)
        {
            if(allSceneNames[i] == sceneName)
            {
                index = i;
                break;
            }
        }
        return index + 1;
    }
}
