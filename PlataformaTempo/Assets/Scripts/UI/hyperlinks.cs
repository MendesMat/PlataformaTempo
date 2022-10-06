using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyperlinks : MonoBehaviour
{
    //Script criado para inserir Hyperlinks 

    //OpenInstagram abre o instagram pelo link inserido na url pela linha de codigo
   public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/demonchicken.studio/");
    }
    //OpenURL permite abrir URLs que são colocados pelo editor, facilitando caso haja muitos buttons com URLs
    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }
}
