using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zonaPelea : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public float desactivarTiempo = 30f;

    public List<GameObject>portales;
    BoxCollider2D boxCollider;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            
            object2.SetActive(true);
            foreach (GameObject obj in portales)
            {
                obj.SetActive(true);
            }
            Invoke("DesactivarObjetos", desactivarTiempo);
            Invoke("DesactivarTodosLosGameObjects", desactivarTiempo);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            
            object1.SetActive(true);
            object2.SetActive(true);
            foreach (GameObject obj in portales)
            {
                obj.SetActive(true);
            }
            Invoke("DesactivarObjetos", desactivarTiempo);
            Invoke("DesactivarTodosLosGameObjects", desactivarTiempo);
        }
    }

    private void DesactivarObjetos()
    {
        object1.SetActive(false);
        object2.SetActive(false);
    }

    private void DesactivarTodosLosGameObjects()
    {
        foreach (GameObject obj in portales)
        {
            obj.SetActive(false);
        }
    }

    private void ActivarTodosLosGameObjects()
    {
        foreach (GameObject obj in portales)
        {
            obj.SetActive(true);
        }
    }
}
