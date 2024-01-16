using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPrincipal : MonoBehaviour
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
        
        Pivote.transform.rotation = Quaternion.Euler(0, 0, 0);
        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        Arma.rotation = Quaternion.Euler(new Vector3(0,0,angle));

        if(tiempitoParaRafaga >= 0.3f && clickPresionado){
                DisparoRafaga();
            }
        if(Input.GetKey(KeyCode.Mouse0)){
            tiempitoParaRafaga += Time.deltaTime;
            clickPresionado = true;
        } else{
            tiempitoParaRafaga = 0f;
            clickPresionado = false;

        }

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            
            Disparo_Pium_Pium();
            
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
    void Disparo_Pium_Pium(){
        
        
        GameObject proyectilInstanciado = Instantiate (Proyectil, Arma.position, transform.rotation) as GameObject;
        proyectilInstanciado.transform.Rotate(0f, 0f, 90f);
        targetRotation.z = 0;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f; // Ajusta la distancia de la cámara al plano del juego si es necesario
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        objetivo = (targetPosition - transform.position);

        Rigidbody2D rbProyectil = proyectilInstanciado.GetComponent<Rigidbody2D>();
        rbProyectil.velocity = objetivo * velocidadProyectil;
        audioManagement.seleccionAudio(1, 0.15f);
        
    }

    void DisparoRafaga(){
        if (Time.time >= nextFireTime){
        audioManagement.seleccionAudio(1, 0.15f);
        GameObject proyectilInstanciado = Instantiate (Proyectil, Arma.position, transform.rotation) as GameObject;
        proyectilInstanciado.transform.Rotate(0f, 0f, 90f);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f; // Ajusta la distancia de la cámara al plano del juego si es necesario
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        targetRotation.z = 0;
        Vector2 dispersionVector = Quaternion.AngleAxis(Random.Range(-dispersion, dispersion), Vector3.forward) * objetivo;
        objetivo = (targetPosition - transform.position);
        proyectilInstanciado.GetComponent<Rigidbody2D>().AddForce(dispersionVector * velocidadProyectil, ForceMode2D.Impulse);
        nextFireTime = Time.time + fireRate;
        }
        
        
        
    }
}

    
