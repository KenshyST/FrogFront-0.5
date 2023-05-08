using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ProyectilAk")
        {
            enemy.healthPoints -= 20f;

            if(enemy.healthPoints <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
