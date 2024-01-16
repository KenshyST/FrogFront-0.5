using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escopeta : MonoBehaviour
{
    
    public Transform Arma;
    public SpriteRenderer ArmaSR;
    public int velocidadProyectil;
    private Vector3 targetRotation;

    public GameObject Proyectil;

    private Vector3 objetivo;

    public MovimientoPersonaje MovimientoPersonaje;

    public SpriteRenderer SpritePersonaje;

    public GameObject Pivote;

    public float dispersion;

    float tiempitoParaRafaga;

    private bool clickPresionado = false;
    public float fireRate = 0.05f;
    private float nextFireTime = 0f;

    [SerializeField] private int contadorBalasDesactivar;

    public int cantidadBalasDesactivar;

    public int totalBalasEnDisparo;

    public GameObject Ak_47;

    private audioManagement audioManagement;

    // Start is called before the first frame update
    void Start()
    {
        MovimientoPersonaje = GetComponent<MovimientoPersonaje>();
        MovimientoPersonaje = FindObjectOfType<MovimientoPersonaje>();
        audioManagement = FindObjectOfType<audioManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cantidadBalasDesactivar <= contadorBalasDesactivar){
            gameObject.SetActive(false);
            contadorBalasDesactivar = 0;
            Ak_47.SetActive(true);
            audioManagement.seleccionAudio(18, 0.2f);
        }
        
        Pivote.transform.rotation = Quaternion.Euler(0, 0, 0);
        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        Arma.rotation = Quaternion.Euler(new Vector3(0,0,angle));

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            
            DisparoRafaga();
        }
            

        
        

    if(MovimientoPersonaje.mirandoDerecha == false){
        
        ArmaSR.flipX = true;
        ArmaSR.flipY = true;
        
    }
    if(MovimientoPersonaje.mirandoDerecha == true){
        
        ArmaSR.flipX = false;
        ArmaSR.flipY = false;
        
    }

       
    }
    

    void DisparoRafaga(){
        if (Time.time >= nextFireTime){
            audioManagement.seleccionAudio(7, 0.2f);
            for(int i = 0; i < totalBalasEnDisparo; i++)
        {
            float tiempoDeEsperaAleatorio = Random.Range(-0.08f, 0.08f);
            StartCoroutine(DispararDespuesDeEsperar(tiempoDeEsperaAleatorio));
            
        }
        nextFireTime = Time.time + fireRate;
        contadorBalasDesactivar += 1;
        
        }
        
    }

    IEnumerator DispararDespuesDeEsperar(float tiempoDeEsperaAleatorio)
{
    yield return new WaitForSeconds(tiempoDeEsperaAleatorio);
    GameObject proyectilInstanciado = Instantiate (Proyectil, Arma.position + new Vector3(Random.Range(0f, 0.8f), Random.Range(-0.1f, 0.1f), 0), transform.rotation) as GameObject;
    var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
    Vector3 mousePosition = Input.mousePosition;
    mousePosition.z = 0f; // Ajusta la distancia de la cámara al plano del juego si es necesario
    targetRotation.z = 0;
    Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    Vector2 dispersionVector = Quaternion.AngleAxis(Random.Range(-dispersion, dispersion), Vector3.forward) * objetivo;
    
    objetivo = (targetPosition - transform.position);
    proyectilInstanciado.GetComponent<Rigidbody2D>().AddForce(dispersionVector * velocidadProyectil, ForceMode2D.Impulse);
    
}
}
