using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallGenerator : MonoBehaviour
{
    [SerializeField] GameObject lavaBall;
    [SerializeField] GameObject danger;
    [SerializeField] Transform lavaBallsParent;
    [SerializeField] float minWaitAmountBetweenLavas;
    [SerializeField] float maxWaitAmountBetweenLavas;
    [SerializeField] float dangerObjectYPosition;
    private ScaleManager scaleManager;
    private GameHandler gameHandler;
    private float lavaBallGenerationYPosition;
    private float minLavaBallPosition;
    private float maxLavaBallPosition;
    // Start is called before the first frame update
    void Start()
    {
        scaleManager = FindObjectOfType<ScaleManager>();
        gameHandler = FindObjectOfType<GameHandler>();
        minLavaBallPosition = scaleManager.GetLavaBallMinimumXPosition();
        maxLavaBallPosition = scaleManager.GetLavaBallMaximumXPosition();
        lavaBallGenerationYPosition = scaleManager.GetLavaBallYGenerationPosition();
        StartCoroutine(GenerateLavaBalls());
    }

    private IEnumerator GenerateLavaBalls()
    {
        yield return new WaitUntil(() => gameHandler.GetTheGameIsStarted());
        while (gameHandler.GetTheGameIsPlaying())
        {
            InstantiateLavaBall();
            yield return new WaitForSeconds(Random.Range(minWaitAmountBetweenLavas, maxWaitAmountBetweenLavas));
        }
    }

    private void InstantiateLavaBall()
    {
        GameObject lava = Instantiate(lavaBall, lavaBallsParent);
        lava.transform.position = new Vector3(Random.Range(minLavaBallPosition, maxLavaBallPosition), lavaBallGenerationYPosition, 0);
        InstantiateDangerObject(lava.transform.position.x);
    }

    private void InstantiateDangerObject(float xPos)
    {
        GameObject dangerObj = Instantiate(danger);
        dangerObj.transform.position = new Vector3(xPos, dangerObjectYPosition, 0);
        Destroy(dangerObj, 1);
    }
}
