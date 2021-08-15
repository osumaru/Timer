using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBarView : MonoBehaviour
{
    [SerializeField]
    Image timeGauge;
    
    public void OnTimeChange(float currentTime, float maxTime)
    {
        timeGauge.fillAmount = currentTime / maxTime;
    }
    
}
