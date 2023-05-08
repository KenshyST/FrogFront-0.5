using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aplastador : MonoBehaviour
{
    public float velocidadMovimiento;
    public float rangoDetectar;
    public float checkearDelay;

    private float checkTimer;

    private Vector3 destino;

    private bool estaAtacando;

    private Vector3[] todasDirecciones = new Vector3[4]; //Arriba,abajo,derecha e izquierda

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(estaAtacando){
            transform.Translate(destino * Time.deltaTime * velocidadMovimiento);
        } else{
            checkTimer += Time.deltaTime;
            if(checkTimer > checkearDelay){
                MirandoHaciaTodosLados();
            }
        }
        
    }

    private void MirandoHaciaTodosLados(){
        CalcularDireccion();

        for(int i= 0; i < todasDirecciones.Length; i++){
            Debug.DrawRay(transform.position, todasDirecciones[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, todasDirecciones[i], rangoDetectar);
            if (hit.collider != null && hit.collider.CompareTag("Player") && !estaAtacando)
        {
            estaAtacando = true;
            destino = todasDirecciones[i];
            checkTimer = 0;
        }
        }
    }

    private void CalcularDireccion(){
        todasDirecciones[0] = transform.right * rangoDetectar;
        todasDirecciones[1] = -transform.right * rangoDetectar;
        todasDirecciones[2] = transform.up * rangoDetectar;
        todasDirecciones[3] = -transform.up * rangoDetectar;
    }

    private void DetenElMovimiento(){
        destino = transform.position;
        estaAtacando = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){

        DetenElMovimiento();
    }
}
