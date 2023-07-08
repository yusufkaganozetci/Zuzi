using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheFootofCanopusSituationManager : MonoBehaviour, IWinnable
{
    [SerializeField] GameHandler gameHandler;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] SFXManager sfxManager;
    [SerializeField] CollectableManager collectableManager;

    [SerializeField] TextMeshProUGUI collectedPuzzleCountText;
    [SerializeField] TextMeshProUGUI collectedEggCountText;
    [SerializeField] TextMeshProUGUI collectedRocketCountText;
    [SerializeField] TextMeshProUGUI sceneSituationText;

    [SerializeField] GameObject trophy;
    [SerializeField] GameObject[] stars;

    private int footOfCanopusLoadSceneIndex;
    private string levelSituation;

    
    public void AssignSceneSituation(string situation)
    {
        levelSituation = situation;
    }

    public void HandleSituation()
    {
        
        int puzzleCount = CollectableManager.GetPuzzleCount();
        int rocketCount = CollectableManager.GetRocketCount();
        int eggCount = CollectableManager.GetEggCount();
        if (levelSituation == "fail")
        {
            sfxManager.PlaySceneFailedSFX();
            sceneSituationText.text = "FAILED";
            footOfCanopusLoadSceneIndex = collectableManager.DecreaseRocketCountByOne();
            rocketCount = CollectableManager.GetRocketCount();
            if (footOfCanopusLoadSceneIndex == 1)
            {
                collectableManager.ResetCollectables();
                //collectableManager.UpdateCollectablesCount();
            }
        }
        else if (levelSituation == "success")
        {
            sfxManager.PlaySceneCompletedSFX();
            trophy.SetActive(true);
            sceneSituationText.text = "COMPLETED";
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(true);
            }
        }

        collectedPuzzleCountText.text = puzzleCount.ToString();
        collectedEggCountText.text = eggCount.ToString();
        collectedRocketCountText.text = rocketCount.ToString();
    }

    public void OnPressContinue()
    {
        if (levelSituation == "success")
        {
            sceneLoader.LoadScene(gameHandler.GetCurrentSceneIndex() + 1);
        }
        else if (levelSituation == "fail")
        {
            sceneLoader.LoadScene(footOfCanopusLoadSceneIndex);
        }
    }

    public string GetSceneSituation()
    {
        return levelSituation;
    }

}