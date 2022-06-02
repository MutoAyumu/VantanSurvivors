using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    [SerializeField, Tooltip("�o�ߎ��Ԃ�\������e�L�X�g")] Text _timerText = default;
    [SerializeField, Tooltip("�G�𐶐�����C�x���g�����s���鎞��")] float[] _timerLimit = default;
    [SerializeField, Tooltip("���̃t�F�[�Y�ɑJ��܂ł̎���")] float _phaseTime = 20f;

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
