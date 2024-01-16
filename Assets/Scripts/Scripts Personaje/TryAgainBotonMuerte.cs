using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainBotonMuerte : MonoBehaviour
{
    public GameObject pantallaMuerte;
    public MovimientoPersonaje movimientoPersonaje;
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void cambiarEscena()
    {
        movimientoPersonaje.cayoEnVeneno = false;
        //player.transform.position = movimientoPersonaje.respawnPoint;
        Time.timeScale = 1f;
        pantallaMuerte.SetActive(false);
        movimientoPersonaje.Healt = 60;
        SceneManager.LoadScene(0);
    }

    public void tryAgainButton()
    {
        movimientoPersonaje.cayoEnVeneno = false;
        //player.transform.position = movimientoPersonaje.respawnPoint;
        Time.timeScale = 1f;
        pantallaMuerte.SetActive(false);
        movimientoPersonaje.Healt = 60;
        SceneManager.LoadScene(1);

        
    }
}
