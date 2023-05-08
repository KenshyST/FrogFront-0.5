using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmaActual : MonoBehaviour
{
    public Image imagen2D; 

    public Transform padre;

    public TextMeshProUGUI nombreArma;

    private int numHijos;
    private Transform[] hijos;

    public Image imagenInicial;
    void Start()
    {
        
        numHijos = padre.childCount;
        hijos = new Transform[numHijos];
        for (int i = 0; i < numHijos; i++) {
            hijos[i] = padre.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
         for (int i = 0; i < numHijos; i++) {
            if (hijos[i].gameObject.activeSelf) {
                if(hijos[i].name == "Ak-47"){
                    imagen2D.sprite = Resources.Load<Sprite>("Sprites/Ak-47");
                    nombreArma.text = hijos[i].name; 
                }
                if(hijos[i].name == "Lanzacohetes"){
                    imagen2D.sprite = Resources.Load<Sprite>("Sprites/Lanzacohetes");
                    nombreArma.text = hijos[i].name; 
                }
                if(hijos[i].name == "Shotgun"){
                    imagen2D.sprite = Resources.Load<Sprite>("Sprites/Nova");
                    nombreArma.text = hijos[i].name; 
                } 
                
            } 
        }
    }
}
