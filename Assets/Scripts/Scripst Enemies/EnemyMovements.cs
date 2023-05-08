using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    public bool isStatic;
    public bool isWalker;
    public bool walksRight;
    public bool isPatrol;
    public bool shouldWait;
    public float timeToWait;
    public bool isWaiting;

    public Transform wallCheck, pitCheck, groundCheck;
    bool wallDetected, pitDetected, isGrounded;

    public float detectionRadius;
    public LayerMask whatIsGround;

    public Transform pointA, pointB;
    bool goToA, goToB;
    void Start()
    {
        goToA = true;
        speed = GetComponent<Enemy>().speed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius, whatIsGround);

        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGround);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, whatIsGround);

        if ((pitDetected || wallDetected) && isGrounded)
        {
            Flip();
        }


        CheckScale();
    }
    private void FixedUpdate()
    {
        if(isStatic)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //Aquí va la animación de IDLE (se activa)

        }

        if (isWalker)
        {
            //Aquí se desactiva el IDLE
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!walksRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }

        if (isPatrol)
        {
            //desactivar idle
            if (goToA)
            {
                if (!isWaiting)
                {
                    //desactivar anim idle
                    rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                }
                
                if(Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {

                    if (shouldWait)
                    {
                        StartCoroutine(Waiting());
                        
                    }

                    Flip();
                    goToA = false;
                    goToB = true;
                }
            }
            if (goToB)
            {
                if (!isWaiting)
                {
                    //desactivar anim idle
                    rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                }
               
                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {

                    if (shouldWait)
                    {
                        StartCoroutine(Waiting());

                    }
                    Flip();
                    goToB = false;
                    goToA = true;
                }

            }
        }
    }

    IEnumerator Waiting()
    {
        //activar idle
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(timeToWait);
        isWaiting = false;
        //desactivar idle
        Flip();
    }

    public void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    public void CheckScale()
    {
        if ((rb.velocity.x == speed) && transform.localScale.x == -1)
        {
            Flip();
        }
        if ((rb.velocity.x == -speed) && transform.localScale.x == 1)
        {
            Flip();
        }
    }
}
