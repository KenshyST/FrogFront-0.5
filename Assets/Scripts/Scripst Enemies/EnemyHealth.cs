using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Enemy enemy;
    public GameObject spriteObjectPrefab;
    public float tiempoAparicion = 0.2f;
    public float tiempoDifuminado = 0.1f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ProyectilAk")
        {
            enemy.healthPoints -= 20f;

            StartCoroutine(AparicionDifuminado(collision.gameObject.transform.position));

            if (enemy.healthPoints <= 0)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "machete")
        {
            enemy.healthPoints -= 60f;

            StartCoroutine(AparicionDifuminado(collision.gameObject.transform.position));

            if (enemy.healthPoints <= 0)
            {
                GameObject spriteObject = Instantiate(spriteObjectPrefab, transform.parent.position, transform.parent.rotation);
                spriteObject.transform.SetParent(transform.parent);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private System.Collections.IEnumerator AparicionDifuminado(Vector3 posicion)
    {
        GameObject spriteObject = Instantiate(spriteObjectPrefab, posicion, transform.rotation);
        spriteObject.transform.SetParent(transform);

        yield return new WaitForSeconds(tiempoAparicion);

        SpriteRenderer spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("El objeto instanciado no tiene un componente SpriteRenderer.");
            yield break;
        }

        float tiempoInicio = Time.time;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoDifuminado)
        {
            float alpha = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoDifuminado);
            Color currentColor = spriteRenderer.color;
            currentColor.a = alpha;
            spriteRenderer.color = currentColor;

            tiempoTranscurrido = Time.time - tiempoInicio;
            yield return null;
        }

        Destroy(spriteObject);
    }
}