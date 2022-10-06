using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarTexto : MonoBehaviour
{
    //Mostra Textos ao colider com objetos
    
    public GameObject Object;
    void Start()
    {
        Object.SetActive(false);
    }

    void OnPointeEnter()
    {
        Object.SetActive(true);
    }

    private void OnPointerExit()
    {
         Object.SetActive(false);
    }
    
    /*
    Obtject removido do triggerenter2d e colocado no onmouse
    void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    void OnTriggerExit2D(Collider2D collision)
    {
       
    }
    */
}
