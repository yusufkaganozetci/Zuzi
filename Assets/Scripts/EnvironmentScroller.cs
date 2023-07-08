using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeed;
    [SerializeField] Transform[] rightRocks;
    [SerializeField] Transform[] leftRocks;

    private ScaleManager scaleManager;
    private GameHandler gameHandler;
    private float rightRockYChangePosition;
    private float leftRockYChangePosition;
    // Start is called before the first frame update
    void Start()
    {
        scaleManager = FindObjectOfType<ScaleManager>();
        gameHandler = FindObjectOfType<GameHandler>();
        rightRockYChangePosition = -1 * scaleManager.GetRightRockHeight();
        leftRockYChangePosition = -1 * scaleManager.GetLeftRockHeight();
        Application.targetFrameRate = 144;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameHandler.GetTheGameIsPlaying() && gameHandler.GetTheGameIsStarted())
        {
            MoveTheRocks();
            Reposition();
        }
    }


    private void MoveTheRocks()
    {
        for (int i = 0; i < rightRocks.Length; i++)
        {
            float yPos = rightRocks[i].transform.position.y - (scrollingSpeed * Time.deltaTime);
            rightRocks[i].transform.position = new Vector3(rightRocks[i].transform.position.x,
                   yPos,rightRocks[i].transform.position.z);
        }
        for (int i = 0; i < leftRocks.Length; i++)
        {
            float yPos = leftRocks[i].transform.position.y - (scrollingSpeed * Time.deltaTime);
            leftRocks[i].transform.position = new Vector3(leftRocks[i].transform.position.x,
                   yPos, leftRocks[i].transform.position.z);
        }

    }

    

    private void Reposition()
    {
        for(int i = 0; i < rightRocks.Length; i++)
        {
            if(rightRocks[i].position.y <= rightRockYChangePosition)
            {
                int index = i + 1;
                if (index == rightRocks.Length)
                {
                    index = 0;
                }
                float newYPos = rightRocks[index].position.y + scaleManager.GetRightRockHeight();
                rightRocks[i].position = new Vector3(
                    rightRocks[i].position.x,
                    newYPos,
                    rightRocks[i].position.z);
            }
        }
        for (int i = 0; i < leftRocks.Length; i++)
        {
            if (leftRocks[i].position.y <= leftRockYChangePosition)
            {
                int index = i + 1;
                if (index == leftRocks.Length)
                {
                    index = 0;
                }
                float newYPos = leftRocks[index].position.y + scaleManager.GetLeftRockHeight();
                leftRocks[i].position = new Vector3(
                    leftRocks[i].position.x,
                    newYPos,
                    leftRocks[i].position.z);
            }
        }
    }
    
}