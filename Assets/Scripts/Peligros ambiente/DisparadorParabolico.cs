using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorParabolico : MonoBehaviour
{
   public GameObject proyectilPrefab; // Prefab del proyectil a disparar
    public Transform jugador; // Transform del jugador al que disparar

    public float velocidadInicial = 10f; // Velocidad inicial del proyectil
    public float gravedad = 9.81f; // Gravedad del mundo en el que se desarrolla el juego

    public float curva = 3f; // Altura de la curva del proyectil
    
    public Transform canon;
    public Camera camara;

    public float cadencia;

    public float siguienteDisparo;
    private void Update()
    {
        // Si el jugador no está activo, no hacemos nada
        Plane[] planosDeCorte = GeometryUtility.CalculateFrustumPlanes(camara);

        // Calculamos la dirección y la distancia entre el disparador y el jugador
        Vector3 direccion = jugador.position - transform.position;
        float distancia = direccion.magnitude;

        // Calculamos la velocidad necesaria para que el proyectil alcance al jugador
        float velocidad = Mathf.Sqrt(distancia * gravedad / Mathf.Sin(2f * Mathf.PI / 4f));

        // Si la velocidad necesaria es menor que la velocidad inicial, utilizamos la velocidad necesaria
        if (velocidad < velocidadInicial) velocidad = velocidadInicial;

        // Calculamos el ángulo de elevación necesario para el disparo
        float angulo = Mathf.Asin(distancia * gravedad / (velocidad * velocidad)) / 2f;
        
        angulo += curva;
        

        if (GeometryUtility.TestPlanesAABB(planosDeCorte, this.GetComponent<Renderer>().bounds))
        {
            if(Time.time >= siguienteDisparo){
            // Calculamos la dirección del disparo
            Vector3 direccionDisparo = direccion.normalized * Mathf.Cos(angulo) * velocidad + Vector3.up * Mathf.Sin(angulo) * velocidad;

            // Creamos el proyectil y le aplicamos la dirección del disparo
            GameObject proyectil = Instantiate(proyectilPrefab, canon.transform.position, Quaternion.identity) as GameObject;
            Rigidbody2D proyectilRb = proyectil.GetComponent<Rigidbody2D>();
            proyectilRb.velocity = direccionDisparo;
            siguienteDisparo = Time.time + cadencia;
            }
       
        }   
        
    }
}
