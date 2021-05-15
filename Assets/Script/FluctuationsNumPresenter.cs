using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluctuationsNumPresenter : MonoBehaviour
{
    
    
    FluctuationsNumModel model;

    public FluctuationsNumModel Model
    {
        get { return model; }
    }

    [SerializeField]
    FluctuationsNumView view;

    [SerializeField] 
    int maxNum = 100;
    
    
    void Awake()
    {
        model = new FluctuationsNumModel();
        model.maxNum = maxNum;
        view.OnClickCountDown = model.CountDown;
        view.OnClickCountUp = model.CountUp;
        model.OnChangedNum = view.OnChangedNum;
        view.OnClickNum = model.OnClickButtonNum;
        view.OnPointerExitFunc = model.OnPointerExit;
        view.OnPointerEnterFunc = model.OnPointerEnter;
        view.OnInputNum = model.OnInputNum;
        view.OnDeleteNum = model.OnDeleteNum;
        view.OnInputWheel = model.OnInputMouseWheel;
        model.CurrentNum = 0;
    }
    
}
