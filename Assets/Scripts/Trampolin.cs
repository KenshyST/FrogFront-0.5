using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
     public float FuerzaEmpuje = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            Vector3 contactPoint = collision.transform.position; 
            Vector3 direction = (contactPoint - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(direction * FuerzaEmpuje, contactPoint); // Aplica la fuerza en el punto de contacto
        }
    }
}
