using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropearCosas : MonoBehaviour
{
    public GameObject[] objetosSoltados;
    public float probabilidadGeneracion = 0.35f; // Probabilidad de generación entre 0 y 1


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void OnDestroy()
    {
        if (objetosSoltados.Length > 0 && Random.value <= probabilidadGeneracion)
        {
            int randomIndex = Random.Range(0, objetosSoltados.Length);
            GameObject objetoSoltado = Instantiate(objetosSoltados[randomIndex], transform.position, Quaternion.identity);
            // Hacer cualquier otra acción deseada con el objeto soltado
            if (objetoSoltado != null)
            {
                Destroy(objetoSoltado, 10f);
            }
        }
        
    }
}
