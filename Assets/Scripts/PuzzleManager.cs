using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] VegaPuzzleSituationManager vegaPuzzleSituationManager;
    [SerializeField] Transform[] puzzleBackgrounds;
    [SerializeField] float snapDistance;
    [SerializeField] WinLoseManager winLoseManager;
    [SerializeField] GameHandler gameHandler;
    private Dictionary<int, int> backgroundAndPiecesPair = new Dictionary<int, int>() {
        {0,0},
        {1,0},
        {2,0},
        {3,0},
        {4,0},
        {5,0},
        {6,0},
        {7,0},
        {8,0},
    };

    
    public Transform[] GetPuzzleBackgrounds()
    {
        return this.puzzleBackgrounds;
    }

    public float GetSnapDistance()
    {
        return this.snapDistance;
    }

    public void ChangeKeyValuePair(int key, int value)
    {
        backgroundAndPiecesPair[key] = value;
        CheckIsSceneCompleted();
    }

    public void CheckIsSceneCompleted()
    {
        bool isFinished = true;
        for(int i = 0; i < backgroundAndPiecesPair.Count; i++)
        {
            if(backgroundAndPiecesPair[i] != i)
            {
                isFinished = false;
            }
        }
        if (isFinished)
        {
            gameHandler.StopTheGame();
            vegaPuzzleSituationManager.GetComponent<IWinnable>().AssignSceneSituation("success");
            //winLoseManager.levelSituation = "success";
            StartCoroutine(winLoseManager.ManageWinOrLose(0));
            
        }
    }
}
