using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FluctuationsNumModel
{
    int currentNum = 0;
    bool isPointerStay = false;
    bool isButtonActive = false;
    public int maxNum = 100;
    public int CurrentNum
    {
        get { return currentNum; }
        set
        {
            currentNum = value;
            while (currentNum < 0)
            {
                currentNum += maxNum;
            }
            currentNum %= maxNum;            
            OnChangedNum(currentNum);
        }
    }

    public Action<int> OnChangedNum;
    
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
        isButtonActive = isPointerStay;
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
        if (isButtonActive)
        {
            int tempNum = CurrentNum;
            tempNum *= 10;
            tempNum += num;
            tempNum %= 100;
            CurrentNum = tempNum;
        }
    }

    public void OnDeleteNum()
    {
        if (isButtonActive)
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