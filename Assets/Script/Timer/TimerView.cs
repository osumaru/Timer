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
    Image playButtonImage;

    [SerializeField]
    Image resetButtonImage;

    [SerializeField]
    Text playButtonText;
    [SerializeField]
    GameObject soundPrefab;

    [SerializeField]
    float timeSpeed = 1.0f;

    [SerializeField]
    Color ActiveColor = Color.white;

    [SerializeField]
    Color DeactiveColor = Color.black;


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
        #if DEBUG
        deltaTime *= timeSpeed;
        #endif
        OnTimeUpdate(deltaTime);
        
    }

    public void OnStateChange(ITimerState preState, ITimerState nextState)
    {
        preState?.Exit();
        nextState?.Enter();
    }

    public void StateIdleEnter()
    {
        if (sound != null)
        {
            Destroy(sound);
        }
        playButtonText.text = "Play";
        ResetButtonDeactive();
        
    }

    public void StatePauseEnter()
    {
        playButtonText.text = "Play";
        ResetButtonActive();
    }

    public void StatePauseExit()
    {
        ResetButtonDeactive();
    }
    public void StatePlayingEnter()
    {
        playButtonText.text = "Stop";
    }

    public void StateStopEnter()
    {
        if (sound == null)
        {
            sound = Instantiate(soundPrefab);
        }
        playButtonText.text = "Stop";
        ResetButtonActive();
    }

    public void StateStopExit()
    {
        ResetButtonDeactive();
    }

    void ResetButtonDeactive()
    {
        resetButton.enabled = false;
        resetButtonImage.color = DeactiveColor;
    }

    void ResetButtonActive()
    {
        
        resetButton.enabled = true;
        resetButtonImage.color = ActiveColor;
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
