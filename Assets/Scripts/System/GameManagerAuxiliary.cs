using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAuxiliary : MonoBehaviour
{
    [SerializeField, Tooltip("経過時間を表示するテキスト")] Text _timerText = default;

    GameManager _gameManager;

    public delegate void MonoEvent();
    MonoEvent _updateCall;

    public Text TimerText { get => _timerText;}

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _gameManager.SetGameListener(this);
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
