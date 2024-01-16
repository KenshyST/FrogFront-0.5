using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeAve : MonoBehaviour
{
    public GameObject[] puntos;

    public GameObject jugador;
    public float velocidad;

    private int siguientePunto = 0;
    private bool haciaSiguientePunto = true;

    public GameObject iconoPrefab; // Asignar el prefab del icono en el inspector

    public GameObject iconoCrosshair;

    private GameObject icono; // Referencia al objeto del 

    private GameObject icono2;

    // Variables para controlar los patrones de ataque del jefe
    public float IntervaloAtaque = 8f;
    private float tiempoUltimoAtaque = 0f;

    private bool realizandoAtaqueEspecial = false;

    private audioManagement audioManagement;


    //<--------------Ataque Bomba ----------------------->//

    // Referencia al objeto que se generará
    public GameObject objetoBomba;

    // Fuerza máxima con la que se lanzará el objeto generado
    public float maxLaunchForce = 500f;

    // Tiempo en segundos entre cada generación de objeto
    public float generationInterval = 0.2f;

    // Rango en el que se generará el ángulo de lanzamiento (en grados)
    public float launchAngleRange = 180f;

    //<----------------Disparo Basico -------------------->
    public GameObject[] puntosDisparo;

    public float velocidadProyectil = 1;

    public GameObject Proyectil;

    public GameObject ProyectilPerseguidor;

    public GameObject PlumaRebotadora;

    public GameObject PlumaEscudo;
    public GameObject PlumaEscudo2;
    public float tiempoDisparoEspecial = 10f;

    public float tiempoDisparoNormal = 4f;

    private float tiempoUltimoAtaqueDisparo;

    private float tiempoUltimoAtaqueDisparoEspecial;
    private float tiempoSiguienteDisparo; // Tiempo en el que se podrá realizar el siguiente disparo

    private bool realizandoTacleada = false;

    private Vector2 ultimaPosicionJugador;

    private bool rotandoEscudo = false;


    float velocidadInicial;

    public GameObject[] puntosInvocacion;

    public GameObject SawBlade;
    public float fireRate = 0.7f;

    private float nextFireTime = 0f;


    void Start()
    {
        siguientePunto = Random.Range(0, 8);
        velocidadInicial = velocidad;
        audioManagement = FindObjectOfType<audioManagement>();
    }
    private void Update()
    {
        tiempoUltimoAtaque += Time.deltaTime;

        tiempoUltimoAtaqueDisparo += Time.deltaTime;

        tiempoUltimoAtaqueDisparoEspecial += Time.deltaTime;

        Debug.Log("tiempoSiguienteDisparo: " + tiempoSiguienteDisparo);

    }

    private void FixedUpdate()
    {
        if (Time.time >= nextFireTime){
                audioManagement.seleccionAudio(20, 0.1f);
                nextFireTime = Time.time + fireRate;
        }

        if (realizandoAtaqueEspecial == false || (realizandoAtaqueEspecial == true && realizandoTacleada == true))
        {
            movimientoJefe(ultimaPosicionJugador);
        }

        if (tiempoUltimoAtaque >= IntervaloAtaque)
        {

            StartCoroutine(DetenerseAtaqueEspecial(0.45f));

        }

        if (tiempoUltimoAtaqueDisparo >= tiempoDisparoNormal)
        {
            CrearIconoCrosshair(jugador.transform.position);
            StartCoroutine(ReducirVelocidadJefe(2));
            audioManagement.seleccionAudio(2, 0.2f);
            for (float i = 0; i < 65; i++)
            {
                StartCoroutine(DispararConDelay(i / 50));
                


            }
            tiempoUltimoAtaqueDisparo = 0f;


        }

        if (tiempoUltimoAtaqueDisparoEspecial >= IntervaloAtaque)
        {
            PatronesDisparosEspeciales();

        }





    }

    void movimientoJefe(Vector3 ultimaposicion = default(Vector3))
    {

        Vector3 jugadorPosicion = jugador.transform.position;
        int puntoCercanoIndex = 0;
        float distanciaMinima = Mathf.Infinity;

        if (realizandoTacleada == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, ultimaposicion, (velocidad + 25) * Time.deltaTime);
        }
        else
        {
            for (int i = 0; i < puntos.Length; i++)
            {
                float distancia = Vector3.Distance(puntos[i].transform.position, jugadorPosicion);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    puntoCercanoIndex = i;
                }
            }

            if (puntos.Length > 1)
            {
                if (haciaSiguientePunto)
                {
                    transform.position = Vector3.MoveTowards(transform.position, puntos[siguientePunto].transform.position, velocidad * Time.deltaTime);

                    if (transform.position == puntos[siguientePunto].transform.position)
                    {
                        float probabilidad = Random.Range(0f, 1f);
                        if (probabilidad <= 0.5f)
                        {
                            haciaSiguientePunto = false;
                            siguientePunto = Random.Range(0, 8);
                            CrearIcono();
                        }
                        else
                        {
                            haciaSiguientePunto = false;
                            siguientePunto = puntoCercanoIndex;
                            CrearIcono();
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



    }

    void PatronesAtaques()
    {
        audioManagement.seleccionAudio(21, 0.1f);
        int IndexPatron = Random.Range(0, 3); // Número de patrones de ataque disponibles
        if (IndexPatron == 0)
        {


            // Si ha pasado el tiempo suficiente, generar un nuevo objeto y lanzarlo
            for (int i = 1; i < 4; i++)
            {

                // Generar un nuevo objeto
                GameObject newObject = Instantiate(objetoBomba, puntosDisparo[Random.Range(0, puntosDisparo.Length)].transform.position, Quaternion.identity);

                // Lanzar el objeto con una fuerza y dirección aleatorias
                Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Calcular una fuerza y dirección aleatorias
                    float launchForce = Random.Range(30f + Random.Range(0, 5 + i), maxLaunchForce);
                    float launchAngle = Random.Range(-launchAngleRange, launchAngleRange);
                    Vector3 launchDirection = Quaternion.AngleAxis(launchAngle, Vector3.up) * Vector3.forward;

                    // Aplicar la fuerza al objeto
                    rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
                }




            }
            tiempoUltimoAtaque = 0f;
            realizandoAtaqueEspecial = false;
            IndexPatron = 100;

        }
        else if (IndexPatron == 1)
        {
            ultimaPosicionJugador = jugador.transform.position;
            Destroy(icono);
            StartCoroutine(taclear(2f));



        }
        else if (IndexPatron == 2)
        {
            // Generar un nuevo objeto
                GameObject newObject = Instantiate(SawBlade, puntosInvocacion[0].transform.position, Quaternion.identity);
                PlataformaMovil PlataformaMovil = newObject.GetComponent<PlataformaMovil>();
                PlataformaMovil.puntos = puntosInvocacion;
                Destroy(newObject, 10f);

        }
        tiempoUltimoAtaque = 0f;
        realizandoAtaqueEspecial = false;
        IndexPatron = 100;


    }

    void PatronesDisparosEspeciales()
    {
        audioManagement.seleccionAudio(22, 0.2f);
        int IndexPatron = Random.Range(0,4); // Número de patrones de ataque disponibles
        if (IndexPatron == 0)
        {
            for (float i = 0; i < 4; i++)
            {
                StartCoroutine(DispararConDelayEspecial1(i));
                tiempoUltimoAtaqueDisparo -= 0.7f;


            }


        }
        else if (IndexPatron == 1)
        {
            for (float i = 0; i < 6; i++)
            {
                StartCoroutine(DispararConDelayEspecial2(i));
                tiempoUltimoAtaqueDisparo -= 0.3f;

            }


        } else if(IndexPatron == 2){
            for (int i = 0; i < puntosDisparo.Length; i++)
            {
                if(((4 <= i) && (i <= 6)) || ((10 <= i) && (i <= 12))){
                    GameObject bala = Instantiate(PlumaEscudo2, puntosDisparo[i].transform.position, puntosDisparo[i].transform.rotation);
                    bala.transform.parent = puntosDisparo[i].transform;
                    Destroy(bala, 9f);
                } else {
                    GameObject bala = Instantiate(PlumaEscudo, puntosDisparo[i].transform.position, puntosDisparo[i].transform.rotation);
                    bala.transform.parent = puntosDisparo[i].transform;
                    Destroy(bala, 9f);
                }
                
            }
        } else if (IndexPatron == 3){
            for (int i = 0; i < puntosDisparo.Length; i++)
            {
                for(int x = 0; x < 4; x++){
                    tiempoUltimoAtaqueDisparo -= 0.1f;
                    GameObject bala = Instantiate(Proyectil, puntosDisparo[i].transform.position, puntosDisparo[i].transform.rotation);
                    Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
                    Vector2 direccionAleatoria = Random.insideUnitCircle.normalized;
                    rb.AddForce(direccionAleatoria * (velocidadProyectil/3), ForceMode2D.Impulse);
                    tiempoUltimoAtaqueDisparoEspecial = 0f;
                }
                
            }
        }
        tiempoUltimoAtaqueDisparoEspecial = 0f;
        realizandoAtaqueEspecial = false;
    }



    void disparoNormal()
    {
        
        // Seleccionamos un punto de disparo aleatorio
        
        int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);

        // Creamos una instancia de la bala en el punto de disparo
        GameObject bala = Instantiate(Proyectil, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation);
        CrearIconoCrosshair(jugador.transform.position);
        // Calculamos la dirección hacia el jugador
        Vector3 direccion = (jugador.transform.position - puntosDisparo[puntoSeleccionado].transform.position).normalized;


        // Calcula el ángulo de rotación hacia el jugador
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Rota el objeto del disparador hacia el jugador
        bala.transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);

        // Obtenemos el componente Rigidbody2D de la bala y le aplicamos una fuerza en la dirección calculada
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        rb.AddForce(direccion * velocidadProyectil, ForceMode2D.Impulse);
        

        // Destruimos la bala después de 2 segundos
        Destroy(bala, 2f);
        tiempoUltimoAtaqueDisparo = 0f;
    }

    IEnumerator taclear(float delay)
    {

        realizandoAtaqueEspecial = true;
        realizandoTacleada = true;
        yield return new WaitForSeconds(delay);
        realizandoAtaqueEspecial = false;
        realizandoTacleada = false;



    }


    IEnumerator DispararConDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        disparoNormal();
        
    }

    IEnumerator DetenerseAtaqueEspecial(float delay)
    {
        realizandoAtaqueEspecial = true;
        tiempoUltimoAtaque = 0;
        realizandoTacleada = false;
        yield return new WaitForSeconds(delay);
        PatronesAtaques();

    }
    IEnumerator ReducirVelocidadJefe(float delay)
    {
        velocidad -= 3;
        yield return new WaitForSeconds(delay);
        velocidad = velocidadInicial;
    }

    IEnumerator DispararConDelayEspecial1(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Seleccionamos un punto de disparo aleatorio
        int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);

        // Creamos una instancia de la bala en el punto de disparo
        GameObject bala = Instantiate(ProyectilPerseguidor, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation);

        // Calculamos la dirección hacia el jugador
        Vector3 direccion = (jugador.transform.position - puntosDisparo[puntoSeleccionado].transform.position).normalized;

        // Calcula el ángulo de rotación hacia el jugador
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Rota el objeto del disparador hacia el jugador
        bala.transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);

        tiempoUltimoAtaqueDisparoEspecial = 0f;
    }

    IEnumerator DispararConDelayEspecial2(float delay)
    {


        yield return new WaitForSeconds(delay);

        int puntoSeleccionado = Random.Range(0, puntosDisparo.Length);
        GameObject bala = Instantiate(PlumaRebotadora, puntosDisparo[puntoSeleccionado].transform.position, puntosDisparo[puntoSeleccionado].transform.rotation);
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        Vector2 direccionAleatoria = Random.insideUnitCircle.normalized;
        rb.AddForce(direccionAleatoria * (velocidadProyectil), ForceMode2D.Impulse);
        tiempoUltimoAtaqueDisparoEspecial = 0f;
    }




    private void CrearIcono()
    {
        if (icono != null)
        {
            Destroy(icono);
        }

        // Crear un objeto vacío y colocarlo en la posición del siguiente punto
        icono = Instantiate(iconoPrefab, puntos[siguientePunto].transform.position, Quaternion.identity);

        Destroy(icono, 3);


    }

    private void CrearIconoCrosshair(Vector2 posicion)
    {

        // Crear un objeto vacío y colocarlo en la posición del siguiente punto
        icono2 = Instantiate(iconoCrosshair, posicion, Quaternion.identity);

        Destroy(icono2, 1.4f);


    }


}




















/*
    void PatronesAtaques()
    {
        realizandoAtaqueEspecial = true;
        int IndexPatron = Random.Range(0, 3); // Número de patrones de ataque disponibles
        switch (IndexPatron)
        {
            case 0:


                break;

            case 1:

                break;
            case 2:

                break;
            default:
                Debug.LogError("Invalid attack pattern index!");
                break;
        }
    }
*/
