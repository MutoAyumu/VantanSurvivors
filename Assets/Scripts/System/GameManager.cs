using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
/// <summary>
/// ゲーム時間の管理・ゲームオーバー・クリアのイベント管理・フェーズの管理
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public event Action OnPause;

    public event Action OnResume;

    public event Action OnGameOver;

    public event Action OnGameClear;

    public event Action OnSetEnemy;

    [SerializeField, Tooltip("経過時間を表示するテキスト")] Text _timerText = default;
    [SerializeField, Tooltip("敵を生成するイベントを実行する時間")] float[] _timerLimit = default;
    [Tooltip("ゲームの時間上限")] float _gameEndTime;
    [SerializeField, Tooltip("次のフェーズに遷るまでの時間")] float _phaseTime = 20f;

    Timer _timer = new Timer();
    Timer _gameTimer = new Timer();
    Timer _phaseTimer = new Timer();
    int _phaseCount;
    bool _isClear;
    bool _isPause;
    bool _isEnemyDebugLogFlag;

    List<EnemyBase> _enemies = new List<EnemyBase>();

    public bool IsClear { get => _isClear;}
    static public int PhaseCount { get => Instance._phaseCount;}
    public bool EnemyDebugLog { get => _isEnemyDebugLogFlag;}
    public List<EnemyBase> Enemies { get => Instance._enemies; }

    private void Start()
    {
        OnGameClear += Clear;
        _gameEndTime = _phaseTime * _timerLimit.Length;

        if(!_timerText)
        {
            Debug.LogError($"{this.name} : タイマーテキストがセットされていません");
        }

        _timer.Setup(_timerLimit[_phaseCount]);
        _phaseTimer.Setup(_phaseTime);
        _gameTimer.Setup(_gameEndTime);
    }
    private void Update()
    {
        if (!_isPause)
        {
            if (_timerText)
            {
                var time = _gameTimer.ReturnTime();
                _timerText.text = ((int)(time / 60)).ToString() + ":" + ((int)(time % 60)).ToString("00");
            }

            if (_timer.RunTimer())
            {
                OnSetEnemy?.Invoke();
            }

            if(_gameTimer.RunTimer())
            {
                if (_isClear) return;

                OnGameClear?.Invoke();
            }

            if(_phaseTimer.RunTimer())
            {
                Debug.Log($"{this.name} : <color=red>フェーズ{_phaseCount}</color>");

                if (_phaseCount < _timerLimit.Length - 1)
                    _phaseCount++;
            }
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

    void Clear()
    {
        _isClear = true;
        Debug.Log($"{this.name} : <color=red>ゲームクリア</color>");
    }
    public void SetUp()
    {
        _enemies = FindObjectsOfType<EnemyBase>(true).ToList();
    }
    public void SetEnemyFlag(bool flag)
    {
        _isEnemyDebugLogFlag = flag;
    }
}
