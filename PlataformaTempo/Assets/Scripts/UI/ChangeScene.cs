using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /*Script responsavel por grande parte da transi��o das cenas, sendo colocado em 
    *um bot�o onde ser� descrito para qual cena ser� encaminhado apos clicar no bot�o*/
    public void btn_change_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    //Fun��o responsavel por fechar o jogo, no editor ele apenas mostra no console "Quit"
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}