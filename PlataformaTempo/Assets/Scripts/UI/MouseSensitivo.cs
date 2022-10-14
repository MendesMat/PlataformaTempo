using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseSensitivo : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        //define a descrição como inativo ao começar
        text.SetActive(false);
    }

    //Ao clicar com o mouse em cima de um gameobject
    public void OnPointerClick(PointerEventData eventData)
    {
        //Quando clicado??
        SceneManager.LoadScene("1-2");
        Debug.Log("Pulo Ativado");
    }

    //Ao passar o mouse em cima de um gameobject
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Quando o mouse passa por cima a descrição é mostrada
        text.SetActive(true);
    }

    //Ao sair com o mouse de cima de um gameobject
    public void OnPointerExit(PointerEventData eventData)
    {
        //Quando o mouse sai a descrição para de aparecer
        text.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
