using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TimerView : MonoBehaviour
{
    [SerializeField]
    Image timeGauge;
    
    [SerializeField]
    float timeSpeed = 1.0f;
    
    public Action<float> OnTimeUpdate;

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        #if DEBUG
        deltaTime *= timeSpeed;
        #endif
        OnTimeUpdate(deltaTime);
    }
    
    public void OnTimeChange(float currentTime, float maxTime)
    {
        timeGauge.fillAmount = currentTime / maxTime;
    }
    

}
