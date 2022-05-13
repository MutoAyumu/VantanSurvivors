using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public event Action OnPause;

    public event Action OnResume;

    public event Action OnGameOver;

    public event Action OnGameClear;

    public event Action OnSetTarget;

    [SerializeField] float _timerLimit = 15f;
    float _timer;

    private void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (OnSetTarget == null) return;

        _timer += Time.deltaTime;

        if(_timer >= _timerLimit)
        {
            _timer = 0;
            Debug.Log($"{this.name} : <color=red>OnSetTargetを呼び出した</color>");

            OnSetTarget();
        }
    }
}
