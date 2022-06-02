using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
/// <summary>
/// ゲーム時間の管理・ゲームオーバー・クリアのイベント管理・フェーズの管理
/// </summary>
public class GameManager
{
    public event Action OnPause;

    public event Action OnResume;

    public event Action OnGameOver;

    public event Action OnGameClear;

    public event Action OnSetEnemy;

    [Tooltip("経過時間を表示するテキスト")] Text _timerText;
    [Tooltip("敵を生成するイベントを実行する時間")] float[] _timerLimit;

    Timer _timer = new Timer();
    Timer _gameTimer = new Timer();
    Timer _phaseTimer = new Timer();

    int _phaseCount;
    bool _isClear;
    bool _isPause;
    bool _isEnemyDebugLogFlag;

    static GameManager _instance = new GameManager();

    List<EnemyBase> _enemies = new List<EnemyBase>();

    public bool IsClear { get => _isClear;}
    static public int PhaseCount { get => Instance._phaseCount;}
    public bool EnemyDebugLog { get => _isEnemyDebugLogFlag;}
    public List<EnemyBase> Enemies { get => Instance._enemies; }
    static public GameManager Instance { get => _instance;}

    public void SetUp()
    {
        _enemies = GameObject.FindObjectsOfType<EnemyBase>(true).ToList();
    }

    public void SetGame(GameTime gameTime)
    {
        OnGameClear += Clear;

        _timerLimit = gameTime.TimerLimit;

        var endTime = gameTime.PhaseTime * _timerLimit.Length;//なんで？

        _timer.Setup(_timerLimit[_phaseCount]);
        _phaseTimer.Setup(gameTime.PhaseTime);
        _gameTimer.Setup(endTime);

        _timerText = gameTime.TimerText;

        var attach = gameTime;
        attach.SetUpdateCallback(Update);
    }

    public void SetEnemyFlag(bool flag)
    {
        _isEnemyDebugLogFlag = flag;
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
                Debug.Log($"{this} : <color=red>フェーズ{_phaseCount}</color>");

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
        Debug.Log($"{this} : <color=red>ゲームクリア</color>");
    }
}
