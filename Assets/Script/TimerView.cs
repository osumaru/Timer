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
    Button playButton;

    [SerializeField] 
    Button resetButton;

    [SerializeField]
    Text playButtonText;
    [SerializeField]
    GameObject soundPrefab;

    [SerializeField]
    float timeSpeed = 1.0f;
    
    GameObject sound;
    
    public Action OnClickPlay;
    public Action OnClickReset;
    public Action<float> OnTimeUpdate;

    void Awake()
    {
        playButtonText.text = "Play";
        playButton.onClick.AddListener(OnClickPlayButton);
        resetButton.onClick.AddListener(OnClickResetButton);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        deltaTime *= timeSpeed;
        OnTimeUpdate(deltaTime);
    }

    public void OnStateChange(TimerModel.State state)
    {
        switch (state)
        {
            case TimerModel.State.Idle:
                if (sound != null)
                {
                    Destroy(sound);
                }
                playButtonText.text = "Play";
                break;
            case TimerModel.State.Pause:
                playButtonText.text = "Play";
                break;
            case TimerModel.State.Playing:
                playButtonText.text = "Stop";
                break; 
            case TimerModel.State.Stop:
                if (sound == null)
                {
                    sound = Instantiate(soundPrefab);
                }
                playButtonText.text = "Stop";
                break;
        }
    }

    public void OnTimeChange(float currentTime, float maxTime)
    {
        timeGauge.fillAmount = currentTime / maxTime;
    }
    
    public void OnClickPlayButton()
    {
        if (OnClickPlay != null)
        {
            OnClickPlay();
        }
    }
    public void OnClickResetButton()
    {
        if (OnClickReset != null)
        {
            OnClickReset();
        }
    }
}
