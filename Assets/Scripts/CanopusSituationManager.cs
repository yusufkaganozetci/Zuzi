using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanopusSituationManager : MonoBehaviour, IWinnable
{
    [SerializeField] TextMeshProUGUI collectedPuzzleCountText;
    [SerializeField] TextMeshProUGUI collectedEggCountText;
    [SerializeField] TextMeshProUGUI collectedRocketCountText;
    [SerializeField] TextMeshProUGUI sceneSituationText;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] CollectableManager collectableManager;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameHandler gameHandler;
    [SerializeField] int puzzleCountInScene;
    [SerializeField] int rocketCountInScene;
    [SerializeField] int eggCountInScene;
    [SerializeField] GameObject trophy;
    [SerializeField] GameObject[] stars;
    public string levelSituation;
    private int lastStarIndex = 0;

    public void HandleSituation()
    {
        int puzzleCount = CollectableManager.GetPuzzleCount();
        int rocketCount = CollectableManager.GetRocketCount();
        int eggCount = CollectableManager.GetEggCount();
        collectedPuzzleCountText.text = puzzleCount.ToString();
        collectedEggCountText.text = eggCount.ToString();
        collectedRocketCountText.text = rocketCount.ToString();
        if (puzzleCount < puzzleCountInScene || rocketCount == 0 || levelSituation == "fail")
        {
            sfxManager.PlaySceneFailedSFX();
            levelSituation = "fail";
            sceneSituationText.text = "FAILED";
        }
        else
        {
            //different win situations
            sfxManager.PlaySceneCompletedSFX();
            trophy.SetActive(true);
            levelSituation = "success";
            sceneSituationText.text = "COMPLETED";
            if (puzzleCountInScene == puzzleCount)
            {
                stars[lastStarIndex].SetActive(true);
                lastStarIndex++;
            }
            if (rocketCountInScene == rocketCount)
            {
                stars[lastStarIndex].SetActive(true);
                lastStarIndex++;
            }
            if (eggCountInScene == eggCount)
            {
                stars[lastStarIndex].SetActive(true);
                lastStarIndex++;
            }
            lastStarIndex = 0;
        }
    }

    public void OnPressContinue()
    {
        if (levelSituation == "fail")
        {
            Debug.Log("Canopus will be loaded again!");
            collectableManager.ResetCollectables();//her seyi sýfýrlýyo
            sceneLoader.LoadScene(gameHandler.GetCurrentSceneIndex());
            //load canopus again
        }
        else if (levelSituation == "success")
        {
            sceneLoader.LoadScene(gameHandler.GetCurrentSceneIndex() + 1);
            //load next scene
        }
    }

    public void AssignSceneSituation(string situation)
    {
        levelSituation = situation;
    }

    public string GetSceneSituation()
    {
        return levelSituation;
    }
}