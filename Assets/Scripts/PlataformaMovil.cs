using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public GameObject[] puntos;
    public float velocidad = 1f;
    
    private int siguientePunto = 0;
    private bool haciaSiguientePunto = true;
    
    private void FixedUpdate()
    {
        if (puntos.Length > 1)
        {
            if (haciaSiguientePunto)
            {
                transform.position = Vector3.MoveTowards(transform.position, puntos[siguientePunto].transform.position, velocidad * Time.deltaTime);
                if (transform.position == puntos[siguientePunto].transform.position)
                {
                    haciaSiguientePunto = false;
                    siguientePunto++;
                    if (siguientePunto >= puntos.Length)
                    {
                        siguientePunto = 0;
                    }
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, puntos[siguientePunto].transform.position, velocidad * Time.deltaTime);
                if (transform.position == puntos[siguientePunto].transform.position)
                {
                    haciaSiguientePunto = true;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("plataforma"))
        {
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        }

        
    }

   
}
    


