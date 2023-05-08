using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CAC : MonoBehaviour
{
    public GameObject Machete;

    public Animator animadorMachete;

    public float tiempoReutilizacion = 0.8f; // Tiempo de reutilización en segundos
    private float tiempoUltimoMachetazo = 0f; // Tiempo en el que se realizó el último enganche

    public Slider sliderLengua;
    private float cdAtaque;
    private float cdAtaqueActual = 0.0f;

    void Start()
    {
        cdAtaque = tiempoReutilizacion;
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Time.time > tiempoUltimoMachetazo + tiempoReutilizacion && Input.GetKeyDown(KeyCode.E)){
          Machete.SetActive(true);
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
