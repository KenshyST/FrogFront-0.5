using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispIzquierda : MonoBehaviour
{
    public GameObject proyectilPrefab; // Prefab del proyectil a disparar
    public float fuerzaDisparo = 10f; // Fuerza con la que se dispara el proyectil

    public Transform canon;
    public Camera camara;

    public float cadencia;

    public float siguienteDisparo;

    public bool dispararFueraDeCamara = true;

    public AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //
        Plane[] planosDeCorte = GeometryUtility.CalculateFrustumPlanes(camara);

        // Cada x tiempo
        if (GeometryUtility.TestPlanesAABB(planosDeCorte, this.GetComponent<Renderer>().bounds) && !dispararFueraDeCamara)
        {
            if(Time.time >= siguienteDisparo){
            GameObject proyectil = Instantiate(proyectilPrefab, canon.transform.position, Quaternion.identity) as GameObject;
            
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(proyectil, 4f);
            // Le aplica una fuerza para que se dispare en la dirección calculada
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * fuerzaDisparo, ForceMode2D.Impulse);
                
            siguienteDisparo = Time.time + cadencia;
            }
       
        } else {
            if(Time.time >= siguienteDisparo){
            GameObject proyectil = Instantiate(proyectilPrefab, canon.transform.position, Quaternion.identity) as GameObject;
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(proyectil, 4f);
            // Le aplica una fuerza para que se dispare en la dirección calculada
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * fuerzaDisparo, ForceMode2D.Impulse);
                
            siguienteDisparo = Time.time + cadencia;
            }
        }
    }
}
