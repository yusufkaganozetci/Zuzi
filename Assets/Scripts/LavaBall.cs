using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    [SerializeField] float minYVelocity;
    [SerializeField] float maxYVelocity;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, Random.Range(minYVelocity, maxYVelocity));
    }

    private void Update()
    {
        if(transform.position.y <= -7)
        {
            Destroy(gameObject);
        }
    }


}
