using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

public class TimerList : MonoBehaviour
{
    [SerializeField]
    GameObject timer;

    [SerializeField]
    Transform content;

    TimerPresenter selectTimer;
    
    List<TimerPresenter> timerList = new List<TimerPresenter>();
    
    // Start is called before the first frame update
    // Update is called once per framee
    
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.A))
        {
            ShowAllList();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowPlayingList();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Collider2D col = Physics2D.OverlapPoint(mousePos);
            selectTimer = col?.gameObject.GetComponent<TimerPresenter>();
        }
    }

    public void AddTimer()
    {
        GameObject gameObj = Instantiate(timer);
        gameObj.transform.SetParent(content);
        timerList.Add(gameObj.GetComponent<TimerPresenter>());
    }

    public void RemoveTimer()
    {
        if (0 < timerList.Count)
        {
            TimerPresenter removeObj = selectTimer ?? timerList[0];
            timerList.Remove(removeObj);
            Destroy(removeObj);
        }
    }

    public void ShowAllList()
    {
        foreach (TimerPresenter timer in timerList)
        {
            timer.gameObject.SetActive(true);
        }
    }

    public void ShowPlayingList()
    {
        foreach (TimerPresenter timer in timerList)
        {
            
            bool isActive = !(timer.Model.timerState.Value is TimerStateIdle);
            timer.gameObject.SetActive(isActive);
        }
    }
}
