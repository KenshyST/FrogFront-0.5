using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntosRespawn : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") // Aquí puedes cambiar la etiqueta para que se destruya la caja con otro objeto
        {
        gameObject.SetActive(false);       
           
        } 
    }
}
