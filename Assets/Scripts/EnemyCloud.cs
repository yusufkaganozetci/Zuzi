using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloud : MonoBehaviour
{
    [SerializeField] float yVelocity;
    private ScaleManager scaleManager;
    private SceneLoader sceneLoader;
    private SFXManager sfxManager;
    private GameHandler gameHandler;
    private CollectableManager collectableManager;
    private WinLoseManager winLoseManager;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float minXPosition;
    private float maxXPosition;

    private float minYPosition;
    private float maxYPosition;
    private float leftXPosition;
    private float rightXPosition;

    private float xVelocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        AssignVariables();
    }

    private void AssignVariables()
    {
        scaleManager = FindObjectOfType<ScaleManager>();
        collectableManager = FindObjectOfType<CollectableManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        sfxManager = FindObjectOfType<SFXManager>();
        gameHandler = FindObjectOfType<GameHandler>();
        winLoseManager = FindObjectOfType<WinLoseManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        xVelocity = Random.Range(3, 7);
        minYPosition = (-scaleManager.GetCameraHeight() / 2) + (spriteRenderer.bounds.size.y / 2);
        maxYPosition = (scaleManager.GetCameraHeight() / 2) - (spriteRenderer.bounds.size.y / 2);
        leftXPosition = (-scaleManager.GetCanopusSecondBackgroundWidth() / 2) - (spriteRenderer.bounds.size.x / 2);
        rightXPosition = (scaleManager.GetCanopusSecondBackgroundWidth() / 2) + (spriteRenderer.bounds.size.x / 2);
        SetPositionAndVelocity();
    }

    private void SetPositionAndVelocity()
    {
        int choice = Random.Range(0, 2);
        float yPosition = Random.Range(minYPosition, maxYPosition);
        if (choice == 0) // means left to right
        {
            transform.position = new Vector3(leftXPosition, yPosition, transform.position.z);
            rb.velocity = new Vector2(xVelocity, 0);
        }
        else if(choice == 1) // means right to left
        {
            transform.position = new Vector3(rightXPosition, yPosition, transform.position.z);
            rb.velocity = new Vector2(-xVelocity, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zuzi"))//death of zuzi
        {
            sfxManager.PlayDeathSFX();
            gameHandler.StopTheGame();
            FindObjectOfType<TheFootofCanopusSituationManager>().GetComponent<IWinnable>().AssignSceneSituation("fail");
            StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));
            /*winLoseManager.levelSituation = "fail";
            StartCoroutine(winLoseManager.ManageWinOrLose(0.5f));*/
        }
        
        else if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
        
    }

}