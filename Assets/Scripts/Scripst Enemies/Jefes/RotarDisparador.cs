using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarDisparador : MonoBehaviour
{
    public Transform jugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        // Calcula el ángulo de rotación hacia el jugador
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Rota el objeto del disparador hacia el jugador
        transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);
    }
}
