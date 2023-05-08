using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirAUnObjeto : MonoBehaviour
{
    public Transform objetoAseguir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    // Obtener la posición global del objeto que deseamos seguir sin tener en cuenta la escala
    Vector3 posicionGlobal = objetoAseguir.TransformPoint(Vector3.zero);

    // Actualizar la posición del objeto actual a la posición global sin la escala
    transform.position = new Vector3(posicionGlobal.x, posicionGlobal.y, transform.position.z);
    }
}
