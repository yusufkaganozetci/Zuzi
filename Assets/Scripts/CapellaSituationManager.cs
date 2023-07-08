using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapellaSituationManager : MonoBehaviour, IWinnable
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

    private int lastStarIndex = 0;
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
        collectedPuzzleCountText.text = puzzleCount.ToString();
        collectedEggCountText.text = eggCount.ToString();
        collectedRocketCountText.text = rocketCount.ToString();

        if (eggCount < 1 || levelSituation == "fail")
        {
            sfxManager.PlaySceneFailedSFX();
            levelSituation = "fail";
            sceneSituationText.text = "FAILED";
        }
        else
        {
            sfxManager.PlaySceneCompletedSFX();
            trophy.SetActive(true);
            levelSituation = "success";
            sceneSituationText.text = "COMPLETED";
            for (int i = 0; i < eggCount; i++)
            {
                stars[lastStarIndex].SetActive(true);
                lastStarIndex++;
            }
            lastStarIndex = 0;
        }
    }

    public void OnPressContinue()
    {
        if (levelSituation == "success")
        {
            sceneLoader.LoadScene(gameHandler.GetCurrentSceneIndex() + 1);
        }
        else if (levelSituation == "fail")
        {
            collectableManager.ResetCollectables();
            sceneLoader.LoadScene(gameHandler.GetCurrentSceneIndex());
        }
    }

    public string GetSceneSituation()
    {
        return levelSituation;
    }

}
