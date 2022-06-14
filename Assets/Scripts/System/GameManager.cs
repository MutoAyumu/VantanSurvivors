using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム時間の管理・ゲームオーバー・クリアのイベント管理・フェーズの管理
/// </summary>
public class GameManager
{
    public event Action OnPause;

    public event Action OnResume;

    public event Action OnGameOver;

    public event Action OnGameClear;

    Timer _gameTimer = new Timer();
    FloatReactiveProperty _timer;

    int _phaseCount;
    bool _isClear;
    bool _isPause;
    bool _isEnemyDebugLogFlag;

    static GameManager _instance = new GameManager();

    List<EnemyBase> _enemies = new List<EnemyBase>();

    private GameManager() { }

    public bool IsClear { get => _isClear; }
    static public int PhaseCount { get => Instance._phaseCount; }
    public bool EnemyDebugLog { get => _isEnemyDebugLogFlag; }
    public List<EnemyBase> Enemies { get => Instance._enemies; }
    static public GameManager Instance { get => _instance; }

    public IReadOnlyReactiveProperty<float> GameTimer => _timer;

    public void SetUp()
    {
        _enemies = GameObject.FindObjectsOfType<EnemyBase>(true).ToList();
    }
    public void SetTimer()
    {
        _timer = new FloatReactiveProperty(0);
    }

    public void SetGameListener(GameManagerAuxiliary gameTime)
    {
        OnGameClear += Clear;

        gameTime.SetUpdateCallback(Update);
    }

    public void SetEnemyFlag(bool flag)
    {
        _isEnemyDebugLogFlag = flag;
    }

    private void Update()
    {
        if (!_isPause)
        {
            _timer.Value += Time.deltaTime;

            if (_gameTimer.RunTimer())
            {
                if (_isClear) return;

                OnGameClear?.Invoke();
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (!_isPause)
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

    public void SetPhaseListener(Spawner spawner)
    {
        spawner.OnPhaseCallback += PhaseCountUp;

        _gameTimer.Setup(spawner.PhaseTime * spawner.SpawnTimes.Length);
    }

    void PhaseCountUp()
    {
        _phaseCount++;
    }


    void Clear()
    {
        _isClear = true;
        Debug.Log($"{this} : <color=red>ゲームクリア</color>");
    }
}
