using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoheteLanzado : MonoBehaviour
{
    public LayerMask capaPlataforma;
    public float fuerzaPersecucion = 5f; // Fuerza de la persecución
    public string tagEnemigo = "Enemy"; // Tag del objeto a perseguir

    public Rigidbody2D rbProyectil;

    public Camera camara;

    public float tiempoDestruccion = 3f;

    public string projectileName = "Projectile";
    
    
    void Start()
    {
        GameObject Gameobjectcamara = GameObject.Find("Main Camera");
        camara = Gameobjectcamara.GetComponent<Camera>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Rigidbody2D rbProyectil = GetComponent<Rigidbody2D>();
        rbProyectil.velocity *= 0.3f;
        Destroy(gameObject, tiempoDestruccion);
         // Buscar el prefab con el nombre "Projectile"
        GameObject projectilePrefab = Resources.Load<GameObject>(projectileName);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Plane[] planosDeCorte = GeometryUtility.CalculateFrustumPlanes(camara);
        
         GameObject enemigoCercano = BuscarEnemigoCercano();
         if (enemigoCercano != null && GeometryUtility.TestPlanesAABB(planosDeCorte, enemigoCercano.GetComponent<Renderer>().bounds))
            {
                
                // Se calcula la dirección hacia el enemigo más cercano
                Vector2 direccion = enemigoCercano.transform.position - transform.position;
                rbProyectil.velocity *= 0.75f;
                float anguloRotacion = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

                // Se normaliza la dirección
                direccion.Normalize();

                // Se aplica una fuerza al proyectil para que persiga al enemigo
                rbProyectil.AddForce(direccion * fuerzaPersecucion, ForceMode2D.Impulse);
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, enemigoCercano.transform.position);
                transform.rotation = Quaternion.Euler(0f, 0f, anguloRotacion);
            }
            else
            {
                
            }

    }

    private void OnTriggerEnter2D(Collider2D other) {
         if ((capaPlataforma.value & (1 << other.gameObject.layer)) != 0   && (!other.CompareTag("PlataformaUnSentido")))
        {
            Destroy(gameObject);
        }
        
        
        

        
    }

     private GameObject BuscarEnemigoCercano()
    {
        // Se obtienen todos los objetos con el tag "enemigo" en la escena
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigo);

        // Se inicializa la distancia mínima a un valor muy grande
        float distanciaMinima = Mathf.Infinity;

        // Se inicializa la referencia al enemigo más cercano a null
        GameObject enemigoCercano = null;

        // Se recorren todos los enemigos y se busca el más cercano
        foreach (GameObject enemigo in enemigos)
        {
            

            if (enemigo.name.Contains("ShootingEnemy") || (enemigo.name.Contains("Personaje") && (enemigo.gameObject.tag =="Player")))
            {
                float distancia = Vector2.Distance(transform.position, enemigo.transform.position);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    enemigoCercano = enemigo;
                }
            }
        }

        return enemigoCercano;
    }
}
