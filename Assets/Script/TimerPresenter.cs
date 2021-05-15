using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField]
    TimerView view;

    TimerModel model;

    [SerializeField]
    FluctuationsNumPresenter hourNumPresenter;

    [SerializeField] 
    FluctuationsNumPresenter minutePresenter;

    [SerializeField]
    FluctuationsNumPresenter secondPresenter;


    
    // Start is called before the first frame update
    void Awake()
    {
        model = new TimerModel();
        model.Initialize(hourNumPresenter, minutePresenter, secondPresenter);
        view.OnClickPlay = model.OnClickPlayButton;
        view.OnClickReset = model.OnClickResetButton;
        model.OnStateChange = view.OnStateChange;
        model.OnTimeChange = view.OnTimeChange;
        view.OnTimeUpdate = model.TimeUpdate;
    }
}
