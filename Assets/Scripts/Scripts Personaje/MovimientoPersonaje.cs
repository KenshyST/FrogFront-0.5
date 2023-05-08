using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoPersonaje : MonoBehaviour
{
    private Rigidbody2D rgb2D;

    private float movimientoHorizontal = 0f;

    public float velocidadMovimiento = 0f;

    public float intensidadAceleracion;

    [Range(0, 0.5f)] public float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero; // Bloquear la region z

    public bool mirandoDerecha = true;

    public float aceleracion;

    public float desaceleracion;

    public Vector2 input;

    public float Healt;
    public bool isInmune;

    // Parte para el Salto:
    [Header("Salto")]

    bool haSaltado = false;
    public float fuerzaDeSalto;

    public float fuerzaCaida;

    public float fuerzaDash;

    public LayerMask queEsSuelo;

    public Transform controladorSuelo;

    public Vector3 dimensionesCaja; //Identifica cuanddo esta tocando suelo;

    public bool enSuelo;

    public float knockBackForceX;
    public float knockBackForceY;

    public bool EstaSaltando;

    public double TiempoPermisivoCoyote;

    private double ultimoTiempoEnTierra;

    private bool inputSaltoSoltado;

    //Dashes

    public float doubleTapTime = 0.2f; // tiempo máximo permitido entre dos pulsaciones para considerarlo doble tap
    public float delayTime = 1f;

    private float delayTimeJump = 0.25f;
    private float lastTapTime = 0f;
    private bool waitingForDoubleTap = false;
    private float lastPressTime = 0f;

    //
    [Range(0, 0.1f)] public float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;

    float multiplicadorInicialGravedad;

    float escalaGravedadInicial;

    public PhysicsMaterial2D materialPersonaje;
    float velocidadMovimientoInicial;
    float fuerzaSaltoInicial;

    [SerializeField] bool estaEnAgua;

    public int vidasPersonaje = 3;

    bool EnPegajoso;

    private Vector3 respawnPoint;


    private void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        escalaGravedad = rgb2D.gravityScale;
        velocidadMovimientoInicial = velocidadMovimiento;
        fuerzaSaltoInicial = fuerzaDeSalto;
        escalaGravedadInicial = escalaGravedad;
        multiplicadorInicialGravedad = multiplicadorGravedad;
        // Verificar si ya hay una posición guardada
        if (PlayerPrefs.HasKey("spawnPersonajeX") && PlayerPrefs.HasKey("spawnPersonajeY") && PlayerPrefs.HasKey("spawnPersonajeZ"))
        {
            respawnPoint = new Vector3(PlayerPrefs.GetFloat("spawnPersonajeX"), PlayerPrefs.GetFloat("spawnPersonajeY"), PlayerPrefs.GetFloat("spawnPersonajeZ"));
        }
        else // Si no hay ninguna posición guardada, usar la posición establecida en el inspector
        {
            respawnPoint = transform.position;
        }

        // Establecer la posición del jugador al punto de inicio
        transform.position = respawnPoint;


    }

    // Update is called once per frame
    private void Update()
    {
        // Reiniciar la escena con la tecla }, para pruebas con el checkpoint XD
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //check de corazones
        HealthCheck();


        if (EnPegajoso)
        {
            materialPersonaje.friction = 40f;
            rgb2D.gravityScale = 0.01f;
            multiplicadorGravedad = 0.009f;

        }
        else
        {
            materialPersonaje.friction = 0f;
            velocidadMovimiento = velocidadMovimientoInicial;
            fuerzaDeSalto = fuerzaSaltoInicial;
            rgb2D.gravityScale = escalaGravedadInicial;
            multiplicadorGravedad = multiplicadorInicialGravedad;
        }


        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        input.y = Input.GetAxisRaw("Vertical");
        if (enSuelo || Time.time - ultimoTiempoEnTierra <= TiempoPermisivoCoyote || estaEnAgua)
        {
            if (Input.GetButtonDown("Jump") && !EstaSaltando || Input.GetButtonDown("Jump") && estaEnAgua)
            {
                if (input.y >= 0)
                {
                    EstaSaltando = true;
                    if (Time.time - lastPressTime > delayTimeJump)
                    {
                        RealizarSalto();
                    }

                }
                else
                {
                    DesactivarPlataformas();
                }


            }

        }
        if (!enSuelo)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rgb2D.AddForce(Vector2.down * fuerzaCaida, ForceMode2D.Impulse);
            }

        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (waitingForDoubleTap && Time.time - lastTapTime < doubleTapTime)
            {
                // Se detectó un doble tap en la tecla "S"
                rgb2D.AddForce(Vector2.left * fuerzaDash, ForceMode2D.Impulse);
                waitingForDoubleTap = false;
            }
            else
            {
                // Se detectó una sola pulsación en la tecla "S"
                waitingForDoubleTap = true;
                lastTapTime = Time.time;
            }


        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (waitingForDoubleTap && Time.time - lastTapTime < doubleTapTime)
            {
                // Se detectó un doble tap en la tecla "S"
                rgb2D.AddForce(Vector2.right * fuerzaDash, ForceMode2D.Impulse);
                waitingForDoubleTap = false;
            }
            else
            {
                // Se detectó una sola pulsación en la tecla "S"
                waitingForDoubleTap = true;
                lastTapTime = Time.time;
            }

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (waitingForDoubleTap && Time.time - lastTapTime < doubleTapTime)
            {
                if (Time.time - lastPressTime > delayTime)
                {
                    rgb2D.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                    waitingForDoubleTap = false;
                    lastPressTime = Time.time;
                }

            }
            else
            {
                // Se detectó una sola pulsación en la tecla "S"
                waitingForDoubleTap = true;
                lastTapTime = Time.time;
            }

        }



    }


    //Fixed update es mas recomendable para cambios fisicos dentro de la escena
    private void FixedUpdate()
    {
        // LogicaMovimiento

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        if (enSuelo)
        {
            ultimoTiempoEnTierra = Time.time;
        }
        LogicaMovimiento(movimientoHorizontal * Time.fixedDeltaTime);

        EstaSaltando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            haSaltado = false;
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy") && !isInmune)
        {
            Healt -= other.GetComponent<Enemy>().DamageToGive;
            Destroy(other.gameObject);
            StartCoroutine(Inmunity());

            if (other.transform.position.x > transform.position.x)
            {
                rgb2D.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);

            }
            else
            {
                rgb2D.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
            }

        }


        if (other.gameObject.tag == "PlataforMovil") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            transform.parent = other.transform;

        }
        else
        {
            transform.parent = null;

        }

        if (other.gameObject.tag == "checkpoint") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            PlayerPrefs.SetFloat("spawnPersonajeX", transform.position.x);
            PlayerPrefs.SetFloat("spawnPersonajeY", transform.position.y);
            PlayerPrefs.SetFloat("spawnPersonajeZ", transform.position.z);
            respawnPoint = new Vector3(PlayerPrefs.GetFloat("spawnPersonajeX"), PlayerPrefs.GetFloat("spawnPersonajeY"), PlayerPrefs.GetFloat("spawnPersonajeZ"));

        }

        if (other.gameObject.tag == "pegajoso") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {

            materialPersonaje.friction = 40f;
            fuerzaDeSalto -= 4;
            rgb2D.gravityScale = 0.01f;
            multiplicadorGravedad = 0.009f;
            EnPegajoso = true;
            rgb2D.velocity -= new Vector2(rgb2D.velocity.x, rgb2D.velocity.y * 0.3f);

        }

        if (other.gameObject.tag == "liquido")
        {
            estaEnAgua = true;
            fuerzaDeSalto -= 4;
        }
        else
        {
            return;
        }

    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        yield return new WaitForSeconds(1f);
        isInmune = false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "pegajoso") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            EnPegajoso = true;




        }

        if (other.gameObject.tag == "liquido")
        {
            estaEnAgua = true;

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlataforMovil") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            transform.parent = null;

        }

        if (other.gameObject.tag == "pegajoso") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            EnPegajoso = false;
            materialPersonaje.friction = 0f;
            velocidadMovimiento = velocidadMovimientoInicial;
            fuerzaDeSalto = fuerzaSaltoInicial;
            rgb2D.gravityScale = escalaGravedadInicial;
            multiplicadorGravedad = multiplicadorInicialGravedad;
        }

        if (other.gameObject.tag == "liquido")
        {
            estaEnAgua = false;
            fuerzaDeSalto = fuerzaSaltoInicial;
        }

    }

    private void LogicaMovimiento(float mover)
    {
        // Moverse Horizontalmente
        float velocidadObjetivo = movimientoHorizontal;
        float diferenciaVelocidad = velocidadObjetivo - rgb2D.velocity.x;
        float ratioAceleracion = (Mathf.Abs(velocidadObjetivo) > 0.01f) ? aceleracion : desaceleracion;
        float movimientoFinal = Mathf.Pow(Mathf.Abs(diferenciaVelocidad) * ratioAceleracion, intensidadAceleracion) * Mathf.Sign(diferenciaVelocidad);
        rgb2D.AddForce(movimientoFinal * Vector2.right);
        //Friccion artificial hay dio mio

        if (enSuelo && Input.GetButtonUp("Horizontal"))
        {
            float cantidadFriccion = Mathf.Min(Mathf.Abs(rgb2D.velocity.x), Mathf.Abs(velocidadMovimiento * 0.2f));
            cantidadFriccion *= Mathf.Sign(rgb2D.velocity.x);
            rgb2D.AddForce(Vector2.right * -cantidadFriccion, ForceMode2D.Impulse);
        }

        /*
        Vector3 velocidadObjetivo = new Vector2(mover, rgb2D.velocity.y);
        rgb2D.velocity = Vector3.SmoothDamp(rgb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);
        */
        //TERMINA MOVERSE HORIZONTALMENTE

        //GIRAR A IZQUIERDA Y DERECHA
        if (mover > 0 && !mirandoDerecha)
        {
            //Girar
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            //Girar
            Girar();
        }
        if (rgb2D.velocity.y < 0 && !enSuelo)
        {
            rgb2D.gravityScale = escalaGravedad * multiplicadorGravedad;

        }
        else
        {
            rgb2D.gravityScale = escalaGravedad;
        }


    }

    private void DesactivarPlataformas()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        foreach (Collider2D item in objetos)
        {
            PlatformEffector2D PlatformEffector2D = item.GetComponent<PlatformEffector2D>();
            if (PlatformEffector2D != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);
            }
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }



    private void RealizarSalto()
    {
        haSaltado = true;
        rgb2D.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
        inputSaltoSoltado = false;

    }

    public void HealthCheck()
    {
        if (Healt == 60f) // a cambiar
        {
            vidasPersonaje = 3;
        }

        if ((Healt <= 40f) && (Healt >= 20f))
        {
            vidasPersonaje = 2;
        }

        if ((Healt <= 20f) && (Healt >= 1f))
        {
            vidasPersonaje = 1;
        }

        if (Healt <= 0f)
        {
            vidasPersonaje = 0;
            //AQUI MUERE
        }
    }
}
