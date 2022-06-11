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
    }
}
