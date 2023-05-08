using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaUnSentido : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other.GetComponent<Collider2D>(),false);
        }
        
    }
}
