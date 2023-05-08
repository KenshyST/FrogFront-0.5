using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.GetComponent<EnemyProjectile>().watcher==true)
        {
            transform.GetComponentInParent<EnemyProjectile>().Shoot();
        }
    }
}
