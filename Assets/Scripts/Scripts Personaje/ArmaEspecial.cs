using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaEspecial : MonoBehaviour
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

    float tiempoUltimoDisparo;

    public float tiempoEntreDisparos;

    private bool clickPresionado = false;
    public float fireRate = 0.05f;
    private float nextFireTime = 0f;

    private float nextFireTimePiumPium = 0.2f;

    [SerializeField] private int contadorBalasDesactivar;

    public int cantidadBalasDesactivar;

    public GameObject AK_47;

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
            audioManagement.seleccionAudio(18, 0.2f);
            AK_47.SetActive(true);
        }
        
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
        if (Time.time >= tiempoEntreDisparos){
        audioManagement.seleccionAudio(1, 0.3f);
        GameObject proyectilInstanciado = Instantiate (Proyectil, Arma.position, transform.rotation) as GameObject;
        targetRotation.z = 0;
        objetivo = (targetRotation - transform.position).normalized;
        proyectilInstanciado.GetComponent<Rigidbody2D>().AddForce(objetivo * velocidadProyectil, ForceMode2D.Impulse);
        tiempoEntreDisparos = Time.time + nextFireTimePiumPium;
        contadorBalasDesactivar += 1;
        }
    
        
    }

    void DisparoRafaga(){
        if (Time.time >= nextFireTime){
        GameObject proyectilInstanciado = Instantiate (Proyectil, Arma.position, transform.rotation) as GameObject;
         audioManagement.seleccionAudio(1, 0.3f);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;
        targetRotation.z = 0;
        Vector2 dispersionVector = Quaternion.AngleAxis(Random.Range(-dispersion, dispersion), Vector3.forward) * objetivo;
        objetivo = (targetRotation - transform.position).normalized;
        proyectilInstanciado.GetComponent<Rigidbody2D>().AddForce(dispersionVector * velocidadProyectil, ForceMode2D.Impulse);
        nextFireTime = Time.time + fireRate;
        contadorBalasDesactivar += 1;
        }
        
        
        
    }
}



