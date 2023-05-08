using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akVoladora : MonoBehaviour
{
    public GameObject akFrog;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            akFrog.SetActive(true);

            Destroy(gameObject);
        }
    }
}
