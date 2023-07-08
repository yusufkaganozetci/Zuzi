using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleTimeCounter : MonoBehaviour
{
    [SerializeField] VegaPuzzleSituationManager vegaPuzzleSituationManager;
    [SerializeField] TextMeshProUGUI remainingTimeText;
    [SerializeField] GameHandler gameHandler;
    [SerializeField] WinLoseManager winLoseManager;
    // Start is called before the first frame update
    void Start()
    {
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
        if(vegaPuzzleSituationManager.GetComponent<IWinnable>().GetSceneSituation() != "success")
        {
            vegaPuzzleSituationManager.GetComponent<IWinnable>().AssignSceneSituation("fail");
            StartCoroutine(winLoseManager.ManageWinOrLose(0f));
        }
    }
}