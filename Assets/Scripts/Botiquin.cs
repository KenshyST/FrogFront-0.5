using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public float shakeSpeed = 30f;   // Velocidad del movimiento de agitación
    public float shakeAmount = 0.08f;  // Amplitud del movimiento de agitación
    private Vector3 originalPosition; // Posición original del objeto

    public MovimientoPersonaje movimientoPersonaje;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position; // Almacenar la posición original del objeto
        GameObject personaje= GameObject.Find("Personaje");
        movimientoPersonaje = personaje.GetComponent<MovimientoPersonaje>();

    }

    // Update is called once per frame
    void Update()
    {
        // Calcular la posición de agitación en el eje Y
            float shakeOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            // Actualizar la posición del objeto sumando la posición original y el desplazamiento de agitación en Y
            transform.position = originalPosition + new Vector3(0f, shakeOffset, 0f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            movimientoPersonaje.Healt += 20;
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        // Destruir el objeto cuando se desactive (al salir del modo de edición)
        Destroy(gameObject);
    }
}
