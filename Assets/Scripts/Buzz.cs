using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzz : MonoBehaviour
{
    [SerializeField] float buzzTopYPosition;
    [SerializeField] float buzzBottomYPosition;
    [SerializeField] float buzzMinXPosition;
    [SerializeField] float buzzMaxXPosition;
    [SerializeField] Transform zuziTransform;
    private Rigidbody2D rb;
    
    float buzzXDestinationPosition = 0;
    private WinLoseManager winLoseManager;
    private GameHandler gameHandler;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameHandler = FindObjectOfType<GameHandler>();
        winLoseManager = FindObjectOfType<WinLoseManager>();
        //StartCoroutine(Deneme());
        //StartCoroutine(MoveObject(rb, new Vector3(35, 35, 0), 5));

        //StartCoroutine(MoveObjectWithSameVelocity(rb, new Vector3(50, 35), 30,30, "x"));
        StartCoroutine(MoveTheBuzz());

    }
    private void Update()
    {
        buzzXDestinationPosition = zuziTransform.position.x;
        if(buzzXDestinationPosition < buzzMinXPosition)
        {
            buzzXDestinationPosition = buzzMinXPosition;
        }
        else if(buzzXDestinationPosition > buzzMaxXPosition)
        {
            buzzXDestinationPosition = buzzMaxXPosition;
        }
    }

    private IEnumerator MoveTheBuzz()
    {
        
        
        int counter = 0;
        while (true)
        {
            /*if(counter % 3 == 0)
            {
                yield return new WaitForSeconds(2);
            }*/
            if (gameHandler.GetTheGameIsStarted() && gameHandler.GetTheGameIsPlaying())
            {
                yield return StartCoroutine(MoveObjectWithSameVelocity(rb, new Vector3(buzzXDestinationPosition, buzzTopYPosition), 10, 0, "x"));
                animator.SetBool("BuzzJumpDown", true);
                yield return StartCoroutine(MoveObjectWithSameVelocity(rb, new Vector3(rb.position.x, buzzBottomYPosition), 0, 5, "y"));
                animator.SetBool("BuzzJumpDown", false);
                animator.SetBool("BuzzJumpUp", true);
                yield return StartCoroutine(MoveObjectWithSameVelocity(rb, new Vector3(rb.position.x, buzzTopYPosition), 0, 5, "y"));
                animator.SetBool("BuzzJumpUp", false);
                animator.SetBool("BuzzJumpDown", false);
                counter++;
            }
            else
            {
                yield return null;
            }
            
            

        }
        

    }

    private IEnumerator MoveObjectWithSameVelocity(Rigidbody2D objectRB, Vector2 destinationPos, float xVelocity, float yVelocity, string check)
    {
        float xDiff = destinationPos.x - objectRB.position.x;
        float yDiff = destinationPos.y - objectRB.position.y;
        float currentXPos = objectRB.position.x;
        float currentYPos = objectRB.position.y;
        while (true)
        {
            
            currentXPos += ( xDiff / (Mathf.Epsilon + Mathf.Abs(xDiff))) * xVelocity * Time.fixedDeltaTime;
            currentYPos += (yDiff / (Mathf.Epsilon + Mathf.Abs(yDiff))) * yVelocity * Time.fixedDeltaTime;
            if (check == "x")
            {
                if (xDiff >= 0) // positive
                {
                    if (currentXPos >= destinationPos.x)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
                else
                {
                    if (currentXPos <= destinationPos.x)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
            }
            else if (check == "y")
            {
                if (yDiff >= 0) // positive
                {
                    if (currentYPos >= destinationPos.y)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
                else
                {
                    if (currentYPos <= destinationPos.y)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
            }

            /*if((Mathf.Abs(currentXPos) >= Mathf.Abs(destinationPos.x)) && (Mathf.Abs(currentYPos) >= Mathf.Abs(destinationPos.y)))
            {
                
                break;
            }*/
            objectRB.MovePosition(new Vector2(currentXPos, currentYPos));
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator MoveObject(Rigidbody2D objectRB, Vector2 destinationPos, float time, string check)
    {
        
        float elapsedTime = 0;
        float xDiff = destinationPos.x - objectRB.position.x;
        float yDiff = destinationPos.y - objectRB.position.y;
        float currentXPos = objectRB.position.x;
        float currentYPos = objectRB.position.y;
        while (true)
        {
            elapsedTime += Time.fixedDeltaTime;
            currentXPos += xDiff / (time / Time.fixedDeltaTime);
            currentYPos += yDiff / (time / Time.fixedDeltaTime);
            if(check == "x")
            {
                if (xDiff >= 0) // positive
                {
                    if (currentXPos >= destinationPos.x)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
                else
                {
                    if (currentXPos <= destinationPos.x)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
            }
            else if(check == "y")
            {
                if (yDiff >= 0) // positive
                {
                    if (currentYPos >= destinationPos.y)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
                else
                {
                    if (currentYPos <= destinationPos.y)
                    {
                        objectRB.position = new Vector2(destinationPos.x, destinationPos.y);
                        break;
                    }
                }
            }
            
            /*if((Mathf.Abs(currentXPos) >= Mathf.Abs(destinationPos.x)) && (Mathf.Abs(currentYPos) >= Mathf.Abs(destinationPos.y)))
            {
                
                break;
            }*/
            objectRB.MovePosition(new Vector2(currentXPos,currentYPos));
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<SFXManager>().PlayDeathSFX();
        FindObjectOfType<CollectableManager>().ResetOnFinalBossSceneFailed();
        gameHandler.StopTheGame();
        FindObjectOfType<FinalBossSituationManager>().GetComponent<IWinnable>().AssignSceneSituation("fail");
        //winLoseManager.levelSituation = "fail";
        StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));

    }

}
