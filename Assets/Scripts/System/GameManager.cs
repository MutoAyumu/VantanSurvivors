using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public event Action OnPause;

    public event Action OnResume;

    public event Action OnGameOver;

    public event Action OnGameClear;

    public event Action OnSetEnemy;

    [SerializeField, Tooltip("経過時間を表示するテキスト")] Text _timerText = default;
    [SerializeField, Tooltip("敵を生成するイベントを実行する時間")] float[] _timerLimit = default;
    [SerializeField, Tooltip("ゲームの時間上限")] float _gameEndTime = 10f;
    [SerializeField, Tooltip("次のフェーズに遷るまでの時間")] float _phaseTime = 20f;
    float _timer;
    float _gameTimer;
    float _phaseTimer;
    int _phaseCount;
    bool _isClear;
    bool _isPause;

    PlayerController _player = default;

    static public PlayerController Player { get => Instance._player;}
    public bool IsClear { get => _isClear;}

    private void Start()
    {
        OnGameClear += Clear;

        if(!_timerText)
        {
            Debug.LogError($"{this.name} : タイマーテキストがセットされていません");
        }
    }
    private void Update()
    {
        if (!_isPause)
        {
            Timer(OnSetEnemy);
            GameTimer();
            PhaseTimer();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if(!_isPause)
            {
                _isPause = true;
                OnPause?.Invoke();
            }
            else
            {
                _isPause = false;
                OnResume?.Invoke();
            }
        }
    }

    void Timer(Action action)
    {
        _timer += Time.deltaTime;

        if(_timer >= _timerLimit[_phaseCount])
        {
            _timer = 0;

            action?.Invoke();
        }
    }
    void GameTimer()
    {
        _gameTimer += Time.deltaTime;

        if(_timerText)
        _timerText.text = ((int)(_gameTimer / 60)).ToString() + ":" + ((int)(_gameTimer % 60)).ToString("00");

        if (_isClear) return;

        if(_gameTimer >= _gameEndTime)
        {
            OnGameClear?.Invoke();
        }
    }
    void PhaseTimer()
    {
        _phaseTimer += Time.deltaTime;

        if(_phaseTimer >= _phaseTime)
        {
            _phaseTimer = 0;
            Debug.Log($"{this.name} : <color=red>フェーズ{_phaseCount}</color>");

            if (_phaseCount < _timerLimit.Length - 1)
            _phaseCount++;
        }
    }
    public void SetPlayer(PlayerController p)
    {
        _player = p;
    }

    void Clear()
    {
        _isClear = true;
        Debug.Log($"{this.name} : <color=red>ゲームクリア</color>");
    }
}
