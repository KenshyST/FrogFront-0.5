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
    // Start is called before the first frame update
    void Start()
    {
        rgb2DPlataforma = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EstaCayendo){
            transform.Rotate(new Vector3(0,0,-velocidadRotacion * Time.deltaTime));
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
        if(other.gameObject.layer == LayerMask.NameToLayer("plataforma") && jugadorEncima && caer)
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
        yield return new WaitForSeconds(tiempoEspera);
        caer = true;
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        rgb2DPlataforma.constraints = RigidbodyConstraints2D.None;
        rgb2DPlataforma.AddForce(new Vector2(0.1f,0));
        EstaCayendo = true;
        

    }
}
