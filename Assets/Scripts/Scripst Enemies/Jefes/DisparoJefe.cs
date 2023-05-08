using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJefe : MonoBehaviour
{
    public GameObject[] puntosDisparo;

    public GameObject jugador;

    public int velocidadProyectil = 5;

    public GameObject Proyectil;

    public float tiempoDisparoEspecial = 10f;

    public float tiempoDisparoNormal = 4f;

    bool realizandoAtaqueNormal = false;

    private float tiempo;

    private float tiempoUltimoAtaqueDisparo;
    private float tiempoSiguienteDisparo = 0f; // Tiempo en el que se podrá realizar el siguiente disparo


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tiempoUltimoAtaqueDisparo = Time.deltaTime;

        if(realizandoAtaqueNormal){
            tiempo = Time.deltaTime;

        } else{
            tiempo = 0;
        }




    }

    private void FixedUpdate()
    {
        if (tiempoUltimoAtaqueDisparo >= tiempoDisparoNormal)
        {
            realizandoAtaqueNormal = true;
            disparoNormal();


        }
    }

    void disparoNormal()
    {
        if (tiempo == 0.1f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 0.25f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 0.40f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 0.50f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 0.65f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 0.75f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 0.85f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            // Llamamos al método para disparar

        }

        if (tiempo == 1f)
        {
            int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
            GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation); // Creamos una instancia de la bala en el punto de dispar
            Vector3 direccion = (jugador.transform.position - transform.position).normalized;
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D de la bala
            rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
            Destroy(bala, 2f);
            tiempo = 0;
            tiempoUltimoAtaqueDisparo = 0f;
            // Llamamos al método para disparar

        }


    }
}
