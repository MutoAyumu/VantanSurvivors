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

    IntReactiveProperty _objectCount;

    int _phaseCount;
    bool _isGameOver;
    bool _isPause;
    bool _isEnemyDebugLogFlag;

    IntReactiveProperty _enemyCount;

    static GameManager _instance = new GameManager();

    List<EnemyBase> _enemies = new List<EnemyBase>();

    private GameManager() { }

    public bool IsGameOver { get => _isGameOver; }
    static public int PhaseCount { get => Instance._phaseCount; }
    public bool EnemyDebugLog { get => _isEnemyDebugLogFlag; }
    public List<EnemyBase> Enemies { get => Instance._enemies; }
    static public GameManager Instance { get => _instance; }

    public IReadOnlyReactiveProperty<float> GameTimer => _timer;

    public IReadOnlyReactiveProperty<int> ObjectCount => _objectCount;

    public IReadOnlyReactiveProperty<int> EnemyCount { get => _enemyCount;}

    public void SetUp()
    {
        _phaseCount = 0;
        _enemies.Clear();
        _enemies = GameObject.FindObjectsOfType<EnemyBase>(true).ToList();
        _isGameOver = false;
    }
    public void SetTimer()
    {
        _timer = new FloatReactiveProperty(0);
        _objectCount = new IntReactiveProperty(0);
        _enemyCount = new IntReactiveProperty(0);
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
            if (_isGameOver) return;

            _timer.Value += Time.deltaTime;

            if (_gameTimer.RunTimer())
            {
                if (_isGameOver) return;

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

    void GameOver()
    {
        _isGameOver = true;
        OnGameOver?.Invoke();
    }

    public void SetGameOverListener(PlayerController p)
    {
        p.OnDeath += GameOver;
    }

    void Clear()
    {
        Debug.Log($"{this} : <color=red>ゲームクリア</color>");
    }
    public void TestObjectCount(bool flag)
    {
        if(flag)
        {
            _objectCount.Value++;
        }
        else
        {
            _objectCount.Value--;
        }
    }

    public void TestEnemyCount()
    {
        _enemyCount.Value++;
    }
}
