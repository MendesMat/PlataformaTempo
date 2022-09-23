using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timeLevel_txt;
    private float timeLevel;

    void Start()
    {

    }
    
    void Update()
    {
        timeLevel = timeLevel + Time.deltaTime;
        timeLevel_txt.text = timeLevel.ToString("F0");
    }
}
