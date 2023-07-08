using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalBossSituationManager : MonoBehaviour, IWinnable
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

    private int finalBossLoadSceneIndex;
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
            levelSituation = "fail";
            sceneSituationText.text = "FAILED";

            finalBossLoadSceneIndex = collectableManager.DecreaseEggCountByOne();
            eggCount = CollectableManager.GetEggCount();
            if (finalBossLoadSceneIndex == 5) // Capella
            {
                collectableManager.ResetCollectables();
                //collectableManager.UpdateCollectablesCount();
            }
        }

        else if (levelSituation == "success")
        {
            //different win situations
            sfxManager.PlaySceneCompletedSFX();
            trophy.SetActive(true);
            levelSituation = "success";
            sceneSituationText.text = "CONGRATS!!!";
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(true);
            }
        }
        collectedPuzzleCountText.text = puzzleCount.ToString();
        collectedEggCountText.text = eggCount.ToString();
        collectedRocketCountText.text = rocketCount.ToString();
        if (levelSituation == "success")
        {
            collectableManager.ResetCollectables();
        }
    }

    public void OnPressContinue()
    {
        if (levelSituation == "success")
        {
            sceneLoader.LoadScene(0);
        }
        else if (levelSituation == "fail")
        {
            //collectableManager.ResetCollectables();
            sceneLoader.LoadScene(finalBossLoadSceneIndex);
        }
    }

    public string GetSceneSituation()
    {
        return levelSituation;
    }

}
