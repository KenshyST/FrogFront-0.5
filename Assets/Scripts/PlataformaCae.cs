using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCae : MonoBehaviour
{
    [SerializeField] private float tiempoEspera;
    private Rigidbody2D rgb2DPlataforma;

    [SerializeField] private float velocidadRotacion;

    private bool caer = false;

    private bool jugadorEncima = false;

    private bool EstaCayendo = false;

    public float shakeSpeed = 30f;   // Velocidad del movimiento de agitación
    public float shakeAmount = 0.08f;  // Amplitud del movimiento de agitación

    private Vector3 originalPosition; // Posición original del objeto

    private audioManagement audioManagement;

    // Start is called before the first frame update
    void Start()
    {
        rgb2DPlataforma = GetComponent<Rigidbody2D>();
        originalPosition = transform.position; // Almacenar la posición original del objeto
        audioManagement = FindObjectOfType<audioManagement>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EstaCayendo){
            transform.gameObject.tag = "Untagged";
            transform.SetParent(null);
            // Recorre todos los hijos del objeto padre
            foreach (Transform hijo in gameObject.transform)
            {
                // Desvincula cada hijo estableciendo su padre como nulo
                hijo.SetParent(null);
            }
            transform.Rotate(new Vector3(0,0,-velocidadRotacion * Time.deltaTime));
        }

        if(caer && !EstaCayendo){
            // Calcular la posición de agitación en el eje Y
            float shakeOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            // Actualizar la posición del objeto sumando la posición original y el desplazamiento de agitación en Y
            transform.position = originalPosition + new Vector3(0f, shakeOffset, 0f);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            caer = true;
            
            if(jugadorEncima && caer){
                StartCoroutine(Caida(other));
                
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("plataforma") && jugadorEncima && EstaCayendo)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            jugadorEncima = true;
            
        }

        
        
    }

    private IEnumerator Caida(Collision2D other){
        audioManagement.seleccionAudio(9, 0.05f);
        yield return new WaitForSeconds(tiempoEspera);
        transform.gameObject.tag = "Untagged";
        transform.SetParent(null);
         foreach (Transform hijo in gameObject.transform)
            {
                // Desvincula cada hijo estableciendo su padre como nulo
                hijo.SetParent(null);
            }
        caer = true;
        rgb2DPlataforma.constraints = RigidbodyConstraints2D.None;
        rgb2DPlataforma.AddForce(new Vector2(0.1f,0));
        EstaCayendo = true;
        

    }
}
