using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zuzi : MonoBehaviour
{
    private int sceneIndex; // if 0 its canopus first if 1 its canopus second


    
    [SerializeField] float movementSpeed;

    [SerializeField] float jumpingSpeed;

    [SerializeField] BoxCollider2D feetCollider;

    [SerializeField] float minXPosition;//These two floats are used in canopus and vega
    [SerializeField] float maxXPosition;

    private ScaleManager scaleManager;
    private SceneLoader sceneLoader;
    private CollectableManager collectableManager;
    private GameHandler gameHandler;
    private SFXManager sfxManager;

    private float zuziHorizontalSpeedInput;
    private float zuziVerticalSpeedInput;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private float maxYPosition;
    private float minYPosition;

    private WinLoseManager winLoseManager;

    private IWinnable winnableObject;

    void Start()
    {
        winnableObject = GameObject.FindGameObjectWithTag("Winnable").GetComponent<IWinnable>();
        
        AssignVariables();
    }

    void Update()
    {
        HandleMovementInputs();
        HandleGameOver();
    }

    private void HandleMovementInputs()
    {
        if(sceneIndex == 2 && gameHandler.GetTheGameIsStarted())
        {
            zuziHorizontalSpeedInput = Input.GetAxis("Horizontal");
            zuziVerticalSpeedInput = Input.GetAxis("Vertical");
        }
        else if(gameHandler.GetTheGameIsStarted())
        {
            zuziHorizontalSpeedInput = Input.GetAxis("Horizontal");
            zuziVerticalSpeedInput = Input.GetAxisRaw("Vertical");
            Flip();
            if (zuziVerticalSpeedInput < 0)
            {
                zuziVerticalSpeedInput = 0;
            }

            if (Mathf.Abs(zuziHorizontalSpeedInput) > 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        
        /*if ((sceneIndex == 1 || sceneIndex == 3 || sceneIndex == 4 || sceneIndex == 6) && gameHandler.GetTheGameIsStarted())
        {
            zuziHorizontalSpeedInput = Input.GetAxis("Horizontal");
            zuziVerticalSpeedInput = Input.GetAxisRaw("Vertical");
            Flip();
            if (zuziVerticalSpeedInput < 0)
            {
                zuziVerticalSpeedInput = 0;
            }
            
            if (Mathf.Abs(zuziHorizontalSpeedInput) > 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else if (sceneIndex == 2 && gameHandler.GetTheGameIsStarted())
        {
            zuziHorizontalSpeedInput = Input.GetAxis("Horizontal");
            zuziVerticalSpeedInput = Input.GetAxis("Vertical");
        }*/
    }
    
    private void HandleGameOver()
    {
        if (sceneIndex == 1 || sceneIndex == 5)
        {
            CheckIsFinished();
        }

    }

    private void CheckIsFinished()
    {
        if(gameHandler.GetTheGameIsPlaying() && transform.position.y <= -6)
        {
            sfxManager.PlayDeathSFX();
            gameHandler.StopTheGame();
            winnableObject.AssignSceneSituation("fail");
            StartCoroutine(winLoseManager.ManageWinOrLose(0));
            /*winLoseManager.levelSituation = "fail";
            StartCoroutine(winLoseManager.ManageWinOrLose(0));*/

        }
    }

    private void AssignVariables()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        winLoseManager = FindObjectOfType<WinLoseManager>();
        sfxManager = FindObjectOfType<SFXManager>();
        sceneIndex = gameHandler.GetCurrentSceneIndex();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sceneIndex == 1 || sceneIndex == 3 || sceneIndex == 5 || sceneIndex == 6)//Canopus or Vega
        {
            collectableManager = FindObjectOfType<CollectableManager>();
            animator = GetComponent<Animator>();
            sceneLoader = FindObjectOfType<SceneLoader>();
        }
        else if(sceneIndex == 2)
        {
            scaleManager = FindObjectOfType<ScaleManager>();
            minYPosition = (-scaleManager.GetCameraHeight() / 2) + (spriteRenderer.bounds.size.y / 2);
            maxYPosition = (scaleManager.GetCameraHeight() / 2) - (spriteRenderer.bounds.size.y / 2);
        }
    }
    
    private void Flip()
    {
        if(zuziHorizontalSpeedInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (zuziHorizontalSpeedInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if ((sceneIndex == 1 || sceneIndex == 3 || sceneIndex == 5) && gameHandler.GetTheGameIsStarted())
        {
            Walk();
            Jump();
        }
        else if (sceneIndex == 2)
        {
            MoveFourWays();
        }
        else if(sceneIndex == 6)
        {
            Walk();
            
        }
    }

    

    private void MoveFourWays()
    {
        float yVelocity = zuziVerticalSpeedInput * Time.fixedDeltaTime * movementSpeed;
        if ((transform.position.y <= minYPosition && yVelocity < 0) ||
            (transform.position.y >= maxYPosition && yVelocity > 0))
        {
            yVelocity = 0;
        }
        rb.velocity = new Vector2(zuziHorizontalSpeedInput * Time.fixedDeltaTime * movementSpeed, yVelocity);
    }

    private void Walk()
    {
        float xSpeed = movementSpeed * Time.fixedDeltaTime * zuziHorizontalSpeedInput;
        if (CanWalk(xSpeed))
        {
            rb.velocity = new Vector2(xSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (IsTouchingGround() && zuziVerticalSpeedInput > 0)
        {
            sfxManager.PlayJumpSFX();
            rb.velocity = new Vector2(rb.velocity.x, jumpingSpeed * Time.fixedDeltaTime);
        }
    }

    private bool IsTouchingGround()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool CanWalk(float xSpeed)
    {
        if((transform.position.x <= minXPosition && xSpeed < 0) ||
            (transform.position.x >= maxXPosition && xSpeed > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return false;
        }
        return true;
    }

    public float GetPlayerXVelocity()
    {
        return rb.velocity.x;
    }

    public IEnumerator PlayZuziEnterofCapellaAnim(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Animator>().SetTrigger("EnterCapella");
        yield return new WaitForSeconds(1);
        StartCoroutine(sceneLoader.LoadSceneWithTransition(0, gameHandler.GetCurrentSceneIndex()+1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishScene")) // on finish object collected
        {
            gameHandler.StopTheGame();
            Destroy(collision.gameObject);

            StartCoroutine(winLoseManager.ManageWinOrLose(0));
        }

        else if (collision.CompareTag("LavaBall"))
        {
            sfxManager.PlayDeathSFX();
            gameHandler.StopTheGame();
            //winLoseManager.levelSituation = "fail";

            winnableObject.AssignSceneSituation("fail");
            StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));
            
        }

        else if(collision.CompareTag("Puzzle") || collision.CompareTag("Egg") || collision.CompareTag("Rocket"))
        {
            collectableManager.IncreasePieceCount(collision.gameObject);
        }

        else if (collision.CompareTag("Key"))
        {
            gameHandler.StopTheGame();
            collectableManager.CollectTheKey(collision.gameObject);
            winnableObject.AssignSceneSituation("success");
            StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));
            //StartCoroutine(winLoseManager.ManageWinOrLose(1));
        }
        
        
    }


}
