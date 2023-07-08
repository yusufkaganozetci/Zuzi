using System.Collections;
using UnityEngine;

public class EnemyCloudManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyClouds;
    [SerializeField] Transform enemyCloudsParent;
    [SerializeField] float minWaitAmountForInstantiation;
    [SerializeField] float maxWaitAmountForInstantiation;
    private GameHandler gameHandler;

    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        StartCoroutine(CreateEnemyClouds());
    }

    private IEnumerator CreateEnemyClouds()
    {
        yield return new WaitUntil(() => gameHandler.GetTheGameIsStarted());
        
        while (true)
        {
            if (gameHandler.GetTheGameIsPlaying())
            {
                Instantiate(enemyClouds[Random.Range(0, enemyClouds.Length - 1)], enemyCloudsParent);
                yield return new WaitForSeconds(Random.Range(minWaitAmountForInstantiation, maxWaitAmountForInstantiation));
            }
            else
            {
                yield return null;
            }
        }

    }

}