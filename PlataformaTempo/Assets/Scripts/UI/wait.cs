using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wait : MonoBehaviour
{
    //Codigo usado para a intro, ele espera um tempo e depois vai para o menu

    public float wait_time = 5f;

    void Start()
    {
        StartCoroutine(Wait_for_Intro());
    }

    IEnumerator Wait_for_Intro()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);

    }
    
}
