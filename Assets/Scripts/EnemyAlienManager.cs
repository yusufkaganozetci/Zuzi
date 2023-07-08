using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlienManager : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] Transform enemyAlienParent;
    [SerializeField] GameObject enemyHorizontalType;
    [SerializeField] GameObject enemyVerticalType;
    [SerializeField] GameObject danger;
    //horizontal enemy values
    [SerializeField] float leftXPosition;
    [SerializeField] float rightXPosition;
    [SerializeField] float maxYPosition;
    [SerializeField] float minYPosition;
    //vertical enemy values
    [SerializeField] float minXPosition;
    [SerializeField] float maxXPosition;
    [SerializeField] float topYPosition;
    [SerializeField] float bottomYPosition;

    [SerializeField] float minWaitAmountBetweenEnemyGenerations;
    [SerializeField] float maxWaitAmountBetweenEnemyGenerations;

    private GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        StartCoroutine(GenerateEnemies());
        //GenerateEnemyAlien();
    }

    private IEnumerator GenerateEnemies()
    {
        yield return new WaitUntil(() => gameHandler.GetTheGameIsStarted());
        while (true)
        {
            if (gameHandler.GetTheGameIsPlaying())
            {
                GenerateEnemyAlien();
                yield return new WaitForSeconds(Random.Range(minWaitAmountBetweenEnemyGenerations, maxWaitAmountBetweenEnemyGenerations));
            }
            else
            {
                yield return null;
            }
            
        }
    }

    

    private void GenerateEnemyAlien()
    {
        int chosenVal = Random.Range(0, 2);
        if(chosenVal == 0)//horizontal alien
        {
            GameObject alien = Instantiate(enemyHorizontalType, enemyAlienParent);
            int q = Random.Range(0, 2);
            if(q == 0)
            {
                alien.transform.position = new Vector3(leftXPosition,
                    Random.Range(minYPosition, maxYPosition), 0);
                GameObject dangerObject = Instantiate(danger, enemyAlienParent);
                dangerObject.transform.position = new Vector3(leftXPosition + 4, alien.transform.position.y, 0);
                Destroy(dangerObject, 1f);
                alien.GetComponent<EnemyAlien>().StartEnemyMovement(new Vector2(Random.Range(minSpeed, maxSpeed),0));
            }
            else
            {
                alien.transform.position = new Vector3(rightXPosition,
                    Random.Range(minYPosition, maxYPosition), 0);
                alien.transform.Rotate(new Vector3(0, 0, 180));
                GameObject dangerObject = Instantiate(danger, enemyAlienParent);
                dangerObject.transform.position = new Vector3(rightXPosition - 4, alien.transform.position.y, 0);
                Destroy(dangerObject, 1f);
                alien.GetComponent<EnemyAlien>().StartEnemyMovement(new Vector2(-Random.Range(minSpeed, maxSpeed), 0));
            }
            
        }
        else if(chosenVal == 1)//vertical alien
        {
            GameObject alien = Instantiate(enemyVerticalType, enemyAlienParent);
            int q = Random.Range(0, 2);
            if (q == 0)
            {
                alien.transform.position = new Vector3(Random.Range(minXPosition, maxXPosition),
                    topYPosition, 0);
                alien.transform.Rotate(new Vector3(0, 0, 180));
                GameObject dangerObject = Instantiate(danger, enemyAlienParent);
                dangerObject.transform.position = new Vector3(alien.transform.position.x, topYPosition - 4, 0);
                Destroy(dangerObject, 1f);
                alien.GetComponent<EnemyAlien>().StartEnemyMovement(new Vector2(0, -Random.Range(minSpeed, maxSpeed)));
            }
            else
            {
                alien.transform.position = new Vector3(Random.Range(minXPosition, maxXPosition),
                    bottomYPosition, 0);
                GameObject dangerObject = Instantiate(danger, enemyAlienParent);
                dangerObject.transform.position = new Vector3(alien.transform.position.x, bottomYPosition + 3, 0);
                Destroy(dangerObject, 1f);
                alien.GetComponent<EnemyAlien>().StartEnemyMovement(new Vector2(0, Random.Range(minSpeed, maxSpeed)));
            }
        }
        
        
    }


}
