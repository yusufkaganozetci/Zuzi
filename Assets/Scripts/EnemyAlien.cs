using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlien : MonoBehaviour
{
    private Vector2 velocity;
    
    private Rigidbody2D rb;
   
    public void StartEnemyMovement(Vector2 enemyVelocity)
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = enemyVelocity;
        rb.velocity = velocity;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Zuzi"))
        {
            FindObjectOfType<SFXManager>().PlayDeathSFX();
            FindObjectOfType<GameHandler>().StopTheGame();
            FindObjectOfType<CapellaSituationManager>().GetComponent<IWinnable>().AssignSceneSituation("fail");
            //FindObjectOfType<WinLoseManager>().levelSituation = "fail";
            StartCoroutine(FindObjectOfType<WinLoseManager>().ManageWinOrLose(0.5f));
        }
    }
}
