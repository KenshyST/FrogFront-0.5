using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LogicaCorazones : MonoBehaviour
{
    public MovimientoPersonaje corazonesPersonaje;

     public Image corazon3;

     public Image corazon2;

     public Image corazon1;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(corazonesPersonaje.vidasPersonaje == 3){
            corazon3.sprite = Resources.Load<Sprite>("Sprites/CorazonLleno");
            corazon2.sprite = Resources.Load<Sprite>("Sprites/CorazonLleno");
            corazon1.sprite = Resources.Load<Sprite>("Sprites/CorazonLleno");  
        }

        if(corazonesPersonaje.vidasPersonaje == 2){
            corazon3.sprite = Resources.Load<Sprite>("Sprites/SinCorazao");
            corazon2.sprite = Resources.Load<Sprite>("Sprites/CorazonLleno");
            corazon1.sprite = Resources.Load<Sprite>("Sprites/CorazonLleno"); 
        }

        if(corazonesPersonaje.vidasPersonaje == 1){
            corazon3.sprite = Resources.Load<Sprite>("Sprites/SinCorazao");
            corazon2.sprite = Resources.Load<Sprite>("Sprites/SinCorazao");
            corazon1.sprite = Resources.Load<Sprite>("Sprites/CorazonLleno"); 
         } 

         if(corazonesPersonaje.vidasPersonaje == 0){
            corazon3.sprite = Resources.Load<Sprite>("Sprites/SinCorazao");
            corazon2.sprite = Resources.Load<Sprite>("Sprites/SinCorazao");
            corazon1.sprite = Resources.Load<Sprite>("Sprites/SinCorazao"); 
         } 
    }
}
