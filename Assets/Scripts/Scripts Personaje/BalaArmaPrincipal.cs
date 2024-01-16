using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaArmaPrincipal : MonoBehaviour
{
    public LayerMask capaPlataforma;
    // Start is called before the first frame update
    void Start()
    {
        GameObject objetoAIgnorar = GameObject.FindGameObjectWithTag("Player");
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(boxCollider, objetoAIgnorar.GetComponent<BoxCollider2D>());
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
         if ((capaPlataforma.value & (1 << other.gameObject.layer)) != 0   && (!other.CompareTag("PlataformaUnSentido")))
        {
            Destroy(gameObject);
        }

        
        
        
        

        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }

    
}
