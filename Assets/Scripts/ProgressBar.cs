using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public int maxValue;
    public int currentValue;

    public void setProgress(float progress){
        if(progress<401.0){
            slider.value = (int)progress;
            currentValue = (int)progress;
        }   
    }

    public void setMax(int max){
        slider.maxValue= max;
        maxValue = max;
    }
    
}
