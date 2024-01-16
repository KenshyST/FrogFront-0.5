using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDestroyer : MonoBehaviour
{
    public GameObject player;
    public Vector3 posicionActualJugador;
    private void Update()
    {
        posicionActualJugador = new Vector3(player.GetComponent<Transform>().transform.position.x - 150f, player.GetComponent<Transform>().transform.position.y + 20f ,  player.GetComponent<Transform>().transform.position.z);

        transform.position = posicionActualJugador;
    }
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
   

    private void OnCollisionStay2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

   
}
