using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] bool isObstacle = false;
    [SerializeField] float parallaxSpeed;
    [SerializeField] Transform parallax;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Rigidbody2D rb;
    private ScaleManager scaleManager;
    private GameHandler gameHandler;
    Zuzi player;

    private float minXPosition, maxXPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        AssignVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.GetTheGameIsStarted())
        {
            Move();
        }
    }

    private void AssignVariables()
    {
        player = FindObjectOfType<Zuzi>();
        scaleManager = FindObjectOfType<ScaleManager>();
        gameHandler = FindObjectOfType<GameHandler>();
        if (!isObstacle)
        {
            float firstPos = -1 * scaleManager.GetCameraWidth() / 2;
            float lastPos = firstPos + (sr.size.x / 2);
            maxXPosition = lastPos;
            firstPos = scaleManager.GetCameraWidth() / 2;
            minXPosition = firstPos - (sr.size.x / 2);
        }
    }

    private void Move()
    {
        if (isObstacle) // converted smoothdelta time to delta time
        {
            parallax.Translate(-1 * parallaxSpeed * Time.smoothDeltaTime, 0, 0);
        }
        else
        {
            if (player.GetPlayerXVelocity() > 0 && transform.position.x > minXPosition)
            {
                parallax.Translate(-1 * parallaxSpeed * Time.deltaTime, 0, 0);
            }

            else if (player.GetPlayerXVelocity() < 0 && transform.position.x < maxXPosition)
            {
                parallax.Translate(1 * parallaxSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

   
}
