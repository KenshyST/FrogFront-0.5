using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    // Variables para controlar la explosión de la bomba
    public float radioExplosion = 1.5f;
    public float fuerzaExplosion = 1000f;
    public float explosionDelay = 6f;
    public float explosionDamage = 50f;

    //public GameObject explosionEffect;

    // Referencia al AudioSource para reproducir el sonido de la explosión
    private AudioSource audioSource;

    void Start () {
        // Obtener la referencia al AudioSource
        //audioSource = GetComponent<AudioSource>();

        // Programar la explosión de la bomba después de un retraso
        float tiempoenElQueExplotara = Random.Range(3f, explosionDelay); // Aqui se puede predecir en cuanto tiempo explotara, para la animacion
        Invoke("Explode", tiempoenElQueExplotara);
    }

    void Explode () {
        // Reproducir el sonido de la explosión
        //audioSource.Play();

        // Obtener una lista de todos los objetos dentro del radio de la explosión
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radioExplosion);

        foreach (Collider2D collider in colliders) {
            // Aplicar fuerza a los objetos dentro del radio de la explosión
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Vector2 explosionDirection = rb.transform.position - transform.position;
                rb.AddForce(explosionDirection.normalized * fuerzaExplosion, ForceMode2D.Impulse);
            }

            // Infligir daño a los objetos dentro del radio de la explosión
            //Health health = collider.GetComponent<Health>();
            //if (health != null) {
            //    health.TakeDamage(explosionDamage);
            //}
        }

        // Reproducir un efecto de explosión
        //Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Desactivar la bomba
        Destroy(gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, 100 * Time.deltaTime);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExplosion);
    }
}
