using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumaRebotadora : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 LastVelocity;

    public bool destruirAlAparecer = true;

    Collider2D miCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(destruirAlAparecer){
            Destroy(gameObject, 8f);
        }
        

        miCollider = GetComponent<Collider2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        LastVelocity = rb.velocity;
        transform.Rotate(0f, 0f, 80f * Time.deltaTime);
        

    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        
        var speed = LastVelocity.magnitude;
        var direccion = Vector3.Reflect(LastVelocity.normalized, other.contacts[0].normal);
        rb.velocity = direccion * Mathf.Max(speed, 0f);

        
    }

    


}
