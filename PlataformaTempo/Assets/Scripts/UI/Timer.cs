using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] float starTime = 5f;

    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;

    [SerializeField] TextMeshProUGUI timerText1;
    [SerializeField] TextMeshProUGUI timerText2;
    

    float timer1 = 0f;
    float timer2 = 0f;
    void Start()
    {
        StartCoroutine(Timer1());
        StartCoroutine(Timer2());
    }
    #region Timer 1
    private IEnumerator Timer1()
    {
         timer1 = starTime;
        do
        {
            timer1 -= Time.deltaTime;
            //se colocar "1- o slider vai no sentido contrario
            slider1.value =  timer1 / starTime;

            FormatText1();
            
            yield return null;
        }
        while (timer1 > 0);
    }

    private void FormatText1()
    {
        int days = (int)(timer1 / 86400) % 365;
        int hours = (int)(timer1 / 3600) % 24;
        int minutes = (int)(timer1 / 60) % 60;
        int seconds = (int)(timer1 % 60);

        timerText1.text = "";

        if (days > 0) { timerText1.text += days + "d "; }
        if (hours > 0) { timerText1.text += hours + "h "; }
        if (minutes > 0) { timerText1.text += minutes + "m "; }
        if (seconds > 0) { timerText1.text += seconds + "s "; }
    }
    #endregion
    private IEnumerator Timer2()
    {
        timer2 = starTime;
        do
        {
            timer2 -= Time.deltaTime;
            //se colocar "1- o slider vai no sentido contrario
            slider2.value = timer2 / starTime;

            FormatText2();

            yield return null;
        }
        while (timer2 > 0);
    }

    private void FormatText2()
    {
        int days = (int)(timer2 / 86400) % 365;
        int hours = (int)(timer2 / 3600) % 24;
        int minutes = (int)(timer2 / 60) % 60;
        float seconds = (int)(timer2 % 60);
        string secondsString = seconds.ToString("F2");

        timerText2.text = days +"d " + hours + "h " + minutes + "m " + secondsString;}
}
