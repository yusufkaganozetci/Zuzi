using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VegaSituationManager : MonoBehaviour, IWinnable
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
        if (levelSituation == "success")
        {
            sfxManager.PlaySceneCompletedSFX();
            sceneSituationText.text = "COMPLETED";
            trophy.SetActive(true);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (levelSituation == "fail")
        {
            sfxManager.PlaySceneFailedSFX();
            sceneSituationText.text = "FAILED";
        }
    }

    public void OnPressContinue()
    {
        sceneLoader.LoadScene(gameHandler.GetCurrentSceneIndex() + 1);
    }

    public string GetSceneSituation()
    {
        return levelSituation;
    }
}