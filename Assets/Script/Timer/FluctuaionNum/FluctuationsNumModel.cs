using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class FluctuationsNumModel
{
    public ReactiveProperty<int> currentNum { get; } = new ReactiveProperty<int>(0);
    bool isPointerStay = false;
    public ReactiveProperty<bool> isButtonActive { get; }= new ReactiveProperty<bool>();
    public int maxNum = 100;
    public int CurrentNum
    {
        get { return currentNum.Value; }
        set
        {
            currentNum.Value = value;
            while (currentNum.Value < 0)
            {
                currentNum.Value += maxNum;
            }
            currentNum.Value %= maxNum;            
        }
    }
    
    public void CountUp()
    {
        CurrentNum++;
    }

    public void CountDown()
    {
        CurrentNum--;
    }

    public void OnClickButtonNum()
    {
        isButtonActive.Value = isPointerStay;
    }

    public void OnPointerEnter()
    {
        isPointerStay = true;
    }

    public void OnPointerExit()
    {
        isPointerStay = false;
    }

    public void OnInputNum(int num)
    {
        if (isButtonActive.Value)
        {
            int tempNum = CurrentNum;
            //数字が2桁に収まるように右(下)から繰り上げて数字を入力
            tempNum *= 10;
            tempNum += num;
            tempNum %= 100;
            if (tempNum <= maxNum)
            {
                CurrentNum = tempNum;
            }
        }
    }

    public void OnDeleteNum()
    {
        if (isButtonActive.Value)
        {
            CurrentNum = 0;
        }
    }

    public void OnInputMouseWheel(float inputWheel)
    {
        if (isPointerStay)
        {
            CurrentNum += (int)(inputWheel);
        }
    }
}