using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /*Script responsavel por grande parte da transição das cenas, sendo colocado em 
    *um botão onde será descrito para qual cena será encaminhado apos clicar no botão*/
    public void btn_change_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    //Função responsavel por fechar o jogo, no editor ele apenas mostra no console "Quit"
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}