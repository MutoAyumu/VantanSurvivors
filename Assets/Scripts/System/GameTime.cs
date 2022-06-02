using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField, Tooltip("経過時間を表示するテキスト")] Text _timerText = default;
    [SerializeField, Tooltip("敵を生成するイベントを実行する時間")] float[] _timerLimit = default;
    [SerializeField, Tooltip("次のフェーズに遷るまでの時間")] float _phaseTime = 20f;

    GameManager _gameManager;

    public delegate void MonoEvent();
    MonoEvent _updateCall;

    public float PhaseTime { get => _phaseTime;}
    public float[] TimerLimit { get => _timerLimit; }
    public Text TimerText { get => _timerText;}

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _gameManager.SetGame(this);
    }
    public void SetUpdateCallback(MonoEvent e)
    {
        _updateCall = e;
    }
    private void Update()
    {
        _updateCall?.Invoke();
    }
}
