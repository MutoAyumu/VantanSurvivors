using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameUIPresenter : MonoBehaviour
{
    [Header("vC[ĚHPÖW")]
    [SerializeField] MVPSlider _hpSlider;
    [SerializeField] float _playerDuration = 0.1f;
    PlayerController _player;

    [Header("oąlÖW")]
    [SerializeField] MVPSlider _expSlider;
    [SerializeField] float _expDuration = 0;
    PlayerManager _playerManager;

    [Header("KEZÖW")]
    [SerializeField] MVPSlider _specialSlider;
    [SerializeField]float _specialDuration = 0;

    [Header("^C}[peLXg")]
    [SerializeField] MVPText _timeText;
    GameManager _gameManager;

    private void Start()
    {
        if (_hpSlider)
        {
            _player = PlayerManager.Player;

            _player.CurrentHp.Subscribe(x =>
            {
                _hpSlider.SetValue(x, _player.Hp, _playerDuration);
            }).AddTo(this);
        }

        if(_expSlider)
        {
            _playerManager = PlayerManager.Instance;

            _playerManager.Exp.Subscribe(x =>
            {
                _expSlider.SetValue(x, _playerManager.NextLevelUpExp, _expDuration);
            }).AddTo(this);
        }

        if(_specialSlider)
        {
            _playerManager = PlayerManager.Instance;
            _player = PlayerManager.Player;

            _playerManager.SpecialPoint.Subscribe(x =>
            {
                _specialSlider.SetValue(x, _player.SpecialValue, _specialDuration);
            }).AddTo(this);
        }

        if(_timeText)
        {
            _gameManager = GameManager.Instance;

            _gameManager.GameTimer.Subscribe(x =>
            {
                _timeText.SetText(((int)(x / 60)).ToString() + ":" + ((int)(x % 60)).ToString("00"));
            }).AddTo(this);
        }
    }
}
