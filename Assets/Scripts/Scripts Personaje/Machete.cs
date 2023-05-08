using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machete : MonoBehaviour
{
    // https://www.youtube.com/watch?v=I2Uo8eEmSFQ falta completar
    public SpriteRenderer spritePersonaje;
    public SpriteRenderer spriteArma;

    public Vector2 PosicionRaton {get; set;}

    public Animator animadorMachete;

    public float delay = 0.12f;
    private bool ataqueBloqueado;

    public bool estaAtacando {get; private set;}

    public Transform origen;
    public float radio;

    public DestruirCaja caja;

    public void ResetearAtaque(){
        estaAtacando = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(estaAtacando){
            return;
        }
        Vector2 direccion = (PosicionRaton - (Vector2)transform.position).normalized;
        transform.right = direccion;

        Vector2 scale = transform.localScale;
        if(direccion.x < 0)
        {
            scale.y = -1;
        } else if (direccion.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            spriteArma.sortingOrder = spritePersonaje.sortingOrder - 1;
        } else {
            spriteArma.sortingOrder = spritePersonaje.sortingOrder + 1;
        }
    }

    public void Ataque()
    {
        if(ataqueBloqueado)
            return;
        animadorMachete.SetTrigger("Ataque");
        estaAtacando = true;
        ataqueBloqueado = true;
        StartCoroutine(delayAtaque());
    }

    private IEnumerator delayAtaque()
    {
        yield return new WaitForSeconds(delay);
        ataqueBloqueado = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 position = origen == null ? Vector3.zero : origen.position; // condición ? resultado_si_cierto : resultado_si_falso
        Gizmos.DrawWireSphere(position, radio);
    }

    public void DetectarColisionAtaque()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(origen.position, radio)){
            if(collider.gameObject.tag == "objetoDestructible" && estaAtacando){
                caja.contadorBalas += 1;
            }
        }
    }

    
}
