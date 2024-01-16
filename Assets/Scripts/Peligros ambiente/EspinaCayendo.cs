using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinaCayendo : MonoBehaviour
{
    public Vector3 dimensionesCaja; 

    BoxCollider2D BoxCollider2DEspina;

    Rigidbody2D rgb2Espina;

    bool EstaCayendo = false;

    public float distancia;

    public float probabilidadCaer;

    public float actualizar;

    public Transform contrSuelo1;

    public Transform contrSuelo2;


    float randomNumber;
    public LayerMask capaPlataforma;

    public float tiempoDestruccion = 2;






    void Start()
    {
        rgb2Espina = GetComponent<Rigidbody2D>();
        BoxCollider2D[] boxColliders = GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(EstaCayendo == false){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distancia);
            RaycastHit2D hit2 = Physics2D.Raycast(contrSuelo1.position, Vector2.down, distancia);
            RaycastHit2D hit3 = Physics2D.Raycast(contrSuelo2.position, Vector2.down, distancia);

            Debug.DrawRay(transform.position, Vector2.down * distancia, Color.red);
            Debug.DrawRay(contrSuelo1.transform.position, Vector2.down * distancia, Color.red);
            Debug.DrawRay(contrSuelo2.transform.position, Vector2.down * distancia, Color.red);
            if(hit.transform != null)
            {
                if((hit.transform.tag == "Player") || (hit2.transform.tag == "Player") || (hit3.transform.tag == "Player")){
                    if(Time.time >= actualizar){
                    randomNumber = Random.Range(0, 11);
                    float probability = 11 / (float)randomNumber;
                    if(probability <=  probabilidadCaer){
                        rgb2Espina.gravityScale = 5;
                        EstaCayendo = true;
                    } else {
                            //Animacion
                    }
                
                    actualizar = Time.time + 1.5f;
                    }
                } 
                 

            }
 
        
        }
    
        }
    
private void OnCollisionEnter2D(Collision2D other) {
    if ((capaPlataforma.value & (1 << other.gameObject.layer)) != 0 && EstaCayendo)
        {
            gameObject.tag = "Untagged";
            Destroy(gameObject, tiempoDestruccion);
        }
}
    
}

    

