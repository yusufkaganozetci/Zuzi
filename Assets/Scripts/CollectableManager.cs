using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI puzzlePiecesText;
    [SerializeField] TextMeshProUGUI eggPiecesText;
    [SerializeField] TextMeshProUGUI rocketPiecesText;

    [SerializeField] List<GameObject> finalBossCollectables;
    [SerializeField] List<GameObject> finalBossCollectImages;
    [SerializeField] List<Animator> prisonBarsAnimators;
    [SerializeField] GameObject prisonBarCollectImage;

    [SerializeField] Animator keyAnimator;

    private static int puzzlePiecesCount = 0;
    private static int eggPiecesCount = 0;
    private static int rocketPiecesCount = 0;


    private GameHandler gameHandler;

    private WinLoseManager winLoseManager;

    private SFXManager sfxManager;

    private bool isReadyForCollectPiece = true;

    private Transform zuzi;

    private int closestFinalBossCollectableIndex = -1;

    private int lastPrisonBar = 0;

    private bool canDestroyPrisonBar = false;


    private void Start()
    {
        puzzlePiecesText.text = puzzlePiecesCount.ToString();
        eggPiecesText.text = eggPiecesCount.ToString();
        rocketPiecesText.text = rocketPiecesCount.ToString();
        gameHandler = FindObjectOfType<GameHandler>();
        sfxManager = FindObjectOfType<SFXManager>();



        //final boss scene
        if(gameHandler.GetTheSceneName() == "FinalBoss")
        {
            zuzi = FindObjectOfType<Zuzi>().transform;
            winLoseManager = FindObjectOfType<WinLoseManager>();
        }
        
    }
    public void ResetOnFinalBossSceneFailed()
    {
        for(int i=0; i < finalBossCollectImages.Count; i++)
        {
            finalBossCollectImages[i].SetActive(false);
            prisonBarCollectImage.SetActive(false);
        }
    }

    private void Update()
    {
        if (gameHandler.GetTheGameIsPlaying() && gameHandler.GetTheGameIsStarted() && gameHandler.GetCurrentSceneIndex() == 6 && lastPrisonBar != 5)
        {
            if (isReadyForCollectPiece)
            {
                GetClosestCollectable();
            }
            else
            {
                ManageCloseToPrisonBar();
            }

            if (Input.GetKeyDown(KeyCode.E) && !isReadyForCollectPiece)
            {
                LiftThePrisonBarUp();
            }

            else if (Input.GetKeyDown(KeyCode.E) && closestFinalBossCollectableIndex != -1 && isReadyForCollectPiece)
            {
                CollectFinalBossCollectable();
            }
        }
        else if(gameHandler.GetTheGameIsPlaying() && lastPrisonBar == 5)
        {
            StartCoroutine(HandleFinalBossCompleted());
            
            
            
            Debug.Log("game is finished!");
        }
    }

    private IEnumerator HandleFinalBossCompleted()
    {
        gameHandler.StopTheGame();
        yield return new WaitForSecondsRealtime(1);
        FindObjectOfType<FinalBossSituationManager>().GetComponent<IWinnable>().AssignSceneSituation("success");

        //winLoseManager.levelSituation = "success";
        yield return StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));

    }

    private void ManageCloseToPrisonBar()
    {
        float zuziXPos = zuzi.position.x;
        float barXPos = prisonBarCollectImage.transform.position.x;
        if (Mathf.Abs(barXPos - zuziXPos) <= 1)
        {
            canDestroyPrisonBar = true;
            prisonBarCollectImage.SetActive(true);
        }
        else
        {
            canDestroyPrisonBar = false;
            prisonBarCollectImage.SetActive(false);
        }
    }

    private void LiftThePrisonBarUp()
    {
        if (canDestroyPrisonBar)
        {
            prisonBarsAnimators[lastPrisonBar].SetTrigger("PlayPrisonBarUp");
            lastPrisonBar++;
            isReadyForCollectPiece = true;
            prisonBarCollectImage.SetActive(false);
        }
    }

    private void CollectFinalBossCollectable()
    {
        prisonBarCollectImage.SetActive(true);
        isReadyForCollectPiece = false;
        GameObject pickedCollectable = finalBossCollectables[closestFinalBossCollectableIndex];
        GameObject collectImage = finalBossCollectImages[closestFinalBossCollectableIndex];
        finalBossCollectables.Remove(pickedCollectable);
        finalBossCollectImages.Remove(collectImage);
        Destroy(pickedCollectable);
        Destroy(collectImage);
    }

    private void GetClosestCollectable()
    {
        for(int i=0; i< finalBossCollectImages.Count; i++)
        {
            finalBossCollectImages[i].SetActive(false);
        }
        float distance = float.MaxValue;
        float currentDistance;
        int index = -5;
        for(int i = 0; i < finalBossCollectables.Count; i++)
        {
            Vector2 firstPos = new Vector2(finalBossCollectables[i].transform.position.x, 0);
            Vector2 secondPos = new Vector2(zuzi.position.x, 0);
            currentDistance = Vector2.Distance(firstPos, secondPos);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                index = i;
            }
        }
        closestFinalBossCollectableIndex = index;
        finalBossCollectImages[closestFinalBossCollectableIndex].SetActive(true);
    }

    public int DecreaseRocketCountByOne()
    {
        if(rocketPiecesCount != 1)
        {
            rocketPiecesCount--;
            rocketPiecesText.text = rocketPiecesCount.ToString();
            return gameHandler.GetCurrentSceneIndex(); // load footofcanopus
        }
        else
        {
            rocketPiecesCount = 0;
            rocketPiecesText.text = rocketPiecesCount.ToString();
            return gameHandler.GetCurrentSceneIndex() - 1; // load canopus
        }
    }

    public int DecreaseEggCountByOne()
    {
        Debug.Log("Decrease wanted!");
        if (eggPiecesCount != 1)
        {
            eggPiecesCount--;
            eggPiecesText.text = eggPiecesCount.ToString();
            return gameHandler.GetCurrentSceneIndex(); // load footofcanopus
        }
        else
        {
            eggPiecesCount = 0;
            eggPiecesText.text = eggPiecesCount.ToString();
            return gameHandler.GetCurrentSceneIndex() - 1; // load canopus
        }
    }

    private void IncreasePuzzlePiecesCount()
    {
        puzzlePiecesCount += 1;
        puzzlePiecesText.text = puzzlePiecesCount.ToString();
        sfxManager.PlayObjectCollectedSFX();
    }

    private void IncreaseEggPiecesCount()
    {
        eggPiecesCount += 1;
        eggPiecesText.text = eggPiecesCount.ToString();
        sfxManager.PlayObjectCollectedSFX();
    }

    private void IncreaseRocketPiecesCount()
    {
        rocketPiecesCount += 1;
        rocketPiecesText.text = rocketPiecesCount.ToString();
        sfxManager.PlayObjectCollectedSFX();
    }

    private IEnumerator ChangeIsReadyForCollectPiece(int time)
    {
        yield return new WaitForSeconds(time);
        isReadyForCollectPiece = true;
    }

    public void IncreasePieceCount(GameObject piece)
    {
        if (isReadyForCollectPiece)
        {
            isReadyForCollectPiece = false;
            StartCoroutine(ChangeIsReadyForCollectPiece(1));
            if (piece.CompareTag("Puzzle"))
            {
                IncreasePuzzlePiecesCount();
            }
            else if (piece.CompareTag("Egg"))
            {
                IncreaseEggPiecesCount();
            }
            else if (piece.CompareTag("Rocket"))
            {
                IncreaseRocketPiecesCount();
            }
            else
            {
                Debug.Log("Undefined tag is come!");
            }
            Destroy(piece);
        }
    }

    public void ResetCollectables()
    {
        puzzlePiecesCount = 0;
        eggPiecesCount = 0;
        rocketPiecesCount = 0;
    }

    public void UpdateCollectablesCount()
    {
        puzzlePiecesText.text = puzzlePiecesCount.ToString();
        eggPiecesText.text = eggPiecesCount.ToString();
        rocketPiecesText.text = rocketPiecesCount.ToString();
    }

    public void CollectTheKey(GameObject key)
    {
        if (isReadyForCollectPiece)
        {
            isReadyForCollectPiece = false;
            sfxManager.PlayObjectCollectedSFX();
            keyAnimator.SetTrigger("PlayKeyAnimation");
        }
    }

    public static int GetPuzzleCount()
    {
        return puzzlePiecesCount;
    }

    public static int GetRocketCount()
    {
        return rocketPiecesCount;
    }

    public static int GetEggCount()
    {
        return eggPiecesCount;
    }
}