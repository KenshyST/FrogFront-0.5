using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorRecto : MonoBehaviour
{
    public Transform jugador; // Referencia al objeto del jugador
    public GameObject proyectilPrefab; // Prefab del proyectil a disparar
    public float fuerzaDisparo = 10f; // Fuerza con la que se dispara el proyectil

    public Transform canon;
    public Camera camara;

    public float cadencia;

    public float siguienteDisparo;


    void Update()
    {
        //
        Plane[] planosDeCorte = GeometryUtility.CalculateFrustumPlanes(camara);

        // Calcula la dirección hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;

        // Calcula el ángulo de rotación hacia el jugador
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Rota el objeto del disparador hacia el jugador
        transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);

        // Cada x tiempo
        if (GeometryUtility.TestPlanesAABB(planosDeCorte, this.GetComponent<Renderer>().bounds))
        {
            if(Time.time >= siguienteDisparo){
            GameObject proyectil = Instantiate(proyectilPrefab, canon.transform.position, Quaternion.identity) as GameObject;

            // Le aplica una fuerza para que se dispare en la dirección calculada
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            rb.AddForce(direccion * fuerzaDisparo, ForceMode2D.Impulse);
                
            siguienteDisparo = Time.time + cadencia;
            }
       
        }   
    }
}
