using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirCaja : MonoBehaviour
{
    public GameObject[] fragmentoPrefab;

    public int contadorBalas;

    public int balasColisionadasActualmente = 0;


    public bool tienePedazos = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ProyectilAk") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            balasColisionadasActualmente += 1;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            
            Vector3 contactPoint = transform.position; // Utiliza la posición del proyectil como punto de contacto
            Vector3 direction = (contactPoint - collision.transform.position).normalized; // Calcula la dirección desde el proyectil hasta el punto de contacto
            float forceMagnitude = 500f; // Magnitud de la fuerza a aplicar
            rb.AddForceAtPosition(direction * forceMagnitude, contactPoint); // Aplica la fuerza en el punto de contacto
            Destroy(collision.gameObject);

            if(balasColisionadasActualmente >= contadorBalas){
                Destroy(gameObject);
                if(tienePedazos){
                    for (int i = 0; i < 3; i++) // Cambia el número para generar más o menos fragmentos de caja
                {
                    
                    GameObject fragmento = Instantiate(fragmentoPrefab[Random.Range(0,fragmentoPrefab.Length)], transform.position, Quaternion.identity);
                    
                    Vector3 originalScale = transform.localScale; // Almacena la escala original del objeto
                    transform.localScale = Vector3.one; // Establece la escala actual a 1, lo que restablece la escala del objeto
                    fragmento.transform.localScale = Vector3.Scale(fragmento.transform.localScale, originalScale); // Multiplica la escala de los pedazos por la escala original
                    
                    Rigidbody2D rb2d = fragmento.GetComponent<Rigidbody2D>();
                    Vector2 direccion = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)).normalized;
                    Quaternion rotacionAleatoria = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                    fragmento.transform.rotation = rotacionAleatoria;
                    rb2d.AddForce(direccion * 200f);

                }
                }

                
            }
            
            
        }

        if (collision.gameObject.tag == "machete") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            balasColisionadasActualmente += 5;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            
            Vector3 contactPoint = transform.position; // Utiliza la posición del proyectil como punto de contacto
            Vector3 direction = (contactPoint - collision.transform.position).normalized; // Calcula la dirección desde el proyectil hasta el punto de contacto
            float forceMagnitude = 1800f; // Magnitud de la fuerza a aplicar
            rb.AddForceAtPosition(direction * forceMagnitude, contactPoint); // Aplica la fuerza en el punto de contacto

            if(balasColisionadasActualmente >= contadorBalas){
                Destroy(gameObject);
                if(tienePedazos){
                    for (int i = 0; i < 3; i++) // Cambia el número para generar más o menos fragmentos de caja
                {
                    
                    GameObject fragmento = Instantiate(fragmentoPrefab[Random.Range(0,fragmentoPrefab.Length)], transform.position, Quaternion.identity);
                    
                    Vector3 originalScale = transform.localScale; // Almacena la escala original del objeto
                    transform.localScale = Vector3.one; // Establece la escala actual a 1, lo que restablece la escala del objeto
                    fragmento.transform.localScale = Vector3.Scale(fragmento.transform.localScale, originalScale); // Multiplica la escala de los pedazos por la escala original
                    
                    Rigidbody2D rb2d = fragmento.GetComponent<Rigidbody2D>();
                    Vector2 direccion = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)).normalized;
                    Quaternion rotacionAleatoria = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                    fragmento.transform.rotation = rotacionAleatoria;
                    rb2d.AddForce(direccion * 200f);

                }
                }

                
            }
            
            
        }
    }
}
