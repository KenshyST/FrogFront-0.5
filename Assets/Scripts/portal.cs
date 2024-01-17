using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float generacionIntervalo = 3f;
    private float tiempoPasado = 0f;

    private void Update()
    {
        tiempoPasado += Time.deltaTime;

        if (tiempoPasado >= generacionIntervalo)
        {
            GenerarEnemigo();
            tiempoPasado = 0f;
        }
    }

    private void GenerarEnemigo()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
