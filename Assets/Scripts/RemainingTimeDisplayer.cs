using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingTimeDisplayer : MonoBehaviour
{

    private TextMeshProUGUI remainingTimeText;
    private GameHandler gameHandler;
    private SceneLoader sceneLoader;
    private WinLoseManager winLoseManager;

    // Start is called before the first frame update
    void Start()
    {
        remainingTimeText = GetComponent<TextMeshProUGUI>();
        gameHandler = FindObjectOfType<GameHandler>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        winLoseManager = FindObjectOfType<WinLoseManager>();
        StartCoroutine(CountDown(30));
    }

    private IEnumerator CountDown(int startValue)
    {
        yield return new WaitUntil(() => gameHandler.GetTheGameIsStarted());
        int currentTime = startValue;
        while (currentTime != 0)
        {
            remainingTimeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1);
            currentTime -= 1;

        }
        remainingTimeText.text = "0";
        gameHandler.StopTheGame();
        FindObjectOfType<TheFootofCanopusSituationManager>().GetComponent<IWinnable>().AssignSceneSituation("success");
        StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));
        
    }
}
