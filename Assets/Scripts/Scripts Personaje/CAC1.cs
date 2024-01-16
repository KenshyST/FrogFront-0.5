using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CAC1 : MonoBehaviour
{
    public GameObject Machete;

    public GameObject padre;

    public Animator animadorMachete;

    public MovimientoControles MovimientoPersonaje;

    public float tiempoReutilizacion = 0.8f; // Tiempo de reutilización en segundos
    private float tiempoUltimoMachetazo = 0f; // Tiempo en el que se realizó el último enganche

    public Slider sliderLengua;
    private float cdAtaque;
    private float cdAtaqueActual = 0.0f;

    private audioManagement audioManagement;

    void Start()
    {
        cdAtaque = tiempoReutilizacion;
        MovimientoPersonaje = FindObjectOfType<MovimientoControles>();
        padre = GameObject.Find("Personaje");
        audioManagement = FindObjectOfType<audioManagement>();
    }

    // Update is called once per frame
    void Update()
    {

        Machete.transform.position = padre.transform.position;
        
        

        if (Time.time > tiempoUltimoMachetazo + tiempoReutilizacion && Input.GetKeyDown(KeyCode.E)){
          Machete.SetActive(true);
          audioManagement.seleccionAudio(8, 0.4f);
          float escalaX = padre.transform.localScale.x;
          float escalaMachete = Machete.transform.localScale.x;
          Vector3 escala = Machete.transform.localScale;
          if (escalaX < 0){
            
            escala.x = -1 * Mathf.Abs(escalaMachete);
            Machete.transform.localScale = escala;

          } else {
            escala.x = Mathf.Abs(escalaMachete);
            Machete.transform.localScale = escala;
          }
        
          animadorMachete.SetTrigger("Attack");
          StartCoroutine(DesactivarMachete(0.2f));

          tiempoUltimoMachetazo = Time.time;
        } else {
            
            cdAtaqueActual += Time.deltaTime;
            cdAtaqueActual = Mathf.Clamp(cdAtaqueActual, 0.0f, cdAtaque);
            
        }
            sliderLengua.value = cdAtaqueActual / cdAtaque;
        
        
            
        
    }
        
    

    IEnumerator DesactivarMachete(float delay)
    {

        cdAtaqueActual = 0.0f;
        yield return new WaitForSeconds(delay);
        Machete.SetActive(false);
    }


}
