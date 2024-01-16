using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAyuda : MonoBehaviour
{
    public GameObject[] SUaYUDA;
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            StartCoroutine(activarAyudas());
            
        }
    }

    IEnumerator activarAyudas()
    {
        yield return new WaitForSeconds(15f);
        for (int i = 0; i < SUaYUDA.Length; i++)
        {
            SUaYUDA[i].SetActive(true);
        }
        Destroy(gameObject);
    }
}
