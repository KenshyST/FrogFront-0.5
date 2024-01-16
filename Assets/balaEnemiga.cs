using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaEnemiga : MonoBehaviour
{
    public LayerMask plataformaLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "machete")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "plataforma"){
            Destroy(gameObject);
        }

        if (plataformaLayer == (plataformaLayer | (1 << other.gameObject.layer)))
        {
            // El objeto colisionó con un objeto en la capa "Plataforma"
            Destroy(gameObject);
        }

    }

   private void OnTriggerStay2D(Collider2D other) {
    if (other.gameObject.tag == "machete")
        {
            Destroy(gameObject);
        }
   }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "plataforma"){
            Destroy(gameObject);
        }

        if (plataformaLayer == (plataformaLayer | (1 << other.gameObject.layer)))
        {
            // El objeto colisionó con un objeto en la capa "Plataforma"
            Destroy(gameObject);
        }
    }
}
