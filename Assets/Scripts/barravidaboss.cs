using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class barravidaboss : MonoBehaviour
{
    public Image colorFill;
    public GameObject sliderFill;
    public Slider cantidadFondo;

    public Enemy enemigo;
    // Start is called before the first frame update
    void Start()
    {
        cantidadFondo = sliderFill.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        cantidadFondo.value = enemigo.healthPoints;

        if(cantidadFondo.value == 0)
        {
            SceneManager.LoadScene(0);
        }
        if (cantidadFondo.value > 0 && cantidadFondo.value < 2000)
        {
            
            colorFill.color = new Color32(180, 0, 6, 255);
        }
        else if (cantidadFondo.value >= 2000 && cantidadFondo.value < 4000)
        {
            colorFill.color = new Color32(180, 85, 0, 255);
            
        }
        else if (cantidadFondo.value >= 4000 && cantidadFondo.value < 6000)
        {
            colorFill.color = new Color32(180, 168, 0, 255);
        }
        else if (cantidadFondo.value >= 6000 && cantidadFondo.value < 8000)
        {
            colorFill.color = new Color32(148, 180, 0, 255);
        }
        else if (cantidadFondo.value >= 8000 && cantidadFondo.value <= 10000)
        {
            colorFill.color = new Color32(79, 180, 0, 255);
        }
    }
}
