using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagement : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;

    private AudioSource controlAudio;

    private void Awake() {
        controlAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void seleccionAudio(int indice, float volumen){
        controlAudio.PlayOneShot(audios[indice], volumen);
    }
}
