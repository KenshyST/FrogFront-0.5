using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedazoDesaparece : MonoBehaviour
{
    public float tiempoDeEspera = 1.5f;
    //public AnimationClip animacionDesaparecer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirObjeto", tiempoDeEspera);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void DesaparecerYDestruir() {
    // Reproducir la animación de desaparecer
    //GetComponent<Animation>().clip = animacionDesaparecer;
    //GetComponent<Animation>().Play();

    // Esperar a que termine la animación y luego destruir el objeto
    //Invoke("DestruirObjeto", animacionDesaparecer.length);
    }

    void DestruirObjeto() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.tag == "ProyectilAk") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            
            Destroy(gameObject);
        }

    if (collision.gameObject.tag == "machete") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
            
            Destroy(gameObject);
        }
    }
}

